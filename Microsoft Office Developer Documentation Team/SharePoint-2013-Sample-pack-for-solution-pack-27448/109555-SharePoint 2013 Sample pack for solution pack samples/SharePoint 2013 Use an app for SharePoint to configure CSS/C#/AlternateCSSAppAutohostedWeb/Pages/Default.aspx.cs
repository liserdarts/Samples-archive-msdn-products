using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.SharePoint.Client;

namespace AlternateCSSAppAutohostedWeb.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var contextToken = TokenHelper.GetContextTokenFromRequest(Page.Request);
                var hostWeb = Page.Request["SPHostUrl"];

                using (var clientContext = TokenHelper.GetClientContextWithContextToken(hostWeb, contextToken, Request.Url.Authority))
                {
                    Web hostWebObj = clientContext.Web;
                    var customActions = hostWebObj.UserCustomActions;

                    clientContext.Load(customActions);

                    clientContext.ExecuteQuery();

                    StringBuilder actionStatus = new StringBuilder();
                    actionStatus.AppendLine("The following custom actions have been applied to the host web: ");
                    foreach (var customAction in customActions)
                    {
                        actionStatus.AppendFormat("Name: {0} - Type: {1}\r\n", customAction.Name, customAction.Location);
                    }
                    actionStatus.AppendLine("Click \"Return to Site\" to see results");
                    StatusMessage.Text = actionStatus.ToString();
                }
            }
        }

        protected void ReturnToSite_Click(object sender, EventArgs e)
        {
            var hostWeb = Page.Request["SPHostUrl"];
            Response.Redirect(hostWeb, false);
        }
    }
}