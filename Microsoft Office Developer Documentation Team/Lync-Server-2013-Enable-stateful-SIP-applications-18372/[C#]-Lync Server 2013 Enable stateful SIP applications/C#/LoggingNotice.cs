//
//
//
// LoggingNotice.cs
//
// Copyright (C) 2004 Microsoft Corporation.
//
// This sample demonstrates stateful session handling.  The first IM in a session
// is marked with a "this conversation may be logged" warning.
//
// Session state is kept in a Hashtable and indexed by call-id.  An incoming
// ACK places a dialog into the modify-IM state, where incoming IMs are
// appended with warning text until all parties in the conversation are
// notified.  A separate session-participant table is used to determine which
// parties have already been notified, so that each party is notified exactly
// once.  Once all parties in a session have been notified, session state is
// deleted.
//
// Note that there are trivial ways to cause this application to leak.  In
// particular, clients that create a session and never send a message will
// cause this application to leak state.
//
// This sample does not support multi-party IM.
//
//
//
// Sample call flow:
//
//
//           Client1                Server              Client2
//
//              ----- INVITE ---->
//                                         ---- INVITE ---->
//
//                                         <----- 100 ------
//              <------ 100 ------
//                                         <----- 200 ------
//              <------ 200 ------
//
//              ------- ACK ----->
//                                   [1]   ------ ACK ----->
//
//
//              ----- MESSAGE --->
//                                   [2]   ---- MESSAGE --->
//
//                                         <----- 200 ------
//              <------ 200 ------
//                                         <--- MESSAGE ----
//              <---- MESSAGE ----   [3]
//
//              ------- 200 ----->
//                                         ------ 200 ----->
//
//                                         <----- BYE ------
//              <------ BYE ------   [4]
//
//
// State logic:
//
// [1] Session is established once ACK is proxied.  Application changes dialog
//     state from Unknown to ModifyNextIM.
// [2] MESSAGE arrives from client1, and dialog is in ModifyNextIM state.  IM 
//     text is modified and proxied to client2, who is added to the warned list.
//     Not all participants have been warned, so dialog state is preserved.
// [3] MESSAGE from client2 arrives, and is proxied to client1.  Client1 is
//     added to the warned list, and since all participants have been warned,
//     dialog state is torn down.
// [4] BYE triggers final session cleanup.  This covers the case where not all
//     parties sent IM during the session, a situation that would otherwise
//     leak state in this application.
//
//
//

namespace LoggingNotice
{
    using System;
    using System.Collections;
    using System.Management;
    using System.Threading;
    using System.Windows.Forms;
    using Microsoft.Rtc.Sip;

    // Common code shared by samples
    using SampleUtils;

    class LoggingNoticeApp
    {
        //
        // Enumerated session states.
        //
        internal enum SessionState
        {
            Unknown,
                // Nothing is known about this session.
                //

            ModifyNextIM
                // The ACK to an INVITE has been seen -- modify the next MESSAGE
                // in each direction.
                //
        }

        //
        // The session state table.
        //
        internal static Hashtable SessionStateTable = new Hashtable();

        //
        //
        // Retrieve the value of the first instance of a message header.
        //
        //
        internal string GetHeaderValue(Microsoft.Rtc.Sip.Message Msg, string Hdr)
        {
            IEnumerator walker = Msg.GetHeaders(Hdr);

            walker.MoveNext();
            if (walker.Current != null)
            {
                //
                // Return first header
                //
                return ((Header)walker.Current).Value;
            }
            else
            {
                //
                // No headers found
                //
                throw new InvalidOperationException("Message doesn't contain a " + Hdr + " header");
            }
        }

        //
        // 
        // Retrieve the SessionState associated with a dialog.
        //
        //
        internal SessionState GetSessionState(Microsoft.Rtc.Sip.Message Msg)
        {
            string CallId = GetHeaderValue(Msg, "Call-Id");

            if (SessionStateTable.ContainsKey(CallId))
            {
                //
                // Return saved session state
                //
                return (SessionState)SessionStateTable[CallId];
            }
            else
            {
                //
                // No state exists -- default state is Unknown
                //
                return SessionState.Unknown;
            }
        }

        //
        //
        // Update the SessionState for a dialog.
        //
        //
        internal void UpdateSessionState(string CallId, SessionState State)
        {
            SessionStateTable[CallId] = State;
        }

        //
        // The session participant table.  Each entry is an ArrayList containing
        // session parties that have been sent the logging notice.
        //
        internal static Hashtable ParticipantTable = new Hashtable();

        //
        //
        // Add a warned participant to the list.
        //
        //
        internal void MarkParticipant(string CallId, string ToAddr)
        {
            ArrayList Parties = ParticipantTable[CallId] as ArrayList;

            //
            // If participant list doesn't exist for this Call-Id, create it.
            if (Parties == null)
            {
                ParticipantTable[CallId] = Parties = new ArrayList();
            }
            else
            {
                //
                // Table already exists
                //
            }

            //
            // Add the party.
            if (!Parties.Contains(ToAddr))
            {
                Parties.Add(ToAddr);
            }
            else
            {
                //
                // Participant already in list
                //
            }
        }
        
        //
        //
        // Test whether a participant has been warned.
        //
        //
        internal bool ParticipantWarned(string CallId, string Addr)
        {
            ArrayList Parties = ParticipantTable[CallId] as ArrayList;

            //
            // List doesn't exist?  Participant hasn't been warned
            if (Parties == null)
            {
                //
                // List doesn't exist, so participant hasn't been warned
                //
                return false;
            }
            else
            {
                //
                // List exists: participant has been warned if address is a list member
                //
                return Parties.Contains(Addr);
            }

        }

        //
        //
        // Count the number of warned participants in a dialog.
        //
        //
        internal int WarnCount(string CallId)
        {
            ArrayList Parties = ParticipantTable[CallId] as ArrayList;

            if (Parties == null)
            {
                //
                // List doesn't exist, so noone's been warned
                //
                return 0;
            }
            else
            {
                //
                // Participant warn count is length of list
                //
                return Parties.Count;
            }
        }

        //
        //
        // Delete all state associated with the specified call-id
        //
        //
        static void DeleteState(string CallId)
        {
            //
            // Delete entry in session table.
            //
            SessionStateTable.Remove(CallId);

            //
            // Delete participant list.
            //
            ParticipantTable.Remove(CallId);
        }

    //
    //
    //  OnRequest:
    //
    //      Request handler.  Called for each ACK and MESSAGE (see
    //      filter in LoggingNotice.am)
    //
    //
        public void OnRequest(object sender, RequestReceivedEventArgs e)
        {
            string CallId = GetHeaderValue(e.Request, "Call-Id");
            SessionState state = GetSessionState(e.Request);

            //
            // State logic.
            //
            switch (state)
            {
                //
                // Unknown state + ACK -> ModifyNextMessage
                //
                case SessionState.Unknown:
                    if (e.Request.StandardMethod == Request.StandardMethodType.Ack)
                    {
                        //
                        // Update session state so that next MESSAGE is modified
                        //
                        UpdateSessionState(CallId, SessionState.ModifyNextIM);
                    }
                    else if (e.Request.StandardMethod == Request.StandardMethodType.Bye)
                    {
                        //
                        // Remove session and party state
                        //
                        DeleteState(CallId);
                    }
                    else
                    {
                        //
                        // Request isn't an ACK, so no state change
                        //
                    }
                    break;

                //
                // ModifyNextIM state + MESSAGE -> modify IM, and update list
                // of participants to be notified
                //
                case SessionState.ModifyNextIM:
                    if (e.Request.StandardMethod == Request.StandardMethodType.Message)
                    {
                        string ToAddr = GetHeaderValue(e.Request, "To");

                        //
                        // If the recipient hasn't been warned, make it so
                        //
                        if (!ParticipantWarned(CallId, ToAddr))
                        {
                            //
                            // Warn participant by appending text to this IM
                            //
                            e.Request.Content += "\r\n(*** This conversation may be logged. ***)";
                            MarkParticipant(CallId, ToAddr);
                        }
                        else
                        {
                            //
                            // Participant has been warned already
                            // 
                        }

                        if (WarnCount(CallId) == 2)
                        {
                            //
                            // If all participants have been warned, clean up state.
                            //
                            DeleteState(CallId);
                        }
                        else
                        {
                            //
                            // More participants remain to be warned.
                            //
                        }
                    }
                    else
                    {
                        //
                        // Request isn't a MESSAGE, so just proxy
                        //
                    }
                    break;

                default:
                    break;
            }

            // 
            // Mark the request as simple proxy. This will turn on
            // performance optimizations that would otherwise be not
            // possible.
            //

            e.Request.SimpleProxy = true;
            e.ServerTransaction.EnableForking = false;

            //
            // In all cases, proxy the request.
            //
            e.ServerTransaction.CreateBranch().SendRequest(e.Request);
        }

    //
    //
    //
    //      Main.
    //
    //
    //
        static void Main(string[] args)
        {
            LoggingNoticeApp app = new LoggingNoticeApp();
            ServerAgent agent = AppUtils.ConnectToServer(app, "LoggingNotice.am");
            if (agent != null)
            {
                //
                // Main loop.
                //
                Console.WriteLine("LoggingNotice sample is running.  Press Control-C to quit.");
                while (true)
                {
                    agent.WaitHandle.WaitOne();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(agent.ProcessEvent));
                }
            }
        }
    }
}
