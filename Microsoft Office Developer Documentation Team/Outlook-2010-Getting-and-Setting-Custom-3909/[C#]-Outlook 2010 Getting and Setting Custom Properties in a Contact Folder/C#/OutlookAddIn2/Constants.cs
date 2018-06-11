using System;
using System.Collections.Generic;

namespace OutlookAddIn2
{
    /// <summary>Contains constants for property names, references to properties
    /// by their namespaces, and property data type information.</summary>
    internal static class Constants
    {
        /// <summary>A reference to the message class property by its namespace.</summary>
        public static readonly string MessageClassID =
            "http://schemas.microsoft.com/mapi/proptag/0x36E5001E";

        /// <summary>The message class identifier, IPM.Contact.Sales Opportunity.</summary>
        public static readonly string MessageClassOpportunities =
            "IPM.Contact.Sales Opportunity";

        /// <summary>A reference to the custom form display name property by its
        /// namespace.</summary>
        public static readonly string MessageClassDisplayNameID =
            "http://schemas.microsoft.com/mapi/proptag/0x36E6001E";

        /// <summary>The display name for the custom form.</summary>
        public static readonly string SalesFormDisplayName = "Sales Opportunity Form";

        #region Built-in properties

        // Display names for built-in properties:

        public static readonly string lastNameDisplayName = "Last Name";
        public static readonly string firstNameDisplayName = "First Name";
        public static readonly string companyNameDisplayName = "Company";
        public static readonly string customerIDDisplayName = "CustomerID";
        public static readonly string primaryEmailDisplayName = "email";

        // References to built-in properties by their namespaces:

        /// <summary>A reference to the last name property by its namespace.</summary>
        public static readonly string lastNamePropRef =
            "http://schemas.microsoft.com/mapi/proptag/0x3A11001F";

        /// <summary>A reference to the first name property by its namespace.</summary>
        public static readonly string firstNamePropRef =
            "http://schemas.microsoft.com/mapi/proptag/0x3A06001F";

        /// <summary>A reference to the company name property by its namespace.</summary>
        public static readonly string companyNamePropRef =
            "http://schemas.microsoft.com/mapi/proptag/0x3A16001F";

        /// <summary>A reference to the customer ID property by its namespace.</summary>
        public static readonly string customerIDProperRef =
            "http://schemas.microsoft.com/mapi/proptag/0x3A4A001F";

        /// <summary>A reference to the primary email property by its namespace.</summary>
        public static readonly string primaryEmailPropRef = "urn:schemas:contacts:email1";

        #endregion

        #region Custom properties

        // Display names for custom properties:

        public static readonly string encounterDateDisplayName = "Encounter Date";
        public static readonly string purchaseEstimateDisplayName = "Purchase Estimate";
        public static readonly string salesRepDisplayName = "Sales Rep";
        public static readonly string salesValuedisplayName = "Sales Value";
        public static readonly string tradeShowDisplayName = "Trade Show";

        // References to custom properties by their namespaces:

        private static readonly string baseUri =
            "http://schemas.microsoft.com/mapi/string";
        private static readonly string contactItemsGuid =
            "{00020329-0000-0000-C000-000000000046}";

        /// <summary>A reference to the encounter date property by its namespace.
        /// </summary>
        public static readonly string encounterDatePropRef =
            string.Format("{0}/{1}/{2}", baseUri, contactItemsGuid,
            encounterDateDisplayName);

        /// <summary>A reference to the purchase estimate propert by its namespacey.
        /// </summary>
        public static readonly string purchaseEstimatePropRef =
            string.Format("{0}/{1}/{2}", baseUri, contactItemsGuid,
            purchaseEstimateDisplayName);

        /// <summary>A reference to the sales rep property by its namespace.</summary>
        public static readonly string salesRepPropRef =
            string.Format("{0}/{1}/{2}", baseUri, contactItemsGuid, salesRepDisplayName);

        /// <summary>A reference to the sales value property by its namespace.</summary>
        public static readonly string salesValuePropRef =
            string.Format("{0}/{1}/{2}", baseUri, contactItemsGuid, salesValuedisplayName);

        /// <summary>A reference to the trade show property by its namespace.</summary>
        public static readonly string tradeShowPropRef =
            string.Format("{0}/{1}/{2}", baseUri, contactItemsGuid, tradeShowDisplayName);

        #endregion

        private static readonly Dictionary<string, Type> dataTypes;

        static Constants()
        {
            dataTypes = new Dictionary<string, Type>();
            dataTypes[lastNameDisplayName] = typeof(string);
            dataTypes[firstNameDisplayName] = typeof(string);
            dataTypes[companyNameDisplayName] = typeof(string);
            dataTypes[primaryEmailDisplayName] = typeof(string);
            dataTypes[customerIDDisplayName] = typeof(string);
            dataTypes[customerIDProperRef] = typeof(string);
            dataTypes[encounterDateDisplayName] = typeof(DateTime);
            dataTypes[purchaseEstimateDisplayName] = typeof(double);
            dataTypes[salesRepDisplayName] = typeof(string);
            dataTypes[salesValuedisplayName] = typeof(int);
            dataTypes[tradeShowDisplayName] = typeof(bool);
        }

        /// <summary>Returns the managed data type for a given property.</summary>
        /// <param name="fieldName">The property name.</param>
        /// <returns>The data type of the property, or typeof(string) if the
        /// property name is not recognized.</returns>
        internal static Type GetDataType(string fieldName)
        {
            if (dataTypes.ContainsKey(fieldName))
            {
                return dataTypes[fieldName];
            }
            else
            {
                return typeof(string);
            }
        }
    }
}
