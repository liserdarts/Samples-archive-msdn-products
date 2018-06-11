using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Outlook.CustomAttachment
{
    public class Utilities
    {
        /// <summary>
        /// Prompts the user to select their preferred SharePoint Server (normally called if a registry entry is not found)
        /// </summary>
        private static void PromptForServer()
        {
            using (FirstLoad dialog = new FirstLoad())
            {
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// Gets the root of the webs web service from any URL.
        /// </summary>
        /// <param name="url">The URL to act on.</param>
        /// <returns>The url of the webs web service.</returns>
        public static string RootWebsWebServiceFromAnyURL(string url)
        {
            return UrlConcat(RootSiteFromAnyURL(url), "_vti_bin/webs.asmx");
        }

        /// <summary>
        /// Concatenates urls.
        /// </summary>
        /// <param name="left">The left part of the url.</param>
        /// <param name="right">The right part of the url.</param>
        /// <returns>A validated url.</returns>
        public static string UrlConcat(string left, string right)
        {
            return left.TrimEnd('/') + "/" + right.TrimStart('/');
        }

        /// <summary>
        /// Gets the root site from any URL.
        /// </summary>
        /// <param name="url">The URL to act on.</param>
        /// <returns>The site url.</returns>
        public static string RootSiteFromAnyURL(string url)
        {
            // start the index at position 9 to get the https:// out of the way.  THe next / is the end of the server name
            string returnVal = url.Substring(0, url.IndexOf('/', 9) + 1);

            if (returnVal == String.Empty)
            {
                // there weren't there / in the URL - the original value must have been the siteurl
                returnVal = url;
            }

            return returnVal;
        }

        /// <summary>
        /// Gets the Web URL from any URL.
        /// </summary>
        /// <param name="url">The URL to act on.</param>
        /// <returns>The SPWeb url.</returns>
        public static string WebURLFromAnyURL(string url)
        {
            string retVal = String.Empty;

            System.ServiceModel.BasicHttpBinding b = new System.ServiceModel.BasicHttpBinding();
            b.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.TransportCredentialOnly;
            b.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Ntlm;

            string endpointAddress = RootWebsWebServiceFromAnyURL(url);

            System.ServiceModel.EndpointAddress e = new System.ServiceModel.EndpointAddress(endpointAddress);
            b.OpenTimeout = new TimeSpan(0, 0, 10);
            b.ReceiveTimeout = new TimeSpan(0, 0, 10);
            b.SendTimeout = new TimeSpan(0, 0, 10);

            using (SPWebWebService.WebsSoapClient svc = new SPWebWebService.WebsSoapClient(b, e))
            {
                svc.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
                retVal = svc.WebUrlFromPageUrl(url).TrimEnd('/') + "/";
            }

            return retVal;
        }

        /// <summary>
        /// Retrieves the file information list from raw multiselect urls.
        /// </summary>
        /// <param name="rawMultiselectUrls">The raw multiselect urls.</param>
        /// <returns>A List of PreferredFileInformation</returns>
        public static List<PreferredFileInformation> PreferredFileInformationListFromRawMultiselectUrls(string rawMultiselectUrls)
        {
            rawMultiselectUrls = rawMultiselectUrls.TrimEnd('#');

            List<PreferredFileInformation> files = new List<PreferredFileInformation>();

            if (!string.IsNullOrEmpty(rawMultiselectUrls))
            {
                foreach (string rawMultiselectUrl in rawMultiselectUrls.Split('#').ToList<string>())
                {
                    PreferredFileInformation p = new PreferredFileInformation();
                    p.IsFolder = rawMultiselectUrl.Split(';')[0].ToLower() == "folder";
                    p.Url = rawMultiselectUrl.Split(';')[1];
                    p.Name = rawMultiselectUrl.Split(';')[2];
                    p.DocId = rawMultiselectUrl.Split(';')[3];
                    p.Version = rawMultiselectUrl.Split(';')[4];
                    files.Add(p);
                }
            }

            return files;
        }

        /// <summary>
        /// The URL of the SharePoint server associated with this user's Addin installation, stored in the registry.  Displays UI to ask the user for the server if unavailable.
        /// </summary>
        /// <returns>URL of the SharePoint server, e.g http://server</returns>
        public static string RootSharePointSite()
        {
            string serverURL = string.Empty;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\CustomAttachment", false);
            if (key == null)
            {
                PromptForServer();
                return RootSharePointSite();
            }
            else
            {
                serverURL = (string)key.GetValue("Root Server URL");

                if (string.IsNullOrEmpty(serverURL))
                {
                    PromptForServer();
                    return RootSharePointSite();
                }
                else
                {
                    return serverURL;
                }
            }
        }

        /// <summary>
        /// Stores information relating to SharePoint files.
        /// </summary>
        public class PreferredFileInformation
        {
            /// <summary>
            /// The url of the file.
            /// </summary>
            private string url = String.Empty;

            /// <summary>
            /// The name of the file.
            /// </summary>
            private string name = String.Empty;

            /// <summary>
            /// The docId of the file.
            /// </summary>
            private string docId = String.Empty;

            /// <summary>
            /// The version of the file.
            /// </summary>
            private string version = String.Empty;

            /// <summary>
            /// The local path of the document if downloaded for use as an attachment.
            /// </summary>
            private string localPath = String.Empty;

            /// <summary>
            /// Indicates whether the object is a folder.
            /// </summary>
            private bool isFolder = false;

            /// <summary>
            /// Specifically used for Compare functionality.
            /// Indicates whether the document is the current document.
            /// </summary>
            private bool isCurrentDocument = false;

            /// <summary>
            /// Initializes a new instance of the <see cref="PreferredFileInformation"/> class.
            /// </summary>
            public PreferredFileInformation()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="PreferredFileInformation"/> class.
            /// </summary>
            /// <param name="url">The URL of the file.</param>
            public PreferredFileInformation(string url)
            {
                this.Url = url;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="PreferredFileInformation"/> class.
            /// </summary>
            /// <param name="url">The URL of the file.</param>
            /// <param name="name">The name of the file.</param>
            public PreferredFileInformation(string url, string name)
            {
                this.Url = url;
                this.Name = name;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="PreferredFileInformation"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="isCurrentDocument">if set to <c>true</c> [is current document].</param>
            public PreferredFileInformation(string name, bool isCurrentDocument)
            {
                this.url = name;
                this.name = name;
                this.isCurrentDocument = isCurrentDocument;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="PreferredFileInformation"/> class.
            /// </summary>
            /// <param name="file">The PreferredFileInformation object.</param>
            public PreferredFileInformation(PreferredFileInformation file)
            {
                this.Url = file.Url;
                this.Name = file.Name;
                this.DocId = file.DocId;
                this.Version = file.Version;
                this.LocalPath = file.LocalPath;
            }

            /// <summary>
            /// Gets or sets the Url of the file.
            /// </summary>
            public string Url
            {
                get { return this.url; }
                set { this.url = value; }
            }

            /// <summary>
            /// Gets or sets the Name of the file.
            /// </summary>
            public string Name
            {
                get { return this.name; }
                set { this.name = value; }
            }

            /// <summary>
            /// Gets or sets the DocId of the file.
            /// </summary>
            public string DocId
            {
                get { return this.docId; }
                set { this.docId = value; }
            }

            /// <summary>
            /// Gets or sets the Version of the file.
            /// </summary>
            public string Version
            {
                get { return this.version; }
                set { this.version = value; }
            }

            /// <summary>
            /// Gets or sets the local path.
            /// </summary>
            /// <value>The local path.</value>
            public string LocalPath
            {
                get { return this.localPath; }
                set { this.localPath = value; }
            }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is current document.
            /// </summary>
            /// <value>
            /// 	<c>true</c> if this instance is current document; otherwise, <c>false</c>.
            /// </value>
            public bool IsCurrentDocument
            {
                get { return this.isCurrentDocument; }
                set { this.isCurrentDocument = value; }
            }

            /// <summary>
            /// Gets the name no ext.
            /// </summary>
            /// <value>The name no ext.</value>
            public string NameNoExtension
            {
                get
                {
                    if (this.Name.Contains("."))
                    {
                        return this.Name.Substring(0, this.Name.LastIndexOf("."));
                    }
                    else
                    {
                        return this.Name;
                    }
                }
            }

            /// <summary>
            /// Gets the file extension.
            /// </summary>
            /// <value>The file extension.</value>
            public string FileExtension
            {
                get { return this.Name.Substring(this.name.LastIndexOf(".") + 1); }
            }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is folder.
            /// </summary>
            /// <value><c>true</c> if this instance is folder; otherwise, <c>false</c>.</value>
            public bool IsFolder
            {
                get { return this.isFolder; }
                set { this.isFolder = value; }
            }

            /// <summary>
            /// Gets a value indicating whether this instance is current version.
            /// </summary>
            /// <value>
            ///     <c>true</c> if this instance is current version; otherwise, <c>false</c>.
            /// </value>
            public bool IsCurrentVersion
            {
                get
                {
                    return !this.Url.Contains("_vti_history");
                }
            }

            /// <summary>
            /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
            /// </summary>
            /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
            /// <returns>
            ///     <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
            /// </returns>
            /// <exception cref="T:System.NullReferenceException">
            /// The <paramref name="obj"/> parameter is null.
            /// </exception>
            public override bool Equals(object obj)
            {
                if (obj.GetType() == typeof(PreferredFileInformation))
                {
                    return this.Url == ((PreferredFileInformation)obj).Url;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// Returns a hash code for this instance.
            /// </summary>
            /// <returns>
            /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
            /// </returns>
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                string docIDAppendage = string.Empty;
                string versionAppendage = string.Empty;

                if (!string.IsNullOrEmpty(this.DocId))
                {
                    docIDAppendage = " " + this.DocId;
                }

                if (!string.IsNullOrEmpty(this.Version))
                {
                    versionAppendage = " v" + this.Version;
                }

                return String.Format("{0}{1}{2}.{3}", this.NameNoExtension, docIDAppendage, versionAppendage, this.FileExtension);
            }

            /// <summary>
            /// Gets properties as strings for debugging
            /// </summary>
            /// <returns>String of debug information</returns>
            public string ToStringDebug()
            {
                string returnVal = string.Empty;

                if (!string.IsNullOrEmpty(this.Url))
                {
                    returnVal += "; Url=" + this.Url;
                }

                if (!string.IsNullOrEmpty(this.Name))
                {
                    returnVal += "; Name=" + this.Name;
                }

                if (!string.IsNullOrEmpty(this.DocId))
                {
                    returnVal += "; DocId" + this.DocId;
                }

                if (!string.IsNullOrEmpty(this.Url))
                {
                    returnVal += "; Version=" + this.Version;
                }

                return returnVal;
            }
        }
    }
}
