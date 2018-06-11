using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Office.Tools.Ribbon;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace OutlookAddIn2
{
    public partial class OpportunitiesRibbon
    {
        private static readonly string dataImportFile = @"D:\ImportData\ContactData.csv";
        private static readonly string dataExportFile = @"D:\ExportedData\UpdatedContactData.csv";

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void importDataButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.OpportunitiesFolder != null)
            {
                ImportData();
            }
        }

        private static void ImportData()
        {
            List<ContactItemHelper> itemData = ContactItemHelper.LoadDataFromFile(dataImportFile);
            foreach (ContactItemHelper contactData in itemData)
            {
                // Create a new sales opportunity contact.
                Outlook.ContactItem newContact =
                    Globals.ThisAddIn.OpportunitiesFolder.Items.Add(
                    Constants.MessageClassOpportunities) as Outlook.ContactItem;

                // Set properties for the contact item.
                object[] errors = newContact.PropertyAccessor.SetProperties(
                    ContactItemHelper.PropertyReferences, contactData.PropertyValues);

                // Check any errors returned by the SetProperties method.
                DumpErrorsFromSetProperties(contactData, errors);

                newContact.Save();
            }
        }

        private void exportDataButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.OpportunitiesFolder != null)
            {
                ExportData();
            }
        }

        private static void ExportData()
        {
            // Get a list of the values from the items to export.
            List<object[]> exportItems = new List<object[]>();
            foreach (Outlook.ContactItem item in Globals.ThisAddIn.OpportunitiesFolder.Items)
            {
                // Ignore items that are not Sales Opportunity contact items.
                if (item.MessageClass != Constants.MessageClassOpportunities) continue;

                // Get values for specified properties from the item's PropertyAccessor.
                object[] values = item.PropertyAccessor.GetProperties(ContactItemHelper.PropertyReferences);

                try
                {
                    // Convert the encounter date value from a UTC time to a local time.
                    values[5] = item.PropertyAccessor.UTCToLocalTime((DateTime)values[5]);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Unable to convert encounter date from UTC to local time for contact {0}: {1}",
                        item.FullName, ex.Message);
                }

                // Add the values to the list of contact item data.
                exportItems.Add(values);
            }

            // Export the items to a data file.
            ContactItemHelper.ExportData(dataExportFile, exportItems);
        }

        private static void DumpErrorsFromSetProperties(
            ContactItemHelper contactData, object[] errors)
        {
            if (errors == null || errors.Length > 0)
            {
                Debug.WriteLine("Set data on {0} without any errors encountered.",
                    contactData);
                return;
            }

            for (int i = 0; i < errors.Length; i++)
            {
                if (errors[i] == null) continue;

                Debug.WriteLine("Error setting data on {0}, field {1}: {2}",
                        contactData, ContactItemHelper.FieldHeadings[i],
                        new COMException("", (int)errors[i]));
            }
        }
    }
}
