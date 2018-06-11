// ***************************************************************
// <copyright file="MailFilterAgentFactory.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// A factory for the MailFilter agent. Stores all session data
// including the lists of temporarily blocked senders.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Agents.MailFilterAgent
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Security.Cryptography;

    using Microsoft.Exchange.Data;
    using Microsoft.Exchange.Data.Transport;
    using Microsoft.Exchange.Data.Transport.Smtp;

    #region Agent Factory Class
    /// <summary>
    /// The agent factory for creating and maintaining a list of temporarily blocked senders.
    /// </summary>
    public class MailFilterAgentFactory : SmtpReceiveAgentFactory
    {
        /// <summary>
        /// The path where data files are stored relative to
        /// the DLL that contains this code.
        /// </summary>
        private const string RelativeDataPath = @"data\";

        /// <summary>
        /// The file name of the XML file that contains configuration
        /// data for the MailFilter agent.
        /// </summary>
        private const string ConfigFileName = "MailFilterConfig.xml";

        /// <summary>
        /// The name of the file where verified temporarily blocked sender
        /// triplets should be stored.
        /// </summary>
        private const string VerifiedDataFile = "VerifiedData.txt";

        /// <summary>
        /// The name of the file where unverified temporarily blocked sender
        /// entries should be stored.
        /// </summary>
        private const string UnverifiedDataFile = "UnverifiedData.txt";

        /// <summary>
        /// A hash code generator that will be used to
        /// calculate the hash values of triplets.
        /// </summary>
        private SHA256Managed hashManager = new SHA256Managed();

        /// <summary>
        /// An object that contains settings to be used by agents and databases.
        /// </summary>
        private MailFilterSettings MailFilterSettings;

        /// <summary>
        /// A shared database element that will be used by
        /// all agent instances to store verified triplets.
        /// </summary>
        private MailFilterDatabase verifiedDB;

        /// <summary>
        /// A shared database element that will be used by
        /// all agent instances to store unverified triplets.
        /// </summary>
        private MailFilterDatabase unverifiedDB;

        /// <summary>
        /// The location on disk where data files such as
        /// the configuration file and persisted data are stored.
        /// The value will be calculated relative to the
        /// location of the DLL that contains the assembly
        /// of this class.
        /// </summary>
        private string dataPath;

        /// <summary>
        /// Factory constructor.
        /// </summary>
        public MailFilterAgentFactory()
        {
            // Initialize fields, parameters, and data structures.
            hashManager = new SHA256Managed();

            // Get the path to where the data files should be.
            // The final result is calculated in relation to the
            // binary DLL that contains this class and can easily 
            // be set to almost any directory. The only requirement
            // is that the "Network Service" user has write permission
            // to whatever directory is specified.
            Assembly currAssembly = Assembly.GetAssembly(this.GetType());
            string assemblyPath = Path.GetDirectoryName(currAssembly.Location);
            this.dataPath = Path.Combine(assemblyPath, RelativeDataPath);

            // Read the XML configuration file and apply its settings.
            this.MailFilterSettings = new MailFilterSettings(Path.Combine(this.dataPath, ConfigFileName));

            // Initialize the empty database tables.
            this.verifiedDB = new MailFilterDatabase(
                                                   this.MailFilterSettings.MaxVerifiedEntries,
                                                   this.MailFilterSettings.BucketSize);

            this.unverifiedDB = new MailFilterDatabase(
                                                     this.MailFilterSettings.MaxUnverifiedEntries,
                                                     this.MailFilterSettings.BucketSize);

            // Try loading the contents of a persisted data file into
            // the temporarily blocked sender list tables.
            this.verifiedDB.ReadPersistedData(Path.Combine(this.dataPath, VerifiedDataFile));

            this.unverifiedDB.ReadPersistedData(Path.Combine(this.dataPath, UnverifiedDataFile));
        }

        /// <summary>
        /// This method is automatically called at the end of the service's lifetime,
        /// and is responsible for persisting contents of the database tables to disk
        /// so that the data can be loaded at the beginning of the next session.
        /// 
        /// The data files are written to the path set in this class's constructor.
        /// </summary>
        public override void Close()
        {
            // Write the verified entries to a file.
            this.verifiedDB.PersistToDisk(Path.Combine(this.dataPath, VerifiedDataFile));

            // Write the unverified entries to a file.
            this.unverifiedDB.PersistToDisk(Path.Combine(this.dataPath, UnverifiedDataFile));
        }

        /// <summary>
        /// Create a new MailFilter Agent.
        /// </summary>
        /// <param name="server">Exchange Edge Transport server.</param>
        /// <returns>A new ComAgent.</returns>
        public override SmtpReceiveAgent CreateAgent(SmtpServer server)
        {
            return new MailFilterAgent(
                                     this.MailFilterSettings,
                                     this.verifiedDB,
                                     this.unverifiedDB,
                                     this.hashManager,
                                     server);
        }
    }
    #endregion Agent Factory Class
}