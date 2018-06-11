using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace getonedrivefiles.ViewModel
{

    /// <summary>
    /// Items is an observable collection that adds Item objects to itself by constructing a 
    /// OneDriveProDocs object, asking the new object to get docs, and handling the event raised
    /// by OneDriveProDocs when a set of folders and files are ready.
    /// </summary>
    public sealed class Items : ObservableCollection<Item>
    {
        HttpRequest _PageRequest;
        getonedrivefiles.DataModel.OneDriveProDocs _oneDriveModel;
        
        /// <summary>
        /// The Items class should be constructed by a Web UI object that gets an HttpRequest when it is loaded.
        /// </summary>
        /// <param name="httpRequest"></param>
        public Items(HttpRequest httpRequest)
            : base()
        {
            _oneDriveModel = new DataModel.OneDriveProDocs(httpRequest);
            _oneDriveModel.NewOneDriveItems += oneDriveModel_NewOneDriveItems;
            _oneDriveModel.Run();
        }


        /// <summary>
        /// The Web UI object that constructed this class can also set the HttpRequest object
        /// after constructing Items. This gives the UI class the flexibility to construct Items in one place
        /// and initialize its functionality in a different method.
        /// </summary>
        public HttpRequest PageRequest
        {
            set
            {
                _PageRequest = value;
                if (_oneDriveModel != null)
                {
                    _oneDriveModel.PageRequest = _PageRequest;
                }
            }
        }

        /// <summary>
        /// This event callback method is invoked by OneDriveProDocs when a set of 
        /// files have been gotten from OneDrivePro.
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="EventData"></param>
        void oneDriveModel_NewOneDriveItems(object Sender, DataModel.OneDriveEventData EventData)
        {
            // Add each file as an Item object to the observable collection of Item objects.
            foreach (getonedrivefiles.DataModel.OneDriveProDocs.ItemAttributes itemAttr in EventData.Items)
            {
                Item item = new Item(itemAttr.FileName, itemAttr.ServerRelativeUrl);

                item.Title = itemAttr.Title; 
                item.Author = itemAttr.Author;
                item.TimeLastModified = itemAttr.TimeLastModified;
                item.ParentFolder = itemAttr.ParentFolder;
                item.LogInfo = itemAttr.LogInfo;
                 
                Add(item);

            }
        }

    }
}