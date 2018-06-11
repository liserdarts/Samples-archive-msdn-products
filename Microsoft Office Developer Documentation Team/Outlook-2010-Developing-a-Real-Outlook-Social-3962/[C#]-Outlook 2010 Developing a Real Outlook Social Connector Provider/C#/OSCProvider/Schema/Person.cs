using System;
using System.Collections.Generic;
using System.Xml;

namespace OSCProvider.Schema
{
    public class Person:SchemaObject
    {
        //private string m_userID;
        private byte[] m_profilePhoto;
        //private string m_username;
        private string m_firstName;
        private string m_lastName;
        private string m_fullName;
        private string m_nickName;
        private string m_fileAs;
        private string m_company;
        private string m_title;
        //private DateTime m_anniversary;
        //private DateTime m_birthday;
        //private string m_email;
        //private string m_email2;
        //private string m_email3;
        //private string m_webProfilePage;
        private string m_phone;
        private string m_cell;
        private string m_homePhone;
        private string m_workPhone;
        private string m_address;
        private string m_city;
        private string m_state;
        private string m_zip;
        private string m_relationship;
        //private DateTime m_creationTime;
        //private DateTime m_lastModifiedTime;
        //private DateTime m_expirationTime;
        //private Gender? m_gender;
        //private int? m_index;


        /// <summary>
        /// Network User ID for this person 
        /// </summary>
        public string UserID
        {
            get;
            set;
        }

        /// <summary>
        /// Not a schema prop, this is the username the user uses to log on to social network.
        /// It is only used for the Person returned in GetMe
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Person first name or given name 
        /// </summary>
        public string FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Person last name or surname
        /// </summary>
        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Person full name 
        /// </summary>
        public string FullName
        {
            get { return m_fullName; }
            set { m_fullName = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Person nickname 
        /// </summary>
        public string NickName
        {
            get { return m_nickName; }
            set { m_nickName = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Person fileas name used for FileAs property of Contact item
        /// </summary>
        public string FileAs
        {
            get { return m_fileAs; }
            set { m_fileAs = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Company name for the person 
        /// </summary>
        public string Company
        {
            get { return m_company; }
            set { m_company = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Person title 
        /// </summary>
        public string Title
        {
            get { return m_title; }
            set { m_title = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Person's anniversary date 
        /// </summary>
        public DateTime Anniversary
        {
            get;
            set;
        }

        /// <summary>
        /// Person's birthday 
        /// </summary>
        public DateTime Birthday
        {
            get;
            set;
        }

        /// <summary>
        /// Unique SMTP address for the person 
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        internal string[] Email_Hash
        {
            get
            {
                return GetHashArray(Email);
            }
        }
        internal bool MatchesEmailHash(string input)
        {
            List<string> hashes = new List<string>();
            hashes.AddRange(Email_Hash);
            hashes.AddRange(Email2_Hash);
            hashes.AddRange(Email3_Hash);
            return hashes.Contains(input);
        }


        /// <summary>
        /// Email2 SMTP address for the person 
        /// </summary>
        public string Email2
        {
            get;
            set;
        }
        internal string[] Email2_Hash
        {
            get
            {
                return GetHashArray(Email2);
            }
        }

        /// <summary>
        /// Email3 SMTP address for the person 
        /// </summary>
        public string Email3
        {
            get;
            set;
        }
        internal string[] Email3_Hash
        {
            get
            {
                return GetHashArray(Email3);
            }
        }
        private string[] GetHashArray(string input)
        {
            string[] retVal = new string[]{};
            if (input != null)
            {
                retVal = new string[]{
                    Helpers.Hash(input, HashFunction.MD5),
                    Helpers.Hash(input, HashFunction.SHA1),
                    Helpers.Hash(input, HashFunction.CRC32MD5)
                };
            }
            return retVal;
        }

        /// <summary>
        /// Web profile page for the person 
        /// </summary>
        public string WebProfilePage
        {
            get;
            set;
        }

        /// <summary>
        /// Person's home phone 
        /// </summary>
        public string Phone
        {
            get { return m_phone; }
            set { m_phone = value==null?null:value.Substring(0, 1024); }
        }

        /// <summary>
        /// Person's cell or mobile phone 
        /// </summary>
        public string Cell
        {
            get { return m_cell; }
            set { m_cell = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Person's home phone (this element is not used in OSC v1.0) 
        /// </summary>
        public string HomePhone
        {
            get { return m_homePhone; }
            set { m_homePhone = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Person's work phone 
        /// </summary>
        public string WorkPhone
        {
            get { return m_workPhone; }
            set { m_workPhone = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Person's address 
        /// </summary>
        public string Address
        {
            get { return m_address; }
            set { m_address = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }


        /// <summary>
        /// Person's city 
        /// </summary>
        public string City
        {
            get { return m_city; }
            set { m_city = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Person's state/province 
        /// </summary>
        public string State
        {
            get { return m_state; }
            set { m_state = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }


        /// <summary>
        /// Person's zip code 
        /// </summary>
        public string ZIP
        {
            get { return m_zip; }
            set { m_zip = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Person's relationship (this element is not used in OSC v1.0)
        /// </summary>
        public string Relationship
        {
            get { return m_relationship; }
            set { m_relationship = value != null && value.Length>1024 ? value.Substring(0, 1024) : value; }
        }

        /// <summary>
        /// Creation time of the person's profile on the network
        /// </summary>
        public DateTime CreationTime
        {
            get;
            set;
        }

        /// <summary>
        /// Last modification time of person's profile on the network 
        /// </summary>
        public DateTime LastModifiedTime
        {
            get;
            set;
        }

        /// <summary>
        /// Expiration time of the person's profile data 
        /// </summary>
        public DateTime ExpirationTime
        {
            get;
            set;
        }


        /// <summary>
        /// Gender for this person 
        /// </summary>
        public Gender? Gender
        {
            get;
            set;
        }

        public FriendStatus? FriendStatus
        {
            get;
            set;
        }

        public int? Index
        {
            get;
            set;
        }

        public Uri PictureUrl
        {
            get;
            set;
        }

        public override string Xml
        {
            get
            {
                return XmlEx.OuterXml;
            }
        }

        internal override XmlElement XmlEx
        {
            get {
                XmlDocument xdoc = XmlHelper.GetXmlDoc(SchemaVersion);
                XmlElement person = xdoc.CreateElement("person", XmlHelper.GetSchemaUrl(SchemaVersion));
                XmlHelper.AddStringElement(person, "userID", UserID);
                XmlHelper.AddStringElement(person, "firstName", FirstName);
                XmlHelper.AddStringElement(person, "lastName", LastName);
                XmlHelper.AddStringElement(person, "fullName", FullName);
                XmlHelper.AddStringElement(person, "nickname", NickName);
                XmlHelper.AddStringElement(person, "fileAs", FileAs);
                XmlHelper.AddStringElement(person, "company", Company);
                XmlHelper.AddStringElement(person, "title", Title);
                XmlHelper.AddStringElement(person, "anniversary", Anniversary,dateFormat:"yyyy-MM-dd");
                XmlHelper.AddStringElement(person, "birthday", Birthday, dateFormat:"yyyy-MM-dd");
                XmlHelper.AddStringElement(person, "emailAddress", Email);
                XmlHelper.AddStringElement(person, "emailAddress2", Email2);
                XmlHelper.AddStringElement(person, "emailAddress3", Email3);
                XmlHelper.AddStringElement(person, "webProfilePage", WebProfilePage);
                XmlHelper.AddStringElement(person, "phone", Phone);
                XmlHelper.AddStringElement(person, "cell", Cell);
                XmlHelper.AddStringElement(person, "homePhone", HomePhone);
                XmlHelper.AddStringElement(person, "workPhone", WorkPhone);
                XmlHelper.AddStringElement(person, "address", Address);
                XmlHelper.AddStringElement(person, "city", City);
                XmlHelper.AddStringElement(person, "state", State);
                XmlHelper.AddStringElement(person, "zip", ZIP);
                XmlHelper.AddStringElement(person, "relationship", Relationship);
                XmlHelper.AddStringElement(person, "creationTime", CreationTime);
                XmlHelper.AddStringElement(person, "lastModificationTime", LastModifiedTime);
                XmlHelper.AddStringElement(person, "expirationTime", ExpirationTime);
                XmlHelper.AddStringElement(person, "gender", Gender);
                if (SchemaVersion >= ProviderSchemaVersion.v1_1)
                {
                    if (Index.HasValue) XmlHelper.AddStringElement(person, "index", Index.Value);
                    XmlHelper.AddStringElement(person, "pictureUrl", PictureUrl);
                    XmlHelper.AddStringElement(person, "friendStatus", FriendStatus);
                }
                xdoc.AppendChild(person);
                return xdoc.DocumentElement ;
            }
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Person)
            {
                if (((Person)obj).UserID == this.UserID)
                    return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.UserID.GetHashCode();
        }

        /// <summary>
        /// Gets or sets the profile photo for this user. Supported formats are JPEG,BMP or PNG.
        /// </summary>
        public byte[] ProfilePhoto
        {
            get { return m_profilePhoto; }
            set { m_profilePhoto = value; }
        }

        /// <summary>
        /// Sets the ProfilePhoto property based on the url provided. The image is automatically downloaded.
        /// </summary>
        /// <param name="url"></param>
        public void SetProfilePhoto(Uri url)
        {
            ProfilePhoto = Helpers.GetBytesFromUrl(url);
            PictureUrl = url;
        }


    }
    public class Friends : SchemaObject
    {
        private List<Person> m_people = new List<Person>();
        public List<Person> People { get { return m_people; } }
        public override string Xml
        {
            get
            {
                return XmlEx.OuterXml;
            }
        }

        internal override XmlElement XmlEx
        {
            get {
                XmlDocument xdoc = XmlHelper.GetXmlDoc(SchemaVersion);
                XmlElement xFriends = xdoc.CreateElement("friends", XmlHelper.GetSchemaUrl(SchemaVersion));

                foreach (Person p in People)
                {
                    p.SchemaVersion = SchemaVersion;
                    xFriends.AppendChild(xdoc.ImportNode(p.XmlEx,true));
                }

                xdoc.AppendChild(xFriends);
                return xdoc.DocumentElement;
            }
        }


    }

    public enum Gender { unspecified, male, female }
    public enum FriendStatus { friend, notfriend, pending, pendingin, pendingout }
}
