using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EditDataInSQLAzureWeb.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (btnNew.Text == "New")
            {
                btnNew.CausesValidation = true;
                btnNew.Text = "Save";
                panel1.Visible = true;
            }
            else
            {
                btnNew.CausesValidation = false;
                btnNew.Text = "New";

                using (SampleDBEntities entity = new SampleDBEntities())
                {
                    Customer customer = new Customer();
                    customer.CompanyName = txtCompanyName.Text.Trim();
                    customer.ContactName = txtContactName.Text.Trim();
                    customer.ContactTitle = txtContactTitle.Text.Trim();
                    customer.Address = txtAddress.Text.Trim();
                    entity.Customers.Add(customer);
                    entity.SaveChanges();
                }

                panel1.Visible = false;

                GridView1.DataBind();

                txtAddress.Text = "";
                txtCompanyName.Text = "";
                txtContactName.Text = "";
                txtContactTitle.Text = "";
            }
        }
    }
}