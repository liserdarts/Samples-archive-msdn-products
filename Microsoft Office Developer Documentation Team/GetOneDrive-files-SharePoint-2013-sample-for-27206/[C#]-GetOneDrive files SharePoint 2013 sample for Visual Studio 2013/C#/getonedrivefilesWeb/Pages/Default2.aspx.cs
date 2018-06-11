using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace getonedrivefiles.Pages
{
    public partial class Default2 : System.Web.UI.Page
    {
        getonedrivefiles.ViewModel.Items _Items;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _Items = new ViewModel.Items(Page.Request);

                GridView1.DataSource = _Items;
               // GridView1.AllowSorting = true;
                GridView1.HeaderStyle.CssClass = "ms-textSmall";
                GridView1.HeaderStyle.Font.Bold = false;

                BoundField fileCol = new BoundField();
                fileCol.DataField = "FileName";
                fileCol.HeaderText = "File Name";
                fileCol.HtmlEncode = false;
                GridView1.Columns.Add(fileCol);

                BoundField folderCol = new BoundField();
                folderCol.DataField = "ParentFolder";
                folderCol.HeaderText = "Parent Folder";
                GridView1.Columns.Add(folderCol);

                BoundField fileAuthor = new BoundField();
                fileAuthor.DataField = "Author";
                fileAuthor.HeaderText = "Modified By";
                GridView1.Columns.Add(fileAuthor);

                BoundField fileTimeModified = new BoundField();
                fileTimeModified.DataField = "TimeLastModified";
                fileTimeModified.HeaderText = "Last Modified";
                GridView1.Columns.Add(fileTimeModified);

                GridView1.DataBind();

                GridView2.DataSource = _Items;
                // GridView1.AllowSorting = true;
                GridView2.HeaderStyle.CssClass = "ms-textSmall";
                GridView2.HeaderStyle.Font.Bold = false;

                BoundField notificationArea = new BoundField();
                notificationArea.DataField = "LogInfo";
                notificationArea.HeaderText = "Log info";
                notificationArea.HtmlEncode = true;
                GridView2.Columns.Add(notificationArea);
                GridView2.DataBind();
            }
            catch (Exception ex)
            {
           //     TextBox1.Text = "Null reference exception in page load. " + "\r\n"
           //         + ex.Message + "\r\n" +
           //         ex.StackTrace + "\r\n";
            }
        }

    }
}