using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.BusinessData.Infrastructure;
using Microsoft.BusinessData.Runtime;
using Microsoft.BusinessData.MetadataModel;
using Microsoft.BusinessData.MetadataModel.Collections;
using System.Collections;

namespace BusinessDataConnectivitySamples.Connectors
{
    public class FileSystemConnector : ISystemUtility, IAdministrableSystem, ISystemPropertyValidator
    {
        const string baseFolderPropertyName = "BaseFolder";

        #region IAdministrableSystem Members

        public IList<AdministrableProperty> AdministrableLobSystemProperties
        {
            get
            {
                return new List<AdministrableProperty>()
                {
                };
            }
        }

        public IList<AdministrableProperty> AdministrableLobSystemInstanceProperties
        {
            get
            {
                return new List<AdministrableProperty>()
                {
                   //This connector features one property which needs to be on LobSystemInstance (external system instance)
                   new AdministrableProperty("Base Folder","The path to the folder that stores the data.", typeof(string), baseFolderPropertyName, typeof(string), true)  
                };
            }
        }
        #endregion

        #region ISystemUtility Members

        public void ExecuteStatic(IMethodInstance methodInstance, ILobSystemInstance lobSystemInstance, object[] methodSignatureArgs, IExecutionContext context)
        {
            if (methodInstance == null)
            {
                throw (new ArgumentNullException("methodInstance"));
            }
            if (lobSystemInstance == null)
            {
                throw (new ArgumentNullException("lobSystemInstance"));
            }
            if (methodSignatureArgs == null)
            {
                throw (new ArgumentNullException("args"));
            }

            #region validate the base folder
            object baseFolderValue;
            if (!lobSystemInstance.GetProperties().TryGetValue(baseFolderPropertyName, out baseFolderValue))
            {
                throw new InvalidOperationException("BaseFolder property is missing");
            }

            String baseFolderName = baseFolderValue as string;

            if (String.IsNullOrEmpty(baseFolderName))
            {
                throw new InvalidOperationException("BaseFolder proeprty contains an invalid value.");
            }

            DirectoryInfo baseFolder = new DirectoryInfo(baseFolderName);

            if (!baseFolder.Exists)
            {
                throw new InvalidOperationException("Base folder doesn't exist.");
            }
            #endregion

            //This connector works based on the type of the MethodInstance
            //Most other connectors work on the name of the method
            if (methodInstance.MethodInstanceType == MethodInstanceType.Finder)
            {
                //Connector assumption:
                //First parameter will always be a wildcarded search string for file name
                //Second parameter will always be the return value
                String wildcard = methodSignatureArgs[0] as string;
                IList<FileInfo> results = new List<FileInfo>();
                methodSignatureArgs[1] = baseFolder.GetFiles(wildcard);
            }
            else if (methodInstance.MethodInstanceType == MethodInstanceType.SpecificFinder)
            {
                //Connector assumption: 
                //First parameter will always be the file name
                //Second parameter will always be the return value
                string fileName = methodSignatureArgs[0] as string;
                FileInfo result = new FileInfo(Path.Combine(baseFolder.FullName, fileName));
                if (result.Exists && result.Directory.FullName.Equals(baseFolder.FullName))
                {
                    methodSignatureArgs[1] = result;
                }
            }
            else if (methodInstance.MethodInstanceType == MethodInstanceType.StreamAccessor)
            {
                //Connector assumption: 
                //First parameter will always be the file name
                //Second parameter will always be the return value
                string fileName = methodSignatureArgs[0] as string;
                FileInfo result = new FileInfo(Path.Combine(baseFolder.FullName, fileName));
                if (result.Exists && result.Directory.FullName.Equals(baseFolder.FullName))
                {
                    methodSignatureArgs[1] = result.OpenRead();
                }
            }

        }

        public virtual IEnumerator
            CreateEntityInstanceDataEnumerator(Object rawAdapterEntityInstanceStream, ISharedEntityState sharedEntityState)
        {
            IEnumerator enumerator = rawAdapterEntityInstanceStream as IEnumerator;
            if (enumerator != null)
            {
                return enumerator;
            }
            IEnumerable enumerable = rawAdapterEntityInstanceStream as IEnumerable;
            if (enumerable != null)
            {
                return enumerable.GetEnumerator();
            }
            throw new InvalidOperationException("Connector or Model error.");
        }
        
        public ITypeReflector DefaultTypeReflector
        {
            get
            {
                //This connector does not require a special type reflection.
                return null;
            }
        }
        
        public IConnectionManager DefaultConnectionManager
        {
            get
            {
                //This connector does not feature explicitly managed connections.
                return null;
            }
        }
        #endregion


        #region ISystemPropertyValidator Members

        public void ValidateLobSystemInstanceProperty(string name, ref object value, string metadataObjectName, Type metadataObjectType)
        {
            //Validate if the given directory exists.
            if (name == baseFolderPropertyName)
            {
                String folderName = value as string;
                if (!Directory.Exists(folderName))
                {
                    throw new ArgumentException("The given directory does not exist.");
                }
            }
        }

        public void ValidateLobSystemProperty(string name, ref object value, string metadataObjectName, Type metadataObjectType)
        {
            //No LobSystem (external system) properties to validate
        }

        #endregion
    }
}

