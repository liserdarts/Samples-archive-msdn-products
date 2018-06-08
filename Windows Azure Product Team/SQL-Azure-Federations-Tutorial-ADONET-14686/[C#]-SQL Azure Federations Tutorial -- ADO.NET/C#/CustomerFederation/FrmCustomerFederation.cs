using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CustomerFederation
{
    public partial class frmCustomerFederation : Form
    {
        private bool bFilteringOn = false;
        const string CONST_FEDERATION_NAME = "CustomerFederation";
        const string CONST_DISTRIBUTION_NAME = "cid";

        public frmCustomerFederation()
        {
            InitializeComponent();
        }

        private void frmCustomerFederation_Load(object sender, EventArgs e)
        {
            dataGridViewCustomer.DataSource = bindingSourceCustomer;
            dataGridViewCustomerAddress.DataSource = bindingSourceCustomerAddress;
            dataGridViewFedMemberColumns.DataSource = bindingSourceFedMemberColumns;
            dataGridViewDBs.DataSource = bindingSourceDBs;

            numericUpDownFederatedKey.Maximum = long.MaxValue;
            numericUpDownFederatedKey.Minimum = long.MinValue;
            numericUpDownFederatedKey.Value = 100;
        }

        #region Form controls
        private void cbFilterOn_CheckedChanged(object sender, EventArgs e)
        {
            bFilteringOn = cbFilterOn.Checked;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtResults.Text = "";
        }
        #endregion

        #region Metadata tab controls
        //====================================================================================================//
        //=== btnRoot_Click                                                                                ===//
        //===                                                                                              ===//
        //=== This function retrives the federation root metadata, including all of the federation members ===//
        //=== and their federation ids, federation member ids, range lows and range highs.                 ===//
        //===                                                                                              ===//
        //=== Both the Federation name and the distribution name are hardcoded: CONST_FEDERATION_NAME,     ===//
        //=== CONST_DISTRIBUTION_NAME.                                                                     ===//
        //===                                                                                              ===//
        //=== To retrive the federation root metadata:                                                     ===//
        //===    1. Connect to the SQL Azure server, the initial catalog is AdventureWorks2.               ===//
        //===    2. Use the USE statement to route the connection to the federation root.                  ===//
        //===    3. Run the T-SQL statement, strSQL.                                                       ===//
        //====================================================================================================//
        private void btnRoot_Click(object sender, EventArgs e)
        {
            StringBuilder sbResults = new StringBuilder();
            DataSet data = null;
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["CustomerFederation"].ConnectionString);

            string strSQL = @"SELECT f.name, fmc.federation_id, fmc.member_id, fmc.range_low, fmc.range_high " +
                                                            "FROM sys.federations f " +
                                                            "JOIN sys.federation_member_distributions fmc " +
                                                            "ON f.federation_id=fmc.federation_id " +
                                                            "ORDER BY fmc.federation_id, fmc.range_low, fmc.range_high";
            string strTableName = "federation_member_distributions";

            try
            {
                //The initial catalog is the AdventureWorks2 database.
                using (SqlConnection connection = new SqlConnection(csb.ToString()))
                {
                    connection.Open();
                    sbResults.AppendFormat("-- Open {0}\r\n\r\n", csb.ToString());

                    data = new DataSet();
                    data.Locale = System.Globalization.CultureInfo.InvariantCulture;

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        // Route the connection to federation root
                        command.CommandText = "USE FEDERATION ROOT WITH RESET";
                        command.ExecuteNonQuery();
                        sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", command.CommandText);
                    }

                    //Retrieve federation root metadata, and populate the data to the DataGridView control
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strSQL, connection);
                    sqlDataAdapter.Fill(data, strTableName);
                    sbResults.AppendFormat("{0}\r\nGO\r\n", strSQL);
                }

                //Display trace information.
                txtResults.Text = String.Format("----- {0} : ----- Federation root metadata-----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;

                bindingSourceFedMemberColumns.DataSource = null;
                bindingSourceFedMemberColumns.DataSource = data;
                bindingSourceFedMemberColumns.DataMember = strTableName;
            }
            catch (Exception ex)
            {
                txtResults.Text = String.Format("----- {0} : ----- Federation root metadata -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                txtResults.Text = String.Format("----- {0} : ----- Federation root metadata -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), 
                    String.Format("-- Error:\r\n{0}\r\n{1}\r\n{2}", ex.Source, ex.Message, ex.StackTrace)) + txtResults.Text;
            }
        }

        //====================================================================================================//
        //=== btnMember_Click                                                                              ===//
        //===                                                                                              ===//
        //=== This function retrives the metadata of the federation member with the federation key         ===//
        //=== specified in the Federated Key textbox, i.e. cid=100. The metadata includes federation id,   ===//
        //=== federation member id and the range of the federation member.                                 ===//
        //===                                                                                              ===//
        //=== To retrive the federation member metadata:                                                   ===//
        //===    1. Connect to the SQL Azure server, the initial catalog is AdventureWorks2.               ===//
        //===    2. Use the USE statement to route the connection to the federation member.                ===//
        //===    3. Run the T-SQL statement, strSQL.                                                       ===//
        //====================================================================================================//
        private void btnMember_Click(object sender, EventArgs e)
        {
            //The values is from the Federated Key textbox from the UI.
            long lFederationKey = Convert.ToInt64(numericUpDownFederatedKey.Value);

            StringBuilder sbResults = new StringBuilder();
            DataSet data = null;
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["CustomerFederation"].ConnectionString);

            string strTableName = "federation_member_distributions";
            string strSQL = @"SELECT f.name, fmc.federation_id, fmc.member_id, fmc.range_low, fmc.range_high " +
                            "FROM sys.federations f " +
                            "JOIN sys.federation_member_distributions fmc " +
                            "ON f.federation_id=fmc.federation_id " +
                            "ORDER BY fmc.federation_id, fmc.range_low, fmc.range_high";

            try
            {
                //The initial catalog is the AdventureWorks2 database
                using (SqlConnection connection = new SqlConnection(csb.ToString()))
                {
                    connection.Open();
                    sbResults.AppendFormat("-- Open {0}\r\n\r\n", csb.ToString());

                    data = new DataSet();
                    data.Locale = System.Globalization.CultureInfo.InvariantCulture;

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        // Route the connection to the federation member with specific cid.
                        command.CommandText = string.Format("USE FEDERATION {0}({1}={2}) WITH RESET, FILTERING={3}",
                                                            CONST_FEDERATION_NAME,      //"CustomerFederation"
                                                            CONST_DISTRIBUTION_NAME,    //"cid"
                                                            lFederationKey,
                                                            (bFilteringOn ? "ON" : "OFF"));
                        command.ExecuteNonQuery();
                        sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", command.CommandText);
                    }

                    //Retrieve the federation member metadata, and populate the data to the DataGridView control
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strSQL, connection);
                    sqlDataAdapter.Fill(data, strTableName);
                    sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", strSQL);
                }

                //Display trace information
                txtResults.Text = String.Format("----- {0} : ----- Federation member metadata -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;

                bindingSourceFedMemberColumns.DataSource = null;
                bindingSourceFedMemberColumns.DataSource = data;
                bindingSourceFedMemberColumns.DataMember = strTableName;
            }
            catch (Exception ex)
            {
                txtResults.Text = String.Format("----- {0} : ----- Federation member metadata -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                txtResults.Text = String.Format("----- {0} : ----- Federation member metadata -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), 
                    String.Format("-- Error:\r\n{0}\r\n{1}\r\n{2}", ex.Source, ex.Message, ex.StackTrace)) + txtResults.Text;
            }
        }

        //====================================================================================================//
        //=== btnGetRowCounts_Click                                                                        ===//
        //===                                                                                              ===//
        //=== This function performs a fan-out operaion to get row counts for the federation members.      ===//
        //=== Fanout querying enables querying across federation members.  This function retrieves the row ===//
        //=== counts for the federation member with the federation key specific on the UI, and the         ===//
        //=== subsequent federation members.                                                               ===//
        //===                                                                                              ===//
        //=== To retrive the row counts for the federations:                                               ===//
        //===    1. Connect to the SQL Azure server, the initial catalog is AdventureWorks2.               ===//
        //===    2. For each of the Federation members starting with the one with the federation key       ===//
        //===       specified:                                                                             ===//
        //===       a. Use the USE statement to route the connection to the federation member.             ===//
        //===       b. Run the T-SQL statement, strSQL.                                                    ===//
        //====================================================================================================//
        private void btnGetRowCounts_Click(object sender, EventArgs e)
        {
            long lFederationKey = Convert.ToInt64(numericUpDownFederatedKey.Value);
            int iFederatedMemberCount = 0;

            StringBuilder sbResults = new StringBuilder();
            DataSet data = null;
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["CustomerFederation"].ConnectionString);

            string strTableName = "sys.tables";
            string strSQL = @"SELECT '[' + s.name + '].[' + t.name + ']' [name],fmc.range_low,fmc.range_high,p.row_count " +
                                                "FROM sys.federation_member_distributions fmc " +
                                                "JOIN sys.federated_table_columns ftc ON fmc.distribution_name=ftc.distribution_name " +
                                                "JOIN sys.tables t ON t.object_id=ftc.object_id  " +
                                                "JOIN sys.schemas s ON t.schema_id=s.schema_id  " +
                                                "JOIN sys.dm_db_partition_stats p ON t.object_id=p.object_id  " +
                                                "WHERE p.index_id=1 ORDER BY s.name, t.name";

            try
            {
                //the intial catalog is the AdventureWorks2 database
                using (SqlConnection connection = new SqlConnection(csb.ToString()))
                {
                    DataSet dataTemp = null;
                    connection.Open();
                    sbResults.AppendFormat("-- Open {0}\r\n\r\n", csb.ToString());

                    data = new DataSet();
                    data.Locale = System.Globalization.CultureInfo.InvariantCulture;

                    do
                    {
                        dataTemp = new DataSet();
                        dataTemp.Locale = System.Globalization.CultureInfo.InvariantCulture;

                        using (SqlCommand command = connection.CreateCommand())
                        {
                            // Connection Routing to the specified Federated Member
                            command.CommandText = String.Format("USE FEDERATION {0}({1}={2}) WITH RESET, FILTERING={3}",
                                              CONST_FEDERATION_NAME,        //"CustomerFederation"
                                              CONST_DISTRIBUTION_NAME,      //"cid"
                                              lFederationKey,
                                              (bFilteringOn ? "ON" : "OFF"));

                            sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", command.CommandText);
                            command.ExecuteNonQuery();

                            // Get Customer DataSet
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strSQL, connection);
                            sqlDataAdapter.Fill(dataTemp, strTableName);
                            sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", strSQL);

                            // Copy the DataSet for local display
                            data.Merge(dataTemp);

                            // Get next range high value
                            command.CommandText = "SELECT CAST(range_high as bigint) FROM sys.federation_member_distributions";
                            sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", command.CommandText);
                            var key = command.ExecuteScalar();
                            if (key != System.DBNull.Value)
                            {
                                lFederationKey = Convert.ToInt64(key);
                                sbResults.AppendFormat("-- The next range_high is {0}.\r\n\r\n", lFederationKey);
                            }
                            else
                            {
                                sbResults.Append("-- Done with fan-out\r\n\r\n");
                                lFederationKey = long.MaxValue;
                            }
                        }
                        iFederatedMemberCount++;
                    } while (lFederationKey < long.MaxValue);
                }

                sbResults.AppendFormat("-- The number of fan-out federated members is {0}.\r\n\r\n", iFederatedMemberCount);
                txtResults.Text = String.Format("----- {0} : ----- Get row counts -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;

                bindingSourceFedMemberColumns.DataSource = null;
                bindingSourceFedMemberColumns.DataSource = data;
                bindingSourceFedMemberColumns.DataMember = strTableName;
            }
            catch (Exception ex)
            {
                txtResults.Text = String.Format("----- {0} : ----- Get row counts -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                txtResults.Text = String.Format("----- {0} : ----- Get row counts -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), 
                    String.Format("-- Error:\r\n{0}\r\n{1}\r\n{2}", ex.Source, ex.Message, ex.StackTrace)) + txtResults.Text;
            }
        }

        //====================================================================================================//
        //=== btnSplit_Click                                                                               ===//
        //===                                                                                              ===//
        //=== This function performs the federation split operation.  You must first specify a federation  ===//
        //=== key value from the UI.                                                                       ===//
        //===                                                                                              ===//
        //=== To perform the split operation:                                                              ===//
        //===    1. Connect to the SQL Azure server, the initial catalog is AdventureWorks2.               ===//
        //===    2. Use the USE statement to route the connection to the federation root                   ===//
        //===    3. Use the ALTER statement to split the federation                                        ===// 
        //====================================================================================================//

        private void btnSplit_Click(object sender, EventArgs e)
        {
            long lFederationKey = Convert.ToInt64(numericUpDownFederatedKey.Value);

            if (DialogResult.OK == MessageBox.Show(
                        String.Format("Verify you want to SPLIT on {0}({1}) by clicking OK.",
                        CONST_FEDERATION_NAME,
                        lFederationKey ),
                        "About to execute SPLIT", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                StringBuilder sbResults = new StringBuilder();
                SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(
                    ConfigurationManager.ConnectionStrings["CustomerFederation"].ConnectionString);

                try
                {
                    //The initial catalog is the AdventureWorks2 database
                    using (SqlConnection connection = new SqlConnection(csb.ToString()))
                    {
                        connection.Open();
                        sbResults.AppendFormat("-- Open {0}\r\n\r\n", csb.ToString());

                        using (SqlCommand command = connection.CreateCommand())
                        {
                            // Connect to federation root
                            command.CommandText = "USE FEDERATION ROOT WITH RESET";
                            command.ExecuteNonQuery();
                            sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", command.CommandText);

                            // Perform the split operation
                            command.CommandText = String.Format("ALTER FEDERATION {0} SPLIT AT ({1}={2})",
                                    CONST_FEDERATION_NAME,
                                    CONST_DISTRIBUTION_NAME,
                                    lFederationKey);
                            command.CommandTimeout = 60 * 4;
                            command.ExecuteNonQuery();
                            sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", command.CommandText);
                        }
                    }

                    txtResults.Text = String.Format("----- {0} : ----- Split  -----\r\n{1}\r\n", 
                        DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                    btnRoot_Click(null, null);
                }
                catch (Exception ex)
                {
                    txtResults.Text = String.Format("----- {0} : ----- Split -----\r\n{1}\r\n", 
                        DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                    txtResults.Text = String.Format("----- {0} : ----- Split -----\r\n{1}\r\n", 
                        DateTime.Now.ToString(), 
                        String.Format("-- Error:\r\n{0}\r\n{1}\r\n{2}", ex.Source, ex.Message, ex.StackTrace)) + txtResults.Text;
                }
            }
        }

        //====================================================================================================//
        //=== btnDropDB_Click                                                                              ===//
        //===                                                                                              ===//
        //=== This function drops a Federation, include the federation members.                            ===//
        //===                                                                                              ===//
        //=== To drop a Federation:                                                                        ===//
        //===    1. Connect to the SQL Azure server, the initial catalog is AdventureWorks2.               ===//
        //===    2. Drop the federation.                                                                    ===// 
        //====================================================================================================//
        private void btnDropDB_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Are you sure you want to drop the federation?",
                "About to drop the federation!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                StringBuilder sbResults = new StringBuilder();
                SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(
                    ConfigurationManager.ConnectionStrings["CustomerFederation"].ConnectionString);

                try
                {
                    string strInitialCatalog = csb.InitialCatalog;

                    //Connect to the master database
                    using (SqlConnection connection = new SqlConnection(csb.ToString()))
                    {
                        connection.Open();
                        sbResults.AppendFormat("-- Open {0}\r\n\r\n", csb.ToString());

                        using (SqlCommand command = connection.CreateCommand())
                        {
                            // Drop the Federation
                            command.CommandText = String.Format("DROP FEDERATION [{0}]", CONST_FEDERATION_NAME);
                            command.ExecuteNonQuery();
                            sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", command.CommandText);

                        }
                    }

                    txtResults.Text = String.Format("----- {0} : ----- Drop federation  -----\r\n{1}\r\n",
                        DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                }
                catch (Exception ex)
                {
                    txtResults.Text = String.Format("-- Error DropDB:\r\n{0}\r\n{1}\r\n{2}",
                        ex.Source, ex.Message, ex.StackTrace) + txtResults.Text;
                    txtResults.Text = String.Format("----- {0} : ----- Drop federation  -----\r\n{1}\r\n",
                        DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                }
            }
        }
        #endregion

        #region Customers tab controls
        //====================================================================================================//
        //=== btnCustomerQueryMember_Click                                                                 ===//
        //===                                                                                              ===//
        //=== This function retrieves the data for the federation member with the federation key specified ===//
        //=== on the UI. If you check Filtering On, it will return the row with the Federation key value.  ===//
        //=== This function retrieves the data for both Customer and CustomerAddress tables.               ===//
        //===                                                                                              ===//
        //=== To query one federation member data:                                                         ===//
        //===    1. Connection to the SQL Azure server, the initial catalog is AdventureWorks2.            ===//
        //===    2. Use the USE statement to route the connection to the federation member.                ===//
        //===    3. Retrieve the Customer table data.                                                      ===// 
        //===    4. Retrieve the CustomerAddress table data.                                               ===// 
        //====================================================================================================//
        private void btnCustomerQueryMember_Click(object sender, EventArgs e)
        {
            long lFederationKey = Convert.ToInt64(numericUpDownFederatedKey.Value);

            StringBuilder sbResults = new StringBuilder();
            DataSet dataCustomer = null, dataCustomerAddress = null;
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["CustomerFederation"].ConnectionString);

            string strTableName_customer = "customer";
            string strSQL_customer = @"SELECT * FROM customer";
            string strTableName_customeraddress = "customeraddress";
            string strSQL_customeraddress = @"SELECT * FROM customeraddress";

            try
            {
                //The initial catalog is the AdventureWorks database.
                using (SqlConnection connection = new SqlConnection(csb.ToString()))
                {
                    connection.Open();
                    sbResults.AppendFormat("-- Open {0}\r\n\r\n", csb.ToString());

                    dataCustomer = new DataSet();
                    dataCustomer.Locale = System.Globalization.CultureInfo.InvariantCulture;

                    dataCustomerAddress = new DataSet();
                    dataCustomerAddress.Locale = System.Globalization.CultureInfo.InvariantCulture;

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        // Connect to the federation member
                        command.CommandText = String.Format("USE FEDERATION {0}({1}={2}) WITH RESET, FILTERING={3}",
                                              CONST_FEDERATION_NAME,
                                              CONST_DISTRIBUTION_NAME,
                                              lFederationKey,
                                              (bFilteringOn ? "ON" : "OFF"));
                        command.ExecuteNonQuery();
                        sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", command.CommandText);
                    }

                    // Retrieve the rows, and populate the data to the DataGridView control
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strSQL_customer, connection);
                    sqlDataAdapter.Fill(dataCustomer, strTableName_customer);
                    sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", strSQL_customer);


                    // Retrieve the rows, and populate the data to the DataGridView control
                    sqlDataAdapter = new SqlDataAdapter(strSQL_customeraddress, connection);
                    sqlDataAdapter.Fill(dataCustomerAddress, strTableName_customeraddress);
                    sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", strSQL_customeraddress);

                }

                txtResults.Text = String.Format("----- {0} : ----- Query federation member data -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;

                bindingSourceCustomer.DataSource = null;
                bindingSourceCustomer.DataSource = dataCustomer;
                bindingSourceCustomer.DataMember = strTableName_customer;

                bindingSourceCustomerAddress.DataSource = null;
                bindingSourceCustomerAddress.DataSource = dataCustomerAddress;
                bindingSourceCustomerAddress.DataMember = strTableName_customeraddress;
            }catch (Exception ex)
            {
                txtResults.Text = String.Format("----- {0} : ----- Query federation member data -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                txtResults.Text = String.Format("----- {0} : ----- Query federation member data -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), 
                    String.Format("-- Error:\r\n{0}\r\n{1}\r\n{2}", ex.Source, ex.Message, ex.StackTrace)) + txtResults.Text;
            }
        }

        //====================================================================================================//
        //=== btnCustomerFanOut_Click                                                                      ===//
        //===                                                                                              ===//
        //=== This function performs a fan-out query to retrieve data from the customer and the            ===//
        //=== CustomerAddress table.                                                                       ===//
        //===                                                                                              ===//
        //=== To do a fan-out query to retrieve data from multiple federation members.                     ===//
        //===    1. Connect to the SQL Azure server, the initial catalog is AdventureWorks2.               ===//
        //===    2. For each of the Federation members starting with the one with the federation key       ===//
        //===       specified:                                                                             ===//
        //===       a. Use the USE statement to route the connection to the federation member.             ===//
        //===       b. Retrieve the Customer table data.                                                   ===// 
        //===       c. Retrieve the CustomerAddress table data.                                            ===// 
        //====================================================================================================//
        private void btnCustomerFanOut_Click(object sender, EventArgs e)
        {
            long lFederationKey = Convert.ToInt64(numericUpDownFederatedKey.Value);
            int iFederatedMemberCount = 0;

            StringBuilder sbResults = new StringBuilder();
            DataSet dataCustomer = null, dataCustomerAddress=null, dataTemp = null;
            SqlDataAdapter sqlDataAdapter = null;
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["CustomerFederation"].ConnectionString);

            string strTableName_Customer = "customer";
            string strSQL_Customer = @"SELECT * FROM customer";
            string strTableName_CustomerAddress = "customeraddress";
            string strSQL_CustomerAddress = @"SELECT * FROM customeraddress";

            try
            {
                //The initial catalog is the AdventureWorks2 database
                using (SqlConnection connection = new SqlConnection(csb.ToString()))
                {
                    connection.Open();
                    sbResults.AppendFormat("-- Open {0}\r\n\r\n", csb.ToString());

                    dataCustomer = new DataSet();
                    dataCustomer.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    dataCustomerAddress = new DataSet();
                    dataCustomerAddress.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    dataTemp = new DataSet();
                    dataTemp.Locale = System.Globalization.CultureInfo.InvariantCulture;

                    do
                    {
                        //dataTemp.Clear();

                        using (SqlCommand command = connection.CreateCommand())
                        {
                            // Connection Routing to the specified Federated Member
                            command.CommandText = String.Format("USE FEDERATION {0}({1}={2}) WITH RESET, FILTERING={3}",
                                              CONST_FEDERATION_NAME,
                                              CONST_DISTRIBUTION_NAME,
                                              lFederationKey,
                                              (bFilteringOn ? "ON" : "OFF"));

                            sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", command.CommandText);
                            command.ExecuteNonQuery();

                            // Get Customer DataSet
                            sqlDataAdapter = new SqlDataAdapter(strSQL_Customer, connection);
                            sqlDataAdapter.Fill(dataTemp, strTableName_Customer);
                            sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", strSQL_Customer);

                            // Copy the DataSet for local display
                            dataCustomer.Merge(dataTemp);

                            // Get CustomerAddress DataSet
                            sqlDataAdapter = new SqlDataAdapter(strSQL_CustomerAddress, connection);
                            sqlDataAdapter.Fill(dataTemp, strTableName_CustomerAddress);
                            sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", strSQL_CustomerAddress);

                            // Copy the DataSet for local display
                            dataCustomerAddress.Merge(dataTemp);

                            // Get next range high value
                            command.CommandText = "SELECT CAST(range_high as bigint) FROM sys.federation_member_distributions";
                            sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", command.CommandText);
                            var key = command.ExecuteScalar();
                            if (key != System.DBNull.Value)
                            {
                                lFederationKey = Convert.ToInt64(key);
                                sbResults.AppendFormat("-- The next range_high is {0}.\r\n\r\n", lFederationKey);
                            }
                            else
                            {
                                sbResults.Append("-- Done with fan-out.\r\n\r\n");
                                lFederationKey = long.MaxValue;
                            }
                        }
                        iFederatedMemberCount++;
                    } while (lFederationKey < long.MaxValue);
                }

                sbResults.AppendFormat("-- The number of fan-out federated members is {0}.\r\n\r\n", iFederatedMemberCount);
                txtResults.Text = String.Format("----- {0} : ----- Fan-out federation members data -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;

                bindingSourceCustomer.DataSource = null;
                bindingSourceCustomer.DataSource = dataCustomer;
                bindingSourceCustomer.DataMember = strTableName_Customer;

                bindingSourceCustomerAddress.DataSource = null;
                bindingSourceCustomerAddress.DataSource = dataCustomerAddress;
                bindingSourceCustomerAddress.DataMember = strTableName_CustomerAddress;
            }
            catch (Exception ex)
            {
                txtResults.Text = String.Format("----- {0} : ----- Fan-out federation members data -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                txtResults.Text = String.Format("----- {0} : ----- Fan-out federation members data -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), 
                    String.Format("-- Error:\r\n{0}\r\n{1}\r\n{2}", ex.Source, ex.Message, ex.StackTrace)) + txtResults.Text;
            }
        }
        #endregion

        #region Clean-up tab controls
        //====================================================================================================//
        //=== btnListDbs_Click                                                                             ===//
        //===                                                                                              ===//
        //=== This function lists all the databases, including federation root and federation members.     ===//
        //====================================================================================================//
        private void btnListDbs_Click(object sender, EventArgs e)
        {
            StringBuilder sbResults = new StringBuilder();
            DataSet data = null;
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["CustomerFederation"].ConnectionString);
            string strSQL ="SELECT name FROM sys.databases WHERE name<>'master'";
            string strTableName = "dbnames";
            try
            {
                // Connect to the master database
                csb.InitialCatalog = "master";
                using (SqlConnection connection = new SqlConnection(csb.ToString()))
                {
                    connection.Open();
                    sbResults.AppendFormat("-- Open {0}\r\n\r\n", csb.ToString());

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        data = new DataSet();
                        data.Locale = System.Globalization.CultureInfo.InvariantCulture;

                        //Populate DataGridView...
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strSQL, connection);
                        sqlDataAdapter.Fill(data, strTableName );
                        sbResults.AppendFormat("{0}'\r\nGO\r\n", strSQL);

                        bindingSourceDBs.DataSource = null;
                        bindingSourceDBs.DataSource = data;
                        bindingSourceDBs.DataMember = strTableName ;
                    }
                }

                txtResults.Text = String.Format("----- {0} : ----- List federated databases -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
            }
            catch (Exception ex)
            {
                txtResults.Text = String.Format("----- {0} : ----- List federated databases -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                txtResults.Text = String.Format("----- {0} : ----- List federated databases -----\r\n{1}\r\n", 
                    DateTime.Now.ToString(), 
                    String.Format("-- Error:\r\n{0}\r\n{1}\r\n{2}", ex.Source, ex.Message, ex.StackTrace)) + txtResults.Text;
            }
        }

        //====================================================================================================//
        //=== btnDropDbs_Click                                                                             ===//
        //===                                                                                              ===//
        //=== This function deletes the selected databases.                                                ===//
        //====================================================================================================//
        private void btnDropDBs_Click(object sender, EventArgs e)
        {
            string strDBName;
            StringBuilder sbResults = new StringBuilder();

            if (dataGridViewDBs.SelectedRows.Count > 0)
            {
                if (DialogResult.OK == MessageBox.Show("Are you sure you want to drop these database(s)?",
                        "About to drop database(s)!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    if (dataGridViewDBs.SelectedRows.Count > 0)
                    {
                        DataGridViewSelectedRowCollection rows = dataGridViewDBs.SelectedRows;
                        for (int i = 0; i < rows.Count; i++)
                        {
                            strDBName = Convert.ToString(rows[i].Cells[0].Value);

                            try
                            {
                                // Connect to master database
                                SqlConnectionStringBuilder csbMaster = new SqlConnectionStringBuilder(
                                    ConfigurationManager.ConnectionStrings["CustomerFederation"].ConnectionString);
                                csbMaster.InitialCatalog = "master";
                                using (SqlConnection connection = new SqlConnection(csbMaster.ToString()))
                                {
                                    connection.Open();
                                    sbResults.AppendFormat("-- Open {0}\r\n\r\n", csbMaster.ToString());

                                    using (SqlCommand command = connection.CreateCommand())
                                    {
                                        command.CommandText = String.Format("DROP DATABASE [{0}]", strDBName);
                                        command.ExecuteNonQuery();
                                        sbResults.AppendFormat("{0}\r\nGO\r\n\r\n", command.CommandText);
                                    }
                                }

                                txtResults.Text = String.Format("----- {0} : ----- Drop databases -----\r\n{1}\r\n", 
                                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                            }
                            catch (Exception ex)
                            {
                                txtResults.Text = String.Format("-- Error DropDB:\r\n{0}\r\n{1}\r\n{2}", ex.Source, ex.Message, ex.StackTrace) + txtResults.Text;
                                txtResults.Text = String.Format("----- {0} : ----- Drop databases -----\r\n{1}\r\n", 
                                    DateTime.Now.ToString(), sbResults.ToString()) + txtResults.Text;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Select rows in the datagridview!", "No databases highlighted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion
    }
}
