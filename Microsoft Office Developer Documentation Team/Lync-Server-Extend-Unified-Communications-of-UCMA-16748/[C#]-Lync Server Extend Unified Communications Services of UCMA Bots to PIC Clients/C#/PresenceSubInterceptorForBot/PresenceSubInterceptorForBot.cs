using System;
using System.IO;
using System.Threading;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Rtc.Sip;       // serverAgent.dll

namespace PresenceSubInterceptorForBot
{
    public class PresenceSubInterceptorForBot
    {
        static AppEndpointManager appEndpointManager;

        /// <summary>
        /// OnSubscribe serves as the entry point to the managed Lync Server application component 
        /// that the MSPL script calls. When this event handler is called, the following must be true:
        ///    1. The message sender is a PIC user
        ///    2. The message is a SipRequest of the SUBSCRIBE method for remote presence
        ///    
        /// What needs to be determined further is if the presence Subscribe request is targeted to 
        /// a bot or not and if the request is a terminating SUBSCRIBE or not.
        ///    
        /// What's performed here includes:
        ///    1. Reroute the sipRequest back to the server if the request is not targeted to a bot. 
        ///    2. Send to the PIC client a SIP 200 OK response to acknowledge that the request to 
        ///       receive a bot's presence is received successfully.
        ///    3. Send to the PIC client a SIP NOTIFY message containing the targeted bot's presence 
        ///       status.
        ///       
        /// The NOTIFY message needs to be sent with the dialog-Id (Call-Id, From-Tag, To-tag) 
        /// as specified in the incoming SUBSCRIBE request. However, the NOTIFY To header value mmust
        /// correspond to the SUBSCRIBE From header value and the NOTIFY From header value must 
        /// correspond to the SUBSCRIBE To header value.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSubscribe(object sender, RequestReceivedEventArgs e)
        {
            Console.WriteLine("Intercepted a Subscribe Request...");
            PrintMessage(e.Request);

            try
            {
                var toHeaderValue = e.Request.AllHeaders.FindFirst(Header.StandardHeaderType.To).Value;

                //var headerValue = e.Request.AllHeaders.FindFirst(Header.StandardHeaderType.To).Value.Split(';')[0];
                string sipUriOfBot = ExtractStringFromFirstAngularBracket(toHeaderValue.Split(';')[0]);
                Console.WriteLine("sipUri of bot: " + sipUriOfBot);

                // If sipUriOfBot is for an activated bot, handle the intercepted subscribe request. 
                // Otherwise, sends the request back to the server for normal processing.
                if (appEndpointManager.IsSipUriApplicationEndpoint(sipUriOfBot))
                {
                    // Accept the request with a 200 OK response
                    this.Send200OkResponse(e);

                    // Send notify to return the Online (Open) presence status of the bot,  
                    // if the request is not a terminating SUBSCRIBE, i.e., with expires=0. 
                    // PIC client sends SUBSCRIBE with Expires = 0 on logout only.
                    if (!IsExpiresZero(e))
                    {
                        // Get the SIP URI of PIC client from the SIP request
                        //headerValue = e.Request.AllHeaders.FindFirst(Header.StandardHeaderType.From).Value.Split(';')[0];
                        var fromHeaderValue = e.Request.AllHeaders.FindFirst(Header.StandardHeaderType.From).Value;
                        string sipUriOfPicClient = ExtractStringFromFirstAngularBracket(fromHeaderValue.Split(';')[0]);
                        Console.WriteLine("sipUri of PIC client: " + sipUriOfPicClient);

                        // Get the Call-ID value from the SIP request
                        string callId = e.Request.AllHeaders.FindFirst(Header.StandardHeaderType.CallID).Value;
                        Console.WriteLine("callId: " + callId);

                        // Get the CSeq header value from the SIP request
                        var headerValue = e.Request.AllHeaders.FindFirst(Header.StandardHeaderType.CSeq).Value.Split(' ')[0];
                        int cseqNum = int.Parse(headerValue);
                        string cseq = cseqNum + 1 + " Notify";
                        Console.WriteLine("cseq: " + cseq);
                        
                        // Send Online Status of Bot to PIC client, reversing the To and From header of the incoming request
                        appEndpointManager.SendBotsOnlineStatusToPicClient(
                            sipUriOfPicClient, sipUriOfBot, callId, cseq, fromHeaderValue, toHeaderValue);
                        Console.WriteLine("Done with appEndpointManager.SendBotsOnlineStatusToPicClient");
                    }
                }
                else
                {
                    // Send the unhandled SUBSCRIBE to the server for normal processing 
                    // because the target of this request is not an activated bot.
                    Console.WriteLine("The target of the request is not an activated bot.");
                    e.ServerTransaction.EnableForking = false;
                    e.ServerTransaction.CreateBranch().SendRequest(e.Request);  
                }
                
            }
            catch (Exception ex)
            {
                // When in error, reroute the message back to the server.
                Console.WriteLine(ex.ToString());
                e.ServerTransaction.EnableForking = false;
                e.ServerTransaction.CreateBranch().SendRequest(e.Request);
            }
        }

        /// <summary>
        /// Private helper method for logging and tracing
        /// </summary>
        /// <param name="msg"></param>
        static void PrintMessage(Message msg)
        {
            if (msg == null)
                return;

            Console.WriteLine("\r\nStart printing a SIP message ...");
            if (msg is Request)
            {
                Request rsq = msg as Request;
                Console.WriteLine("Method: {0}", rsq.Method);
            }
            else if (msg is Response)
            {
                Response rsp = msg as Response;
                Console.WriteLine("StatusClass: {0}\r\nStatusCode: {1}", rsp.StatusClass, rsp.StatusCode);
            }

            foreach (Header h in msg.AllHeaders)
                if (h != null)
                    Console.WriteLine("{0}: {1}", h.Type, h.Value);
            Console.WriteLine();
            Console.WriteLine("Body := {0}\r\n", msg.Content);
            Console.WriteLine("End printing a SIP message.\r\n");
        }

        /// <summary>
        /// Private helper method to extract SIP URI value out of To or From header value.
        /// </summary>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        private string ExtractStringFromFirstAngularBracket(string headerValue)
        {
            if (headerValue.Contains("<"))
            {
                int index1 = headerValue.IndexOf('<');
                int index2 = headerValue.IndexOf('>');
                return headerValue.Substring(index1 + 1, index2 - index1 - 1);
            }
            else
                return headerValue;
        }

        /// <summary>
        /// Private helper function to verfiy if a Subscribe request has its Expires header set to zero
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool IsExpiresZero(RequestReceivedEventArgs e)
        {
            string expires = e.Request.AllHeaders.FindFirst(Header.StandardHeaderType.Expires).Value;
            long expireTicks;
            if (long.TryParse(expires, out expireTicks))
                if (expireTicks == 0)
                    return true;
            return false;
        }

        /// <summary>
        /// Send SIP/200 OK Response to an intercepted SUBSCRIBE request
        /// </summary>
        /// <param name="e"></param>
        void Send200OkResponse(RequestReceivedEventArgs e)
        {
            Console.WriteLine("\r\nBegin to send 200 OK response...");

            try
            {
                // Create a Response instance from the correpsonding Request.
                // It is not ncessary to set up any response header values because 
                // the server will set them properly. If needed, application-specific headers,
                // such as diagnostic-related headers, may be added by an application.
                Response response = e.Request.CreateResponse(200);
                e.ServerTransaction.SendResponse(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ServerTransaction.SendResponse(r) erred: \r\n{0}", ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("InnerException:\r\n{0}", ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Entry point to this console application.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Starting PresenceSubInterceptorForBot...");

            try
            {
                // Instantiate and iitialize the AppEndpointManager class
                appEndpointManager = new AppEndpointManager();

                // Start Processing presence
                var presenceProcess = new Thread(ProcessPresence);
                presenceProcess.Start();
                Console.WriteLine("presence process started.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        static private void ProcessPresence()
        {
            try
            {
                // Connect this Lync Server API application to the server. If successful, 
                // a compiled instance of the application manifest will be loaded to the 
                // server and the embedded script ready to dispatch events to the 
                // OnSubscribe event handler.
                PresenceSubInterceptorForBot app = new PresenceSubInterceptorForBot();
                ServerAgent agent = ConnectToServer(app, "PresenceSubInterceptorForBot.am");
                if (agent != null)
                {
                    // Event dispatch loop.
                    Console.WriteLine("ServerAgent for PresenceSubInterceptorForBot sample started.");
                    Console.WriteLine("Press Control-C to quit.");
                    ManualResetEvent resetEvent = new ManualResetEvent(false);
                    WaitHandle[] handleArray = new WaitHandle[] {agent.WaitHandle, resetEvent};

                    WaitCallback waitCallback = new WaitCallback(agent.ProcessEvent);
                    while (true)
                    {
                        int signaledEvent = WaitHandle.WaitAny(handleArray);
                        if (signaledEvent == 0)
                        {
                            try
                            {
                                if (!ThreadPool.QueueUserWorkItem(waitCallback))
                                {
                                    Console.WriteLine("QueueUserWorkItem failed, quitting.");
                                    return;
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine("Failed to invoke ThreadPool.QueueUserWorkItem: {0}", ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Quit signaled, worker will quit.");
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error while running ProcessPresence: {0}", ex.Message);
            }
        }

        static ServerAgent ConnectToServer(object app, string amFile)
        {
            try
            {
                ServerAgent.WaitForServerAvailable(3);      // 3 Attempts
            }
            catch (Exception e1)
            {
                Console.WriteLine("ERROR: Server unavailable - {0}", e1.Message);
                if (e1 is UnauthorizedException)
                {
                    Console.WriteLine("must be running under an account that is a member of the \"RTC Server Applications\" local group");
                }
                return null;
            }


            ApplicationManifest am = ApplicationManifest.CreateFromFile(amFile);
            if (am == null)
            {
                Console.WriteLine("ERROR: {0} application manifest file not found.", amFile);
                return null;
            }

            try
            {
                am.Compile();
            }
            catch (CompilerErrorException e2)
            {
                Console.WriteLine("ERROR: {0} application manifest file contained errors:", amFile);
                foreach (string message in e2.ErrorMessages)
                {
                    Console.WriteLine(message);
                }
                return null;
            }

            try
            {
                ServerAgent agent = new ServerAgent(app, am);
                Console.WriteLine("ServerAgent instantiated successfully with Role={0}",agent.Role.ToString());
                return agent;
            }
            catch (Exception e3)
            {
                Console.WriteLine("ERROR: Unable insnatiate ServerAgent and to connect to server - {0}", e3.Message);
                Console.WriteLine("ERROR: type - {0}", e3.GetType().ToString());
                Console.WriteLine("ERROR: stacktrace: {0}", e3.StackTrace);
                if (e3.InnerException != null)
                    Console.WriteLine("ERROR: inner exception: {0} ", e3.InnerException.Message);
                return null;
            }
        }

    }
}
