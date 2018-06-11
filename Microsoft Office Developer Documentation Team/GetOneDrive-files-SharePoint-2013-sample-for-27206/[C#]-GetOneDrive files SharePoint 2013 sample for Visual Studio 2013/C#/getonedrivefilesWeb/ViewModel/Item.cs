using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace getonedrivefiles.ViewModel
{
    /// <summary>
    /// An Item instance contains information about a file on oneDrive Pro.
    /// </summary>
    public class Item
    {
        private string _FileName;
        private string _ServerRelativeUrl;
        private string _Title;
        private string _Author;
        private DateTime _TimeLastModified;
        private string _ParentFolder;
        private string _LogInfo;
        
        public Item(string FileName, string ServerRelativeUrl)
        {
            this._FileName = FileName;
            this._ServerRelativeUrl = ServerRelativeUrl;
        }

        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        public string ServerRelativeUrl
        {
            get { return _ServerRelativeUrl; }
            set { _ServerRelativeUrl = value; }
        }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        public DateTime TimeLastModified
        {
            get { return _TimeLastModified; }
            set { _TimeLastModified = value; }
        }

        public string ParentFolder
        {
            get { return _ParentFolder; }
            set { _ParentFolder = value; }
        }

        public string LogInfo
        {
            get { return _LogInfo; }
            set { _LogInfo = value; }
        }
        
    }
}