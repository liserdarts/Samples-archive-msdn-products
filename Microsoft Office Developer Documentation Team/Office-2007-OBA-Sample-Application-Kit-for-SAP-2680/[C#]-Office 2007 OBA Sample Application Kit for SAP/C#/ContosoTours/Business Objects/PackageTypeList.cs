using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.SAPSK.ContosoTours
{
    [Serializable()]
    public class PackageTypeList
    {
        #region > Declarations
        private string _packageType;
        private decimal _packagePrice;
        private decimal _packageCost;
        #endregion

        public PackageTypeList()
        { }

        public PackageTypeList(
            string packageType, 
            decimal packagePrice,
            decimal packageCost)
        {
            _packageType = packageType;
            _packagePrice = packagePrice;
            _packageCost = packageCost;
        }

        #region > Properties
        public string PackageType
        {
            get 
            { 
                return _packageType; 
            }
            set 
            { 
                _packageType = value; 
            }
        }

        public decimal PackagePrice
        {
            get 
            { 
                return _packagePrice; 
            }
            set 
            { 
                _packagePrice = value; 
            }
        }

        public decimal PackageCost
        {
            get 
            { 
                return _packageCost; 
            }
            set 
            { 
                _packageCost = value; 
            }
        }
        #endregion
    }
}
