using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Management.Automation.Runspaces;  // Run PowerShell
using System.Management.Automation;            // Run PowerShell
using Microsoft.Rtc.Collaboration;  // UCMA core
using Microsoft.Rtc.Signaling;
using System.Configuration;    // System.Configuration.dll

namespace PresenceSubInterceptorForBot
{
    /// <summary>
    /// This class serves as a listener for new application endpoints installed on the 
    /// hosted Lync Server. When instantiated, it runs in a separate background thread. 
    /// Every five minutes it checks for the trusted application endpoints and stores 
    /// the SipUri and DisplayName values of the found endpoints that are also enabled 
    /// for internet access as name-value pairs in a Dictionary object, activatedBots. 
    /// In addition, it provides two lookup services to determine if a given SipUri is 
    /// an application endpoint and to provision the display name of an application 
    /// endpoint given its SipUri.
    /// </summary>
    class AppEndpointManager
    {
        static String _appID = null;
        static CollaborationPlatform _collabPlatform;
        static ApplicationEndpoint _superEndpoint;
        static SipPeerToPeerEndpoint _superP2PEndpoint;
        static String DisableHeaderValidationForSendMessage = null;
        static String _pidfNotifyText =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            "<presence xmlns=\"urn:ietf:params:xml:ns:pidf\"" +
            "   xmlns:ep=\"urn:ietf:params:xml:ns:pidf:status:rpid-status\"" +
            "   xmlns:et=\"urn:ietf:params:xml:ns:pidf:rpid-tuple\"" +
            "   xmlns:ci=\"urn:ietf:params:xml:ns:pidf:cipid\" entity=\"{2}\">" +
            "  <tuple id=\"0\">" +
            "    <status> <basic>{0}</basic> </status>" +
            "  </tuple>" +
            "  <ci:icon></ci:icon>" +
            "  <ci:display-name>{1}</ci:display-name>" +
            "</presence>";

        private Dictionary<string, string> activatedBots = new Dictionary<string, string>();
        Thread backgroundThread;

        /// <summary>
        /// Constructor of AppEndpointManager class
        /// </summary>
        public AppEndpointManager()
        {
            ReadAppConfig();
            CreateP2PEndpoint();
            StartBotsChecker();
        }

        /// <summary>
        /// Read application configuration file, which is named as PresenceSubInterceptorForBot.exe.Config
        /// and should be in the same directory as the executable.
        ///     ApplicationID is used to provision ApplicationEndpointSettings in UCMA.
        ///     DiableHeaderValidationForSendMessage is read by UCMA to disable header valication.
        /// </summary>
        void ReadAppConfig()
        {
            // Read the PresenceSubInterceptorForBot.exe.Config file for ApplicationID value.
            if (ConfigurationManager.AppSettings["ApplicationID"] != null)
            {
                _appID = ConfigurationManager.AppSettings["ApplicationID"];
                Console.WriteLine(String.Format("_appID: {0}", _appID));
            }

            // Read the PresenceSubInterceptorForBot.exe.Config file for DisableHeaderValidationForSendMessage value.
            if (ConfigurationManager.AppSettings["DisableHeaderValidationForSendMessage"] != null)
            {
                DisableHeaderValidationForSendMessage = ConfigurationManager.AppSettings["DisableHeaderValidationForSendMessage"];
                Console.WriteLine("DisableHeaderValidationForSendMessage=" + DisableHeaderValidationForSendMessage);
            }

        }

        /// <summary>
        /// Lookup to determine if a given SIP URI corresponds to a bot
        /// </summary>
        /// <param name="sipUri"></param>
        /// <returns></returns>
        public bool IsSipUriApplicationEndpoint(string sipUri)
        {
            string sipUriToFind = sipUri.ToLowerInvariant();
            Console.WriteLine("Looking up SipUri: " + sipUriToFind);
            if (activatedBots.ContainsKey(sipUriToFind))
            {
                Console.WriteLine("Found Application Endpoint [sipUri]: " + sipUriToFind);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Lookup to get the display name of the bot of the given SIP URI
        /// </summary>
        /// <param name="sipUri"></param>
        /// <returns></returns>
        public string GetDisplayNameForApplicationEndpoint(string sipUri)
        {
            string sipUriToFind = sipUri.ToLowerInvariant();
            Console.WriteLine("Looking up DisplayName for SipUri: " + sipUriToFind);
            if (activatedBots.ContainsKey(sipUriToFind))
            {
                Console.WriteLine("Found Application Endpoint [sipUri]: " + sipUriToFind);
                return activatedBots[sipUriToFind];
            }
            return string.Empty;
        }

        /// <summary>
        /// Run the ActivatedBotsChecker routine in a separate thread
        /// </summary>
        public void StartBotsChecker()
        {
            // This is sample code. In production you would want to apply 
            // more appropriate thread synchronization techniques
            backgroundThread = new Thread(new ThreadStart(ActivatedBotsChecker));
            backgroundThread.Start();
        }

        /// <summary>
        /// Sets up a loop to check and update activated application endpoints
        /// </summary>
        private void ActivatedBotsChecker()
        {
            AutoResetEvent quit = new AutoResetEvent(false);
            WaitHandle[] waitHandles = new WaitHandle[1];
            waitHandles[0] = quit;

            CheckAndUpdateApplicationEndpoints();

            while (true)
            {
                int result = WaitHandle.WaitAny(waitHandles, new TimeSpan(0, 5, 0));
                // Wake up
                CheckAndUpdateApplicationEndpoints();
            }
        }

        /// <summary>
        /// Executing Lync Server Management PowerShell cmdlets to obtain the list of
        /// installed ApplicationEndpoint instances.
        /// </summary>
        private void CheckAndUpdateApplicationEndpoints()
        {
            Console.WriteLine("Checking for new application endpoints...");

            // Get ready to call Lync Server Management PowerShell cmdlets.
            InitialSessionState iss = InitialSessionState.CreateDefault();
            iss.ImportPSModule(new[] { "Lync" });

            // Invoke the Lync Server Management PowerShell cmdlet of 
            // Get-CsTrustedApplicationEndpoint.
            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace(iss))
            {
                myRunSpace.Open();

                // Execute the Get-CsTrustedApplication cmdlet to get the list of 
                // activated application endpoint
                using (PowerShell powershell = PowerShell.Create())
                {
                    powershell.Runspace = myRunSpace;
                    powershell.AddCommand("Get-CsTrustedApplicationEndpoint");

                    Collection<PSObject> results = null;
                    Collection<ErrorRecord> errors = null;
                    try
                    {
                        results = powershell.Invoke();
                        errors = powershell.Streams.Error.ReadAll();
                    }

                    catch (Exception ex)
                    {
                        // An error occurred while running the cmdlets.
                        // Should have more robust handling in production code
                        Console.WriteLine("Error: {0}", ex.Message);
                        throw;
                    }

                    if (errors.Count != 0)
                    {
                        // Errors were reported by the cmdlets.
                        // Should be handled in production applications.
                    }

                    // Create a new list of the app endpoints
                    Dictionary<string, string> updatedActivatedBots = new Dictionary<string, string>();

                    // Parse the results returned from the Get-CsTrustedApplicationEndpoint Comdlet and 
                    // cache the display name and sipUri of the found applicaiton endpoints that are also
                    // enabled for PIC users. The cache is maintained in the updatedactivatedBots object.
                    // The results from the cmdlet are a collection of the 
                    // Microsoft.Rtc.Management.ADConnect.Schema.OCSADApplicationContact objects, which 
                    // is apparently undocumented. The shape of the OCSADApplicationContact object can 
                    // be inferred from the Set-CsTrustedApplicationEndpoint cmdlet.
                    // Among other parameters, it has SipAddress, DisplayName and EnabledForFederation 
                    foreach (PSObject result in results)
                    {
                        string endpointSipUri = (string)result.Members["SipAddress"].Value;
                        endpointSipUri = endpointSipUri.ToLowerInvariant();

                        // Check if the endpoint is enabled for PIC users
                        bool enabledForPIC = (bool)result.Members["EnabledForInternetAccess"].Value;
                        if (enabledForPIC == false)
                        {
                            Console.WriteLine("SipAddress: {0} NOT Enabled for PIC. Ignoring...", endpointSipUri);
                            continue;
                        }

                        // Set displayName as the endpoint's SIP URI, if DisplayName property on the
                        // resultant PSObject is empty or null. Otherwise, set the returned value.
                        string displayName = endpointSipUri;
                        if (result.Members["DisplayName"] != null)
                            if (!String.IsNullOrEmpty(result.Members["DisplayName"].Value as string))
                                displayName = result.Members["DisplayName"].Value as string;

                        Console.WriteLine("Adding SipAddress: {0} & DisplayName: {1}", endpointSipUri, displayName);

                        updatedActivatedBots.Add(endpointSipUri, displayName);
                    }

                    // Update the cache with the new list of activated application endpoints
                    this.activatedBots = updatedActivatedBots;
                }
            }
        }

        /// <summary>
        /// Sends the Notify message back to the PIC user
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="_sipAddressessOfBot"></param>
        public void SendBotsOnlineStatusToPicClient(string sipUriOfPicClient, string sipUriOfBot, 
            string callId, string cseq, string toHeaderValue, string fromHeaderValue) 
        {
    
            Console.WriteLine("Sending Bot's Open status to PIC client as a NOTIFY using UCMA...");
            try
            {
                List<SignalingHeader> headers = new List<SignalingHeader>();
                headers.Add(new SignalingHeader("EVENT", "presence"));
                headers.Add(new SignalingHeader("Subscription-State", "active"));
                headers.Add(new SignalingHeader("Call-Id", callId));
                headers.Add(new SignalingHeader("CSeq", cseq));
                headers.Add(new SignalingHeader("To", toHeaderValue));
                headers.Add(new SignalingHeader("From", fromHeaderValue));

                Console.WriteLine("App-configured SignalingHeaders:");
                foreach (SignalingHeader h in headers)
                    Console.WriteLine(h.Name + ": " + h.GetValue());
                Console.WriteLine("PIC user URI: " + sipUriOfPicClient);
                Console.WriteLine("Bot Uri: " + sipUriOfBot);

                string displayName = this.GetDisplayNameForApplicationEndpoint(sipUriOfBot);
                System.Net.Mime.ContentType contentType = new System.Net.Mime.ContentType("application/pidf+xml");
                System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                byte[] notifyBody = enc.GetBytes(String.Format(_pidfNotifyText, "open", displayName, sipUriOfBot));
                Console.WriteLine("PIDFNotifyBody =" + System.Text.UTF8Encoding.UTF8.GetString(notifyBody));

                // Target address of the NOTIFY
                RealTimeAddress targetAddress = new RealTimeAddress(sipUriOfPicClient);

                // Shouldn't be synchronized call in production code
                _superP2PEndpoint.BeginSendMessage(
                    sipUriOfBot,
                    MessageType.Notify,
                    targetAddress,
                    contentType,
                    notifyBody,
                    headers,
                    (a) =>
                    {
                        try
                        {
                            _superP2PEndpoint.EndSendMessage(a);
                        }
                        catch (RealTimeException rtex)
                        {
                            // report and ignore the failure
                            Console.WriteLine("EndSendMessage: Notify failed with exception: {0}", rtex.Message);
                        }
                    }, 
                    null);
                Console.WriteLine("End calling SendBotsOnlineStatusToPicClient!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed in SendBotsOnlineStatusToPicClient: \r\n{0}\r\n{1}", e.Message, e.StackTrace);

            }
        }

        /// <summary>
        /// Creates the P2PEndpoint for sending the Notify. A P2P endpoint enjoys 
        /// auto-provisioning of the necessary application endpoint settings
        /// if it is obtained from an established ApplicationEndpoint created with
        /// ProvisionedApplicationPlatformSettings.
        /// </summary>
        void CreateP2PEndpoint()
        {
            Console.WriteLine("Creating P2P Endpoint...for {0}", _appID);
            try
            {
                if (!string.IsNullOrEmpty(_appID))
                {
                    Console.WriteLine("Creating CollaborationPlatform for the provisioned application with "
                        + "ID \'{0}\' using ProvisionedApplicationPlatformSettings.", _appID);

                    ProvisionedApplicationPlatformSettings settings
                        = new ProvisionedApplicationPlatformSettings("UCMASampleApp", _appID);
                    _collabPlatform = new CollaborationPlatform(settings);
                    // Receive applicationEndpoint settings via auto-provisioning
                    _collabPlatform.RegisterForApplicationEndpointSettings(ApplicationEndpointSettingsDiscovered);
                    _collabPlatform.EndStartup(_collabPlatform.BeginStartup(null, null));

                    Console.WriteLine("platform started.");
                }
                else
                {
                    Console.WriteLine("Error: Application ID not provided.");
                }
            }
            catch (Exception iOpEx)
            {
                Console.WriteLine(iOpEx.ToString());
            }
        }

        /// <summary>
        /// Event handler to receive auto-provisioned ApplicationEndpointSettings for 
        /// a given application Id. Auto-provisioned ApplicationEndpointSettings contains
        /// the correct proxy server and proxy port, as well as other configurational 
        /// information required to establish a connection between the endpoint and the
        /// server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void ApplicationEndpointSettingsDiscovered(object sender,
            ApplicationEndpointSettingsDiscoveredEventArgs e)
        {
            Console.WriteLine("ApplicationEndpointOwnerDiscovered event was raised.");
            Console.WriteLine("provisioned application ID = \'{0}\' ", _appID);
            Console.WriteLine("Owner display name is: " + e.ApplicationEndpointSettings.OwnerDisplayName);
            Console.WriteLine("Owner URI is: " + e.ApplicationEndpointSettings.OwnerUri);
            Console.WriteLine("ProxyHost: " + e.ApplicationEndpointSettings.ProxyHost);
            Console.WriteLine("ProxyPort: " + e.ApplicationEndpointSettings.ProxyPort);

            try
            {
                ApplicationEndpointSettings settings = e.ApplicationEndpointSettings;
                _superEndpoint = new ApplicationEndpoint(_collabPlatform, settings);

                // Establish the endpoint.
                _superEndpoint.EndEstablish(_superEndpoint.BeginEstablish(null, null));

                Console.WriteLine("ApplicationEndpoint state={0}", _superEndpoint.State.ToString());
                _superP2PEndpoint = (SipPeerToPeerEndpoint)_superEndpoint.InnerEndpoint;
            }
            catch (InvalidOperationException iOpEx)
            {
                // Invalid Operation Exception may be thrown if the data provided
                // to the BeginXXX methods was invalid/malformed.
                // TODO (Left to the reader): Error handling code.
                Console.WriteLine("Invalid Operation Exception: " + iOpEx.ToString());
            }
        }
    }
}
