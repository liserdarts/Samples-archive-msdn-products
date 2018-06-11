using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using connector = Microsoft.Office.Server.Search.Connector;
using Microsoft.Office.Server.Search.Connector.BDC;
using Microsoft.BusinessData.MetadataModel;
using System.IO;
using Microsoft.SharePoint.Utilities;

namespace MyFileConnector
{
    
    /// <summary>
    /// Defines methods that map the URL coming in from search to the Object in Metadata store
    /// </summary>
    class MyFileLobUri : LobUri
    {
        public MyFileLobUri(): base("myfile") //myfile is the scheme of the URLs
        {
            this.lobSystem = this.Catalog.GetLobSystem("MyFileSystem"); //Name of LobSystem defined in model file
        }

        private IEntity entity;
        public override Microsoft.BusinessData.MetadataModel.IEntity Entity
        {
            get { return this.entity; }
        }

        private Microsoft.BusinessData.Runtime.Identity identity;
        public override Microsoft.BusinessData.Runtime.Identity Identity
        {
            get { return this.identity; }
        }

        public override void Initialize(Microsoft.Office.Server.Search.Connector.IConnectionContext context)
        {
            //connector.UriParser uri = new connector.UriParser(context.Path);
            //Uri sourceUri = uri.ToUri(false); // No case encoding
            Uri sourceUri = context.Path;
            

            this.lobSystemInstance =
                this.lobSystem.GetLobSystemInstances()[0].Value;

            string filepath = @"\\" + sourceUri.Host + sourceUri.AbsolutePath.Replace('/', '\\');

            ///
            ///To decode spaces and other characters in file names that are encoded by search
            ///
            filepath = SPHttpUtility.UrlPathDecode(filepath, false);
            
                        
            if(Directory.Exists(filepath))
            {
                ///
                ///Only set the entity for the start address so that idEnumerator is called on the entity
                ///
                this.entity = this.Catalog.GetEntity("MyFileConnector", "MyFolder");

                ///
                ///If the Uri belongs to a folder, set the identity so that
                ///associations, specificifnder and streamaccessors are called
                ///                 
                this.identity = new Microsoft.BusinessData.Runtime.Identity(filepath);

            }
            else if (File.Exists(filepath))
            {
                ///
                ///If it is a file, switch the entity name to MyFile
                ///
                this.entity = this.Catalog.GetEntity("MyFileConnector", "MyFile");                

                ///
                ///If the Uri belongs to a file, set the identity so that findSpecific is called
                ///                 
                this.identity = new Microsoft.BusinessData.Runtime.Identity(filepath);
            }
            
        }

        private ILobSystem lobSystem;
        public override Microsoft.BusinessData.MetadataModel.ILobSystem LobSystem
        {
            get { return this.lobSystem; }
        }

        private ILobSystemInstance lobSystemInstance;
        public override Microsoft.BusinessData.MetadataModel.ILobSystemInstance LobSystemInstance
        {
            get { return this.lobSystemInstance; }
        }

        public override Guid PartitionId
        {
            get { throw new NotImplementedException(); }
        }

        private Uri sourceUri;
        public override Uri SourceUri
        {
            get
            {
                return this.sourceUri;
            }
            set
            {
                this.sourceUri = value;
            }
        }
    }
}
