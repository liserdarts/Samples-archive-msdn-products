using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//Need this to process the ATOM Feed
using System.Xml.Linq;
//Specifically for the XMLUrlResolver to pass credentials to the web service
using System.Xml;
//There be streams ahead
using System.IO;    

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //delcare all my namespaces up top so I can just reference a short variable name down below :)
        const string atomNameSpace = "http://www.w3.org/2005/Atom";
        const string xlsvcNameSpace = "http://schemas.microsoft.com/office/2008/07/excelservices/rest";
        //technically, I don't need these for this code, but I might as well cover my bases for future expansion ;-)
        const string dataserviceNameSpace = "http://schemas.microsoft.com/ado/2007/08/dataservice";
        const string metadataNameSpace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";
 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //as long as we have a server name and relative path to the spreadsheet, let's call the web service!
            if(txtSite.Text != string.Empty & txtSpreadsheet.Text != string.Empty)
                LoadRanges();
        }


        private void LoadRanges()
        {
            string relativeUri;
            XNamespace a = atomNameSpace;
            Stream s;

            //Clear the existing values from the list box
            listBox1.Items.Clear();

            //Build the relative URL for the Excel REST web service.
            relativeUri = "/_vti_bin/ExcelRest.aspx/" + txtSpreadsheet.Text + "/model/Ranges?$format=atom";

            //Pass the server portion of the URL and the relative URL down
            //and recieve a stream with the ATOM feed results
            s = GetAtomResultsStream(txtSite.Text, relativeUri);

            //Load the stream into an XDocument
            XDocument atomResults = XDocument.Load(s);

            //Query the XDocument for all title elements that are children of an entry element
            IEnumerable<XElement> ranges =
                from t in atomResults.Descendants(a + "title")
                where t.Parent.Name == a+"entry"
                select t;
                
            //Add all of the elements we've found to the listbox
            foreach (XElement r in ranges)
            {
                listBox1.Items.Add((string)r);
            }
                     
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //We want to get the value of a specific named range 
            string relativeUri;
            XNamespace a = atomNameSpace;
            XNamespace x = xlsvcNameSpace;
            string rangeName;

            if(listBox1.SelectedItems.Count < 1)
                return;

            rangeName = listBox1.SelectedItems[0].ToString();
            relativeUri = "/_vti_bin/ExcelRest.aspx/" + txtSpreadsheet.Text + "/model/Ranges('" + rangeName + "')?$format=atom";

            XDocument atomResults = XDocument.Load(GetAtomResultsStream(txtSite.Text, relativeUri));

            IEnumerable<string> rangeValue =
                from val in atomResults.Descendants(x + "fv")
                select (string)val;

            string values = rangeValue.Aggregate(new StringBuilder(), (sb, i) => sb.Append(i), sp => sp.ToString());
            lblValue.Text = values;
        }

            private Stream GetAtomResultsStream(string serverName, string relativeUri)
            {
                
                XNamespace a = atomNameSpace;

                //I'm using the XMLUrlResolver to pass credentials to the web service
                XmlUrlResolver resolver = new XmlUrlResolver();
                resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //Build the URI to pass the the resolver
                Uri baseUri = new Uri("http://" + serverName);
                Uri fullUri = resolver.ResolveUri(baseUri, relativeUri);

                //Let's display where we're going
                lblUrl.Text = fullUri.ToString();

                //Call the resolver and receive the ATOM feed as a result
                Stream s = (Stream)resolver.GetEntity(fullUri,null,typeof(Stream));

                return s;
            }

        }
    }
