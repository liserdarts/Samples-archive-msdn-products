/*++

Copyright © Microsoft Corporation

Module Name:

    PolicyManager.cs

Abstract:

    This module implements the Allow List specific logic for this application.
    Specifically, it derives from SessionManager and provides the OnRequest handler
    that processes requests. 

Notes:
    Usage:
            - Start() will connect this application to the server, prepare logging.
            - Events will be serviced until a Stop is executed.
            - Stop() to disconnect from server and prepare for shutdown.
            - Dispose() to close all logs and cleanup resources.

    This object is safe for multi-thread operations.
    
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
using Microsoft.Rtc.Sip.SDK.Samples.EnhancedAllowList;
#endregion

namespace Microsoft.Rtc.Sip.SDK.Samples.EnhancedAllowList
{
    /// <summary>
    /// PolicyManager contains the core logic for the EnhancedAllowList application.
    /// </summary>
    public class PolicyManager : SessionManager
    {
        public PolicyManager ()
        {
            configFileFullThrottle = new EventLogThrottle (Config.AppEventLog);
            configFileWriteFailureThrottle = new EventLogThrottle (Config.AppEventLog);
        }

        public object SyncRoot
        {
            get
            {
                return syncRoot;
            }
        }

        /// <summary>
        /// Starts the application, connects to Server and prepares to receive events.
        /// </summary>
        public void Start (Config config)
        {
            this.config = config;

            //
            // Step 1. Open the log files.
            //

            OpenLogFiles ();

            //
            // Step 2. Make a pass and read the enhanced allow list.
            //

            ReloadEnhancedAllowList ();

            //
            // Step 3. Connect to server.
            //

            string appGuid = AppGuid;
            ConnectToServer (config.SplScriptPath, AppName, ref appGuid);

            //
            // Step 4. Done.
            //

            return;
        }

        /// <summary>
        /// Disconnects from server.
        /// </summary>

        public void Stop()
        {
            if (this.Connected) {
                try {
                    InternalDisconnect (null);
                }
                catch {
                }
            }
        }


        /// <summary>
        /// Request handler.
        /// </summary>
        /// <param name="sender">The serveragent object.</param>
        /// <param name="eventArgs">Request parameters.</param>
        public virtual void OnRequest (object sender, RequestReceivedEventArgs eventArgs)
        {
            Request request = eventArgs.Request;
            bool requestIsAllowed = false;
            bool addedToAllowList = false;
            string fromUserAtHost;
            string toUserAtHost;

            Header header = request.AllHeaders.FindFirst (Header.StandardHeaderType.From);
            fromUserAtHost = Utils.UriParser.GetUserAtHost (header.Value);
            header = request.AllHeaders.FindFirst(Header.StandardHeaderType.To);
            toUserAtHost = Utils.UriParser.GetUserAtHost (header.Value);
                
            //
            // Check the request origin and carry out the appropriate action.
            //

            AuthenticationInfo authenticationInfo = (AuthenticationInfo) request.AuthenticationInfo;
            if (authenticationInfo.Origin == AuthenticationInfo.MessageOrigin.NetworkExternal) {
                
                //
                // Get the From domain.
                // 

                string fromDomain = Utils.UriParser.GetHost(fromUserAtHost).ToLower();

                //
                // See if this domain has been validated already.
                // 

                
                lock (this.SyncRoot) {
                    if (allowList.Contains (fromDomain)) {

                        //
                        // This is a validated domain. It can show up here if we
                        // added this in another thread and this has not been picked up
                        // in SPL.
                        //

                        requestIsAllowed = true;
                    }
                }

                if (!requestIsAllowed) {
                    LogEntry logEntry = new LogEntry(fromUserAtHost, toUserAtHost, fromDomain);
                    lock (externalEdgeLog.SyncRoot) {
                        externalEdgeLog.Log (logEntry);
                    }
                }

            }
            else {

                // We should have only internal here - the SPL script should not be
                // dispatching the other cases here.
                Debug.Assert (authenticationInfo.Origin == AuthenticationInfo.MessageOrigin.NetworkInternal);

                //
                // Get the To domain.
                // 

                string toDomain = Utils.UriParser.GetHost (toUserAtHost).ToLower ();
                
                //
                // See if this domain has been validated already.
                // 

                lock (this.SyncRoot) {
                    if (allowList.Contains (toDomain)) {

                        //
                        // This is a validated domain. It can show up here if we
                        // added this in another thread and this has not been picked up
                        // in SPL.
                        //

                        requestIsAllowed = true;
                    }
                    else {

                        //
                        // Policy implementation. Put action to be carried out for unknown
                        // internal domains here.
                        //

                        //
                        // Check whether we are allowed to add this entry automatically.
                        //

                        if (config.ActionForUnknownDomainFromInternalEdge == Config.ActionAutoAdd) {
                            if (currentDomains + 1 < config.MaxDomainsInEnhancedAllowList) {
                                allowList.Add (toDomain, null);
                                currentDomains += 1;
                                addedToAllowList = true;
                                requestIsAllowed = true;
                            }
                            else {
                                RaiseConfigFileFullEvent ();
                            }
                        }
                    }
                }

                if (addedToAllowList) {
                    
                    //
                    // This entry was added to the global allow list and hence we need to add
                    // this to the SPL script flat file.
                    //
                    
                    try {
                        InternalAppendDomainToEnhancedAllowListFile (toDomain);
                    } catch (Exception e) {
                        // Failed to open or write to allow list.
                        lock (this.SyncRoot) {
                            allowList.Remove (toDomain);
                            currentDomains -= 1;
                            addedToAllowList = false;
                            requestIsAllowed = false;
                        }

                        RaiseConfigFileWriteFailureEvent (e.Message);
                    }

                    if (addedToAllowList) {

                        //
                        // Log that we added this entry as well.
                        //

                        LogEntry logEntry = new AllowListLogEntry (fromUserAtHost, toUserAtHost, toDomain);
                        lock (internalEdgeLog.SyncRoot) {
                            internalEdgeLog.Log (logEntry);
                        }
                    }
                }

                if (!requestIsAllowed) {
                    LogEntry logEntry = new LogEntry(fromUserAtHost, toUserAtHost, toDomain);
                    lock (internalEdgeLog.SyncRoot) {
                        internalEdgeLog.Log (logEntry);
                    }
                }
            }

            if (requestIsAllowed) {

                // 
                // Mark the request as simple proxy. This will turn on
                // performance optimizations that would otherwise be not
                // possible.
                //

                request.SimpleProxy = true;
                eventArgs.ServerTransaction.EnableForking = false;


                ClientTransaction ct = eventArgs.ServerTransaction.CreateBranch ();
                ct.SendRequest (request);
            }
            else {
                Response localResponse = request.CreateResponse (403);
                eventArgs.ServerTransaction.SendResponse (localResponse);
            }
        }

        private void InternalAppendDomainToEnhancedAllowListFile(string domain)
        {
            StringBuilder sb = new StringBuilder(domain.Length + 20);
            sb.Append (domain);
            sb.Append ("    allow");
                
            lock (enhancedAllowListUpdateLock) {

                //
                // Open the file in exclusive write mode so that others do not 
                // see our update until we are fully done. This write is not a 
                // transactional write.
                //

                StreamWriter allowListStream = new StreamWriter (File.Open (
                    config.EnhancedAllowListPath, 
                    FileMode.Append, 
                    FileAccess.Write, 
                    FileShare.None));
                try {
                    allowListStream.WriteLine (sb.ToString ());
                }
                finally {
                    if (allowListStream != null) {
                        allowListStream.Close ();
                    }
                }
            }
            return;
        }


        /// <summary>
        /// Dispose implementation.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
        }
        
        /// <summary>
        /// Dispose implementation.
        /// </summary>
        public override void Dispose (bool disposing)
        {
          
            if (!this.disposed) {

                //
                // Dispose the base class, this will disconnect as well.
                //

                base.Dispose(disposing);

                if (disposing) {

                    //
                    // Close all log files.
                    //

                    if (externalEdgeLog != null) {
                        externalEdgeLog.Dispose ();
                        externalEdgeLog = null;
                    }

                    if (internalEdgeLog != null) {
                        internalEdgeLog.Dispose ();
                        internalEdgeLog = null;
                    }
                }

                this.disposed = true;
            }
        }

        private void ReloadEnhancedAllowList()
        {
            StreamReader sr = null;
            Hashtable tempAllowList;
            string str;
            string[] tokens;
            string name, value = null;

            tempAllowList = new Hashtable();

            try {

                sr = new StreamReader (config.EnhancedAllowListPath);

                while ((str = sr.ReadLine()) != null) {
                    str = str.Trim();
                    tokens = str.Split(new char[] { ' ', '\t' });
                    
                    if (tokens.Length > 0) {
                        
                        name = tokens[0];
        
                        for (int i = 1; i < tokens.Length; i++) {
                            if (tokens[i].Length > 0) {
                                value = tokens[i];
                                break;
                            }
                        }

                        if (name != null && value != null) {
                            if (tempAllowList.Contains(name)) {
                                throw new EnhancedAllowListException(
                                    String.Format(SipStringManager.GetString("ConfigFileParseError"), 
                                    config.EnhancedAllowListPath));
                            }
                            else {
                                tempAllowList.Add(name.ToLower(), value);
                                currentDomains += 1;
                            }
                        }
                    } // tokens.Length > 0
                } // while 

                lock (this.SyncRoot) {
                    if (allowList != null) {
                        allowList.Clear ();
                    }
                    allowList = tempAllowList;
                }
            }
            catch (EnhancedAllowListException e1) {
                throw e1;
            }
            catch (Exception e) {
                throw new EnhancedAllowListException (
                    String.Format (SipStringManager.GetString ("ConfigFileOpenFailure"), 
                    config.EnhancedAllowListPath, e.GetType().ToString (), e.Message));
            }
            finally {
                if (sr != null) {
                    sr.Close();
                }
            }
        }

        private void RaiseConfigFileFullEvent ()
        {
            configFileFullThrottle.LogWarning (
                String.Format (SipStringManager.GetString("ConfigFileFull"), config.EnhancedAllowListPath)
                );        
       }

        private void RaiseConfigFileWriteFailureEvent (string message)
        {
            configFileWriteFailureThrottle.LogError (
                String.Format (
                    SipStringManager.GetString("ConfigFileWriteFailure"), config.EnhancedAllowListPath, message)
                    );
        }

        private void OpenLogFiles ()
        {
            string path;
            DateTime currentTime;
                
            currentTime = DateTime.Now;

            path = LogManager.MakeLogName (config.LogPath, ExternalEdgeLogPrefix);

            try {
                externalEdgeLog = new LogManager (config, path);
            } catch (Exception e) {
                throw new EnhancedAllowListException (
                    String.Format (SipStringManager.GetString ("LogOpenFailure"), path, e.GetType().ToString (), e.Message));
            }

            path = LogManager.MakeLogName (config.LogPath, InternalEdgeLogPrefix);

            try {
                internalEdgeLog = new LogManager (config, path);
            } catch (Exception e) {
                throw new EnhancedAllowListException (
                    String.Format (SipStringManager.GetString ("LogOpenFailure"), path, e.GetType().ToString (), e.Message));
            }
        }

        public static void Register ()
        {
            string guid = AppGuid;
            try {
                WMIRegistrationHelper regHelper = new WMIRegistrationHelper (AppName, AppUri, ref guid);
                regHelper.Register ();
            }
            catch (Exception e) {
                Config.AppEventLog.Log (EventLogEntryType.Error,
                    String.Format (SipStringManager.GetString ("WMIRegisterFailed"), e.Message, e.GetType ().ToString ()));
            }
        }

        public static void Unregister ()
        {
            string guid = AppGuid;
            try {
                WMIRegistrationHelper regHelper = new WMIRegistrationHelper (AppName, AppUri, ref guid);
                regHelper.Unregister ();
            }
            catch (Exception e) {
                Config.AppEventLog.Log (EventLogEntryType.Error,
                    String.Format (SipStringManager.GetString ("WMIUnregisterFailed"), e.Message, e.GetType ().ToString ()));
            }
        }

        private Hashtable allowList;
        private LogManager internalEdgeLog;
        private LogManager externalEdgeLog;
        private Config config;

        private EventLogThrottle configFileFullThrottle;
        private EventLogThrottle configFileWriteFailureThrottle;

        private object enhancedAllowListUpdateLock = new Object ();

        private int currentDomains;

        private object syncRoot = new Object ();
        private bool disposed;

        const string ExternalEdgeLogPrefix = "External";
        const string InternalEdgeLogPrefix = "Internal";
        const string AppGuid = "{3CF1C3BB-7D9B-4497-B180-65F2DF1EBDB5}";
        const string AppName = "EnhancedAllowList";
        const string AppUri = "http://www.microsoft.com/LC/SDK/Samples/EnhancedAllowList";
    }

     /// <summary>
    /// Represents a data record that is consumed by a LogManager.WriteLog call. Derived
    /// classes may choose to override Key and WriteRaw methods to provide their own
    /// functionality.
    /// </summary>
    public class LogEntry : ILogEntry
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LogEntry (string fromUser, string toUser, string domain)
        {
            Debug.Assert (domain != null, "Domain cannot be empty.");
            this.fromUser = fromUser;
            this.toUser = toUser;
            this.domain = domain;
            this.time = DateTime.Now;
        }


        /// <summary>
        /// A non-null value returned by this method will act as the primary key
        /// inside the log file.
        /// </summary>
        public virtual string Key
        {
            get
            {
                return domain;
            }
        }


        /// <summary>
        /// Serializer.
        /// </summary>
        public virtual void WriteRaw (TextWriter writer)
        {
            InternalWriteRaw (writer, RecordStart, RecordEnd);
        }

        protected void InternalWriteRaw (TextWriter writer, string recordStart, string recordEnd)
        {
            writer.Write (recordStart);
            writer.Write (" domain=\"");
            writer.Write (domain);
            writer.Write ("\" time=\"");
            writer.Write (time.ToShortTimeString ());
            writer.WriteLine ("\">");
            writer.Write ("\t<from>");
            writer.Write (fromUser);
            writer.WriteLine ("</from>");
            writer.Write ("\t<to>");
            writer.Write (toUser);
            writer.WriteLine ("</to>");
            writer.WriteLine (recordEnd);
        }
    
        private string domain;
        private string fromUser;
        private string toUser;
        private DateTime time;

        const string RecordStart = "<LogEntry ";
        const string RecordEnd = "</LogEntry>";
    };

    public class AllowListLogEntry : LogEntry
    {
        public AllowListLogEntry (string fromUser, string toUser, string domain) 
            : base (fromUser, toUser, domain)
        {
        }

        public override string Key 
        {
            get {
                // These records must always be added, so return null.
                return null;
            }
        }

        public override void WriteRaw (TextWriter writer)
        {
            InternalWriteRaw (writer, AllowListRecordStart, AllowListRecordEnd);
        }

        const string AllowListRecordStart = "<AddToAllowList ";
        const string AllowListRecordEnd = "</AddToAllowList>";
    };    
}
