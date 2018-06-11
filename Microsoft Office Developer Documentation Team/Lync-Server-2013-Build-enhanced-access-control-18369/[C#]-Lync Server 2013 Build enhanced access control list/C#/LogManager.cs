/*++

Copyright © Microsoft Corporation

Module Name:

    LogManager.cs

Abstract:

    Rudimentary logging support.

Notes:

    The ILogEntry interface is used to generate a log record. Logs are
    written until either the configured record count is reached or the size of the log
    file exceeds the preconfigured value. 

    Note that this object does not calculate available disk space nor support roll-over.
    LogManager is NOT safe for multi-threaded operations and hence caller's should synchronize
    using LogManager.SyncRoot.

--*/
#region Using directives
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Xml.Serialization;
using System.Timers;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Text;
using Microsoft.Rtc.Sip;
using Microsoft.Rtc.Sip.SDK.Samples.Utils;
#endregion

namespace Microsoft.Rtc.Sip.SDK.Samples.EnhancedAllowList
{
    public interface ILogEntry
    {
        string Key
        {
            get;
        }
        
        void WriteRaw (TextWriter writer);
    };

    /// <summary>
    /// This class helps decide whether a particular log request for a record
    /// has been already done so no more logging needs to be done.
    /// </summary>
    /// <remarks>
    /// This object is NOT safe for multi-threaded operations and hence must be
    /// synchronized as part of a larger context.
    /// </remarks>
    public class LogFilter
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxUniqueEntries">Count of unique records after which logging has to stop.
        /// </param>
        public LogFilter (int maxUniqueEntries)
        {
            this.maxUniqueEntries = maxUniqueEntries;
            if (maxUniqueEntries > 100) {
                this.entryTable = new Hashtable (maxUniqueEntries / 2);
            }
            else {
                this.entryTable = new Hashtable ();
            }
        }

        /// <summary>
        /// Decides whether the specified ILogEntry can be logged based on policy
        /// </summary>
        /// <param name="logEntry">Identifies a LogEntry</param>
        /// <returns></returns>
        public bool CanLog (ILogEntry logEntry)
        {
            //
            // See if record-count is exceeded.
            //

            int numberOfEntries = entryTable.Count;
            if (numberOfEntries + 1 > maxUniqueEntries) {
                return false;
            }

            try {
                if (logEntry.Key != null) {

                    //
                    // Track the primary key as already logged.
                    //

                    entryTable.Add (logEntry.Key, null);
                }
            }
            catch  {

                //
                // Duplicate entry, already logged, skip logging.
                //

                return false;
            }

            //
            // We do not know this entry, so log this one.
            //

            return true;
        }

        private int maxUniqueEntries;
        private Hashtable entryTable;
    };

    /// <summary>
    /// This class implements a simple logger that can XML-serialize objects and write
    /// to a file. It tracks the current file size and fails write calls if they exceed
    /// the preconfigured maximum size. It is NOT safe for multi-threaded operations
    /// and hence callers must make sure that calls are thread-safe. 
    /// </summary>
    public class LogManager : IDisposable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxLogSize">Takes the maximum size we can write to the log
        /// in Mega Bytes.
        /// </param>
        public LogManager (
            Config config,
            string path
            )
        {
            this.maxLogSize = config.MaxLogFileSize * 1024 * 1024;
            this.path = path;
            logFilter = new LogFilter (config.MaxDomainsLogged);
            logFile = new StreamWriter (
                new BufferedStream (
                    File.Open (this.path, FileMode.CreateNew, FileAccess.Write, FileShare.Read), 4096)
                    );
            logFileFullEvent = new EventLogThrottle (Config.AppEventLog);
        }

        /// <summary>
        /// Logs the entry if allowed by the associated policy.
        /// </summary>
        /// <returns>
        /// true - if the entry was logged successfully.
        /// false - if the entry was not logged because of an error or policy did not allow it.
        /// </returns>
        public bool Log (ILogEntry logEntry)
        {
            //
            // Check with policy first.
            //
            if (logFilter.CanLog (logEntry)) {

                //
                // Policy is okay with it, write it if allowed by file size limits.
                //

                if (!startWritten) {
                    logFile.Write ("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Log>\r\n");
                    startWritten = true;
                }
                if (logFile.BaseStream.Position < maxLogSize) {
                    logEntry.WriteRaw (logFile);
                    return true;
                }
                else {
                    RaiseLogFileFullEvent ();
                    return false;
                }
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Dispose implementation.
        /// </summary>
        public void Dispose ()
        {
            Dispose (true);
        }

        /// <summary>
        /// Dispose implementation.
        /// </summary>
        public void Dispose (bool disposing)
        {
            if (!this.disposed) {

                if (disposing) {

                    //
                    // Close the log file, delete it if it is empty.
                    //

                    if (logFile != null) {
                        if (startWritten) {
                            logFile.WriteLine ("</Log>");
                            logFile.Flush ();
                            logFile.Close ();
                        }
                        else {
                            logFile.Close ();
                            try {
                                File.Delete (path);
                            }
                            catch {
                            }
                        }
                    }
                }

                logFilter = null;
                logFile = null;

                this.disposed = true;
            }
        }

        /// <summary>
        /// Provides the synchronization root for this object.
        /// </summary>
        public object SyncRoot
        {
            get
            {
                return syncRoot;
            }
        }

        /// <summary>
        /// Constructs a file path by concatenating current time to the supplied parameters.
        /// </summary>
        /// <param name="pathToDirectory">Base directory, must be present</param>
        /// <param name="filePrefix">File name prefix</param>
        /// <returns>BaseDir\FilePrefix-DateTime.txt
        /// </returns>
        public static string MakeLogName (string pathToDirectory, string filePrefix)
        {
            DateTime currentTime = DateTime.Now;
            
            return String.Format("{0}\\{1}-{2}-{3}-{4}-{5}-{6}.txt",
                pathToDirectory, filePrefix,
                currentTime.Month,
                currentTime.Day,
                currentTime.Hour, currentTime.Minute, currentTime.Second);
        }

        private void RaiseLogFileFullEvent ()
        {
            logFileFullEvent.LogWarning (String.Format (SipStringManager.GetString ("LogFileFull"), path));
        }

        private EventLogThrottle logFileFullEvent;
        private bool startWritten;
        private int maxLogSize;
        private string path;
        private LogFilter logFilter;
        private StreamWriter logFile;
        private bool disposed;
        private object syncRoot = new Object ();
    };

}
