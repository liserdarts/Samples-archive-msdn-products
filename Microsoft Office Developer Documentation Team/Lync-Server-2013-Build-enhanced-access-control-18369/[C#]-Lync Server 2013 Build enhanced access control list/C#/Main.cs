/*++

Copyright © Microsoft Corporation

Module Name:

    Main.cs

Abstract:

    Main program for the EnhancedAllowList sample. Implements installer logic
    and service start/stop logic.

Notes:

    Supports installation from InstallUtil.exe. The ealsrv.exe.config file must
    be present in same directory as executable for proper functioning. See Readme
    for configuration details.
    
--*/
// #define VSDEBUG
#region Using directives
using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Specialized;
using System.ServiceProcess;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Configuration.Install;
using Microsoft.Win32;
using Microsoft.Rtc.Sip.SDK.Samples.Utils;
using System.ComponentModel;
#if VSDEBUG
using System.Windows.Forms;
#endif
#endregion

namespace Microsoft.Rtc.Sip.SDK.Samples.EnhancedAllowList
{


    #region ServiceLogic

#if !VSDEBUG
    [RunInstallerAttribute (true)]
    public class EALSRVInstaller : Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller processInstaller;

        public EALSRVInstaller ()
        {
            serviceInstaller = new ServiceInstaller ();
            processInstaller = new ServiceProcessInstaller ();

            serviceInstaller.DisplayName = Config.DisplayName;
            serviceInstaller.ServiceName = Config.ServiceName;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServicesDependedOn = new String[] {
                    Config.LCSServiceName
                };

            processInstaller.Account = ServiceAccount.User;

            Installers.Add (serviceInstaller);
            Installers.Add (processInstaller);
        }

        /// <summary>
        /// Carry out custom installer actions. 
        /// </summary>
        /// <remarks>
        /// We need to create WMI configuration only. EventLog will be done by InstallUtil.exe
        /// </remarks>
        public override void Install (IDictionary state)
        {
            base.Install (state);
            PolicyManager.Register ();
            state.Add (RegisteredWithWMI, true);
        }

        public override void Rollback (IDictionary state)
        {
            base.Rollback (state);
            if (state.Contains (RegisteredWithWMI)) {
                try {
                    PolicyManager.Unregister ();
                }
                catch {
                }
            }
        }

        public override void Uninstall (IDictionary state)
        {
            base.Uninstall (state);
            try {
                PolicyManager.Unregister ();
            }
            catch {
            }
        }

        public const string RegisteredWithWMI = "wmiregistered";

    };
#endif

    /// <summary>
    /// Helper class to launch the application from console.
    /// </summary>
    public class ConsoleApp : ServiceMain
    {
        public void Start ()
        {
            OnStart (null);
        }
    };
    
    /// <summary>
    /// This class implements the core server startup/stop logic.
    /// </summary>

    public class ServiceMain : ServiceBase
    {
        public ServiceMain ()
        {
            this.ServiceName = Config.ServiceName;
        }

        protected override void OnStart (string[] args)
        {
            startPending = true;
            if (!ThreadPool.QueueUserWorkItem (new WaitCallback (RealStartService))) {
                throw new Exception ("Fatal internal error.");
            }
        }

        protected override void OnStop ()
        {
            RealStopService ();
        }

        /// <summary>
        /// This routine implements application startup. This is to be executed
        /// usually in a thread pool thread (so that the main thread can wait for stop
        /// events - from SCM or from console).
        /// </summary>
        private void RealStartService (object unused)
        {
            int i, maxRetries = 10;

            try {

                for (i = 0; i < maxRetries; i++) {

                    lock (serviceLock) {
                        if (cancelStart) {
                            Config.AppEventLog.LogInfo (
                                SipStringManager.GetString ("ServiceStartupAbort"));
                            return;
                        }
                    }

                    try {
                        ServerAgent.WaitForServerAvailable (10);
                        break;
                    }
                    catch {
                        if ((i % 2) == 0) {
                            Config.AppEventLog.LogError (
                                SipStringManager.GetString ("ServiceStartPending"));
                        }
                        continue;
                    }
                }

                if (i == maxRetries) {
                    Config.AppEventLog.LogError (
                        SipStringManager.GetString ("ServerUnreachable"));
                    Environment.Exit (1);
                }

                lock (serviceLock) {

                    if (cancelStart) {
                        Config.AppEventLog.LogInfo (
                               SipStringManager.GetString ("ServiceStartupAbort"));
                        return;
                    }

                    policyManager = new PolicyManager ();
                    
                    try {

                        //
                        // Read settings.
                        //

                        NameValueCollection properties;
                        Config config = null;

                        try {
                            
                            properties = ConfigurationManager.AppSettings;

                            config = new Config (properties);
                        }
                        catch (Exception e) {
                            Config.AppEventLog.LogError (
                                String.Format (SipStringManager.GetString ("AppConfigFileParseError"), e.GetType ().ToString (), e.Message));
                            Environment.Exit (1);
                        }

                        //
                        // See if a working directory has been specified.
                        //

                        if (config.WorkingDirectory != null) {
                            try {
                                Environment.CurrentDirectory = config.WorkingDirectory;
                            }
                            catch {
                                Config.AppEventLog.LogError (
                                    String.Format (SipStringManager.GetString ("InvalidWorkingDirectory"), config.WorkingDirectory));
                                Environment.Exit (1);
                            }
                        }

                        policyManager.Start (config);
                    }
                    catch (Exception e) {
                        Config.AppEventLog.LogError (
                            String.Format (SipStringManager.GetString ("AppException"),
                            e.Message, e.GetType ().ToString (), e.StackTrace));

                        Console.WriteLine ("Got exception: " + e.Message);
                        if (e.InnerException != null) {
                            Console.WriteLine ("Inner exception: " + e.InnerException.Message);
                        }

                        Environment.Exit (1);
                    }

                    Config.AppEventLog.LogInfo (SipStringManager.GetString ("ServiceStarted"));
                }
            }
            finally {
                lock (serviceLock) {
                    startPending = false;
                }
            }
        }

        /// <summary>
        /// Core stop functionality.
        /// </summary>
        private void RealStopService ()
        {
            Monitor.Enter (serviceLock);

            try {
            int retries = 0;

            //
            // If service is starting, wait until the startup thread exists.
            // 

            while (startPending && retries < 20) {
                cancelStart = true;
                Monitor.Exit (serviceLock);
                Thread.Sleep (100);
                Monitor.Enter (serviceLock);
                retries++;
            }


            //
            // We can safely stop the service now.
            //

            if (policyManager != null) {

                try {
                    policyManager.Stop ();
                }
                catch {
                }

                policyManager.Dispose ();
            }

            policyManager = null;
            } finally {
                Monitor.Exit (serviceLock);
                Config.AppEventLog.LogInfo (SipStringManager.GetString ("ServiceStopped"));
            }
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);
        }


    #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main (string[] args)
        {
            SipStringManager.CreateResourceManager ("EnhancedAllowList");

            AppEventLog appEventLog = new AppEventLog (Config.ServiceName, "Application");

            Config.AppEventLog = appEventLog;

#if VSDEBUG

                //
                // Registration/Unregistration code for stand alone exe.
                // Service process reg/unreg will be done at install/uninstall time.
                //

                if (args.Length > 0) {

                    string firstArg = args[0].ToLower ();
                    if (firstArg == "/register") {
                        PolicyManager.Register ();
                    }
                    else if (firstArg == "/unregister") {
                        PolicyManager.Unregister ();
                    }
                    else {
                        Console.WriteLine ("Unknown argument - {0}", firstArg);
                    }

                    return;
                }

                ConsoleApp app = new ConsoleApp ();

                app.Start ();
                
                MessageBox.Show ("Hit OK to exit ... ", "EAL Server");

                app.Stop ();

#else
            ServiceBase[] ServicesToRun;

            ServicesToRun = new ServiceBase[] {
                    new ServiceMain ()
                };

            ServiceBase.Run (ServicesToRun);
#endif
        }

        static PolicyManager policyManager;
        static bool startPending;
        static bool cancelStart;
        static Object serviceLock = new Object ();
    };


    /// <summary>
    /// Global configuration manager. 
    /// </summary>
    public class Config
    {
        public string WorkingDirectory;
        public string LogPath;
        public string EnhancedAllowListPath;
        public string SplScriptPath;
        public string ActionForUnknownDomainFromInternalEdge;
        public int MaxLogFileSize;
        public int MaxDomainsLogged;
        public int MaxDomainsInEnhancedAllowList;

        public Config (NameValueCollection configProperties)
        {
            WorkingDirectory = configProperties[WorkingDirectoryKey];
            ReadAndValidate (configProperties, LogPathKey, out LogPath);
            ReadAndValidate (configProperties, EnhancedAllowListPathKey, out EnhancedAllowListPath);
            ReadAndValidate (configProperties, SplScriptPathKey, out SplScriptPath);
            ReadAndValidate (configProperties, ActionForUnknownDomainFromInternalEdgeKey, out ActionForUnknownDomainFromInternalEdge);

            if (ActionForUnknownDomainFromInternalEdge != ActionAutoAdd &&
                ActionForUnknownDomainFromInternalEdge != ActionManual &&
                ActionForUnknownDomainFromInternalEdge != ActionCustom) {
                throw new EnhancedAllowListException (String.Format ("Value for {0} is invalid.", ActionForUnknownDomainFromInternalEdge));
            }

            ReadAndValidate (configProperties, MaxLogFileSizeKey, out MaxLogFileSize);
            ReadAndValidate (configProperties, MaxDomainsLoggedKey, out MaxDomainsLogged);
            ReadAndValidate (configProperties, MaxDomainsInEnhancedAllowListKey, out MaxDomainsInEnhancedAllowList);
        }

        private void ReadAndValidate (NameValueCollection configProperties, string key, out string value)
        {
            value = configProperties[key];
            if (value == null) {
                throw new ArgumentNullException (key);
            }
        }

        private void ReadAndValidate (NameValueCollection configProperties, string key, out int value)
        {
            string strVal;
            ReadAndValidate (configProperties, key, out strVal);
            try {
                value = Int32.Parse (strVal);
                if (value > 0) {
                    return;
                }
            }
            catch {
            }

            throw new ArgumentOutOfRangeException (key, "Must be valid integer > 0");
        }

        const string WorkingDirectoryKey = "WorkingDirectory";
        const string LogPathKey = "LogPath";
        const string EnhancedAllowListPathKey = "EnhancedAllowListPath";
        const string SplScriptPathKey = "SPLScriptPath";
        const string MaxLogFileSizeKey = "MaxLogFileSize";
        const string MaxDomainsLoggedKey = "MaxDomainsLogged";
        const string MaxDomainsInEnhancedAllowListKey = "MaxDomainsInEnhancedAllowList";
        const string ActionForUnknownDomainFromInternalEdgeKey = "ActionForUnknownDomainFromInternalEdge";

        // Valid values for the ActionForUnknownDomainFromInternalEdge field.
        public const string ActionAutoAdd = "auto";
        public const string ActionManual = "manual";
        public const string ActionCustom = "custom";

        // Global constants
        public const string ServiceName = "EALSRV";
        public const string DisplayName = "Enhanced Allow List Service";
        public const string LCSServiceName = "RTCSRV";

        public static AppEventLog AppEventLog;

    }

    public class EnhancedAllowListException : Exception
    {
        public EnhancedAllowListException (string message) : base(message)  { }

        public EnhancedAllowListException (string message, Exception innerException) : base (message, innerException)
        {
        }
    }

    public class TraceListener : TextWriterTraceListener
    {
        public TraceListener ()
        {
            base.Writer = Console.Out;
        }
    }
}
 
