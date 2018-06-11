using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Office.Server.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Taxonomy;

namespace AutomatedTagging
{

    #region Helper Classes

    class ExcelRow
    {
        public string ListItemUrl;

        // InternalName, Guid
        public List<KeyValuePair<string, Guid>> Pairs = new List<KeyValuePair<string, Guid>>();

        public bool Processed = false;
        int CsvLineNumber;

        public ExcelRow(string listItemUrl, int csvLineNumber)
        {
            this.ListItemUrl = listItemUrl;
            this.CsvLineNumber = csvLineNumber;
        }
    }

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Log("\r\nAutomatedTagging (Server OM): Connecting...\r\n");

            using (SPSite site = new SPSite("http://localhost/"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists["Documents"];

#if false
          ExportTemplate(list);
#else
                    ImportTemplate(list);
#endif
                }
            }

            if (Debugger.IsAttached)
                Debugger.Break();
        }

        static void ExportTemplate(SPList list)
        {
            Log("Writing AutomatedTagging.csv...");

            Dictionary<SPContentTypeId, List<Guid>> taxonomyFieldsByContentTypeId = new Dictionary<SPContentTypeId, List<Guid>>();

            using (StreamWriter streamWriter = new StreamWriter("AutomatedTagging.csv"))
            {
                streamWriter.WriteLine("Filename,,Field,Tags,Field,Tags,...");
                ContentIterator contentIterator = new ContentIterator();
                contentIterator.ProcessListItems(list,
                    delegate(SPListItem listItem)
                    {
                        List<Guid> fieldGuids;
                        if (!taxonomyFieldsByContentTypeId.TryGetValue(listItem.ContentTypeId, out fieldGuids))
                        {
                            fieldGuids = new List<Guid>();

                            foreach (SPField field in listItem.ContentType.Fields)
                            {
                                // Alternatively, the code could also support taxonomy fields that can take multiple values here.
                                if (field.TypeAsString == "TaxonomyFieldType")
                                    fieldGuids.Add(field.Id);
                            }

                            taxonomyFieldsByContentTypeId.Add(listItem.ContentTypeId, fieldGuids);
                        }

                        if (fieldGuids.Count > 0)
                        {
                            streamWriter.Write(listItem.Url + ",");

                            foreach (Guid fieldGuid in fieldGuids)
                            {
                                SPField field = listItem.Fields[fieldGuid];
                                streamWriter.Write("," + field.InternalName + ",");
                            }
                            streamWriter.WriteLine();
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
        }

        class TermSetLookup
        {
            Dictionary<string, Guid> TermsByName = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);

            public TermSetLookup(TermSet termSet)
            {
                foreach (Term term in termSet.GetAllTerms())
                    TermsByName.Add(term.Name, term.Id);
            }

            public Guid GetTermId(string label)
            {
                return TermsByName[label];
            }
        }

        static void ImportTemplate(SPList list)
        {
            TaxonomySession taxonomySession = new TaxonomySession(list.ParentWeb.Site, updateCache: true);

            // STEP 1: Load the TermSet objects for the SPField objects.
            Dictionary<string, TermSetLookup> termSetsByInternalName = new Dictionary<string, TermSetLookup>(StringComparer.OrdinalIgnoreCase);

            foreach (SPField field in list.Fields)
            {
                // Alternatively, the code could also support taxonomy fields that can take multiple values here.
                if (field.TypeAsString != "TaxonomyFieldType")
                    continue;

                TaxonomyField taxonomyField = field as TaxonomyField;
                if (taxonomyField == null)
                    continue;

                TermStore termStore = taxonomySession.TermStores[taxonomyField.SspId];
                TermSet termSet = termStore.GetTermSet(taxonomyField.TermSetId);
                termSetsByInternalName.Add(field.InternalName, new TermSetLookup(termSet));
            }

            // STEP 2: Load the Excel file.
            Dictionary<string, ExcelRow> excelRowsByUrl = new Dictionary<string, ExcelRow>(StringComparer.OrdinalIgnoreCase);

            // Parse the input file.
            Log("Reading AutomatedTagging.csv...");

            using (StreamReader streamReader = new StreamReader("AutomatedTagging.csv"))
            {
                if (!streamReader.ReadLine().Contains("Filename"))
                    throw new InvalidOperationException("Invalid file format; header is missing");

                int lineNumber = 1;
                for (; ; )
                {
                    string line = streamReader.ReadLine();
                    ++lineNumber;
                    if (line == null)
                        break;

                    string[] csvValues = ParseCsvLine(line);
                    if (csvValues == null)
                        throw new InvalidOperationException("[line " + lineNumber + "]: Syntax error");

                    if (csvValues.Length < 1)
                        continue;

                    ExcelRow excelRow = new ExcelRow(csvValues[0], lineNumber);
                    for (int i = 2; i + 1 < csvValues.Length; )
                    {
                        string key = csvValues[i++].Trim();
                        if (key == "") break;

                        string value = csvValues[i++].Trim();
                        if (value == "") break;

                        SPField field = list.Fields.GetFieldByInternalName(key);
                        TermSetLookup termSetLookup = termSetsByInternalName[key];
                        Guid termId = termSetLookup.GetTermId(value);

                        excelRow.Pairs.Add(new KeyValuePair<string, Guid>(field.InternalName, termId));
                    }

                    excelRowsByUrl.Add(excelRow.ListItemUrl, excelRow);
                }
            }

            // STEP 3: Update the list items.
            ContentIterator contentIterator = new ContentIterator();
            contentIterator.ProcessListItems(list,
                delegate(SPListItem listItem)
                {
                    ExcelRow excelRow;
                    if (!excelRowsByUrl.TryGetValue(listItem.Url, out excelRow))
                        return;

                    excelRow.Processed = true;

                    bool updated = false;
                    foreach (KeyValuePair<string, Guid> pair in excelRow.Pairs)
                    {
                        TaxonomyField taxonomyField = (TaxonomyField)listItem.Fields.GetFieldByInternalName(pair.Key);

                        TaxonomyFieldValue taxonomyFieldValue = new TaxonomyFieldValue(taxonomyField);
                        taxonomyFieldValue.TermGuid = pair.Value.ToString();

                        TaxonomyFieldValue oldValue = listItem[taxonomyField.Id] as TaxonomyFieldValue;
                        if (oldValue == null || oldValue.TermGuid != taxonomyFieldValue.TermGuid)
                        {
                            taxonomyField.SetFieldValue(listItem, taxonomyFieldValue);
                            updated = true;
                        }
                    }

                    if (updated)
                    {
                        Log("Updating item: " + listItem.Url);
                        listItem.Update();
                    }
                },
                delegate(SPListItem listItem, Exception e)
                {
                    Log("Error processing item " + listItem.Url
                        + ": " + e.Message);
                    return true;
                }
            );

            // Were any items missed?
            Log("");
            List<ExcelRow> missedRows = excelRowsByUrl.Values.Where(row => !row.Processed).ToList();
            if (missedRows.Count > 0)
            {
                Log("Failed to match these rows");
                foreach (ExcelRow row in missedRows)
                    Log("  " + row.ListItemUrl);
            }

        }

        #region Utilities

        static Regex csvParseRegex = new Regex(@"
^
(
   (
     (""
        (?<value>  ( [^""] | """" )*  )
     "") |
     (?<value> [^"",][^,]* ) |
     (?<value>)
   )
   ,
)*
   (
     (""
        (?<value>  ( [^""] | """" )*  )
     "") |
     (?<value> [^"",][^,]* ) |
     (?<value>)
   )
$", RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        static string[] ParseCsvLine(string csvLine)
        {
            Match match = csvParseRegex.Match(csvLine);
            if (match.Success)
            {
                return (from Capture capture in match.Groups["value"].Captures
                        select capture.Value.Replace("\"\"", "\""))
                    .ToArray();
            }
            return null;
        }

        static void Log(string message)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }

        #endregion
    }
}

