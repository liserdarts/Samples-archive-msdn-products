using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OutlookSocialProvider;
using System.Threading;
namespace OSCProvider
{
    class OSCProfile:ISocialProfile
    {
        private OSCProvider m_provider;
        private Schema.Friends m_friends;
        private object m_friendsLock = new object();
        private Schema.Person m_personData;
        private DateTime m_FriendsRetrieved = DateTime.MinValue;
        internal OSCProfile(OSCProvider provider,Schema.Person personData)
        {
            m_provider = provider;
            m_personData = personData;
        }
        public Schema.Person PersonData { get { return m_personData; } }
        public Array AreFriendsOrColleagues(Array userIDs)
        {
            List<bool> lFriends = new List<bool>();

            EnsureFriends();

            foreach (string userId in userIDs)
            {
                if (PersonCache.FindFirst(userId) != null)
                {
                    lFriends.Add(true);
                }
                else
                {
                    lFriends.Add(false);
                }
            }

            return lFriends.ToArray();
        }



        public string GetActivities(DateTime startTime)
        {
            List<string>emailAddresses = new List<string>();
           
            if(!string.IsNullOrEmpty(m_personData.Email))emailAddresses.Add(m_personData.Email);
            if(!string.IsNullOrEmpty(m_personData.Email2))emailAddresses.Add(m_personData.Email2);
            if(!string.IsNullOrEmpty(m_personData.Email3))emailAddresses.Add(m_personData.Email3);

            Schema.ActivityFeed af = null;

            try
            {
                af = m_provider.GetActivities(emailAddresses.ToArray(), startTime,false,false);
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

            af.SchemaVersion = m_provider.ProviderData.SchemaVersion;
            return af.Xml;
            
        }

        public string GetActivitiesOfFriendsAndColleagues(DateTime startTime)
        {
            Schema.ActivityFeed af = null;
            List<string> emailAddressesOfFriends = new List<string>();
           
            EnsureFriends();
            
            
            foreach (Schema.Person p in m_friends.People)
            {
                if(p.Email != null)
                    emailAddressesOfFriends.Add(p.Email);
            }
            
            try
            {
                af = m_provider.GetActivities(emailAddressesOfFriends.ToArray(), startTime,false,false);
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
            af.SchemaVersion = m_provider.ProviderData.SchemaVersion;
            return af.Xml;

        }

        public string GetDetails()
        {
            if (m_personData == null)
                throw new OSCException(@"Details not found.", OSCExceptions.OSC_E_NOT_FOUND);
            m_personData.SchemaVersion = m_provider.ProviderData.SchemaVersion;
            return m_personData.Xml;
        }

        public string GetFriendsAndColleagues()
        {
            string retVal = null;
            EnsureFriends();
            Monitor.Enter(m_friendsLock);
            try
            {
                m_friends.SchemaVersion = m_provider.ProviderData.SchemaVersion;
                retVal = m_friends.Xml;
            }
            finally
            {
                Monitor.Exit(m_friendsLock);
            }
            return retVal;
        }

        public Array GetFriendsAndColleaguesIDs()
        {
            List<string> userIds = new List<string>();
            Monitor.Enter(m_friendsLock);
            try
            {
                EnsureFriends();
                foreach (Schema.Person p in m_friends.People)
                {
                    userIds.Add(p.UserID);
                }
            }
            finally
            {
                Monitor.Exit(m_friendsLock);
            }
            return userIds.ToArray();
        }

        public Array GetPicture()
        {
            if (m_personData == null || m_personData.ProfilePhoto == null)
                throw new OSCException(@"No Picture is available", OSCExceptions.OSC_E_NOT_FOUND);
            return m_personData.ProfilePhoto;
        }

        public string GetStatus()
        {
            try
            {
                return m_provider.GetStatus(m_personData);
            }
            catch (COMException cex)
            {
                if (Helpers.IsOSCException(cex))
                    throw;
                else
                {
                    throw new OSCException(@"GetStatus call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,cex);
                }
            }
            catch(ApplicationException ex)
            {
                throw new OSCException(@"GetStatus call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,ex);
            }
        }

        public void SetStatus(string status)
        {
            try
            {
                m_provider.SetStatus(m_personData, status);
            }
            catch (COMException cex)
            {
                if (Helpers.IsOSCException(cex))
                    throw;
                else
                {
                    throw new OSCException(@"SetStatus call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR, cex);
                }
            }
            catch(ApplicationException ex)
            {
                throw new OSCException(@"SetStatus call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR, ex);
            }
        }
        private void EnsureFriends()
        {
            if (m_friends == null ||
                DateTime.Now.Subtract(m_FriendsRetrieved).TotalMinutes>30 )
            {
                try
                {
                    m_friends = m_provider.GetFriends();
                }
                catch (COMException cex)
                {
                    if (Helpers.IsOSCException(cex))
                        throw ;
                    else
                    {
                        throw new OSCException(@"GetFriends call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,cex);
                    }
                }
                catch(ApplicationException ex)
                {
                    throw new OSCException(@"GetFriends call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,ex);
                }
                PersonCache.AddRange(m_friends.People);
            }
        }
        internal void InvalidateFriends()
        {
            Monitor.Enter(m_friendsLock);
            try
            {
                m_friends = null;
                m_FriendsRetrieved = DateTime.MinValue;
            }
            finally
            {
                Monitor.Exit(m_friendsLock);
            }
        }
    }
}
