using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;

namespace FilteredTaskSite.VisualWebPart1
{
    public partial class VisualWebPart1UserControl : UserControl
    {
       
        SPQuery query;
        SPWeb thisWeb;
        protected void Page_Load(object sender, EventArgs e)
        {
        thisWeb = SPContext.Current.Web;
        taskView.List = thisWeb.Lists["Project Tasks"];
        query = new SPQuery(taskView.List.DefaultView);
        query.ViewFields = "<FieldRef Name='Title' /><FieldRef Name='AssignedTo' /><FieldRef Name='DueDate' />";
        taskView.Query = query;
        }

        protected void filterDate_DateChanged(object sender, EventArgs e)
        {
            String camlQuery = "<Where><Leq><FieldRef Name='DueDate' />"
           + "<Value Type='DateTime'>"
           + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTimeControl1.SelectedDate)
           + "</Value></Leq></Where>";
            query.Query = camlQuery;

        }
    }

}
