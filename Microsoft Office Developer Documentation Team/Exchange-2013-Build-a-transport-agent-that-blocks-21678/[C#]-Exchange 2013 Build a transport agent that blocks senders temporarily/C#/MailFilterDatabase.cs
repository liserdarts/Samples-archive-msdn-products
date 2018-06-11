// ***************************************************************
// <copyright file="MailFilterDatabase.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// A custom implementation of a hash table data structure.
// This database will store all entries in the temporarily blocked senders list for
// the lifetime of the Exchange Transport service.
// When the service is terminated, the database contents 
// are serialized to a file and reloaded the next time 
// the service is launched.
//
// The hash table is implemented by using a list of linked lists.
// This gives you some flexibility that the default .NET Framework HashTable
// class does not have. For example, you can lock individual 
// rows of buckets in the table for updating, rather than locking
// the whole table.
//
// Hash collisions are handled by having a linked list for each
// entry in the array list that the table is implemented with.
// When a collision occurs, the new entry is inserted at the start
// of the linked list. This way, the most recent entry is always
// at the beginning of the list and can be accessed more quickly
// in the future.
// </summary>
// ***************************************************************


namespace Microsoft.Exchange.Samples.Agents.MailFilterAgent
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;

    #region MailFilterDatabase Class
    /// <summary>
    /// This class is used to store triplets that have been encountered
    /// by the MailFilter agent. The triplets are stored in a custom
    /// hash table based on a SHA hash of the triplet that has been
    /// truncated down to 64 bits. The triplet is then placed in the
    /// table based on this value.
    /// </summary>
    public class MailFilterDatabase
    {
        #region Class Variables

        /// <summary>
        /// The amount of padding space to include when initializing the
        /// root list. This optimizes the performance of the
        /// hash hable that the list is used to implement. The recommended
        /// value is between 0.33 and 1.0. This will make it so that the hash
        /// table will never exceed 50% to 75% of its maximum load factor,
        /// thus reducing hash collisions and improving performance.
        /// </summary>
        private const double HashTablePadding = 0.33;

        /// <summary>
        /// The default bucket size to be used when no bucket size is specified.
        /// </summary>
        private const int DefaultBucketSize = 10;

        /// <summary>
        /// The default entry limit to be used when none is specified.
        /// </summary>
        private const int DefaultMaxEntries = 10000;

        /// <summary>
        /// The total maximum number of entries that can
        /// be stored in the database.
        /// </summary>
        private readonly int maxEntries;

        /// <summary>
        /// An object to be used for locking access to entryCount while
        /// incrementing or decrementing.
        /// </summary>
        private static object entryCountLock = new Object();

        /// <summary>
        /// The ArrayList that will store all the linked lists.
        /// </summary>
        private List<LinkedList<MailFilterEntry>> table;

        /// <summary>
        /// The index of the row that is next to be cleaned.
        /// When a thread starts cleaning, this should be set
        /// to the index after the last one it intends to clean.
        /// </summary>
        private int cleanIndex;

        /// <summary>
        /// The number of rows in the table. Default size = DefaultMaxEntries / DefaultBucketSize.
        /// </summary>
        private int tableRootSize;

        /// <summary>
        /// The maximum size of each linked list in
        /// table. See const DefaultBucketSize.
        /// </summary>
        private int bucketSize;

        /// <summary>
        /// The number of entries that are being stored in
        /// the table. Maintained by updating whenever there
        /// is an addition or removal.
        /// </summary>
        private int entryCount;
        #endregion Class Variables

        #region Constructors
        /// <summary>
        /// The constructor initializes the table with a specified maximum bucket size,
        /// and a set maximum number of entries.
        /// </summary>
        /// <param name="maxEntries">The maximum number of entries.</param>
        /// <param name="bucketSize">The maximum size of each bucket.</param>
        public MailFilterDatabase(int maxEntries, int bucketSize)
        {
            this.entryCount = 0;
            this.bucketSize = bucketSize;
            this.maxEntries = maxEntries;

            if (bucketSize > 0)
            {
                this.tableRootSize = (int)((1.0 + HashTablePadding) * (float)(maxEntries / bucketSize));
            }
            else
            {
                this.tableRootSize = (int)((1.0 + HashTablePadding) * (float)maxEntries);
            }

            this.table = new List<LinkedList<MailFilterEntry>>(this.tableRootSize);
            this.cleanIndex = 0;

            // Initialize the database.
            this.InitLinkedLists();
        }

        /// <summary>
        /// The constructor initializes the table with a specified maximum number of entries
        /// and default bucket size.
        /// </summary>
        /// <param name="maxEntries">The maximum number of entries in the table.</param>
        public MailFilterDatabase(int maxEntries)
            : this(maxEntries, DefaultBucketSize)
        {
        }

        /// <summary>
        /// The constructor initializes the table with the default maximum number of entries
        /// and default bucket size.
        /// </summary>
        public MailFilterDatabase()
            : this(DefaultMaxEntries, DefaultBucketSize)
        {
        }

        #endregion Constructors

        /// <summary>
        /// Gets the maximum number of entries that the table is allowed to store.
        /// This value is set in the configuration file and cannot be changed while
        /// the service is running. If you need to change this value, edit the
        /// configuration file and restart the service.
        /// </summary>
        public int MaxEntries
        {
            get { return this.maxEntries; }
        }

        /// <summary>
        /// Gets the current number of entries being stored in the table. This value
        /// reflects the status of the database and is not user-definable.
        /// </summary>
        public int EntryCount
        {
            get { return this.entryCount; }
        }

        #region Methods

        /// <summary>
        /// Returns the entry that matches the specified triplet
        /// or null if it is not found.
        /// </summary>
        /// <param name="tripletHash">The triplet to be looked up.</param>
        /// <returns>The MailFilter entry of that triplet.</returns>
        public MailFilterEntry GetEntry(UInt64 tripletHash)
        {
            // Calculate the index by the hash.
            int index = this.CalcIndex(tripletHash);

            // Look through the row to find the correct entry.
            LinkedList<MailFilterEntry> currentRow = (LinkedList<MailFilterEntry>)this.table[index];

            lock (((ICollection)currentRow).SyncRoot)
            {
                foreach (MailFilterEntry currentEntry in currentRow)
                {
                    if (currentEntry.TripletHash == tripletHash)
                    {
                        return currentEntry;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Removes an entry from the database.
        /// </summary>
        /// <param name="entry">The entry to be removed.</param>
        /// <returns>True if the entry was removed; false if it could not be found.</returns>
        public bool DeleteEntry(MailFilterEntry entry)
        {
            // The return value.
            bool retval = false;

            // Calculate the hash index.
            int index = this.CalcIndex(entry.TripletHash);

            LinkedList<MailFilterEntry> currentRow = (LinkedList<MailFilterEntry>)this.table[index];
            lock (((ICollection)currentRow).SyncRoot)
            {
                retval = currentRow.Remove(entry);
            }

            if (retval)
            {
                this.DecrementEntryCount();
            }

            return retval;
        }

        /// <summary>
        /// Records an entry in the database, either by updating an existing
        /// entry or by creating a new one. 
        /// </summary>
        /// <param name="entry">The entry to be added to the database.</param>
        public void SaveEntry(MailFilterEntry entry)
        {
            int index = this.CalcIndex(entry.TripletHash);

            LinkedList<MailFilterEntry> currentRow = (LinkedList<MailFilterEntry>)this.table[index];
            lock (((ICollection)currentRow).SyncRoot)
            {
                // If the triplet is already there, remove it
                // so that you can put it back at the start of the list.
                if (currentRow.Contains(entry))
                {
                    currentRow.Remove(entry);
                    this.DecrementEntryCount();
                }

                // Insert the triplet at the front of the list.
                entry.TimeStamp = DateTime.UtcNow;
                currentRow.AddFirst(entry);
                this.IncrementEntryCount();

                // If the bucket is full, delete the oldest entry in it.
                if (currentRow.Count > this.bucketSize)
                {
                    currentRow.RemoveLast();
                    this.DecrementEntryCount();
                }
            }
        }

        /// <summary>
        /// Writes the entire contents of the database table to
        /// a file. If an I/O error occurs, this method will abort
        /// at whatever point it is at in writing the file.
        /// </summary>
        /// <param name="path">The path to the file that should be written to.</param>
        public void PersistToDisk(string path)
        {
            try
            {
                using (StreamWriter outputStream = new StreamWriter(File.Open(path, FileMode.Create)))
                {
                    lock (((ICollection)this.table).SyncRoot)
                    {
                        foreach (LinkedList<MailFilterEntry> currentRow in this.table)
                        {
                            lock (((ICollection)currentRow).SyncRoot)
                            {
                                foreach (MailFilterEntry currentEntry in currentRow)
                                {
                                    currentEntry.Persist(outputStream);
                                }
                            }
                        }
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.ToString());
                return;
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e.ToString());
                return;
            }
            catch (IOException e)
            {
                Debug.WriteLine(e.ToString());
                return;
            }
        }

        /// <summary>
        /// Reads data from a file that contains database entries that were
        /// persisted from a previous session. If the data files cannot
        /// be found, the action is aborted. If invalid entries are
        /// found in the file, they will be skipped, and the method
        /// will continue reading the file after that entry. If 
        /// any other unforeseen I/O errors occur, the method will
        /// simply stop where it is in the file and return.
        /// 
        /// It is possible to record any errors reading the file
        /// in the system event logs, but this is not done here
        /// for simplicity.
        /// </summary>
        /// <param name="sourcePath">The path to the file that the data should be read from.</param>
        public void ReadPersistedData(string sourcePath)
        {
            try
            {
                using (StreamReader source = new StreamReader(File.OpenRead(sourcePath)))
                {
                    lock (entryCountLock)
                    {
                        while (!source.EndOfStream && this.entryCount < this.maxEntries)
                        {
                            MailFilterEntry newEntry = MailFilterEntry.TryParse(source.ReadLine());
                            if (newEntry != null)
                            {
                                this.SaveEntry(newEntry);
                            }
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Debug.WriteLine(e.ToString());
                return;
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e.ToString());
                return;
            }
        }

        /// <summary>
        /// Iterates over a set number of rows in the database and
        /// will check each one for any entries that are past their
        /// expiration date. It will start at the back of each row
        /// and work up because the oldest entries are at the back. You
        /// know that a row is finished when you find one entry
        /// that has not passed its expiration time.
        /// </summary>
        /// <param name="rowCount">The number of rows to clean.</param>
        /// <param name="lifeSpan">The expiration delay time from last access.</param>
        internal void RetireOldEntries(int rowCount, TimeSpan lifeSpan)
        {
            // Determine whether the input parameter is 0, negative, or would cause an overflow.
            if ((rowCount < 1) || ((rowCount + this.cleanIndex) < 0))
            {
                return;
            }

            // Get the cleaning index and then set it to the next
            // entry after the last one you intend to clean.
            int currentIndex = this.cleanIndex;
            this.cleanIndex = (this.cleanIndex + rowCount) % this.tableRootSize;

            for (int i = 0; i < rowCount; i++)
            {
                currentIndex = (currentIndex + 1) % this.tableRootSize;

                LinkedList<MailFilterEntry> currentRow = (LinkedList<MailFilterEntry>)this.table[currentIndex];
                lock (((ICollection)currentRow).SyncRoot)
                {
                    DateTime now = DateTime.UtcNow;
                    while ((null != currentRow.Last) &&
                           (now.Subtract(currentRow.Last.Value.TimeStamp) > lifeSpan))
                    {
                        currentRow.RemoveLast();
                        this.DecrementEntryCount();
                    }
                }
            }
        }

        /// <summary>
        /// Initializes all rows as empty linked lists.
        /// </summary>
        private void InitLinkedLists()
        {
            for (int index = 0; index < this.table.Capacity; index++)
            {
                this.table.Insert(index, new LinkedList<MailFilterEntry>());
            }
        }

        /// <summary>
        /// Increments an integer but ensures that an integer overflow doesn't occur.
        /// </summary>
        private void IncrementEntryCount()
        {
            // entryCount should never overflow.
            if (this.entryCount == Int32.MaxValue)
            {
                return;
            }

            // Safely increment.
            this.entryCount = Interlocked.Increment(ref this.entryCount);
        }

        /// <summary>
        /// Decrements currentEntry but ensures that an integer overflow doesn't occur.
        /// </summary>
        private void DecrementEntryCount()
        {

            // entryCount should never be negative.
            if (this.entryCount == 0)
            {
                return;
            }

            // Safely decrement.
            this.entryCount = Interlocked.Decrement(ref this.entryCount);
        }

        /// <summary>
        /// Calculates a row index into the table based on the hash code.
        /// </summary>
        /// <param name="tripletHash">The hash code to be indexed.</param>
        /// <returns>The calculated index</returns>
        private int CalcIndex(UInt64 tripletHash)
        {
            return (int)(tripletHash % (UInt32)(this.tableRootSize));
        }
        #endregion Methods
    }
    #endregion MailFilterDatabase Class
}