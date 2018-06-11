using System;
using Microsoft.Exchange.WebServices.Data;

namespace Exchange101
{
    // This sample is for demonstration purposes only. Before you run this sample, make sure that the code meets the coding requirements of your organization.
    class Ex15_ViewCustomExtendedProperties_CS
    {
        static ExchangeService service = Service.ConnectToService(UserDataFromConsole.GetUserData(), new TraceListener());

        static void Main(string[] args)
        {
            ViewCustomExtendedProperties(service);
            Console.WriteLine("Press or select Enter...");
            Console.Read();
        }
        /// <summary>
        /// Find and view a custom extended property on an email message. 
        /// </summary>
        /// <param name="service">An ExchangeService object with credentials and the EWS URL.</param>
        private static void ViewCustomExtendedProperties(ExchangeService service)
        {
            // Define a view of 10 items in the Drafts folder.
            ItemView view = new ItemView(10);

            // Get the GUID for the property set.
            Guid MyPropertySetId = new Guid("{C11FF724-AA03-4555-9952-8FA248A11C3E}");

            // Create a definition for the extended property.
            ExtendedPropertyDefinition extendedPropertyDefinition =
              new ExtendedPropertyDefinition(MyPropertySetId, "Expiration Date", MapiPropertyType.String);

            // Create a search filter that filters email based on the existence of the extended property.
            SearchFilter.Exists customPropertyExistsFilter = new SearchFilter.Exists(extendedPropertyDefinition);

            // Create a property set that includes the extended property definition.
            view.PropertySet =
             new PropertySet(BasePropertySet.IdOnly, ItemSchema.Subject, extendedPropertyDefinition);
            
            // Search the Drafts folder by using the defined view and search filter. This results in a FindItem operation call to EWS.
            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Drafts, customPropertyExistsFilter, view);
            
            // Search the email collection returned in the results for the extended property.
            foreach (Item item in findResults.Items)
            {
                Console.WriteLine(item.Subject);

                // Determine whether the item has the custom extended property set.
                if (item.ExtendedProperties.Count > 0)
                {
                    // Display the extended name and value of the extended property.
                    foreach (ExtendedProperty extendedProperty in item.ExtendedProperties)
                    {
                        Console.WriteLine(" Extended Property Name: " + extendedProperty.PropertyDefinition.Name);
                        Console.WriteLine(" Extended Property Value: " + extendedProperty.Value);
                    }
                }
                else
                {
                    Console.WriteLine(" This email message does not have the 'Expiration Date' extended property set on it");
                }
            }
        }
    }
}
