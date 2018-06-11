/*++

Copyright © Microsoft Corporation

Module Name:

    Utils.cs
    
Abstract:

    Helper classes.    
    
--*/
using System;
using System.Management;
using System.Diagnostics;
using System.Collections;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace Microsoft.Rtc.Sip.SDK.Samples.Utils
{

    #region WMIRegistrationHelper


    /// <summary>
    /// Register/Unregister a Live Communications Server application with
    /// WMI (MSFT_SIPApplicationSetting)
    /// </summary>
    sealed public class WMIRegistrationHelper 
    {
        private ManagementObject appSetting;

        /// <summary>
        /// register a Live Communications Server application with wmi
        /// </summary>
        /// <param name="name">a friendly name</param>
        /// <param name="uri">the application uri. this must
        /// be same as the value of the r:appUri tag used in the
        /// application's SPL manifest</param>
        /// <param name="guid">A guid, that is enclosed inside { }.
        /// If a null guid is specified, then a new one is created
        /// and returned
        /// </param>
        /// <exception cref="FormatException">If the guid was not enclosed
        /// inside { }</exception>
        public WMIRegistrationHelper(string name, string uri, ref string guid)
        {

            ///Connect to cimv2, and create an instance of 
            ///MSFT_SIPApplicationSetting
            ManagementClass appSettingClass = new ManagementClass(
                "root/cimv2",
                "MSFT_SIPApplicationSetting",
                new ObjectGetOptions()
                );

            appSetting = appSettingClass.CreateInstance();

            ///Create a new guid if necessary
            if (guid == null)
            {
                guid = "{" + Guid.NewGuid().ToString().ToUpper() + "}";
            }

            if (guid.StartsWith("{") == false || guid.EndsWith("}") == false)
            {
                throw new FormatException("Guid must be enclosed inside { } when calling");
            }

            ///set the properies of the MSFT_SIPApplicationSetting instance
            appSetting["InstanceID"] = guid;
            
            ///uri != null, Caller is likely to register
            if (uri != null) 
            {
                appSetting["URI"] = uri;
                appSetting["Name"] = name;
                appSetting["Enabled"] = "TRUE"; 
                appSetting["Critical"] = "FALSE"; 
            }
        
            return;
        }


        /// <summary>
        /// Registers with WMI the specific SIP application
        /// </summary>
        public void Register() 
        {
            try
            {
                appSetting.Put();
            } 
            catch (Exception e)
            {
                ///ignore, can happen if already registered
                Debug.Write(e.Message);
            }

        }


        /// <summary>
        /// Unregister the app from WMI
        /// </summary>
        /// <exception cref="System.Exception">if unable to 
        /// commit the new application setting in wmi
        /// </exception>
        public void Unregister() 
        {
            try
            {
                appSetting.Delete();
            }
            catch (Exception e)
            { 
                ///ignore, can happen if class was not found
                Debug.Write(e.Message);
            }
        }



        public enum AppLocation
        {
            FirstApp,
            LastApp
        };

        //
        // Manipulates the MSFT_SIPApplicationPriorityList.  Moves the uri
        // to the start or end of the list, depending on the value of loc.
        //
        public void ConfigAppOrder(string uri, AppLocation loc)
        {
            try
            {
                ManagementClass appSettingClass;
                ManagementClass appPriorityClass;
                ManagementObject thisApp = null;
                string thisId = null;
                ArrayList appList = null;

                appSettingClass = new ManagementClass("root/cimv2", "MSFT_SIPApplicationSetting", new ObjectGetOptions());
                appPriorityClass = new ManagementClass("root/cimv2", "MSFT_SIPApplicationPriorityList", new ObjectGetOptions());

                //
                // Look up the URI
                //
                foreach (ManagementObject obj in appSettingClass.GetInstances())
                {
                    if ((string)obj["URI"] == uri)
                    {
                        thisApp = obj;
                        thisId = (string)obj["InstanceID"];
                        break;
                    }
                }

                if (thisApp == null)
                {
                    return;
                }

                //
                // Get the InstanceID list, which is a literal "{GUID}","{GUID}",...
                //
                foreach (ManagementObject priList in appPriorityClass.GetInstances())
                {
                    appList = new ArrayList(((string[])priList["InstanceIDList"]));

                    //
                    // Move the URI
                    //
                    int listlen = appList.Count;

                    appList.Remove(thisId);
                    switch (loc)
                    {
                        case AppLocation.FirstApp:
                            appList.Insert(0, thisId);
                            break;
                        case AppLocation.LastApp:
                            appList.Add(thisId);
                            break;
                        default:
                            appList.Add(thisId);
                            break;
                    }

                    //
                    // Commit
                    //
                    string[] newIdList = new string[appList.Count];
                    appList.CopyTo(newIdList);
                    priList["InstanceIDList"] = newIdList;
                    priList.Put();

                    break;
                }
            }
            catch (Exception x)
            {
                Console.WriteLine("Error inside ConfigApp: " + x.Message);
                throw;
            }
        }

    }

    #endregion WMIRegistrationHelper


    #region URIParser

    /// <summary>
    /// A simple utility class to parse SIP From and To headers,
    /// and extract the user@host uri
    /// </summary>
    public class UriParser
    {
        /// <summary>
        /// Parse a SIP address header (auch as From or To)
        /// and return the parameter string
        /// </summary>
        /// <returns>parameters if parsable, null if not</returns>
        public static string GetParameters(string sipAddressHeader)
        {
            int indexOfSemicolon = sipAddressHeader.IndexOf(';');

            if (-1 == indexOfSemicolon)
            {
                return null;
            }

            int indexOfParams = indexOfSemicolon + 1;

            return sipAddressHeader.Substring(indexOfParams);
        }


        /// <summary>
        /// Parse a SIP address header (specifically From or To)
        /// and return the user@host 
        /// </summary>
        /// <returns>user@host if parsable, null if not</returns>
        public static string GetUserAtHost(string sipAddressHeader)
        {
            if (sipAddressHeader == null) return null;
            
            string uri = null;

            /// If the header has < > present, then extract the uri
            /// else treat the input as uri
            int index1 = sipAddressHeader.IndexOf('<');

            if (index1 != -1)
            {   
                int index2 = sipAddressHeader.IndexOf('>');
                ///address, extract uri
                uri = sipAddressHeader.Substring(index1 + 1, index2 - index1 - 1);
            }
            else
            {
                uri = sipAddressHeader;
            }
    
            ///chop off all parameters. we assume that there is no
            ///semicolon in the user part (which is allowed in some cases!)
            index1 = uri.IndexOf(';');
            if (index1 != -1)
            {
                uri = uri.Substring(0, index1);
            }

            ///we will process only SIP uri (thus no sips or tel)
            ///and wont accept those without user names
            if (uri.StartsWith("sip:") == false || 
                uri.IndexOf('@') == -1) 
                return null;
            
            ///now we have sip:user@host most likely, with some exceptions that
            /// are ignored
            ///  1) user part contains semicolon separated user parameters
            ///  2) user part also has the password (as in sip:user:pwd@host)
            ///  3) some hex escaped characters are present in user part
            ///  4) the host part also has the port (Contact header for example)
            /// 
 
            string userAtHost = uri.Substring("sip:".Length);

            // remove port if present (strings like "user@host:5060")
            int portStartsAt = userAtHost.IndexOf(":");

            if (-1 == portStartsAt)
            {
                return userAtHost;
            }

            return userAtHost.Substring(0, portStartsAt);
        }


        /// <summary>
        /// Parse the host part from user@host string
        /// </summary>
        /// <returns>host part of user uri if parsable, null if not</returns>
        public static string GetHost(string userAtHost)
        {
            string hostPart = null;

            int indexOfAt = userAtHost.IndexOf('@');

            if (indexOfAt == -1)
            {
                return null;
            }

            ///address, extract uri
            hostPart = userAtHost.Substring(indexOfAt + 1, userAtHost.Length - indexOfAt - 1);
            userAtHost = userAtHost.TrimEnd(null);

            return hostPart;
        }

        public static string RemoveParam(string stringToWorkWith, string paramName)
        {

            int beginningOfParam = stringToWorkWith.IndexOf(paramName);
            if (-1 == beginningOfParam)
            {
                return stringToWorkWith;
            }

            string noParam = "";
            
            if (0 != beginningOfParam)
            {
                // get the string before the parameter, including the semicolon if present
                noParam = stringToWorkWith.Substring(0, beginningOfParam - 1);
            }


            string paramAndBeyond = stringToWorkWith.Substring(beginningOfParam);

            int semicolonAfterParam = paramAndBeyond.IndexOf(";");

            if (-1 != semicolonAfterParam)
            {
                // append the string after the semicolon after the param
                noParam  += paramAndBeyond.Substring(semicolonAfterParam + 1);
            }

            // if the string ends with a semicolon, remove it
            noParam = noParam.TrimEnd();

            noParam = noParam.TrimEnd(';');

            return noParam;
        }

    }


    #endregion URIParser

    #region EventLogHelper
    public class AppEventLog
    {
        public static void RegisterEventSource (string eventLogSource, string eventLogTarget)
        {
            if (!EventLog.SourceExists (eventLogSource)) {
                EventLog.CreateEventSource (eventLogSource, eventLogTarget);
            }
        }

        public static void UnregisterEventSource (string eventLogSource)
        {
            if (EventLog.SourceExists (eventLogSource)) {
                EventLog.DeleteEventSource (eventLogSource);
            }
        }

        public AppEventLog (string eventLogSource, string eventLogTarget)
        {
            if (!EventLog.SourceExists (eventLogSource)) {
                EventLog.CreateEventSource (eventLogSource, eventLogTarget);
            }

            this.eventLog = new EventLog (eventLogTarget);
            this.eventLog.Source = eventLogSource;
        }

        public void Log (EventLogEntryType type, string message)
        {
            if (eventLog != null) {
                eventLog.WriteEntry (message, type);
            }
        }

        public void LogInfo (string message)
        {
            Log (EventLogEntryType.Information, message);
        }

        public void LogError (string message)
        {
            Log (EventLogEntryType.Error, message);
        }

        public void LogWarning (string message)
        {
            Log (EventLogEntryType.Warning, message);
        }

        EventLog eventLog;
    };

    /// <summary>
    /// An event log throttle helps throttle all events of same type. It
    /// ensures that events are logged at a rate not exceeding the configured rate.
    /// </summary>
    public class EventLogThrottle
    {
        public EventLogThrottle (AppEventLog eventLog)
        {
            this.eventLog = eventLog;
            this.duration = EventLogThrottle.DefaultThrottleTime;
            this.lastWrite = DateTime.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration">Value in minutes. One event per duration will be allowed.</param>
        public EventLogThrottle (AppEventLog eventLog, int duration)
        {
            this.eventLog = eventLog;
            this.duration = duration;
            this.lastWrite = DateTime.MinValue;
        }

        public void LogWarning (string message)
        {
            Log (EventLogEntryType.Warning, message);
        }

        public void LogError (string message)
        {
            Log (EventLogEntryType.Error, message);
        }

        public void Log (EventLogEntryType type, string message)
        {
            DateTime time = DateTime.Now;

            lock (this) {
                TimeSpan elapsed = time - lastWrite;
                if (elapsed.TotalMinutes > duration) {
                    lastWrite = time;
                    eventLog.Log (type, message);
                }
            }
        }

        private AppEventLog eventLog;
        public const int DefaultThrottleTime = 30; // 30 Minutes per event.
        private int duration;
        private DateTime lastWrite;
    }
    #endregion

    #region ResourceStrings
    /// <summary>
    ///     Encapsulates a ResourceManager object used to acquire localized
    ///     strings specific to this tool.
    /// </summary>
    public class SipStringManager
    {
        private static ResourceManager resourceManager;

        public static void CreateResourceManager (string type)
        {
            resourceManager = new ResourceManager (type, Assembly.GetExecutingAssembly ());
        }

        public static string GetString (string name)
        {
            return resourceManager.GetString (name, CultureInfo.CurrentUICulture);
        }
    }
    #endregion
}
