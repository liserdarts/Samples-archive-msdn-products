using System;
using Microsoft.SharePoint;
using System.Xml;

namespace DuetSalesOrderSolution.Customizations
{
    /// <summary>
    /// Class that manages the list containing currency values.
    /// </summary>
    class CurrencyList
    {
        internal static string ListTitle = "CurrencyData";
        internal static string[] ColumnNames = { "Title", "Identifier" };

        /// <summary>
        /// Activate the artifacts needed for currency list.
        /// </summary>
        /// <param name="spWeb">Web site on which list is to be activated.</param>
        internal void Activate(SPWeb spWeb)
        {
            const string listDescription = "Currency data for sales order forms.";

            // Check for the list and create one if it doesn't exist.
            SPList list = spWeb.Lists.TryGetList(ListTitle);
            if (list == null)
            {
                // Create new list and add columns.
                Guid listId = spWeb.Lists.Add(ListTitle, listDescription, SPListTemplateType.GenericList);
                list = spWeb.Lists[listId];

                foreach(string columnName in ColumnNames)
                {
                    if (columnName != ColumnNames[0])
                    {
                        list.Fields.Add(columnName, SPFieldType.Text, true);
                    }
                }
                               
                // Set list properties for required scenario.
                list.OnQuickLaunch = false;
                list.EnableAttachments = false;
                list.EnableFolderCreation = false;
                list.ReadSecurity = 2;
                list.WriteSecurity = 4; // Allow only admins to update the list.
                list.Update();
            }

            // If there is no data in the list already, add data from resources file.
            if (list.ItemCount == 0)
            {
                string currencyData = DuetSalesOrderSolution.Properties.Resources.CurrencyData;
                
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(currencyData);

                XmlNode currenciesNode = xmlDocument.FirstChild.NextSibling;

                foreach (XmlNode node in currenciesNode.ChildNodes)
                {
                    SPListItem listItem = list.Items.Add();
                    foreach (string columnName in ColumnNames)
                    {
                        listItem[columnName] = node.Attributes[columnName].Value;
                    }
                    listItem.Update();
                }
            }
        }
    }
}
