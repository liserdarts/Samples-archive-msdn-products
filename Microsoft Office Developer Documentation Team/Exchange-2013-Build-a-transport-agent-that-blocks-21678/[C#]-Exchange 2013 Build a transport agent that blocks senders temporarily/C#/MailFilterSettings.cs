// ***************************************************************
// <copyright file="MailFilterSettings.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// A class that contains all the settings that the agent
// and its database will use. This class will also have the
// methods to read the settings in from the XML configuration file.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Agents.MailFilterAgent
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// This class stores settings that will be
    /// used by the MailFilter agent and factory.
    /// </summary>
    public class MailFilterSettings
    {
        #region Fields
        /// <summary>
        /// The amount of time that inactive verified entries will be
        /// stored in the database.
        /// </summary>
        private TimeSpan verifiedExpirationPeriod;

        /// <summary>
        /// The amount of time that unverified entries will be
        /// stored in the database.
        /// </summary>
        private TimeSpan unverifiedExpirationPeriod;

        /// <summary>
        /// The length of time after a new triplet is added to the
        /// unverified list that its retry attempts will be ignored.
        /// </summary>
        private TimeSpan initialBlockingPeriod;

        /// <summary>
        /// The maximum number of verified entries allowed in the database.
        /// </summary>
        private int maxVerifiedEntries;

        /// <summary>
        /// The maximum number of unverified entries allowed in the database.
        /// </summary>
        private int maxUnverifiedEntries;

        /// <summary>
        /// The number of slots in the bucket for each hash row.
        /// </summary>
        private int bucketSize;

        /// <summary>
        /// The number of rows that each agent should attempt to clean in each pass.
        /// </summary>
        private int cleanRowCount;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// An empty constructor initializes with default values.
        /// </summary>
        /// <param name="filePath">The path to an XML file that contains the settings.</param>
        public MailFilterSettings(string filePath)
        {
            // Default VerifiedExpirationTime: 14 days.
            this.VerifiedExpirationTime = new TimeSpan(14, 0, 0, 0);

            // Deafault UnverifiedExpirationTime: 4 hours.
            this.UnverifiedExpirationTime = new TimeSpan(0, 4, 0, 0);

            // Default InitialBlockingPeriod: 2 minutes.
            this.InitialBlockingPeriod = new TimeSpan(0, 0, 2, 0);

            // Default MaxVerifiedEntries.
            this.MaxVerifiedEntries = 100000;

            // Default MaxUnverifiedEntries.
            this.MaxUnverifiedEntries = 10000;

            // Default bucket size.
            this.BucketSize = 5;

            // Default cleaning row count.
            this.CleanRowCount = 10;

            // Read nondefault settings from file.
            this.ReadXMLConfig(filePath);
        }

        /// <summary>
        /// The Copy constructor makes a copy of another settings object.
        /// </summary>
        /// <param name="other">The settings object to be copied.</param>
        public MailFilterSettings(MailFilterSettings other)
        {
            this.VerifiedExpirationTime = other.VerifiedExpirationTime;
            this.UnverifiedExpirationTime = other.UnverifiedExpirationTime;
            this.InitialBlockingPeriod = other.InitialBlockingPeriod;
            this.MaxVerifiedEntries = other.MaxVerifiedEntries;
            this.MaxUnverifiedEntries = other.MaxUnverifiedEntries;
            this.BucketSize = other.BucketSize;
            this.CleanRowCount = other.CleanRowCount;
        }
        #endregion Constructors

        #region Parameters
        /// <summary>
        /// Gets or sets the expiration period for verified entries.
        /// </summary>
        public TimeSpan VerifiedExpirationTime
        {
            get { return this.verifiedExpirationPeriod; }

            set { this.verifiedExpirationPeriod = value; }
        }

        /// <summary>
        /// Gets or sets the expiration period for unverified entries.
        /// </summary>
        public TimeSpan UnverifiedExpirationTime
        {
            get { return this.unverifiedExpirationPeriod; }

            set { this.unverifiedExpirationPeriod = value; }
        }

        /// <summary>
        /// Gets or sets the initial blocking period for all unverified entries.
        /// </summary>
        public TimeSpan InitialBlockingPeriod
        {
            get { return this.initialBlockingPeriod; }

            set { this.initialBlockingPeriod = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of verified entries to store.
        /// </summary>
        public int MaxVerifiedEntries
        {
            get { return this.maxVerifiedEntries; }

            set { this.maxVerifiedEntries = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of unverified entries to store.
        /// </summary>
        public int MaxUnverifiedEntries
        {
            get { return this.maxUnverifiedEntries; }

            set { this.maxUnverifiedEntries = value; }
        }

        /// <summary>
        /// Gets or sets the bucket size used in the hash tables.
        /// </summary>
        public int BucketSize
        {
            get { return this.bucketSize; }

            set { this.bucketSize = value; }
        }

        /// <summary>
        /// Gets or sets the number of rows that should be checked for
        /// expired entries during each cleanup check.
        /// </summary>
        public int CleanRowCount
        {
            get { return this.cleanRowCount; }

            set { this.cleanRowCount = value; }
        }
        #endregion Parameters

        #region XML File Parsing
        /// <summary>
        /// Reads in configuration options from an XML file and sets the instance
        /// variables to the corresponding values that are read in if they
        /// are valid. If an invalid value is found, or a value is not
        /// set in the XML file, the variable will not be changed from its
        /// default value.
        /// </summary>
        /// <param name="path">The path to the XML configuration file.</param>
        /// <returns>True if the file was read.</returns>
        public bool ReadXMLConfig(string path)
        {
            bool retval = false;

            try
            {
                // Some temp variables that will be used during validation.
                int fileInt = 0;
                TimeSpan fileTime = new TimeSpan();

                // Load the file into the XML reader.
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                XmlNode xmlRoot = xmlDoc.SelectSingleNode("MailFilterConfig");

                // Read in the maximum number of verified entries to store.
                fileInt = this.ReadXmlInt(xmlRoot, "MaxVerifiedEntries");
                if (fileInt > 0)
                {
                    this.maxVerifiedEntries = fileInt;
                }

                // Read in the maximum number of unverified entries to store.
                fileInt = this.ReadXmlInt(xmlRoot, "MaxUnverifiedEntries");
                if (fileInt > 0)
                {
                    this.maxUnverifiedEntries = fileInt;
                }

                // Read in the bucket size to use.
                fileInt = this.ReadXmlInt(xmlRoot, "BucketSize");
                if (fileInt > 0 &&
                    fileInt < this.maxUnverifiedEntries &&
                    fileInt < this.maxVerifiedEntries)
                {
                    this.bucketSize = fileInt;
                }

                // Read in the number of rows to clean in each instance.
                fileInt = this.ReadXmlInt(xmlRoot, "CleanRowCount");
                if (fileInt > 0 &&
                    fileInt < this.maxVerifiedEntries &&
                    fileInt < this.maxUnverifiedEntries)
                {
                    this.cleanRowCount = fileInt;
                }

                // Read in the initial blocking period.
                fileTime = this.ReadXmlTimeSpan(xmlRoot, "BlockPeriod");
                if (fileTime > new TimeSpan())
                {
                    this.initialBlockingPeriod = fileTime;
                }

                // Read in the verified entry lifetime.
                fileTime = this.ReadXmlTimeSpan(xmlRoot, "VerifiedLifetime");
                if (fileTime > new TimeSpan())
                {
                    this.verifiedExpirationPeriod = fileTime;
                }

                // Read in the unverified entry lifetime.
                fileTime = this.ReadXmlTimeSpan(xmlRoot, "UnverifiedLifetime");
                if (fileTime > new TimeSpan())
                {
                    this.unverifiedExpirationPeriod = fileTime;
                }
            }
            #region XML and File IO Catches
            catch (XmlException e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
            catch (IOException e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
            #endregion XML File IO Catches
            return retval;
        }

        /// <summary>
        /// Reads an integer value based on the name of the element it is the value of.
        /// </summary>
        /// <param name="root">The root element to start searching from.</param>
        /// <param name="xmlParam">The name of the element to get the value of.</param>
        /// <returns>The parsed value, or 0 if it was not found.</returns>
        private int ReadXmlInt(XmlNode root, string xmlParam)
        {
            int retval = 0;

            if (root != null && xmlParam != null)
            {
                XmlNode valNode = root.SelectSingleNode(xmlParam);
                if (valNode != null)
                {
                    XmlNode childNode = valNode.FirstChild;
                    if (childNode != null)
                    {
                        int.TryParse(childNode.Value, out retval);
                    }
                }
            }

            return retval;
        }

        /// <summary>
        /// Reads, finds, and parses a time span value based on the name of the element it is in.
        /// </summary>
        /// <param name="root">The root element to start searching from.</param>
        /// <param name="xmlParam">The element name to look for.</param>
        /// <returns>The parsed value, or a value of 00:00:00 if not found.</returns>
        private TimeSpan ReadXmlTimeSpan(XmlNode root, string xmlParam)
        {
            TimeSpan retval = new TimeSpan();

            if (root != null && xmlParam != null)
            {
                XmlNode xmlParamNode = root.SelectSingleNode(xmlParam);
                if (xmlParamNode != null)
                {
                    XmlNode childNode = xmlParamNode.FirstChild;
                    if (childNode != null)
                    {
                        TimeSpan.TryParse(childNode.Value, out retval);
                    }
                }
            }

            return retval;
        }
        #endregion XML File Parsing
    }
}
