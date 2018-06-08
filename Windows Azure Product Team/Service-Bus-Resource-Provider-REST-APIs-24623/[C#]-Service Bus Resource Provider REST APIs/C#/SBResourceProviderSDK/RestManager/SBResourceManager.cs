//---------------------------------------------------------------------------------
// Microsoft (R)  Windows Azure SDK
// Software Development Kit
// 
// Copyright (c) Microsoft Corporation. All rights reserved.  
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
//---------------------------------------------------------------------------------

namespace Microsoft.Samples.ServiceBus.ResourceProviderSDK
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Cryptography.X509Certificates;
    using System.ServiceModel.Syndication;
    using System.Text;
    using System.Threading;
    using System.Xml;
    
    public abstract class SBResourceManager
    {
        protected EntityDescription EntityDescription
        {
            get;
            set;
        }

        protected string AzureSubscriptionID;
        protected X509Certificate2 ManagementCertificate;
        
        // Specific to resource implementation
        protected abstract string RequestURI { get; }

        public bool Create()
        {
            HttpStatusCode? statusCode = null;
            bool IsCreateSuccessfull = this.Create(out statusCode);
            if (!IsCreateSuccessfull)
            {
                Console.WriteLine("Entity {0}, Create failed with Error: {1}",EntityDescription.Name, statusCode);
            }
            return IsCreateSuccessfull;
        }

        public bool Create(out HttpStatusCode? httpStatusCode)
        {
            HttpWebRequest request = WebRequest.Create(RequestURI) as HttpWebRequest;

            //add Subscription management Certificate to the request
            request.ClientCertificates.Add(ManagementCertificate);

            //create the request headers and specify the method required for this type of operation
            request.Headers.Add(RestConstants.RDFEHeader, RestConstants.RDFEHeaderValue);
            request.ContentType = "application/atom+xml";
            request.Method = "PUT";
            request.KeepAlive = true;

            // Serialize NamespaceDescription, if additional properties needs to be specified http://msdn.microsoft.com/en-us/library/jj873988.aspx
            string requestBody = string.Format(
                RestConstants.RestRequestBodyFormat, 
                SerializeResourceDescription(EntityDescription, this.EntityDescription.GetType()));

            byte[] byteArray = Encoding.UTF8.GetBytes(requestBody);
            request.ContentLength = byteArray.Length;
                
            //write the data to the stream that holds the request body content
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            // Response contains the Namespace Description if the SB Namespace creation is Successfull
            httpStatusCode = HttpStatusCode.Unused;
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    httpStatusCode = response.StatusCode;
                    return true;
                }
            }
            catch (WebException webException)
            {
                using (HttpWebResponse exceptionResponse = webException.Response as HttpWebResponse)
                {
                    // Log that the Exception is swallowed and reason why 
                    httpStatusCode = exceptionResponse.StatusCode;
                }

                Trace.WriteLine(string.Format("Swallowed Exception {0}", webException.Message));
            }

            return false;
        }

        public bool Delete()
        {
            HttpStatusCode? statusCode = null;
            bool IsDeleted = this.Delete(out statusCode);
            if (!IsDeleted)
            {
                Console.WriteLine("Entity {0}, Delete failed with Error: {1}", EntityDescription.Name, statusCode);
            }
            return IsDeleted;
        }

        public bool Delete(out HttpStatusCode? httpStatusCode)
        {
            HttpWebRequest request = WebRequest.Create(RequestURI) as HttpWebRequest;

            //add Subscription management Certificate to the request
            request.ClientCertificates.Add(ManagementCertificate);

            //create the request headers and specify the method required for this type of operation
            request.Headers.Add(RestConstants.RDFEHeader, RestConstants.RDFEHeaderValue);
            request.ContentType = "application/atom+xml";
            request.Method = "Delete";
            request.KeepAlive = true;

            // Serialize NamespaceDescription, if additional properties needs to be specified http://msdn.microsoft.com/en-us/library/jj873988.aspx
            string requestBody = "";

            byte[] byteArray = Encoding.UTF8.GetBytes(requestBody);
            request.ContentLength = byteArray.Length;

            //write the data to the stream that holds the request body content
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            // Response contains the Namespace Description if the SB Namespace creation is Successfull
            httpStatusCode = HttpStatusCode.Unused;
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    httpStatusCode = response.StatusCode;
                    return true;
                }
            }
            catch (WebException webException)
            {
                using (HttpWebResponse exceptionResponse = webException.Response as HttpWebResponse)
                {
                    // Log that the Exception is swallowed and reason why 
                    httpStatusCode = exceptionResponse.StatusCode;
                }

                Trace.WriteLine(string.Format("Swallowed Exception {0}", webException.Message));
            }

            return false;
        }

        private string LookUp(out HttpStatusCode? httpStatusCode)
        {
            HttpWebRequest request = WebRequest.Create(RequestURI) as HttpWebRequest;
            //add Subscription management Certificate to the request
            request.ClientCertificates.Add(ManagementCertificate);
            StreamReader responseReader = null;
            string lookupResponse = string.Empty;

            //create the request headers and specify the method required for this type of operation
            request.Headers.Add(RestConstants.RDFEHeader, RestConstants.RDFEHeaderValue);
            request.ContentType = "application/atom+xml";
            request.Method = "GET";
            request.KeepAlive = true;

            // Response contains the Namespace Description if the GET SB Namespace is Successfull
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                httpStatusCode = response.StatusCode;
                responseReader = new StreamReader(response.GetResponseStream());
                lookupResponse = responseReader.ReadToEnd();
            }

            return lookupResponse;
        }

        public EntityDescription LookUp()
        {
            HttpStatusCode? httpStatusCode = null;
            string lookupResponse = this.LookUp(out httpStatusCode);
            MethodInfo methodInfo = typeof(SBResourceManager).GetMethod("DeserializeResourceDescription");
            methodInfo = methodInfo.MakeGenericMethod(this.EntityDescription.GetType());
            EntityDescription entityDescription = methodInfo.Invoke(this, new object[]{ lookupResponse}) as EntityDescription;
            entityDescription.ParentEntity = this.EntityDescription.ParentEntity;
            if (string.IsNullOrEmpty(entityDescription.Name))
            {
                entityDescription.Name = this.EntityDescription.Name;
            }
            return entityDescription;
        }

        public void WaitUntillActive()
        {
            bool isEntityActive = false;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (sw.Elapsed < TimeSpan.FromMinutes(8))
            {
                HttpStatusCode? httpStatusCode = null;
                // lookup Entity              
                EntityDescription entity = this.LookUp();
                if (entity.IsActive())
                {
                    isEntityActive = true;
                    break;
                }
                
                Trace.WriteLine("Waiting for 30 seconds for the Entity to be active");
                Thread.Sleep(TimeSpan.FromSeconds(30));
            }

            sw.Stop();

            if (!isEntityActive)
            {
                throw new ApplicationException(string.Format("Entity didn't get active in {0} seconds", sw.Elapsed.TotalSeconds));
            }
        }

        internal string SerializeResourceDescription(object resourceDescription, Type resourceType)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = " ";
            settings.NewLineOnAttributes = true;
            settings.OmitXmlDeclaration = true;
            XmlWriter xmlWriter = XmlWriter.Create(sb, settings);
            DataContractSerializer serializer = new DataContractSerializer(resourceType);
            serializer.WriteObject(xmlWriter, resourceDescription);
            xmlWriter.Close();
            return sb.ToString();
        }

        public T DeserializeResourceDescription<T>(string resourceDescription)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            Atom10ItemFormatter formater = new Atom10ItemFormatter();
            formater.ReadFrom(new XmlTextReader(new StringReader(resourceDescription)));
            XmlSyndicationContent content = formater.Item.Content as XmlSyndicationContent;
            return content.ReadContent<T>(serializer);
        }
    }
}