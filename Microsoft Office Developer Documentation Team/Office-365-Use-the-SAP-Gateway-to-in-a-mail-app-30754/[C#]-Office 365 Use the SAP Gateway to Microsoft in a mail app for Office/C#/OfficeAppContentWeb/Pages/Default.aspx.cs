using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

using OfficeAppContentWeb.Utilities;

namespace OfficeAppContentWeb.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AADAuthHelper.CurrentHostType = HttpContext.Current.Request.QueryString["_host_Info"];
                AADAuthHelper.StoreAuthorizationCodeFromRequest(this.Request);
            }
        }

        [WebMethod]
        public static string GetHostType()
        {
            return Utilities.AADAuthHelper.CurrentHostType;
        }

        [WebMethod]
        public static string GetAuthorizeUrl()
        {
            if (!AADAuthHelper.IsAuthorized)
            {
                return AADAuthHelper.AuthorizeUrl;
            }

            return string.Empty;
        }

        [WebMethod]
        public static string[][] GetDataHeaders()
        {
            var modelType = typeof(Utilities.DataModel);
            var fields = modelType.GetFields();
            var fieldsNames = fields.Select((field) => {
                return field.Name;
            }).ToArray();

            return new string[][] { fieldsNames };
        }

        [WebMethod]
        public static string[][] GetDataAsTable()
        {
            var accessToken = AADAuthHelper.EnsureValidAccessToken(HttpContext.Current);
            // Replace the value with your SAP OData endpoint stem and OData query parameters.
            return DataGetter.GetDataMatrix("https://stem_of_SAP_OData_endpoint/some_data_collection?$top=5&$skip=1", accessToken);
        }

        [WebMethod]
        public static string GetDataAsText()
        {
            var text = new StringBuilder();
            var table = GetDataAsTable();
            foreach (var row in table)
            {
                foreach (var item in row)
                {
                    text.AppendFormat("{0} ", item);
                }

                text.AppendLine();
            }

            return text.ToString();
        }
    }
}