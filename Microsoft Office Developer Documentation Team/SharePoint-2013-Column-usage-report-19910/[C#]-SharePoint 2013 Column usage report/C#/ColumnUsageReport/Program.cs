using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.SharePoint;

namespace ColumnUsageReport
{
    class Program
    {
        static void Main(string[] args)
        {
            Log("Opening web...");

            using (SPSite site = new SPSite("http://intranet.contoso.com/"))
            {
                Log("Building report...");
                Report report = new Report();
                report.BuildRowsAndFields(site.RootWeb);

                Log("Writing output...");
                using (StreamWriter streamWriter = new StreamWriter("ColumnUsageReport.csv"))
                {
                    report.WriteOutput(streamWriter);
                }
            }
            Log("Done.");

            if (Debugger.IsAttached)
                Debugger.Break();
        }

        static void Log(string message)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }

    }
    class Report
    {
        private Dictionary<string, List<string>> rowIdentitiesByName = new Dictionary<string, List<string>>();

        public Dictionary<Guid, ReportField> ReportFieldsById = new Dictionary<Guid, ReportField>();
        public List<KeyValuePair<string, string>> ReportColumnKeysAndTitles = new List<KeyValuePair<string, string>>();

        private ReportRow rootRow = null;
        public int MaxDepth = 0;

        public void BuildRowsAndFields(SPWeb rootWeb)
        {
            this.rootRow = ScanWeb(rootWeb);
        }

        private ReportRow ScanWeb(SPWeb web)
        {
            ReportRow webRow = new ReportRow(this, web);

            foreach (SPField field in web.Fields)
                this.GetFieldLabel(field);

            foreach (SPList list in web.Lists.Cast<SPList>().OrderBy(l => l.ID).OrderBy(l => l.Title))
            {
                foreach (SPField field in list.Fields)
                    this.GetFieldLabel(field);

                ReportRow listRow = new ReportRow(this, list);
                listRow.ParentRow = webRow;
            }

            foreach (SPWeb childWeb in web.Webs.OrderBy(w => w.ServerRelativeUrl).OrderBy(w => w.Title))
            {
                ReportRow childWebRow = this.ScanWeb(childWeb); // recurse
                childWebRow.ParentRow = webRow;
            }

            return webRow;
        }

        public string GetFieldLabel(SPField field)
        {
            ReportField reportField;
            if (!this.ReportFieldsById.TryGetValue(field.Id, out reportField))
            {
                reportField = new ReportField(field);
                this.ReportFieldsById.Add(field.Id, reportField);
            }
            return reportField.GetFieldLabelForReport(field);
        }

        public int GetRowIdentityNumber(string name, string identity)
        {
            // Calculate the identity number (used as suffix for PathName)
            List<string> identitiesForName;
            if (!this.rowIdentitiesByName.TryGetValue(name, out identitiesForName))
            {
                identitiesForName = new List<string>();
                this.rowIdentitiesByName.Add(name, identitiesForName);
            }
            if (!identitiesForName.Contains(identity))
                identitiesForName.Add(identity);
            return identitiesForName.IndexOf(identity);
        }

        public void WriteOutput(StreamWriter streamWriter)
        {
            this.MaxDepth = this.rootRow.GetMaxDepth();

            this.AddColumn("Kind");
            this.AddColumn("Path", "Path0");
            for (int i = 1; i < this.MaxDepth; ++i)
                this.AddColumn("", "Path" + i);

            this.AddColumn("URL");
            this.AddColumn("", Guid.NewGuid().ToString());

            foreach (ReportField field in this.ReportFieldsById.Values
              .Where(f => f.EverNotHidden)
              .OrderBy(f => f.Id).OrderBy(f => f.ColumnName))
            {
                this.AddColumn(field.ColumnName, field.Id.ToString());
            }

            // Write the header row
            Dictionary<string, string> headerRow = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> reportColumn in this.ReportColumnKeysAndTitles)
                headerRow[reportColumn.Key] = reportColumn.Value;
            this.WriteRowValues(streamWriter, headerRow, null);

            // Write the second header row
            headerRow.Clear();
            foreach (KeyValuePair<string, string> reportColumn in this.ReportColumnKeysAndTitles)
                headerRow[reportColumn.Key] = reportColumn.Key;
            this.WriteRowValues(streamWriter, headerRow, null);

            this.rootRow.WriteOutput(streamWriter, 0);
        }

        void AddColumn(string title, string key = null)
        {
            if (key == null)
                key = title;
            if (this.ReportColumnKeysAndTitles.Count(c => c.Key == key) > 0)
                throw new InvalidOperationException("A column already exists with key \"" + key + "\"");
            this.ReportColumnKeysAndTitles.Add(new KeyValuePair<string, string>(key, title));
        }

        public void WriteRowValues(StreamWriter streamWriter, Dictionary<string, string> rowValues,
            ReportRow row)
        {

            for (int i = 0; i < this.ReportColumnKeysAndTitles.Count; ++i)
            {
                string key = this.ReportColumnKeysAndTitles[i].Key;

                if (i > 0)
                    streamWriter.Write(",");

                string value = string.Empty;
                if (rowValues.TryGetValue(key, out value))
                {
                    if (value.Contains("\"") || value.Contains(","))
                    {
                        value = "\"" + value.Replace("\"", "\"\"") + "\"";
                    }
                }
                streamWriter.Write(value);
            }
            streamWriter.WriteLine();
        }

    }
    class ReportField
    {
        private readonly List<string> fieldIdentities = new List<string>();

        public readonly string ColumnName;
        public readonly Guid Id;

        public bool EverNotHidden = false;

        public ReportField(SPField originalField)
        {
            this.ColumnName = originalField.Title;
            this.Id = originalField.Id;
        }

        public string GetFieldLabelForReport(SPField field)
        {
            Debug.Assert(field.Id == this.Id);
            if (!field.Hidden)
                this.EverNotHidden = true;

            int identityNumber = this.GetFieldIdentityNumber(field);
            string fieldName = field.Title + ":" + identityNumber.ToString();

            return field.Hidden ? "(" + fieldName + ")" : fieldName;
        }

        private int GetFieldIdentityNumber(SPField field)
        {
            string fieldIdentity = GetFieldIdentity(field);
            if (!fieldIdentities.Contains(fieldIdentity))
                fieldIdentities.Add(fieldIdentity);
            return fieldIdentities.IndexOf(fieldIdentity) + 1;
        }

        static string GetFieldIdentity(SPField field)
        {
            XElement element = XElement.Load(new StringReader(field.SchemaXml));
            XElement normalizedElement = GetNormalizedXml(element);
            normalizedElement.SetAttributeValue("Version", null);
            return normalizedElement.ToString().ToLowerInvariant();
        }

        static XElement GetNormalizedXml(XElement element)
        {
            XElement normalizedElement = new XElement(element.Name);
            foreach (XAttribute attribute in element.Attributes().OrderBy(a => a.Value).OrderBy(a => a.Name.ToString()))
            {
                if (!string.IsNullOrEmpty(attribute.Value))
                    normalizedElement.Add(attribute);
            }
            foreach (XElement childElement in element.Elements().OrderBy(e => e.Value).OrderBy(e => e.Name.ToString()))
            {
                normalizedElement.Add(GetNormalizedXml(childElement)); // recurse
                if (!string.IsNullOrEmpty(childElement.Value))
                    normalizedElement.SetValue(childElement.Value.Trim());
            }
            return normalizedElement;
        }

        public List<KeyValuePair<int, string>> GetSchemaXmls()
        {
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
            for (int i = 0; i < this.fieldIdentities.Count; ++i)
                list.Add(new KeyValuePair<int, string>(i, this.fieldIdentities[i]));
            return list;
        }
    }
    class ReportRow
    {
        public readonly Report Report;
        private ReportRow parentRow = null;

        public readonly string Id;

        readonly SPWeb web = null;
        readonly SPList list = null;
        public readonly List<ReportRow> ChildRows = new List<ReportRow>();

        public string PathName;

        public SPList List
        {
            get { return this.list; }
        }
        public SPWeb Web
        {
            get { return this.web; }
        }
        public string Url
        {
            get { return this.List != null ? this.List.RootFolder.ServerRelativeUrl : this.Web.ServerRelativeUrl; }
        }

        public SPFieldCollection Fields
        {
            get
            {
                if (this.List != null) return this.List.Fields;
                return this.Web.Fields;
            }
        }

        public ReportRow ParentRow
        {
            get { return this.parentRow; }
            set
            {
                if (this.parentRow != null)
                    throw new InvalidOperationException("ParentRow has already been set");
                this.parentRow = value;
                if (this.parentRow != null)
                    this.parentRow.ChildRows.Add(this);
            }
        }

        public ReportRow(Report ownerReport, SPWeb web)
        {
            this.Report = ownerReport;
            this.web = web;
            this.Id = web.ID.ToString();
            this.PathName = web.Title;

        }
        public ReportRow(Report ownerReport, SPList list)
        {
            this.Report = ownerReport;
            this.list = list;
            this.Id = list.ID.ToString();
            this.PathName = list.Title;
        }

        public int GetMaxDepth()
        {
            int maxChildDepth = 0;
            foreach (ReportRow childRow in this.ChildRows)
            {
                maxChildDepth = Math.Max(maxChildDepth, childRow.GetMaxDepth());
            }
            return maxChildDepth + 1;
        }

        public void WriteOutput(StreamWriter streamWriter, int depth)
        {
            Dictionary<string, string> rowValues = new Dictionary<string, string>(1000);

            string kind = this.List != null ? "List" : "Web";
            rowValues["Kind"] = kind;

            rowValues["URL"] = this.Url;

            string rowIdentity = "";
            foreach (SPField field in this.Fields)
            {
                string label = this.Report.GetFieldLabel(field);
                rowValues[field.Id.ToString()] = label;
                rowIdentity += label;
            }

            int rowIdentityNumber = this.Report.GetRowIdentityNumber(this.PathName, rowIdentity);
            rowValues["Path" + depth] = rowIdentityNumber == 0 ? this.PathName : this.PathName + ":" + rowIdentityNumber;

            this.Report.WriteRowValues(streamWriter, rowValues, this);

            foreach (ReportRow childRow in this.ChildRows)
                childRow.WriteOutput(streamWriter, depth + 1);
        }
    }
}






