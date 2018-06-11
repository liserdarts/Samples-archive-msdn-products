//
//
//    Archiver.cs
//
//    This sample demonstrates simple IM archiving.  When run on a server,
//    this application logs all requests and responses to a text file,
//    Archiver.log.  Note that REGISTER and other requests consumed by
//    the ES are not logged by this application when run on a Homeserver
//    or Front End server.
//
//    In topologies where there are multiple HS/FE servers, each running an
//    instance of Archiver, messages traversing multiple servers are logged
//    only once.  Messages are stamped as they're processed, and already-
//    stamped messages are proxied without logging.  
//
//    Note that messages are archived at the first host they traverse, so
//    each request is logged at the From: user's FE.  In federated scenarios,
//    messages will be logged in both domains, since stamp data is stripped
//    at edge servers.
//
//    Features demonstrated in this sample:
//
//    - Topology-aware application design, using message stamping for
//      app communication
//    - Use of the Stamp message property in C#.
//
//    Copyright (C) 2004 Microsoft Corporation
//
//
//


namespace Archiver
{
    using System;
    using System.IO;
    using System.Management;
    using System.Threading;
    using Microsoft.Rtc.Sip;

    // Common code shared by samples
    using SampleUtils;

    public class ArchiverApp
    {
        //
        // The output logfile.
        //
        static private StreamWriter LogFile;

        //
        //
        // Request handler.  Proxy the message, then log to disk if the request hasn't
        // been logged on another server by an instance of this application.
        //
        //
        public void OnRequest(object sender, RequestReceivedEventArgs e)
        {
            // 
            // Mark the request as simple proxy. This will turn on
            // performance optimizations that would otherwise be not
            // possible.
            //

            e.Request.SimpleProxy = true;
            e.ServerTransaction.EnableForking = false;

            //
            // Proxy the request.
            //
            e.ServerTransaction.CreateBranch().SendRequest(e.Request);

            //
            // If this request hasn't been marked as already archived, log it
            // to file.
            //
            if (e.Request.Stamp != "Archived")
            {
                lock (LogFile)
                {
                    LogFile.WriteLine("-----------------------------------------------------------");
                    LogFile.WriteLine(" Request: {0} {1}", e.Request.Method, e.Request.RequestUri);

                    foreach (Header h in e.Request.AllHeaders)
                    {
                        LogFile.WriteLine("  {0}: {1}", h.Type, h.Value);
                    }

                    LogFile.WriteLine();
                    LogFile.WriteLine(e.Request.Content);
                    LogFile.Flush();
                }

                //
                // Stamp the message as having been archived. 
                //
                e.Request.Stamp = "Archived";
            }
            else
            {
                //
                // This message has a stamp, and has been logged by another instance
                // of Archiver.
                //
            }
        }

        //
        //
        // Response handler.  Proxy the message, then log to disk if it hasn't been
        // logged on another server by an instance of this application.
        //
        // Responses to INVITE, ACK, MESSAGE, and BYE are handled here, as configured
        // in Archiver.am.
        //
        public void OnResponse(object sender, ResponseReceivedEventArgs e)
        {
            //
            // Proxy the response.
            //
            e.ClientTransaction.ServerTransaction.SendResponse(e.Response);

            //
            // If this response hasn't been marked as already archived, log it
            // to file.
            //
            if (e.Response.Stamp != "Archived")
            {
                lock (LogFile)
                {
                    LogFile.WriteLine("-----------------------------------------------------------");
                    LogFile.WriteLine(" Response: {0} {1}", e.Response.StatusCode, e.Response.ReasonPhrase);

                    foreach (Header h in e.Response.AllHeaders)
                    {
                        LogFile.WriteLine("  {0}: {1}", h.Type, h.Value);
                    }

                    LogFile.WriteLine();
                    LogFile.WriteLine(e.Response.Content);
                    LogFile.Flush();
                }

                //
                // Stamp the message as having been archived. 
                //
                e.Response.Stamp = "Archived";
            }
            else
            {
                //
                // This message has a stamp, and has been logged by another instance
                // of Archiver.
                //
            }
        }

        //
        //
        // Main.
        //
        //
        static void Main(string[] args)
        {
            ArchiverApp app = new ArchiverApp();
            ServerAgent agent = AppUtils.ConnectToServer(app, "Archiver.am");
            if (agent != null)
            {
                //
                // Open logfile.
                //
                try
                {
                    LogFile = new StreamWriter(new FileStream("IM.log", FileMode.Create, FileAccess.Write));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create IM.log: " + e.Message);
                    return;
                }
                
                //
                // Event dispatch loop.
                //
                Console.WriteLine("Archiver sample started.  Press Control-C to quit.");
                while (true)
                {
                    agent.WaitHandle.WaitOne();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(agent.ProcessEvent));
                }
            }
        }
    }
}
