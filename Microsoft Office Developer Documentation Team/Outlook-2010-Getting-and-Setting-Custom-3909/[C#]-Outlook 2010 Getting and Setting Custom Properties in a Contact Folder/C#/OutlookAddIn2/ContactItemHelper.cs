using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OutlookAddIn2
{
    /// <summary>Contains contact information for a sales opportunity.</summary>
    /// <remarks>This is a helper class to aid in importing and exporting contact
    /// information to and from Outlook. This class is used by the OpportunitiesRibbon
    /// class ImportData and ExportData methods.</remarks>
    internal class ContactItemHelper
    {
        /// <summary>Array of references to properties by namespace.</summary>
        private static readonly string[] propertyReferences = new string[] {
                        Constants.firstNamePropRef,
                        Constants.lastNamePropRef,
                        Constants.companyNamePropRef,
                        Constants.primaryEmailPropRef,

                        Constants.customerIDProperRef,

                        Constants.encounterDatePropRef,
                        Constants.purchaseEstimatePropRef,
                        Constants.salesRepPropRef,
                        Constants.salesValuePropRef,
                        Constants.tradeShowPropRef};

        /// <summary>Array of property display names.</summary>
        private static readonly string[] propertyDisplayNames = new string[] {
                        Constants.firstNameDisplayName,
                        Constants.lastNameDisplayName,
                        Constants.companyNameDisplayName,
                        Constants.primaryEmailDisplayName,

                        Constants.customerIDDisplayName,

                        Constants.encounterDateDisplayName,
                        Constants.purchaseEstimateDisplayName,
                        Constants.salesRepDisplayName,
                        Constants.salesValuedisplayName,
                        Constants.tradeShowDisplayName};

        // Instance fields.

        private string lastName;
        private string firstName;
        private string company;
        private string email;
        private string customerId;
        private DateTime encounterDate;
        private double purchaseEstimate;
        private string salesRep;
        private int salesValue;
        private bool tradeShow;

        private object[] exportData;

        // Instance property accessors.

        public DateTime EncounterDate { get { return encounterDate; } }
        public double PurchaseExstimate { get { return purchaseEstimate; } }
        public string SalesRep { get { return salesRep; } }
        public int SalesValue { get { return salesValue; } }
        public bool TradeShow { get { return tradeShow; } }

        /// <summary>Returns a readable name for a sales contact object.</summary>
        public override string ToString()
        {
            return string.Format("Sales Contact[{0} {1}]", firstName, lastName);
        }

        /// <summary>Initializes a new ContactItemHelper object.</summary>
        /// <param name="lastName">The last name of the contact.</param>
        /// <param name="firstName">The first name of the contact.</param>
        /// <param name="company">The company name of the contact.</param>
        /// <param name="email">The primary email address of the contact.</param>
        /// <param name="customerId">The customer ID of the contact.</param>
        /// <param name="encounterDate">The encounter date of the contact, as a string.</param>
        /// <param name="purchaseEstimate">The purchase estimate for the contact, as a string.
        /// </param>
        /// <param name="salesRep">The sales rep for the contact.</param>
        /// <param name="salesValue">The sales value for the contact, from 1 through 3, as a
        /// string.</param>
        /// <param name="tradeShow">Whether the contact was met at a trade show, true or false,
        /// as a string.</param>
        /// <remarks>The encounterDate, purchaseEstimate, salesRep, salesValue, and tradeShow
        /// parameters correspond to custom properties for the sales form.</remarks>
        public ContactItemHelper(
            string lastName,
            string firstName,
            string company,
            string email,
            string customerId,
            string encounterDate,
            string purchaseEstimate,
            string salesRep,
            string salesValue,
            string tradeShow)
        {
            this.lastName = lastName.Trim();
            this.firstName = firstName.Trim();
            this.company = company.Trim();
            this.email = email.Trim();
            this.customerId = customerId.Trim();
            DateTime.TryParse(encounterDate, out this.encounterDate);
            this.encounterDate =
                Globals.ThisAddIn.OpportunitiesFolder.PropertyAccessor.LocalTimeToUTC(
                this.encounterDate);
            double.TryParse(purchaseEstimate, out this.purchaseEstimate);
            this.salesRep = salesRep.Trim();
            int.TryParse(salesValue, out this.salesValue);
            bool.TryParse(tradeShow, out this.tradeShow);
        }

        /// <summary>Gets the array of references to properties by their MAPI namespace.</summary>
        public static object PropertyReferences
        {
            get
            {
                return propertyReferences;
            }
        }

        /// <summary>Gets the array of property values.</summary>
        public object PropertyValues
        {
            get
            {
                return new object[] {
                        firstName,
                        lastName,
                        company,
                        email,

                        customerId,

                        encounterDate,
                        purchaseEstimate,
                        salesRep,
                        salesValue,
                        tradeShow
                    };
            }
        }

        private static readonly char[] comma = new char[] { ',' };

        /// <summary>Gets contact information imported from a file.</summary>
        /// <param name="importDataFileName">The complete file path for the file
        /// from which to load the data.</param>
        /// <returns>A list of ContactItemHelper objects generated from the file data.
        /// </returns>
        public static List<ContactItemHelper> LoadDataFromFile(string importDataFileName)
        {
            List<ContactItemHelper> items = new List<ContactItemHelper>();

            string[] lines = File.ReadAllLines(importDataFileName);
            List<string> headings = getFields(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                string line = (lines[i] ?? string.Empty).Trim();
                if (line.Length == 0) continue;

                List<string> fields = getFields(line);
                ContactItemHelper contactData = new ContactItemHelper(
                    getField(Constants.lastNameDisplayName, fields, headings),
                    getField(Constants.firstNameDisplayName, fields, headings),
                    getField(Constants.companyNameDisplayName, fields, headings),
                    getField(Constants.primaryEmailDisplayName, fields, headings),
                    getField(Constants.customerIDDisplayName,fields,headings),
                    getField(Constants.encounterDateDisplayName, fields, headings),
                    getField(Constants.purchaseEstimateDisplayName, fields, headings),
                    getField(Constants.salesRepDisplayName, fields, headings),
                    getField(Constants.salesValuedisplayName, fields, headings),
                    getField(Constants.tradeShowDisplayName, fields, headings));

                items.Add(contactData);
            }
            return items;
        }

        /// <summary>Gets an array containing the property names to load from or save
        /// to a data file.</summary>
        public static string[] FieldHeadings
        {
            get
            {
                return propertyDisplayNames;
            }
        }


        public static string GetFieldName(int i)
        {
            return propertyDisplayNames[i];
        }

        private static string getField(
            string headingName, List<string> fields, List<string> headings)
        {
            int index = headings.IndexOf(headingName);
            if (index < 0)
            {
                return null;
            }
            else
            {
                return fields[index];
            }
        }

        private static List<string> getFields(string line)
        {
            return line.Split(comma, StringSplitOptions.None).ToList();
        }

        internal static void ExportData(string dataExportFile, List<object[]> exportItems)
        {
            // Create the export directory if it does not already exist.
            string directory = Path.GetDirectoryName(dataExportFile);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            List<string> lines = new List<string>();

            // Create the heading line.
            StringBuilder sb = new StringBuilder();
            sb.Append(ContactItemHelper.FieldHeadings[0]);
            for (int i = 1; i < ContactItemHelper.FieldHeadings.Length; i++)
            {
                sb.Append("," + ContactItemHelper.FieldHeadings[i]);
            }
            lines.Add(sb.ToString());

            // Create a line for each contact in the folder.
            foreach (object[] itemValues in exportItems)
            {
                if (itemValues == null || itemValues.Length == 0) continue;

                sb = new StringBuilder();

                sb.AppendFormat("{0}", itemValues[0]);
                for (int i = 1; i < itemValues.Length; i++)
                {
                    sb.AppendFormat(",{0}", itemValues[i]);
                }

                lines.Add(sb.ToString());
            }

            // Write the data to the file.
            File.WriteAllLines(dataExportFile, lines.ToArray());
        }
    }
}
