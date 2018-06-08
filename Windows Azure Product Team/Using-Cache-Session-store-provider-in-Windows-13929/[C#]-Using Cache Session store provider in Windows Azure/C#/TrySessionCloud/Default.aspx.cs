using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TextBox1.Text))
            {
                Session.Add(Guid.NewGuid().ToString(), this.TextBox1.Text);
            }

            foreach (string key in Session.Keys)
            {
                TableCell cell = new TableCell();
                cell.Text = (string)Session[key];

                TableRow row = new TableRow();
                row.Cells.Add(cell);

                this.Table1.Rows.Add(row);
            }

        }
    }
}
