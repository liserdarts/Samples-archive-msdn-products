using System;
using System.Windows.Forms;
using Microsoft.SAPSK.ContosoTours.DAL;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class SelectYearForm : Form
    {
        public SelectYearForm()
        {
            InitializeComponent();
        }

        public int SelectedYear
        {
            get 
            {
                try
                {
                    return Convert.ToInt32(comboBoxYear.SelectedItem.ToString());
                }
                catch(Exception ex)
                {
                    return 0;
                }
            }
        }

        private void SelectYearForm_Load(object sender, EventArgs e)
        {
            SAPEventAttendeeReadWrite attendee =
                new SAPEventAttendeeReadWrite(Config._dbConnectionName);

            SAPDataReaderEventAttendee readerAttendee =
                attendee.ReaderSelectAll();

            if(readerAttendee.DataReader.HasRows)
            {
                while (readerAttendee.DataReader.Read())
                {
                    int year = readerAttendee.Created.Year;
                    if(!comboBoxYear.Items.Contains(year))
                    {
                        comboBoxYear.Items.Add(year);
                    }                    
                }// while (readerAttendee.DataReader.Read()); 
            }
            else
            {
                comboBoxYear.Items.Add("No Data");
                comboBoxYear.SelectedIndex = 0;
            }
        }

        private void comboBoxYear_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Close();
        }
    }
}