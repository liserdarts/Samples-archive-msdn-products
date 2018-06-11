using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Microsoft.SharePoint.Client;

namespace ProductBrowser
{
    public class ListItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // stop if no objects
            if (value == null)
                return null;

            // get the list item's field to display
            string fieldToDisplay = parameter as string;
            if (string.IsNullOrEmpty(fieldToDisplay))
                fieldToDisplay = "Title";

            ListItem sourceListItem = value as ListItem;
            if (sourceListItem == null)
                throw new ArgumentException("value");

            // if it's a lookup field, get that value
            if (fieldToDisplay.StartsWith("Lookup."))
            {
                string lookupFieldName = fieldToDisplay.Replace("Lookup.", string.Empty);
                FieldLookupValue lookupFieldValue = sourceListItem[lookupFieldName] as FieldLookupValue;
                if (lookupFieldValue == null)
                    throw new ArgumentException("Invalid lookup field.");
                else
                    return lookupFieldValue.LookupValue;
            }
            else
                return sourceListItem[fieldToDisplay];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
