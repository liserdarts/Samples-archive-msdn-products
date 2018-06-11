using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.SAPSK.ContosoTours
{
    [Serializable()]
    public class PackageList
    {
        #region > declarations
        private int _pagkageID;
        private string _packageName;
        private string _packageDescription;
        private byte[] _packagePhoto;
        private bool _packageTag;

        #endregion

        public PackageList()
        { }

        #region > properties
        public int PackageID
        {
            get
            {
                return _pagkageID;
            }
            set
            {
                _pagkageID = value;
            }
        }

        public string PackageName
        {
            get
            {
                return _packageName;
            }
            set
            {
                _packageName = value;
            }
        }

        public string PackageDescription
        {
            get
            {
                return _packageDescription;
            }
            set
            {
                _packageDescription = value;
            }
        }

        public byte[] PackagePhoto
        {
            get
            {
                return _packagePhoto;
            }
            set
            {
                _packagePhoto = value;
            }
        }

        public bool PackageTag
        {
            get 
            { 
                return _packageTag; 
            }
            set 
            { 
                _packageTag = value; 
            }
        }
        #endregion
    }
}
