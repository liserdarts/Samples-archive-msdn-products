using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Diagnostics;

namespace ScheduleTracking_AddIn
{
    /// <summary>A form for requesting a new schedule report.</summary>
    /// <remarks>Collects the from and to dates, the categories on which to 
    /// report, whether to include daily subtotals in the report, and the 
    /// path of the file to which to save the report.</remarks>
    public partial class RequestSummaryForm : Form
    {
        private bool ignoreDateChangeEvents = true;
        private bool fromDateChanged = false;
        private bool toDateUpdated = false;
        private int prevCategoryIndex;

        public RequestSummaryForm()
        {
            InitializeComponent();
        }

        private void RequestSummaryForm_Load(object sender, EventArgs e)
        {
            // Initialize the from- and to- DateTimePicker initial dates.
            fromDateTimePicker.Value = toDateTimePicker.Value = DateTime.Now;

            // Initialize the category selection CheckedListBox.
            categoriesCheckedListBox.SelectionMode = SelectionMode.One;
            categoriesCheckedListBox.Items.AddRange(
                GetCategoryList().ToArray());
            prevCategoryIndex = categoriesCheckedListBox.SelectedIndex;

            ignoreDateChangeEvents = false;
        }

        /// <summary>Gets a list of category names for the current Outlook
        /// session.</summary>
        private List<string> GetCategoryList()
        {
            List<string> categories = new List<string>();
            Outlook.NameSpace session = Globals.ThisAddIn.Application.Session;
            foreach (Outlook.Category category in session.Categories)
            {
                string trimmed = category.Name.Trim();
                if (!categories.Contains(trimmed))
                {
                    categories.Add(trimmed);
                }
            }
            categories.Sort();
            return categories;
        }

        private void fromDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (ignoreDateChangeEvents) return;

            fromDateChanged = true;
        }

        private void fromDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            if (ignoreDateChangeEvents) return;

            if (fromDateChanged)
            {
                toDateTimePicker.Value = fromDateTimePicker.Value;
            }
        }

        private void toDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (ignoreDateChangeEvents || toDateUpdated) return;

            toDateUpdated = true;
        }

        private void categoriesCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = categoriesCheckedListBox.SelectedIndex;
            if (index != prevCategoryIndex)
            {
                categoriesCheckedListBox.SetItemChecked(
                    index, !categoriesCheckedListBox.GetItemChecked(index));
                prevCategoryIndex = index;
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            for(int i=0; i<categoriesCheckedListBox.Items.Count; i++)
            {
                categoriesCheckedListBox.SetItemChecked(i, true);
            }
        }

        private void selectNoneButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < categoriesCheckedListBox.Items.Count; i++)
            {
                categoriesCheckedListBox.SetItemChecked(i, false);
            }
        }

        private void generateReportButton_Click(object sender, EventArgs e)
        {
            if (fromDateTimePicker.Value.Date > toDateTimePicker.Value.Date)
            {
                MessageBox.Show("The to date is earlier than the from date " +
                    "for the report. No report generated.");
                return;
            }
            if (categoriesCheckedListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show(
                    "No categories selected. No report generated.");
                return;
            }

            // Get the path for the output file to create.
            DialogResult result = this.saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                // Change the reporting period to be from 12:00AM on the start
                // date to 12:00AM on the day after the end date.
                DateTime fromDate = fromDateTimePicker.Value.Date;
                DateTime toDate = toDateTimePicker.Value.Date.AddDays(1);

                // Create and save the time tracking report.
                ScheduleReportGenerator.GenerateReport(
                    fromDate, toDate, Categories,
                    dailySubtotalsCheckBox.Checked, saveFileDialog1.FileName);

                Cursor = DefaultCursor;

                result = MessageBox.Show("Report was generated successfully.");

                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>Gets the categories on which to report.</summary>
        /// <remarks>Returns the categories that are checked in the categories
        /// CheckedListBox.</remarks>
        private HashSet<string> Categories
        {
            get
            {
                HashSet<string> categories = new HashSet<string>();
                foreach (string category in
                    categoriesCheckedListBox.CheckedItems)
                {
                    categories.Add(category);
                }
                return categories;
            }
        }
    }
}
