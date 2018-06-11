using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Office.Server.Utilities;
using Microsoft.SharePoint;


namespace ContentTypeReport
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
                using (StreamWriter streamWriter = new StreamWriter("ContentTypeReport.csv"))
                {
                    report.WriteOutput(streamWriter);
                }
#if false
        ReportField reportField = report.ReportFieldsById[new Guid("67df98f4-9dec-48ff-a553-29bece9c5bf4")];
        foreach (var pair in reportField.GetSchemaXmls()) {
          Log("-- " + pair.Key + " --------------------------------------------------------------------");
          Log(pair.Value);
        }
        Log("----------------------------------------------------------------------");
#endif
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
        private Dictionary<SPContentTypeId, ReportRow> rowsByContentTypeId = new Dictionary<SPContentTypeId, ReportRow>();
        private Dictionary<string, List<string>> rowIdentitiesByName = new Dictionary<string, List<string>>();

        public Dictionary<Guid, ReportField> ReportFieldsById = new Dictionary<Guid, ReportField>();
        public List<KeyValuePair<string, string>> ReportColumnKeysAndTitles = new List<KeyValuePair<string, string>>();

        private ReportRow rootRow = null;
        public int MaxDepth = 0;

        private bool reachedContentTypes = false;

        public void BuildRowsAndFields(SPWeb rootWeb)
        {
            this.rootRow = ScanWeb(rootWeb);

            this.CollectContentTypes(rootWeb);

            ReportRow contentTypeRoot = this.rootRow.ChildRows.First(row => row.IsContentType);
            this.PostProcessContentTypes(contentTypeRoot);
        }

        private ReportRow ScanWeb(SPWeb web)
        {
            ReportRow webRow = new ReportRow(this, web);

            foreach (SPField field in web.Fields)
                this.GetFieldLabel(field, contentType: null);

            foreach (SPList list in web.Lists.Cast<SPList>().OrderBy(l => l.ID).OrderBy(l => l.Title))
            {
                foreach (SPField field in list.Fields)
                    this.GetFieldLabel(field, contentType: null);

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

        public string GetFieldLabel(SPField field, SPContentType contentType)
        {
            ReportField reportField;
            if (!this.ReportFieldsById.TryGetValue(field.Id, out reportField))
            {
                reportField = new ReportField(field);
                this.ReportFieldsById.Add(field.Id, reportField);
            }
            return reportField.GetFieldLabelForReport(field, contentType);
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

        private void CollectContentTypes(SPWeb web)
        {
            this.CreateContentTypeRows(web.ContentTypes);

            foreach (SPList list in web.Lists)
            {
                this.CreateContentTypeRows(list.ContentTypes);

                this.MeasureUsage(list);
            }

            foreach (SPWeb childWeb in web.Webs)
                this.CollectContentTypes(childWeb); // recurse
        }

        private void CreateContentTypeRows(SPContentTypeCollection contentTypes)
        {
            foreach (SPContentType contentType in contentTypes)
            {
                ReportRow parentRow = null;

                if (!this.rowsByContentTypeId.TryGetValue(contentType.Id.Parent, out parentRow))
                {
                    if (!contentType.Id.Parent.Equals(contentType.Id))
                        throw new Exception("Cannot find parentRow for " + contentType.Name + " " + contentType.Id);

                    parentRow = this.rootRow;
                }

                ReportRow contentTypeRow = new ReportRow(this, contentType);
                contentTypeRow.ParentRow = parentRow;

                this.rowsByContentTypeId.Add(contentType.Id, contentTypeRow);
            }
        }

        private void PostProcessContentTypes(ReportRow contentTypeRow)
        {
            List<ReportRow> childRows = contentTypeRow.ChildRows;

            // Sort the child rows
            var sortedRows = childRows
                .OrderBy(row => row.Url) // if names are the same, sort by URL
                .OrderBy(row => row.ContentType.Name)
                .OrderBy(row => row.List == null) // show list CT's before web CT's
                .ToList();
            childRows.Clear();
            childRows.AddRange(sortedRows);

            contentTypeRow.PathName = contentTypeRow.ContentType.Name;

            foreach (ReportRow childRow in childRows)
                this.PostProcessContentTypes(childRow); // recurse
        }

        private void MeasureUsage(SPList list)
        {
            SPQuery query = new SPQuery();
            query.Query = @"
                <View Scope='RecursiveAll'>

                <ViewFields>
                  <FieldRef Name='FileDirRef'/>
                  <FieldRef Name='ContentTypeId'/>
                </ViewFields>

                </View>
                ";

            Console.WriteLine("Querying usage for list: " + list.RootFolder.Url);
            Debug.WriteLine("Querying usage for list: " + list.RootFolder.Url);

            ContentIterator contentIterator = new ContentIterator();
            contentIterator.ProcessListItems(list,
                delegate(SPListItem listItem)
                {
                    ReportRow row;
                    if (this.rowsByContentTypeId.TryGetValue(listItem.ContentTypeId, out row))
                    {
                        ++row.UsageCount;
                    }
                    else
                    {
                        Debug.WriteLine("Error for item " + listItem.Url
                            + ": Content Type Not Found: " + listItem.ContentTypeId.ToString());
                    }
                },
                delegate(SPListItem listItem, Exception e)
                {
                    Debug.WriteLine("Error for item " + listItem.Url
                        + ": " + e.Message);
                    return true;
                }
            );
        }

        public void WriteOutput(StreamWriter streamWriter)
        {
            this.MaxDepth = this.rootRow.GetMaxDepth();

            this.AddColumn("Kind");
            this.AddColumn("Path", "Path0");
            for (int i = 1; i < this.MaxDepth; ++i)
                this.AddColumn("", "Path" + i);

            this.AddColumn("URL");
            this.AddColumn("Usages");
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

            if (!this.reachedContentTypes && row != null && row.IsContentType)
            {
                this.reachedContentTypes = true;

                // Write a mini-header to delineate the Content Types section                
                Dictionary<string, string> miniHeader = new Dictionary<string, string>();
                miniHeader["Path0"] = "CONTENT TYPES:";
                streamWriter.WriteLine();
                this.WriteRowValues(streamWriter, miniHeader, null);
            }

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

        public string GetFieldLabelForReport(SPField field, SPContentType contentType)
        {
            Debug.Assert(field.Id == this.Id);
            if (!field.Hidden)
                this.EverNotHidden = true;

            int identityNumber = this.GetFieldIdentityNumber(field);
            string fieldName = field.Title + ":" + identityNumber.ToString();

            if (contentType != null)
            {
                SPFieldCollection sourceFields = contentType.ParentList != null ? contentType.ParentList.Fields : contentType.ParentWeb.Fields;
                SPField sourceField = sourceFields[field.Id];
                int sourceIdentityNumber = this.GetFieldIdentityNumber(sourceField);
                if (sourceIdentityNumber != identityNumber)
                    fieldName += "/" + sourceIdentityNumber;
            }

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

        public readonly SPContentType ContentType = null;

        public int UsageCount = 0;

        public bool IsContentType { get { return this.ContentType != null; } }

        public SPList List
        {
            get { return this.IsContentType ? this.ContentType.ParentList : this.list; }
        }
        public SPWeb Web
        {
            get { return this.IsContentType ? this.ContentType.ParentWeb : this.web; }
        }
        public string Url
        {
            get { return this.List != null ? this.List.RootFolder.ServerRelativeUrl : this.Web.ServerRelativeUrl; }
        }

        public SPFieldCollection Fields
        {
            get
            {
                if (this.IsContentType) return this.ContentType.Fields;
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
        public ReportRow(Report ownerReport, SPContentType contentType)
        {
            this.Report = ownerReport;
            this.ContentType = contentType;
            this.Id = contentType.Id.ToString();
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
            if (this.IsContentType)
                kind += "CT";
            rowValues["Kind"] = kind;

            rowValues["URL"] = this.Url;

            if (this.UsageCount > 0)
                rowValues["Usages"] = this.UsageCount.ToString();

            string rowIdentity = "";
            foreach (SPField field in this.Fields)
            {
                string label = this.Report.GetFieldLabel(field, this.ContentType);
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
