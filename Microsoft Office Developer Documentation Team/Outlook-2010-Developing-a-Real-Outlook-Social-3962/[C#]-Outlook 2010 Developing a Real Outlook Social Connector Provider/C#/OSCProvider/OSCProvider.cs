using System;
using System.Runtime.InteropServices;
using OSCProvider.Schema;
using OutlookSocialProvider;


namespace OSCProvider
{
    [ComVisible(true)]
    public abstract class OSCProvider : ISocialProvider
    {
        #region ISocialProvider

        ProviderData m_providerData;
        
        private string m_socialProviderInterfaceVersion = null;
        private string m_languageTag = null;
        
        internal ProviderData ProviderData { get { return m_providerData; } }
        protected string SocialProviderInterfaceVersion { get { return m_socialProviderInterfaceVersion; } }
        protected string LanguageTag { get { return m_languageTag; } }
        
        public Array DefaultSiteUrls
        {
            get
            {
                EnsureProviderData();

                if (m_providerData == null || m_providerData.Urls == null)
                {
                    return null;
                }

                OnTraceEvent(this, 
                    new OSCEventArgs(
                        TraceType.Function, 
                        @"DefaultSiteUrls returning " + string.Join(",",m_providerData.Urls)
                        )
                );

                return m_providerData.Urls;
            }
        }

        [CLSCompliantAttribute(false)]
        public ISocialSession GetAutoConfiguredSession()
        {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"GetAutoConfiguredSession called."));

            return new OSCSession(this);
        }

        public string GetCapabilities()
        {
            EnsureProviderData();
            if (m_providerData == null || m_providerData.ProviderCapabilities == null)
            {
                OSCException oex = new OSCException(@"Capabilities not available. Be sure you are returning a value in GetProviderData.", OSCExceptions.OSC_E_NOT_FOUND);
                OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, oex.Message, oex));
                throw oex;
            }
            
            return m_providerData.ProviderCapabilities.Xml;
        }

        [CLSCompliantAttribute(false)]
        public ISocialSession GetSession()
        {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"GetSession called"));

            return new OSCSession(this);
        }

        public void GetStatusSettings(out string statusDefault, out int maxStatusLength)
        {
            statusDefault = string.Empty;
            maxStatusLength = 1024;
        }

        public void Load(string socialProviderInterfaceVersion, string languageTag)
        {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, string.Format(@"ISocialProvider::Load called with {0},{1}",socialProviderInterfaceVersion,languageTag)));
            m_languageTag = languageTag;
            m_socialProviderInterfaceVersion = socialProviderInterfaceVersion;
            EnsureProviderData();
            if (m_providerData != null) m_providerData.m_CalledAfterLoad = true;
        }
        public void EnsureProviderData()
        {
            if (m_providerData != null && m_providerData.m_CalledAfterLoad) 
                return;


            try
            {
                OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"Calling GetProviderData"));
                m_providerData = GetProviderData();
            }
            catch (COMException cex)
            {
                OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, cex.Message,cex));
                if (Helpers.IsOSCException(cex))
                    throw;
                else
                {
                    throw new OSCException(@"GetProviderData call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR, cex);
                }
            }
            catch(ApplicationException ex)
            {
                OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, ex.Message,ex));

                throw new OSCException(@"GetProviderData call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,ex);
            }
        }

        public Array SocialNetworkIcon
        {
            get
            {
                OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"SocialNetworkIcon called."));

                EnsureProviderData();
                if (m_providerData == null || m_providerData.Icon == null)
                {
                    return null;
                }
                return m_providerData.Icon;
            }
        }

        public string SocialNetworkName
        {
            get
            {
                OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"SocialNetworkName called."));

                EnsureProviderData();
                if (m_providerData == null || m_providerData.NetworkName == null)
                {
                    return null;
                }
                return m_providerData.NetworkName;
            }
        }

        public Guid SocialNetworkGuid
        {
            get
            {
                OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"SocialNetworkGuid called."));

                EnsureProviderData();
                if (m_providerData == null || m_providerData.NetworkGuid == null)
                {
                    return Guid.Empty;
                }
                return m_providerData.NetworkGuid;
            }
        }

        public string Version
        {
            get
            {
                OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"Version called."));

                EnsureProviderData();
                if (m_providerData == null || m_providerData.Version == null)
                {
                    return null;
                }
                return m_providerData.Version;
            }
        }
        #endregion

        #region Must be implemented by provider consumers
        /// <summary>
        /// GetProviderData is called by the OSCProvider during Load to get information needed at various points during startup of the OSC
        /// </summary>
        /// <returns>Return a ProviderData object that includes the Capabilities your provider supports.</returns>
        public abstract ProviderData GetProviderData();

        /// <summary>
        /// GetActivities is called during dynamic and cached activity syncs
        /// </summary>
        /// <param name="emailAddresses">Array of email addresses that OSC needs updates for.</param>
        /// <param name="startTime">ActivityFeed should include items newer than the startTime</param>
        /// <param name="dynamic">If dynamic is true, emailAddresses will contain email addresses for one person (the social network need only recognize one).
        /// If dynamic is false, then it is a cached update, the emailAddresses will each represent a separate user.</param>
        /// <param name="hashed">If hashed is true, the emailAddresses will contained hashed email addresses, otherwise they will be plain text.</param>
        /// <returns>Return an ActivityFeed object that includes the activity items for the users represented by the emailAddresses array </returns>
        public abstract ActivityFeed GetActivities(string[] emailAddresses, DateTime startTime, bool dynamic, bool hashed);

        /// <summary>
        /// GetFriends is called by the provider during contact sync and activity syncs.
        /// </summary>
        /// <returns>Return a Friends object representing the list of "friends" or "following" list of the logged on user.</returns>
        public abstract Friends GetFriends();

        /// <summary>
        /// Return the person details of the given user
        /// </summary>
        /// <param name="userId">A generic user id (could be a name, email address, or userid)</param>
        /// <returns></returns>
        public abstract Person GetPerson(string userId);

        /// <summary>
        /// Called by the provider to get the person data of the logged on user
        /// </summary>
        /// <returns>Person object representing the logged on user</returns>
        public abstract Person GetMe();

        /// <summary>
        /// Called by the provider when the user has requested to follow the user represented by the given email address
        /// </summary>
        /// <param name="emailAddress">The e-mail address of the user the logged on user wishes to follow</param>
        public virtual void FollowPerson(string emailAddress) {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"FollowPerson called (unhandled)."));
        }

        public virtual void FollowPersonEx(string[] hashedEmailAddresses,string displayName) {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"FollowPersonEx called (unhandled)."));
        }

        /// <summary>
        /// Called by the provider when the logged on user requests to stop following the given user
        /// </summary>
        /// <param name="userID">The user id of the user to stop following</param>
        public virtual void UnFollowPerson(string userID) {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"UnFollowPerson called (unhandled)."));
        }

        /// <summary>
        /// Providers should override this method when implementing Basic authentication
        /// </summary>
        /// <param name="username">Contains the username provided by the user</param>
        /// <param name="password">Contains the password provided by the user</param>
        public virtual void Logon(string username, string password)
        {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"Logon called (unhandled)."));

        }

        /// <summary>
        /// Providers should override this method when implementing forms-based (web) authentication
        /// </summary>
        /// <param name="connectIn">contains the value last returned from LogonWeb - used for keeping track of state</param>
        /// <returns>Return the connectOut string that will be passed as connectIn the next time the provider needs to logon.</returns>
        public virtual string LogonWeb(string connectIn)
        {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"LogonWeb called (unhandled)."));

            return null;
        }


        public virtual string LogonCached(string connectIn, string userName, string password)
        {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"LogonCached called (unhandled)."));

            return null;
        }

        /// <summary>
        /// Providers should override this method when they want notified that the user has selected a different Url
        /// </summary>
        /// <param name="url">Url the user selected.</param>
        public virtual void SiteUrlSet(string url)
        {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, @"SiteUrlSet called (unhandled)."));
        }

        /// <summary>
        /// (Not used by OSC v1.0) Providers should override this method to get the current status of the given person.
        /// </summary>
        /// <param name="person">The person of whom to get the current status</param>
        /// <returns>The status of the given person</returns>
        public virtual string GetStatus(Person person)
        {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"GetStatus called (unhandled)."));
            return string.Empty;
        }

        /// <summary>
        /// (Not used by OSC v1.0) Providers should override this method to set the current status of the given person.
        /// </summary>
        /// <param name="person">The person of whom to set the status</param>
        /// <param name="status">The status entered by the user</param>
        public virtual void SetStatus(Person person, string status) {
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"SetStatus called (unhandled)."));
        }

        public virtual Friends GetPeopleDetails(HashedAddresses hashedAddresses){
            OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"GetPeopleDetails called (unhandled)."));
            return new Friends();
    }

        #endregion

        public static event TraceEventHandler Trace;

        internal static void OnTraceEvent(object sender, OSCEventArgs e)
        {
            if (Trace != null)
                Trace(sender, e);
        }

    }


    #region Tracer
    public delegate void TraceEventHandler(object sender, OSCEventArgs e);
    public class OSCEventArgs:EventArgs{
        private TraceType m_traceType;
        private string m_traceText;
        private Exception m_ex;
        public TraceType TraceType { get { return m_traceType; } }
        public string Text { get { return m_traceText; } }
        public Exception Exception { get { return m_ex; } }
        public OSCEventArgs(TraceType traceType, string traceText,Exception ex = null)
        {
            m_traceText = traceText;
            m_traceType = traceType;
            m_ex = ex;
        }
    }
    public enum TraceType
    {
        Function,
        ReturnValue,
        Exceptions
    }
#endregion
}
