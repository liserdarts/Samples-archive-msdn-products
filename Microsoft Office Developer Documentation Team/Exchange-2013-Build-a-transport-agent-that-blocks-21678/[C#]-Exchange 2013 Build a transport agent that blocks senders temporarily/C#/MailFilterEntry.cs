// ***************************************************************
// <copyright file="MailFilterEntry.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// A simple class to store one record in the MailFilterDatabase.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Agents.MailFilterAgent
{
    using System;
    using System.IO;
    using System.Text;


    #region MailFilterEntry Class

    /// <summary>
    /// A class that stores one record of the MailFilterDatabase.
    /// </summary>
    public class MailFilterEntry
    {
        #region Fields
        /// <summary>
        /// The creation time or the last time that this triplet
        /// matched a successful delivery.
        /// </summary>
        private DateTime timeStamp;

        /// <summary>
        /// The hash value of the triplet.
        /// </summary>
        private UInt64 tripletHash;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// The default constructor.
        /// </summary>
        public MailFilterEntry()
            : this(0)
        {
        }

        /// <summary>
        /// Creates a new entry with the given triplet hash
        /// and a timestamp of Now.
        /// </summary>
        /// <param name="tripletHash">The triplet hash to set.</param>
        public MailFilterEntry(UInt64 tripletHash)
        {
            this.tripletHash = tripletHash;
            this.timeStamp = DateTime.UtcNow;
        }

        /// <summary>
        /// The copy constructor.
        /// </summary>
        /// <param name="other">The entry object to be copied.</param>
        public MailFilterEntry(MailFilterEntry other)
        {
            this.tripletHash = other.TripletHash;
            this.timeStamp = other.TimeStamp;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Allows access to the triplet hash.
        /// </summary>
        public UInt64 TripletHash
        {
            get { return this.tripletHash; }
            set { this.tripletHash = value; }
        }

        /// <summary>
        /// Gets and sets the last success time for the entry.
        /// </summary>
        public DateTime TimeStamp
        {
            get { return this.timeStamp; }
            set { this.timeStamp = value; }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Tries to parse the contents of a string into a new entry.
        /// </summary>
        /// <param name="currentLine">The string to be parsed.</param>
        /// <returns>The new entry, or null if unsuccessful.</returns>
        public static MailFilterEntry TryParse(String currentLine)
        {
            // Set up a return value variable.
            MailFilterEntry retval = null;

            // Split the two values in the line.
            string[] splitLine = currentLine.Split(',');

            // Verify that you got exactly two values from the line in the file.
            if (splitLine.Length == 2)
            {
                // Try parsing the hash code from the file. If you
                // can't parse one, do not return an
                // entry object at all.
                UInt64 inputHash;
                if (!UInt64.TryParse(splitLine[0].Trim(), out inputHash))
                {
                    return null;
                }

                long inputTicks;
                DateTime inputTime;
                if (long.TryParse(splitLine[1].Trim(), out inputTicks))
                {
                    inputTime = new DateTime(inputTicks);
                }
                else
                {
                    return null;
                }

                retval = new MailFilterEntry(inputHash);
                retval.timeStamp = inputTime;
            }

            return retval;
        }

        /// <summary>
        /// Returns 32 bits of the hash code.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return (int)this.tripletHash;
        }

        /// <summary>
        /// An override of the Object class's Equals method.
        /// </summary>
        /// <param name="obj">The other object to be compared to.</param>
        /// <returns>True if the other object is a MailFilterEntry and has the same TripletHash as this.</returns>
        public override bool Equals(Object obj)
        {
            MailFilterEntry entry = obj as MailFilterEntry;

            if (entry == null)
            {
                return false;
            }

            return this.Equals(entry);
        }

        /// <summary>
        /// Compares this MailFilterEntry's tripletHash to that of another MailFilterEntry.
        /// </summary>
        /// <param name="entry">The other MailFilterEntry that this will be compared to.</param>
        /// <returns>True if the other entry has the same hash as this entry.</returns>
        public bool Equals(MailFilterEntry entry)
        {
            return (this.tripletHash == entry.tripletHash);
        }

        /// <summary>
        /// Determines whether an entry is older than the specified time span.
        /// </summary>
        /// <param name="lifespan">The time span to compare the entry's age against.</param>
        /// <returns>True if the entry is older than the life span.</returns>
        public bool IsPastPeriod(TimeSpan lifespan)
        {
            return DateTime.UtcNow.Subtract(lifespan) > this.timeStamp;
        }

        /// <summary>
        /// Returns a string representation of the data in the entry.
        /// </summary>
        /// <returns>A string representation of the entry.</returns>
        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder(64);
            buffer.Append(this.tripletHash);
            buffer.Append(", ");
            buffer.Append(this.timeStamp.ToString());
            buffer.Append(";");

            return buffer.ToString();
        }

        /// <summary>
        /// Writes the data contained in the entry to an output stream.
        /// </summary>
        /// <param name="outputStream">The stream to be written to.</param>
        public void Persist(StreamWriter outputStream)
        {
            outputStream.WriteLine(this.tripletHash + "," + this.timeStamp.Ticks);
        }

        #endregion Methods
    }
    #endregion MailFilterEntry Class
}