/*++

Copyright © Microsoft Corporation

Module Name:

    SessionManager.cs

Abstract:

    This module implements server connection management
    and basic SIP state management for SIPSnoop.
    
Notes:

    A typical Live Communications Server application loads and
    compiles an application manifest on start up, registers it 
    with WMI if not done already, and then connects to the Live
    Communications Server (through ServerAgent). 
    
    It then creates a main message pump that receives events 
    from the Live Communications Server (through ServerAgent)
    and dispatches it to various event handlers for processing. 
    
    These event handlers perform application specific actions
    including SIP state management (dialog state maintenance, 
    for example). 
    
    On exit, the application optionally deregisters the 
    application from WMI.
    
--*/
using System;
using System.Text;
using System.Windows;
using System.Management;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Resources;
using System.Collections.Specialized;
using Microsoft.Rtc.Sip;
using Microsoft.Rtc.Sip.SDK.Samples.Utils;

namespace Microsoft.Rtc.Sip.SDK.Samples.SIPSnoop
{

    #region SessionManager

    public class SessionManager : IDisposable
    {

        #region PublicInterface

        /// <summary>
        /// the SessionManager instance for this application
        /// </summary>
        /// <returns></returns>
        public static SessionManager GetSessionManager ()
        {
            return sessionManager;
        }



        /// <summary>
        /// Global variable that controls various operations.
        /// </summary>
        public static bool AddHeader = false;
        public static bool ModifyToHeader = false;
        public static bool SimpleProxy = false;
        public static bool AsyncCall = false;

        /// <summary>
        /// delegate for session disconnect notifications
        /// </summary>
        public delegate void DisconnectListener (string reason);

        /// <summary>
        /// add to disconnect listener if you want to receive
        /// notifications
        /// </summary>
        public DisconnectListener DisconnectListeners;

        /// <summary>
        /// delegate for receiving SIP messages
        /// </summary>
        /// <remarks>args will be either RequestReceivedEventArgs
        /// or ResponseReceivedEventArgs
        /// </remarks>
        public delegate void StateChangeListener (Object args);

        /// <summary>
        /// add to state change listener to receive SIP
        /// requests or responses
        /// </summary>
        public StateChangeListener StateChangeListeners;

        /// <summary>
        /// contains summaries of SIP events 
        /// </summary>
        public Statistics Statistics
        {
            get
            {
                return statistics;
            }
        }

        /// <summary>
        /// disconnect from server, cleanup
        /// </summary>
        public void Disconnect ()
        {
            InternalDisconnect ();
            return;
        }

        /// <summary>
        /// Are we connected to the server ?
        /// </summary>
        public bool Connected
        {
            get
            {
                return this.serverAgent != null;
            }
        }

        /// <summary>
        /// What is the current ServerRole ?
        /// </summary>
        public ServerAgent.ServerRole Role
        {
            get
            {
                if (this.serverAgent == null) {
                    throw new InvalidOperationException ("Not connected to server.");
                }

                return this.serverAgent.Role;
            }
        }


        #endregion PublicInterface


            #region InternalStaticFunctions

        /// <summary>
        /// construct the session manager 
        /// </summary>
        static SessionManager ()
        {
            SessionManager.sessionManager = new SessionManager ();
        }

        #endregion InternalStaticFunctions


            #region ConnectionManagementBoilerPlate

        /// <summary>
        /// Load and compile the application manifest, and try
        /// to connect to the Live Communications Server.
        /// </summary>
        /// <exception cref="System.Exception">If unable to
        /// connect. The exception string contains the details
        /// </exception>
        /// <param name="manifestFile">file name of the SPL manifest.
        /// This file must be present in the working directory of the 
        /// executable
        /// </param>
        /// <param name="applicationName">A friendly name for use while
        /// registering with WMI. Use a null value if you dont want 
        /// the function to do registration
        /// </param>
        /// <param name="appGuid">Guid for use during WMI registration.
        /// If a null guid is specified but applicationName is not 
        /// null, a new guid will be generated, used, and returned. 
        /// </param>
        /// <seealso cref="Microsoft.Rtc.Sip.CompilerErrorException"/>
        /// <seealso cref="Microsoft.Rtc.Sip.ServerAgent"/>
        public void ConnectToServer (string manifestFile, string applicationName, ref string appGuid)
        {
            ///load and compile the application manifest
            applicationManifest = ApplicationManifest.CreateFromFile (manifestFile);
            if (applicationManifest == null) {
                throw new Exception (
                    String.Format ("The manifest file {0} was not found", manifestFile));
            }

            try {
                applicationManifest.Compile ();

                ///try to connect to server
                serverAgent = new ServerAgent (this, applicationManifest);
            }
            catch (CompilerErrorException cee) {
                ///collapse all compiler errors into one, and return it
                StringBuilder sb = new StringBuilder (1024, 1024);
                foreach (string errorMessage in cee.ErrorMessages) {
                    if (errorMessage.Length + sb.Length + 2 < sb.MaxCapacity) {
                        sb.Append (errorMessage);
                        sb.Append ("\r\n");
                    }
                    else {
                        ///compiler returns really large error message
                        ///so just return what we can accomodate
                        sb.Append (errorMessage.Substring (0, sb.MaxCapacity - sb.Length - 1));
                        break;
                    }
                }

                throw new Exception (sb.ToString ());
            }
            catch (Exception e) {
                if (applicationManifest != null) {
                    applicationManifest = null;
                }

                //ServerNotFoundException || UnauthorizedException
                throw e;
            }


            ///hook the connection dropped event handler
            serverAgent.ConnectionDropped += new ConnectionDroppedEventHandler (this.ConnectionDroppedHandler); //ConnectionDroppedEventHandler

            ///start the eventManager thread after making sure one is 
            ///not already running
            eventManagerQuit.Reset ();
            eventManagerThread = new Thread (new ThreadStart (EventManagerHandler));
            eventManagerThread.Start ();

            return;
        }


        /// <summary>
        /// Event manager routine. Listen for events
        /// from the server, and dispatch them.
        /// </summary>
        protected virtual void EventManagerHandler ()
        {
            Debug.Write ("Event manager is started");

            ///Wait on the serverAgent event notification handle 
            ///and the eventManager exit notification handle. 
            WaitHandle[] waitHandle = new WaitHandle[2];
            waitHandle[0] = serverAgent.WaitHandle;
            waitHandle[1] = this.eventManagerQuit;

            WaitCallback wcb = new WaitCallback (serverAgent.ProcessEvent);

            DateTime lastExpiration = DateTime.Now;
            int passCount = 0;

            while (true) {
                int handleSignalled = WaitHandle.WaitAny (waitHandle);

                ///are we asked to quit ?
                if (handleSignalled == 1) {
                    Debug.Write ("Event manager exiting");
                    return;
                }

                ///an event was received from the server, Dispatch it
                ThreadPool.QueueUserWorkItem (wcb);

                passCount += 1;
                if ((passCount % 32) == 0) {
                    //
                    // Check whether we need to perform soft state expiration.
                    //

                    DateTime currentTime = DateTime.Now;
                    TimeSpan tsDiff = currentTime - lastExpiration;
                    if (tsDiff.TotalMilliseconds > Session.ExpirationPassPeriod) {
                        //
                        // It is about 2 hours since we checked our call state
                        // for stale entries. Make another pass.
                        //

                        CleanupStaleEntries ();
                        lastExpiration = currentTime;
                    }
                }
            }
        }


        /// <summary>
        /// Disconnect from the Live Communications Server, cleanup
        /// </summary>
        /// <remarks> If we are connected to the server,
        /// to disconnect and cleanup is to dispose the
        /// server agent object. 
        /// </remarks>
        protected void InternalDisconnect ()
        {
            if (serverAgent == null)
                return; ///already gone

            if (eventManagerThread != null) {
                ///first stop our event manager thread
                eventManagerQuit.Set ();
                eventManagerThread.Join (1000 /* upto a second */);
                eventManagerThread = null;
            }

            if (serverAgent != null) {
                ///remove the connection to server
                ServerAgent serverAgentToDispose = serverAgent;
                serverAgent = null;
                serverAgentToDispose.Dispose ();

            }

            applicationManifest = null;

            return;
        }


        /// <summary>
        /// This callback will be invoked by ServerAgent when we are
        /// disconnected by the server due to some external reason
        /// </summary>
        /// <param name="cde">reason for connection drop</param>
        protected void ConnectionDroppedHandler (object sender, ConnectionDroppedEventArgs cde)
        {
            ///stop event manager and cleanup
            InternalDisconnect ();

            ///notify all listeners who want to know that we lost
            ///the server connection
            string reason = String.Format ("Reason: {0}", cde.Reason);


            this.DisconnectListeners (reason);

            return;
        }

        #endregion ConnectionManagementBoilerPlate


        #region SIPMessageHandlers

        /// <summary>
        /// This function receives SIP requests, updates
        /// session state variables, and proxies the request
        /// to the default request uri
        /// </summary>
        /// <remarks>
        /// The request handler's name must be the name of the 
        /// function that is given in the SPL Dispatch function 
        /// for SIP requests. 
        /// </remarks>
        /// <param name="sender">not used</param>
        /// <param name="e">the request state</param>
        public void RequestHandler (object sender, RequestReceivedEventArgs e)
        {
            /* If this is a SIP INVITE, then create an entry 
             *  in the session state table for this call-id. 
             *  A session is established, when an ACK for this 
             *  call-id is received. 
             */
            Request request = e.Request;

            if (request.StandardMethod == Request.StandardMethodType.Invite) {

                ///extract the call-id and create session state
                Header callIdHeader = request.AllHeaders.FindFirst ("Call-ID");

                if (callIdHeader != null) {
                    Session newSession = new Session ();
                    newSession.State = Session.States.Initializing;
                    newSession.CallId = callIdHeader.Value;
                    lock (sessionStateTable.SyncRoot) {
                        sessionStateTable[callIdHeader.Value] = newSession;
                    }
                }
            }
            else if (request.StandardMethod == Request.StandardMethodType.Ack) {

                ///extract the call-id and update session state, ignore errors
                Header callIdHeader = request.AllHeaders.FindFirst ("Call-ID");

                if (callIdHeader != null) {
                    Session session = sessionStateTable[callIdHeader.Value] as Session;
                    if (session != null) {
                        session.State = Session.States.Established;
                        statistics.Update (true /* new session */);
                    }
                }
            }

            ///update other counters
            statistics.Update (request.StandardMethod);

            Header fromHeader = request.AllHeaders.FindFirst ("From");
            Header toHeader = request.AllHeaders.FindFirst ("To");


            statistics.Update (SipUriParser.GetUserAtHost (fromHeader.Value));
            statistics.Update (SipUriParser.GetUserAtHost (toHeader.Value));

            ///notify the state change
            this.StateChangeListeners (e);

            ///We will not be forking, and marking this explicitly
            ///allows ServerAgent to optimize message proxying.
            e.ServerTransaction.EnableForking = false;

            ///we are done with state management, proxy
            ClientTransaction ct = e.ServerTransaction.CreateBranch ();

            if (SimpleProxy) {
                // Setting SimpleProxy to true will turn on performance
                // optimizations during message proxying.
                request.SimpleProxy = true;
            }

            ///Add a header if requested by user.
            if (AddHeader) {
                Header h = new Header (SipSnoopHeaderName, SipSnoopHeaderValue);
                request.AllHeaders.Add (h);
            }

            ///Modify To header if requested by user by adding a parameter.
            if (ModifyToHeader) {
                NameValueCollection toParamColl = toHeader.Parameters;
                toParamColl.Set (SipSnoopParamName, SipSnoopParamValue);
                toHeader.Parameters = toParamColl;
            }

            ct.SendRequest (e.Request);

            return;
        }



        /// <summary>
        /// This function receives SIP responses, updates
        /// session state variables, and proxies the response
        /// </summary>
        /// <remarks>
        /// The response handler's name must be the name of 
        /// the function that is given in the SPL Dispatch 
        /// function for SIP responses. 
        /// </remarks>
        /// <param name="sender">not used</param>
        /// <param name="e">the response state</param>
        public void ResponseHandler (object sender, ResponseReceivedEventArgs e)
        {
            /* If this is a 200 response and the method is 
               BYE then the session is ending. 
            */
            Response response = e.Response;

            if (e.ClientTransaction.ServerTransaction.Request.StandardMethod == Request.StandardMethodType.Bye) {
                ///extract the call-id and cleanup session state
                ///ignore errors
                Header callIdHeader = response.AllHeaders.FindFirst ("Call-ID");

                if (callIdHeader != null) {
                    lock (sessionStateTable.SyncRoot) {
                        if (sessionStateTable[callIdHeader.Value] != null) {
                            sessionStateTable.Remove (callIdHeader.Value);
                            statistics.Update (false /* SessionTearDown */);
                        }
                    }
                }
            }

            ///update other counters
            statistics.Update (response.StatusClass);

            ///notify listeners
            this.StateChangeListeners (e);

            ///Add a header if requested by user.
            if (AddHeader) {
                Header h = new Header (SipSnoopHeaderName, SipSnoopHeaderValue);
                e.Response.AllHeaders.Add (h);
            }

            if (ModifyToHeader) {
                Header toHeader = response.AllHeaders.FindFirst ("To");
                NameValueCollection toParamColl = toHeader.Parameters;
                toParamColl.Set (SipSnoopParamName, SipSnoopParamValue);
                toHeader.Parameters = toParamColl;
            }

            ///done with state management, so forward the response
            e.ClientTransaction.ServerTransaction.SendResponse (e.Response);

            return;
        }


        #endregion SIPMessageHandlers


        #region AsyncCallHandlers


        /// <summary>
        /// This function receives asynchronous one-way
        /// notification for requests. 
        /// </summary>
        /// <remarks>
        /// The request handler's name must be the name of the 
        /// function that is given in the SPL Dispatch function 
        /// for SIP request async calls. 
        /// </remarks>
        /// <param name="sender">not used</param>
        /// <param name="e">the arguments for async call</param>
        public void RequestNotificationHandler (object sender, NotificationReceivedEventArgs e)
        {
            object[] parameters = e.Parameters;

            // First argument from manifest is SIP Method,
            // use it for statistics

            statistics.Update(Request.GetStandardMethod((string) parameters[0]));

            // Next two arguments are From and To user, use it
            // for statistics
            statistics.Update(SipUriParser.GetUserAtHost((string) parameters[1]));

            statistics.Update(SipUriParser.GetUserAtHost((string) parameters[2]));

            // Raise the event finally
            this.StateChangeListeners(e);
        }


        /// <summary>
        /// This function receives asynchronous one-way
        /// notification for responses. 
        /// </summary>
        /// <remarks>
        /// The response handler's name must be the name of the 
        /// function that is given in the SPL Dispatch function 
        /// for SIP request async calls. 
        /// </remarks>
        /// <param name="sender">not used</param>
        /// <param name="e">the arguments for async call</param>
        public void ResponseNotificationHandler (object sender, NotificationReceivedEventArgs e)
        {
            object[] parameters = e.Parameters;

            // First argument from manifest is SIP response code,
            // use it for statistics

            statistics.Update(Response.GetStatusClass((int) parameters[0]));

            // Last two arguments are From and 
            // To user, use it for statistics
            statistics.Update(SipUriParser.GetUserAtHost((string) parameters[1]));

            statistics.Update(SipUriParser.GetUserAtHost((string) parameters[2]));

            // Raise the event finally
            this.StateChangeListeners(e);
        }

        #endregion AsyncCallHandlers
        
        #region StateManagement

        protected void CleanupStaleEntries ()
        {
            DateTime currentTime = DateTime.Now;
            ArrayList staleList = new ArrayList ();

            lock (sessionStateTable.SyncRoot) {

                foreach (DictionaryEntry de in sessionStateTable) {

                    Session session = (Session) de.Value;

                    if ((currentTime - session.LastChanged).TotalMilliseconds >
                         Session.SessionTimeoutPeriod) {

                        staleList.Add (session);
                    }
                }


                for (int i = 0; i < staleList.Count; i++) {
                    Session session = (Session) staleList[i];
                    sessionStateTable.Remove (session.CallId);
                    if (session.State == Session.States.Established) {
                        // An established session is being teared down.
                        statistics.Update (false);
                    }
                }

            }

        }
        #endregion


        #region MemberVariables

        /// <summary>
        /// the session manager singleton instance
        /// </summary>
        protected static SessionManager sessionManager;

        /// <summary>
        /// objects needed for connecting to the server
        /// </summary>
        protected ApplicationManifest applicationManifest;
        protected ServerAgent serverAgent;

        /// <summary>
        /// the Event manager thread implements the main
        /// message pump for receiving SIP messages and 
        /// delivers it to various handlers. 
        /// </summary>
        protected Thread eventManagerThread;
        protected AutoResetEvent eventManagerQuit;


        /// <summary>
        /// session state table 
        /// </summary>
        protected Hashtable /* <Call-Id, Session> */ sessionStateTable;

        /// <summary>
        /// helper for request/response aggregate counters
        /// </summary>
        protected Statistics statistics;


        #endregion MemberVariables


        #region Miscellany

        /// <summary>
        /// Instantiate the state manager for managing various state
        /// counters. 
        /// </summary>
        protected SessionManager ()
        {
            ///initialize members
            ///
            sessionStateTable = new Hashtable ();

            statistics = new Statistics ();

            eventManagerQuit = new AutoResetEvent (false);
            return;
        }

        public void Dispose ()
        {
            InternalDisconnect ();
        }

        #endregion Miscellany

        public const string SipSnoopHeaderName = "Inspected-By";
        public const string SipSnoopHeaderValue = "SipSnoop";
        public const string SipSnoopParamName = "modified-by";
        // Parameters can be quoted for case sensitivity.
        // Unquoted parameters are case insensitive.
        public const string SipSnoopParamValue = "\"sipsnoop\"";
    }

    #endregion SessionManager

    #region Statistics

    public class Statistics
    {

        #region PublicAccessors
        /// <summary>
        /// We support only 7 here, as that leads to fast
        /// calculations for our GUI
        /// </summary>
        public enum StandardMethod : int
        {
            Invite = 0,
            Message = 1,
            Notify = 2,
            Register = 3,
            Bye = 4,
            Info = 5,
            Other = 6
        };

        /// <summary>
        /// returns the aggregate requests seen for the 
        /// particular method type
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// if bad param is passed</exception>
        public int Count (StandardMethod sm)
        {
            if (sm >= StandardMethod.Invite && sm <= StandardMethod.Other)
                return requests[(int) sm];

            return 0;
        }

        /// <summary>
        /// returns the aggregate responses seen for the 
        /// particular response class (100 to 600). 
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// if bad param is passed</exception>
        public int Count (int sc)
        {
            if (sc >= 100 && sc <= 600)
                return responses[sc / 100 - 1];
            else
                return 0;
        }

        /// <summary>
        /// total users seen by the application
        /// </summary>
        public int Users
        {
            get
            {
                return userTable.Count;
            }
        }

        /// <summary>
        /// total sessions established so far
        /// </summary>
        public int TotalSessions
        {
            get
            {
                return totalSessions;
            }
        }

        /// <summary>
        /// active sessions
        /// </summary>
        public int ActiveSessions
        {
            get
            {
                return activeSessions;
            }
        }

        /// <summary>
        /// increments number of requests received 
        /// for the particular method type
        /// </summary>
        /// <param name="sm">the sip method</param>
        public void Update (Request.StandardMethodType sm)
        {
            Interlocked.Increment (ref requests[MapStandardMethodTypeToIndex (sm)]);
        }

        /// <summary>
        /// increments the number of responses received
        ///  for the particular status class
        /// </summary>
        /// <param name="sc">response status class</param>
        public void Update (int sc)
        {
            Interlocked.Increment (ref responses[sc / 100 - 1]);
        }

        /// <summary>
        /// increments user counters based on the user name
        /// </summary>
        /// <param name="User">user@host (can be null, in which
        /// case it is skipped) </param>
        public void Update (string user)
        {
            if (user != null) {
                lock (userTable.SyncRoot) {
                    userTable[user] = user;
                }
            }
        }

        /// <summary>
        ///  updates the session management counters
        /// </summary>
        /// <param name="established">true if the session
        /// was established, false if the session was teared down
        /// </param>
        public void Update (bool sessionEstablished)
        {
            if (sessionEstablished) {
                Interlocked.Increment (ref totalSessions);
                Interlocked.Increment (ref activeSessions);
            }
            else {
                Interlocked.Decrement (ref activeSessions);
            }
        }

        #endregion PublicAccessors


        /// <summary>
        /// request aggregates
        /// </summary>
        protected int[] requests;

        /// <summary>
        /// response aggregates
        /// </summary>
        protected int[] responses;


        /// <summary>
        /// user table - store users seen for counting
        /// the number of users that we saw
        /// </summary>
        protected Hashtable userTable;

        /// <summary>
        /// some more session aggregates
        /// </summary>
        protected int totalSessions, activeSessions;

        public Statistics ()
        {
            requests = new int[7 /* sizeof(StandardMethod) */];
            responses = new int[6 /* sizeof(StatusClass (1xx to 6xx)) */];
            userTable = new Hashtable ();
        }

        /// <summary>
        /// Map a StandardMethodType to StandardMethod that can
        /// then be used as an index into our requests array
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        protected int MapStandardMethodTypeToIndex (Request.StandardMethodType sm)
        {
            switch (sm) {
                case Request.StandardMethodType.Invite:
                    return (int) StandardMethod.Invite;
                case Request.StandardMethodType.Bye:
                    return (int) StandardMethod.Bye;
                case Request.StandardMethodType.Info:
                    return (int) StandardMethod.Info;
                case Request.StandardMethodType.Register:
                    return (int) StandardMethod.Register;
                case Request.StandardMethodType.Notify:
                    return (int) StandardMethod.Notify;
                case Request.StandardMethodType.Message:
                    return (int) StandardMethod.Message;
                default:
                    return (int) StandardMethod.Other;
            }
        }

    }


    #endregion Statistics

    #region Session

    /// <summary>
    /// this class implements a primitive SIP session.
    /// </summary>
    public class Session
    {
        public enum States : int
        {
            Initializing, Established, TearedDown
        };

        public States State
        {
            set
            {
                this.state = value;
                this.refresh = DateTime.Now;
            }
            get
            {
                return this.state;
            }
        }

        public string CallId
        {
            set
            {
                this.callId = value;
            }
            get
            {
                return callId;
            }
        }


        public DateTime LastChanged
        {
            get
            {
                return refresh;
            }
        }

        /// <summary>
        /// current session state
        /// </summary>
        protected States state;

        /// <summary>
        /// call-id (unique identifier) of the session
        /// </summary>
        protected string callId;

        /// <summary>
        /// last accessed, not used in this implementation
        /// </summary>
        protected DateTime refresh;

        public Session ()
        {
        }


        /// <summary>
        /// Session expiration checks will be done every 1 hour.
        /// </summary>

        public const int ExpirationPassPeriod = 60 * 60 * 1000;


        /// <summary>
        /// This is the duration that a call is allowed to be in
        /// established state. If it does not receive a BYE until
        /// then it is automatically removed. In practice, this may
        /// be based on other parameters.
        /// </summary>

        public const int SessionTimeoutPeriod = 60 * 60 * 1000;
    }

    #endregion Session
}


