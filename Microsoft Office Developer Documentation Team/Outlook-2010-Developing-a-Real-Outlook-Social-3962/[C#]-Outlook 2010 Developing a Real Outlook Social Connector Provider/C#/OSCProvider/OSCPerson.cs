using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OutlookSocialProvider;

namespace OSCProvider
{
    class OSCPerson:ISocialPerson
    {
        private OSCProvider m_provider;
        private Schema.Friends s_friends;
        private Schema.Person m_personData;
        internal OSCPerson(OSCProvider provider,Schema.Person personData)
        {
            m_provider = provider;
            m_personData = personData;
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
                    throw new OSCException(@"GetActivities call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR);
                }
            }
            catch
            {
                throw new OSCException(@"GetActivities call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR);
            }
            af.SchemaVersion = m_provider.ProviderData.SchemaVersion;
            return af.Xml;
        }

        public string GetDetails()
        {
            if (m_personData == null)
            {
                throw new OSCException(@"Details not available.", OSCExceptions.OSC_E_NOT_FOUND);
            }
            m_personData.SchemaVersion = m_provider.ProviderData.SchemaVersion;
            return m_personData.Xml;
        }

        public string GetFriendsAndColleagues()
        {
            EnsureFriends();
            s_friends.SchemaVersion = m_provider.ProviderData.SchemaVersion;
            return s_friends.Xml;
        }

        public Array GetFriendsAndColleaguesIDs()
        {
            List<string> userIds = new List<string>();
            EnsureFriends();
            foreach (Schema.Person p in s_friends.People)
            {
                userIds.Add(p.UserID);
            }
            return userIds.ToArray();
        }

        public Array GetPicture()
        {
            if (m_personData == null || m_personData.ProfilePhoto == null)
            {
                throw new OSCException(@"ProfilePhoto not found.", OSCExceptions.OSC_E_NOT_FOUND);
            }
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
        private void EnsureFriends()
        {
            if (s_friends == null)
            {
                try
                {
                    s_friends = m_provider.GetFriends();
                }
                catch (COMException cex)
                {
                    if (Helpers.IsOSCException(cex))
                        throw;
                    else
                    {
                        throw new OSCException(@"GetFriends call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,cex);
                    }
                }
                catch(ApplicationException ex)
                {
                    throw new OSCException(@"GetFriends call failed.", OSCExceptions.OSC_E_INTERNAL_ERROR,ex);
                }
                PersonCache.AddRange(s_friends.People);
            }
        }
    }
}
