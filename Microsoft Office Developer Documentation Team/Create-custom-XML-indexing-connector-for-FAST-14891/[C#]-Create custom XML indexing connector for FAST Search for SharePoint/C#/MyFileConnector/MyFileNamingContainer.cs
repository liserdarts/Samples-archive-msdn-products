using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using connector = Microsoft.Office.Server.Search.Connector;
using Microsoft.Office.Server.Search.Connector.BDC;
using Microsoft.BusinessData.MetadataModel;
using Microsoft.BusinessData.Runtime;

namespace MyFileConnector
{
    /// <summary>
    /// This class defines functions that return the URL of the 
    /// entity instance to the search system
    /// </summary>
    class MyFileNamingContainer : INamingContainer
    {
        #region INamingContainer Members

        private Uri sourceUri;
        private Uri accessUri;
        //GUID of the property set for the connector
        private static Guid propertySetGuid = new Guid("{AC0E43DF-52CF-401f-97BD-912CE683FE1C}");


        public void Initialize(Uri uri)
        {
            this.sourceUri = uri;
        }

        public Uri GetAccessUri(IEntityInstance entityInstance, IEntityInstance parentEntityInstance)
        {
            return this.GetAccessUri(entityInstance);
        }

        public Uri GetAccessUri(IEntityInstance entityInstance)
        {
            object[] ids = entityInstance.GetIdentity().GetIdentifierValues();
            string idString = ids[0].ToString();
            idString = idString.Substring(idString.LastIndexOf('\\') + 1);
            this.accessUri = new Uri(this.sourceUri + "/" + idString);
            return this.accessUri;
        }

        public Uri GetAccessUri(IEntity entity, ILobSystemInstance lobSystemInstance)
        {
            throw new NotImplementedException();
        }

        public Uri GetAccessUri(ILobSystemInstance lobSystemInstance)
        {
            throw new NotImplementedException();
        }

        public Uri GetAccessUri(ILobSystem lobSystem)
        {
            throw new NotImplementedException();
        }

        public Uri GetDisplayUri(IEntityInstance entityInstance, IEntityInstance parentEntityInstance)
        {
            return this.sourceUri;
        }

        public Uri GetDisplayUri(IEntityInstance entityInstance, string computedDisplayUri)
        {
            if (string.IsNullOrEmpty(computedDisplayUri))
                return this.sourceUri;
            return new Uri(computedDisplayUri);
        }

        public Uri GetDisplayUri(IEntity entity, ILobSystemInstance lobSystemInstance)
        {
            return this.accessUri;
        }

        public Uri GetDisplayUri(ILobSystemInstance lobSystemInstance)
        {
            throw new NotImplementedException();
        }

        public Uri GetDisplayUri(ILobSystem lobSystem)
        {
            return this.sourceUri;
        }



        public Guid PartitionId
        {
            get { return Guid.Empty; }
        }

        public Guid PropertySet
        {
            get { return propertySetGuid; }
        }
        #endregion


    }
}
