//*********************************************************
//
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;

namespace AddressBookWebRole
{
    public partial class Import : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdUpload_Click(object sender, EventArgs e)
        {
            // Check for file.
            if (this.Uploader.PostedFile.FileName == string.Empty)
            {
                this.lblInfo.Text = "No file specified.";
                this.lblInfo.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                try
                {
                    // Import csv data into a table.
                    this.ImportData(this.Uploader.PostedFile.InputStream);
                }
                catch (Exception err)
                {
                    this.lblInfo.Text = err.Message;
                    this.lblInfo.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        // Process csv file and import data into the application.
        private void ImportData(Stream csvStream)
        {
            this.lblInfo.Text = "";
                
            // Create data table to hold the data in memory.
            DataTable dt = new DataTable();

            // Parse the csv file and add the data to the data table.
            using (var csvFile = new TextFieldParser(csvStream))
            {
                csvFile.TextFieldType = FieldType.Delimited;
                csvFile.SetDelimiters(",");
                csvFile.HasFieldsEnclosedInQuotes = true;

                // Read the first row of data (which should contain column names).
                string[] fields = csvFile.ReadFields();

                // Add columns to data table, using first (header) row.
                DataColumn col = null;
                List<int> fieldIndices = new List<int>();

                if (!csvFile.EndOfData)
                {
                    // The FirstName field is required, since it's used for the partition key and row key.
                    if (!fields.Contains("FirstName"))
                    {
                        this.lblInfo.Text = "The .csv file must contain a FirstName field, named in the first row of data.";
                        this.lblInfo.ForeColor = System.Drawing.Color.Red;
                    }

                    // Create array of property names from ContactEntity.
                    List<string> propertyNames = new List<string>();
                    foreach (PropertyInfo info in typeof(ContactEntity).GetProperties())
                    {
                        propertyNames.Add(info.Name);
                    }

                    // Add a field to the data table if it matches one defined by ContactEntity.
                    for (int i = 0; i < fields.Length; i++)
                    {
                        if (propertyNames.Contains(fields[i]))
                        {
                            col = new DataColumn(fields[i]);
                            dt.Columns.Add(col);

                            // Track the field's index, so we know which ones to add data for below.
                            // This way any fields other than those named by ContactEntity will be ignored.  
                            fieldIndices.Add(i);
                        }
                    }
                }

                // Add data from each row to data table where it matches column name.
                DataRow row = null;
                while (!csvFile.EndOfData)
                {
                    // Get the current row from the csv file.
                    string[] currentRow = csvFile.ReadFields();

                    // Create a new row in the data table.
                    row = dt.NewRow();

                    // Copy the data from the csv to the data table.
                    foreach (var index in fieldIndices)
                    {
                        row[index] = currentRow[index];
                    }

                    // Add the row.
                    dt.Rows.Add(row);
                }
            }

            // Insert values from the data table into a Windows Azure table.
            try
            {
                DataLayer.BulkInsertContacts(dt);

                // Redirect to main page.
                Response.Redirect("Default.aspx");
            }
            catch (ApplicationException e)
            {
                this.lblInfo.Text = "Error importing csv file: " + e.Message;
                this.lblInfo.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}