using System;

namespace SAPBridgeMaster
{
    public class Customer
    {
        private string _customerID;
        private string _customerName;
        private string _street;
        private string _pobox;
        private string _postalCode;
        private string _city;
        private string _country;
        private string _phoneNumber;
        
        public Customer()
        {
            //empty
        }

        public Customer(
            string customerID,
            string customerName,
            string street,
            string pobox,
            string postalCode,
            string city,
            string country,
            string phoneNumber)
        {
            _customerID = customerID;
            _customerName = customerName;
            _street = street;
            _pobox = pobox;
            _postalCode = postalCode;
            _city = city;
            _country = country;
            _phoneNumber = phoneNumber;
        }

        public string CustomerID
        {
            get
            {
                return _customerID;
            }
            set
            {
                _customerID = value;
            }
        }

        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
            }
        }

        public string Street
        {
            get
            {
                return _street;
            }
            set
            {
                _street = value;
            }
        }

        public string POBox
        {
            get
            {
                return _pobox;
            }
            set
            {
                _pobox = value;
            }
        }

        public string PostalCode
        {
            get
            {
                return _postalCode;
            }
            set
            {
                _postalCode = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }

        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
            }
        }
    }
}
