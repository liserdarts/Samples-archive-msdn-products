using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.SharePoint.Client;

namespace getonedrivefiles.DataModel
{
    
    /// <summary>
    /// Gets all files from a SharePoint site (including OneDrivePro) and raises an event when the collection is complete.
    /// </summary>
    public class OneDriveProDocs
    {
        private String documentsList = "Documents";
        private String sharedDocumentsList = "Shared Documents";
        private String logInfo = "";

        public delegate void NewOneDriveItemsEvent(object Sender, OneDriveEventData EventData);
        public event NewOneDriveItemsEvent NewOneDriveItems;

        private List<getonedrivefiles.DataModel.OneDriveProDocs.ItemAttributes> _ItemsList = new List<ItemAttributes>();
        private HttpRequest _PageRequest;

        public struct ItemAttributes
        {
            public string FileName;
            public string ServerRelativeUrl;
            public string Title;
            public string Author;
            public DateTime TimeLastModified;
            public string ParentFolder;
            public string LogInfo;
        }

        public HttpRequest PageRequest
        {
            set
            {
                _PageRequest = value;
            }
        }

        public OneDriveProDocs(HttpRequest pageRequest)
        {
            _PageRequest = pageRequest;
        }
        
        /// <summary>
        /// Navigate to the "Documents" list. 
        /// </summary>
        /// <remarks>
        /// When all folders under "Documents" have been traversed, fire the NewOnekyDriveItems event.
        /// </remarks>
        
        public void Run()
        {
            ClientContext clientContext = null;
            String hostWeb = "";

            // Get the top-level Lists collection.
            ListCollection lists = GetLists(ref clientContext, ref hostWeb);

            // Get all items under the Documents list.
            GetItemsInDocumentsList(clientContext, lists.GetByTitle(documentsList), hostWeb);

            if (NewOneDriveItems != null)
            {
                if (_ItemsList.Count == 0)
                {
                    ItemAttributes itemAttrs = new ItemAttributes();

                    itemAttrs.FileName = logInfo;
                    _ItemsList.Add(itemAttrs);

                }
                OneDriveEventData data = new OneDriveEventData(_ItemsList);
                NewOneDriveItems(this, data);
            }
        }

        /// <summary>
        /// Set up the prerequisite plumbing to make queries into SharePoint, and get the Lists item at the top of the hierarchy in an SharePoint site.
        /// </summary>
        /// <remarks>
        /// This method calls TokenHelper methods to create a context token and client context, using 
        /// information contained in the HTTP request that loads the ASP page. The client context is needed 
        /// for many SharePoint queries that download server data to the client. 
        /// When this method returns, clientContext and hostWeb will be set.
        /// </remarks>
        /// <param name="clientContext">The client context.</param>
        /// <param name="hostWeb">The host Web.</param>
        /// <returns>A ListCollection object that contains the Lists item.</returns>
        private ListCollection GetLists(ref ClientContext clientContext, ref String hostWeb)
        {
            if (_PageRequest == null)
            {
                throw new Exception("Bad page request.");
            }
            var contextToken = getonedrivefiles.TokenHelper.GetContextTokenFromRequest(_PageRequest);
            if (contextToken == null)
            {
                throw new Exception("Bad token from GetContextTokenFromRequest.");
            }

            hostWeb = _PageRequest["SPHostUrl"];

            clientContext = getonedrivefiles.TokenHelper.GetClientContextWithContextToken(hostWeb, contextToken, _PageRequest.Url.Authority);
            if (clientContext == null)
            {
                throw new Exception("Bad client context. ");
            }

            clientContext.Load<Web>(clientContext.Web);
            clientContext.ExecuteQuery();

            if (clientContext.Web == null)
            {
                throw new Exception("Bad clientContext.Web. ");
            }

            // Get all lists.
            ListCollection lists = clientContext.Web.Lists;
            clientContext.Load<ListCollection>(lists);
            clientContext.ExecuteQuery();

            if (lists == null)
            {
                throw new Exception("Web.Lists was null.");
            }
            return lists;
        }

        /// <summary>
        /// Navigate through the items under the given list. 
        /// </summary>
        /// <param name="clientContext">The client context.</param>
        /// <param name="list">The list we're looking at.</param>
        /// <param name="hostWeb">The host Web.</param>
        private void GetItemsInDocumentsList(ClientContext clientContext, List list, String hostWeb)
        {

            CamlQuery query = new CamlQuery();
            ListItemCollection itemCollection = list.GetItems(query);
            clientContext.Load<ListItemCollection>(itemCollection);
            clientContext.ExecuteQuery();

            if (itemCollection == null)
            {
                throw new Exception("Item collection was null. ");
            }

            foreach (ListItem item in itemCollection)
            {
                clientContext.Load<ListItem>(item);
                clientContext.ExecuteQuery();
                FileSystemObjectType objType = item.FileSystemObjectType;
                
                // Check to see if the item is a file. 
                if (objType == FileSystemObjectType.File)
                {
                    // At the topmost level, files don't belong to a folder, so set the folder name to "Documents".
                    String folderName = documentsList;
                    File file = item.File;
                    clientContext.Load<File>(file);
                    clientContext.ExecuteQuery();
                    ItemAttributes itemAttrs = GetFileInfo(clientContext, hostWeb, folderName, file);
                    _ItemsList.Add(itemAttrs);
                }

                // Check to see if the item is a folder.
                else if (objType == FileSystemObjectType.Folder)
                {
                    Folder folder = item.Folder;
                    clientContext.Load<Folder>(folder);
                    clientContext.ExecuteQuery();

                    if (folder == null)
                    {
                        throw new Exception("Null folder.");
                    }
                    GetSubfolders(clientContext, folder, hostWeb);
                }
                
                else
                {
                    throw new Exception("Item is not a folder and not a file.");
                }
            }
        }
 
    
        ///<summary>
        ///Get all of the files and folders in the specified folder.
        ///</summary>
        ///<param name="clientContext">The client context.</param>
        ///<param name="folder">The folder to read.</param>
        ///<param name="hostWeb">The host Web.</param>
        private void GetSubfolders(ClientContext clientContext, Folder folder, String hostWeb)
        {
            if (folder.ItemCount > 0)
            {
                // Are there files in this folder?
                if (folder.Files != null)
                {
                    GetFilesInFolder(clientContext, folder, hostWeb);
                }

                // Next, look for any subfolders in this folder.
               FolderCollection subfolders = folder.Folders;
                clientContext.Load<FolderCollection>(subfolders);
                clientContext.ExecuteQuery();
                if (subfolders.Count > 0)
                {
                    // There must have been some subfolders.
                    foreach (Folder subfolder in subfolders)
                    {
                        clientContext.Load<Folder>(subfolder);
                        clientContext.ExecuteQuery();
                        if (subfolder != null)
                        {
                            GetSubfolders(clientContext, subfolder, hostWeb);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get all files in the specified folder.
        /// </summary>
        /// <remarks>The information for each file is used to initialize the fields of an ItemAttributes object.
        /// </remarks>
        /// <param name="clientContext">The client context.</param>
        /// <param name="folder">The folder to scan.</param>
        /// <param name="hostWeb">The host Web.</param>
        private void GetFilesInFolder(ClientContext clientContext, Folder folder, String hostWeb)
        {
            FileCollection files = folder.Files;
            clientContext.Load<FileCollection>(files);
            clientContext.ExecuteQuery();
            if (files == null)
            {
                logInfo += "Empty file collection.";
                return;
            }
            foreach (File file in files)
            {
                if (file == null)
                {
                    throw new Exception("File was null.");
                }
                clientContext.Load<File>(file);
                clientContext.ExecuteQuery();

                ItemAttributes itemAttrs = new ItemAttributes();
                itemAttrs = GetFileInfo(clientContext, hostWeb, folder.Name, file);
                _ItemsList.Add(itemAttrs);
            }
        }

        /// <summary>
        /// Get information about a file.
        /// </summary>
        /// <param name="clientContext">The client context.</param>
        /// <param name="hostWeb">The host Web.</param>
        /// <param name="folderName">The name of the folder the file belongs to.</param>
        /// <param name="file">The file whose information is to be obtained. This parameter must not be null.</param>
        /// <returns>An ItemAttributes object that contains the file information.</returns>
        private ItemAttributes GetFileInfo(ClientContext clientContext, String hostWeb, String folderName, File file)
        {
            // Display the file name with a hyperlink.
            
            String hyperLink = "";
            // The logic here fixes the problem of overlap between the hostWeb and the ServerRelativeUrl 
            // for a given file on the OneDrive Pro site, causing a part of the URL that is formed to have 
            // a repeated section. The code in the if block removes the repeated part.
            // To distinguish between OneDrive Pro (where the problem exists) and  the Team site (where it 
            // doesn't exist), the code checks for a folder
            // named "Shared Documents". On the OneDrive Pro site there's a folder named "Documents".
            if (!(file.ServerRelativeUrl.Contains(sharedDocumentsList)))
            {
                int DocumentsIdx = file.ServerRelativeUrl.IndexOf(documentsList);
                String shortRelativeURL = file.ServerRelativeUrl.Substring(DocumentsIdx - 1);
                hyperLink = "<a href=\"" + hostWeb + shortRelativeURL + "\">";
            }
            else
            {
                hyperLink = "<a href=\"" + hostWeb + file.ServerRelativeUrl + "\">";
            }

            hyperLink += file.Name;
            hyperLink += "</a>";

            logInfo = "Host web: " + hostWeb + "<br/>";
            logInfo += "Server relative URL: " + file.ServerRelativeUrl + "<br/>";
            
            // Fill in the ItemAttributes struct members.
            ItemAttributes itemAttrs = new ItemAttributes();

            itemAttrs.FileName = hyperLink;
            itemAttrs.ServerRelativeUrl = file.ServerRelativeUrl;
            itemAttrs.Title = file.Title;

            // Get the User object, which is used to set the Author property on the file.
            User author = file.Author;
            clientContext.Load<User>(author);
            clientContext.ExecuteQuery();

            itemAttrs.Author = author.Email;
            itemAttrs.TimeLastModified = file.TimeLastModified;
            itemAttrs.ParentFolder = folderName;
            itemAttrs.LogInfo = logInfo;

            return itemAttrs;
        }
    
    
    
    }

     

    public class OneDriveEventData : EventArgs
    {
        List<getonedrivefiles.DataModel.OneDriveProDocs.ItemAttributes> _ItemList;
        public List<getonedrivefiles.DataModel.OneDriveProDocs.ItemAttributes> Items
        {
            get
            {
                return _ItemList;
            }

        }
        public OneDriveEventData(List<getonedrivefiles.DataModel.OneDriveProDocs.ItemAttributes> Items)
        {
            _ItemList = Items;
        }
    }

}