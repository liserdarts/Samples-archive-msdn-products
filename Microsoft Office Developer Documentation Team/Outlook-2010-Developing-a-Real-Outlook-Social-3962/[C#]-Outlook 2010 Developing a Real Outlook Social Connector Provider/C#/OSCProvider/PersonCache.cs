using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSCProvider.Schema;

namespace OSCProvider
{
    public static class PersonCache
    {
        private static Dictionary<Person, DateTime> m_people = new Dictionary<Person, DateTime>();
        private static bool m_expireOn = true;
        public static void AddPerson(Person person,DateTime expiration)
        {
            _Expire();
            if (person == null) return;

            if (!m_people.ContainsKey(person))
            {
                m_people.Add(person, expiration);
            }
        }
        public static void AddPerson(Person person)
        {
            AddPerson(person, DateTime.MaxValue);
        }
        public static void AddRange(List<Person> people, DateTime expiration)
        {
            m_expireOn = false;
            foreach (Person p in people) AddPerson(p, expiration);
            m_expireOn = true;
        }
        public static void AddRange(List<Person> people)
        {
            AddRange(people, DateTime.MaxValue);
        }
        public static void RemovePerson(Person person)
        {
            if (person == null) return;
            if (m_people.ContainsKey(person))
            {
                m_people.Remove(person);
            }
        }

        public static Person FindFirst(string userData)
        {
            _Expire();
            if (Helpers.IsNE(userData)) return null;
            foreach (Person p in m_people.Keys)
            {
                if (IsMatch(p,userData)) return p;
            }
            return null;
        }
        public static List<Person> FindAll(string userData)
        {
            _Expire();
            List<Person> people = new List<Person>();
            foreach (Person p in m_people.Keys)
            {
                if (IsMatch(p, userData))
                {
                    people.Add(p);
                }
            }
            return people;
        }
        private static bool IsMatch(Person p, string userData)
        {
            if (Helpers.IsNE(userData)) return false;
            return p.UserID == userData ||
                    p.FullName == userData ||
                    p.Email == userData ||
                    p.Email2 == userData ||
                    p.Email3 == userData ||
                    p.FileAs == userData ||
                    p.MatchesEmailHash(userData);
        }
        private static void _Expire()
        {
            if (!m_expireOn) return;

            int pc = m_people.Count;
            IEnumerable<KeyValuePair<Person, DateTime>> expiredPeople = m_people.Where(person => person.Value < DateTime.Now);
            foreach(KeyValuePair<Person,DateTime> personE in expiredPeople)
            {
                if (personE.Value < DateTime.Now)
                {
                    m_people.Remove(personE.Key);
                }
            }
        }

        


    }
}
