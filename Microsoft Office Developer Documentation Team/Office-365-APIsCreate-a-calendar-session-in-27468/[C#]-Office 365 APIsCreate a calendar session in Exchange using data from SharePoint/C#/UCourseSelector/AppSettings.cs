using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCourseSelector
{
    public static class AppSettings
    {
        // ----- You *must* change this to use the app. Replace this with your own SharePoint list and SharePoint hostname
        //
        public static string SharePointSessionListUri = "https://<your-tenant-id-here>.sharepoint.com/_api/web/lists/getbytitle('Sessions')";
        // SharePoint host name example: https://contoso.sharepoint.com/
        public static string SharePointHostResourceId = "[enter your SharePoint hostname here]"; 
       //SharePointSessionListUri = "

        //------ Format of SharePoint List:
        //-- Title       : Single line of text	
        //-- Room        : Single line of text	
        //-- Start       : Date and Time	
        //-- End         : Date and Time	
        //-- Code        : Single line of text	
        //-- Description : Multiple lines of text
        //-----

        // ----  Do *not* change anything below.
        // -- Unless you want to use your own ClientId and Register the app in your own AAD tenancy, leave this as is.
        // --

        // --- Client app information
    }
}
