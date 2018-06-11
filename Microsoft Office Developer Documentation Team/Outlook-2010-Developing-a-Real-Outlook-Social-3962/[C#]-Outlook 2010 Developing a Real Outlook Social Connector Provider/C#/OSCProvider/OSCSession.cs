using System;
using System.Runtime.InteropServices;
using OSCProvider.Schema;
using OutlookSocialProvider;
namespace OSCProvider
{
    internal class OSCSession:ISocialSession,ISocialSession2
    {
        private OSCProvider m_provider;
        private OSCProfile m_me;
        private bool m_logonSequence = false;

        public OSCSession(OSCProvider provider)
        {
            m_provider = provider;
            EnsureMe();
        }
        
        public string FindPerson(string userID)
        {
            OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"FindPerson called."));
            Person p;
            p = PersonCache.FindFirst(userID);
            if (p == null)
            {
                try
                {
                    OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"Calling GetPerson."));

                    p = m_provider.GetPerson(userID);
                }
                catch (COMException cex)
                {
                    OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, cex.Message,cex));

                    if (Helpers.IsOSCException(cex))
                        throw;
                    else
                    {
                        throw new OSCException(@"GetPerson call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,cex);
                    }
                }
                catch(ApplicationException ex)
                {
                    OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, ex.Message, ex));

                    throw new OSCException(@"GetPerson call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,ex);
                }
                PersonCache.AddPerson(p);
            }
            p.SchemaVersion = m_provider.ProviderData.SchemaVersion;
            if (p != null)
                return p.Xml;
            else
                return string.Empty;   
        }

        public ISocialProfile GetLoggedOnUser()
        {
            OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"GetLoggedOnUser called."));

            EnsureMe();
            return m_me;
        }

        public string GetLogonUrl()
        {
            OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"GetLogonUrl"));

            m_provider.EnsureProviderData();
            return string.Format("{0}",m_provider.ProviderData.LogonUrl);
        }

        public string GetNetworkIdentifier()
        {
            OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"GetNetworkIdentifier called."));

            m_provider.EnsureProviderData();
            return string.Format("{0}",m_provider.ProviderData.NetworkGuid.ToString("D"));
        }

        public ISocialPerson GetPerson(string userID)
        {
            OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"GetPerson called."));

            Person p;
            p = PersonCache.FindFirst(userID);
            if (p == null)
            {
                try
                {
                    OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"Calling OSCProvider.GetPerson."));

                    p = m_provider.GetPerson(userID);
                }
                catch (COMException cex)
                {
                    OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, cex.Message,cex));

                    if (Helpers.IsOSCException(cex))
                        throw;
                    else
                    {
                        throw new OSCException(@"GetPerson call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,cex);
                    }
                }
                catch(ApplicationException ex)
                {
                    OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, ex.Message,ex));
                    throw new OSCException(@"GetPerson call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,ex);
                }
                if(p!=null)PersonCache.AddPerson(p);
            }
            if (p != null)
                return new OSCPerson(m_provider, p); 
            else
                return null;//TODO : if p==null, i need to throw OSC_E_NOT_FOUND;
        }


        public string LoggedOnUserID
        {
            get
            {
                EnsureMe();
                return m_me.PersonData.UserID;
            }
        }



        public string LoggedOnUserName
        {
            get
            {
                EnsureMe();
                if (m_me != null && m_me.PersonData != null && m_me.PersonData.UserName != null)
                {
                    return m_me.PersonData.UserName;
                }
                return string.Empty;
            }
        }

        public void Logon(string userName, string password)
        {
            try
            {
                OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"Calling OSCProvider.Logon"));

                m_provider.Logon(userName, password);
            }
            catch (COMException cex)
            {
                OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, cex.Message, cex));

                if (Helpers.IsOSCException(cex))
                    throw;
                else
                {
                    throw new OSCException(@"Logon call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,cex);
                }
            }
            catch(ApplicationException ex)
            {
                OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, ex.Message, ex));

                throw new OSCException(@"Logon call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,ex);
            }
        }

        public void LogonWeb(string connectIn, out string connectOut)
        {
            if (connectIn == null && !m_logonSequence)
            {
                m_logonSequence = true;
                throw new OSCException(@"Unable to authenticate", OSCExceptions.OSC_E_AUTH_ERROR);
            }

            try
            {
                OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function,@"Calling OSCProvider.LogonWeb."));

                connectOut = m_provider.LogonWeb(connectIn);
            }
            catch (COMException cex)
            {
                OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, cex.Message, cex));

                if (Helpers.IsOSCException(cex))
                    throw;
                else
                {
                    throw new OSCException(@"LogonWeb call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,cex);
                }
            }
            catch(ApplicationException ex)
            {
                OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, ex.Message, ex));

                throw new OSCException(@"LogonWeb call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR, ex);
            }
        }

        private string m_siteUrl;
        public string SiteUrl
        {
            set
            {
                m_siteUrl = value;
                try
                {
                    OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Function, @"Calling OSCProvider.SiteUrl"));

                    m_provider.SiteUrlSet(m_siteUrl);
                }
                catch (COMException cex)
                {
                    OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, cex.Message, cex));

                    if (Helpers.IsOSCException(cex))
                        throw;
                    else
                    {
                        throw new OSCException(@"SiteUrlSet call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,cex);
                    }
                }
                catch (ApplicationException ex)
                {
                    OSCProvider.OnTraceEvent(this, new OSCEventArgs(TraceType.Exceptions, ex.Message, ex));

                    throw new OSCException(@"SiteUrlSet call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,ex);
                }
            }
        }


        public void FollowPerson(string emailAddress)
        {
            try
            {
                m_provider.FollowPerson(emailAddress);
                EnsureMe();
                if (m_me != null) m_me.InvalidateFriends();
            }
            catch (COMException cex)
            {
                if (Helpers.IsOSCException(cex))
                    throw;
                else
                {
                    throw new OSCException(@"FollowPerson call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,cex);
                }
            }
            catch(ApplicationException ex)
            {
                throw new OSCException(@"FollowPerson call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,ex);
            }
                
        }

        public string GetActivities(Array emailAddresses, DateTime startTime)
        {
            ActivityFeed af = null;
            try
            {
                af = m_provider.GetActivities((string[])emailAddresses, startTime,true,m_provider.ProviderData.SchemaVersion == ProviderSchemaVersion.v1_1);
            }
            catch (COMException cex)
            {
                if (Helpers.IsOSCException(cex))
                    throw;
                else
                {
                    throw new OSCException(@"GetActivities call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,cex);
                }
            }
            catch(ApplicationException ex)
            {
                throw new OSCException(@"GetActivities call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,ex);
            }
            if (af == null || af.Activities == null || af.Activities.Count == 0)
            {
                throw new OSCException(@"No changes", OSCExceptions.OSC_E_NO_CHANGES);
            }
            af.SchemaVersion = m_provider.ProviderData.SchemaVersion;
            return af.Xml;
        }

        public void UnFollowPerson(string userID)
        {
            try
            {
                m_provider.UnFollowPerson(userID);
                EnsureMe();
                if (m_me != null) m_me.InvalidateFriends();
            }
            catch (COMException cex)
            {
                if (Helpers.IsOSCException(cex))
                    throw;
                else
                {
                    throw new OSCException(@"UnfollowPerson call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,cex);
                }
            }
            catch(ApplicationException ex)
            {
                throw new OSCException(@"UnfollowPerson call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,ex);
            }
        }

        #region ISocialSession2
        public void FollowPersonEx(Array emailAddresses, string displayName)
        {
            try{
                m_provider.FollowPersonEx((string[])emailAddresses, displayName);
                EnsureMe();
                if(m_me!=null)m_me.InvalidateFriends();
            }
            catch (COMException cex)
            {
                if (Helpers.IsOSCException(cex))
                    throw;
                else
                {
                    throw new OSCException(@"FollowPersonEx call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR, cex);
                }
            }
            catch (ApplicationException ex)
            {
                throw new OSCException(@"FollowPersonEx call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR, ex);
            }
        }

        public string GetActivitiesEx(Array hashedAddresses, DateTime startTime)
        {
            return GetActivities(hashedAddresses, startTime);
        }

        public string GetPeopleDetails(string personsAddresses)
        {

            HashedAddresses hashedAddresses = null;
            Friends friends = null;
            try
            {
                hashedAddresses = HashedAddresses.Create(personsAddresses,m_provider.ProviderData.SchemaVersion);
            }
            catch (Exception ex)
            {
                throw new OSCException(@"Internal error in GetPeopleDetails", OSCExceptions.OSC_E_INTERNAL_ERROR, ex);
            }

            try
            {
                friends = m_provider.GetPeopleDetails(hashedAddresses);
            }
            catch (COMException cex)
            {
                if (Helpers.IsOSCException(cex))
                    throw;
                else
                {
                    throw new OSCException(@"GetPeopleDetails call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR, cex);
                }
            }
            catch (ApplicationException ex)
            {
                throw new OSCException(@"GetPeopleDetails call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR, ex);
            }
            friends.SchemaVersion = m_provider.ProviderData.SchemaVersion;
            if(friends != null)
                return friends.Xml;

            throw new OSCException(@"No friends found.", OSCExceptions.OSC_E_NOT_FOUND);
        }

        public void LogonCached(string connectIn, string userName, string password, out string connectOut)
        {
            connectOut = m_provider.LogonCached(connectIn, userName, password);
        }
        #endregion

        private void EnsureMe()
        {
            if (m_me == null)
            {
                Schema.Person me = null;
                try
                {
                    me = m_provider.GetMe();
                }
                catch (COMException cex)
                {
                    if (Helpers.IsOSCException(cex))
                        throw;
                    else
                    {
                        throw new OSCException(@"GetMe call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR, cex);
                    }
                }
                catch(ApplicationException ex)
                {
                    throw new OSCException(@"GetMe call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR, ex);
                }

                m_me = new OSCProfile(m_provider, me);
            }
        }

    }
}
