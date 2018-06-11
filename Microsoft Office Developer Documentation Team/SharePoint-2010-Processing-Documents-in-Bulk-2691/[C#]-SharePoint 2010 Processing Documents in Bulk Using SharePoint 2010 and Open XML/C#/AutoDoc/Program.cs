using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Packaging;
using System.Xml;
using System.Xml.Linq;
using System.Threading;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.CustomXmlDataProperties;
using DocumentFormat.OpenXml.Wordprocessing;

using Microsoft.BusinessData.MetadataModel;
using Microsoft.BusinessData.MetadataModel.Collections;
using Microsoft.BusinessData.Runtime;
using Microsoft.Office.BusinessData.MetadataModel;
using Microsoft.Office.BusinessApplications.Runtime;
using Microsoft.Office.Word.Server.Conversions;

using Microsoft.SharePoint;

using System.Printing;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace AutoDoc
{
    class Program
    {
        readonly static string WordAutomationServicesName = "Word Automation Services";

        [System.STAThreadAttribute()]
        static void Main(string[] args)
        {
            string siteURL = args[0];
            string docLib = args[1];

            using (SPSite spSite = new SPSite(siteURL))
            {
                SPDocumentLibrary library = spSite.RootWeb.Lists[docLib] as SPDocumentLibrary;
                string docPath = spSite.MakeFullUrl(library.RootFolder.ServerRelativeUrl) + "/Automation.xml";
                SPFile file = spSite.RootWeb.GetFile(docPath);
                XDocument automation = XDocument.Load(new StreamReader(file.OpenBinaryStream()));
                XElement autoElement = automation.Element("Automation");
                string masterName = autoElement.Attribute("Master").Value;
                string printerName = autoElement.Attribute("Printer").Value;

                // Open the master document
                docPath = spSite.MakeFullUrl(library.RootFolder.ServerRelativeUrl) + "/" + masterName + ".docx";
                SPFile masterFile = spSite.RootWeb.GetFile(docPath);
                Stream docStream = new MemoryStream();
                Stream docMasterStream = masterFile.OpenBinaryStream();
                BinaryReader docMasterReader = new BinaryReader(docMasterStream);
                BinaryWriter docWriter = new BinaryWriter(docStream);
                docWriter.Write(docMasterReader.ReadBytes((int)docMasterStream.Length));
                docWriter.Flush();
                docMasterReader.Close();
                docMasterStream.Dispose();
                Package package = Package.Open(docStream, FileMode.Open, FileAccess.ReadWrite);
                WordprocessingDocument master = WordprocessingDocument.Open(package);
                string guid;
                Uri XMLUri = CreateCustomXML(master, automation.Descendants("Record").First(), out guid);
                BindControls(master, guid);
                master.Close();
                docPath = spSite.MakeFullUrl(library.RootFolder.ServerRelativeUrl) + "/Output 00001.docx";
                spSite.RootWeb.Files.Add(docPath, docStream, true);

                // Loop through all the records from the XML file
                int count = 1;
                foreach (XElement element in automation.Descendants("Record"))
                {
                    if (count != 1)
                    {
                        package = Package.Open(docStream, FileMode.Open, FileAccess.ReadWrite);
                        master = WordprocessingDocument.Open(package);
                        foreach (CustomXmlPart part in master.MainDocumentPart.CustomXmlParts)
                        {
                            if (part.Uri == XMLUri)
                            {
                                Stream stream = part.GetStream(FileMode.Create, FileAccess.ReadWrite);
                                StreamWriter sw = new StreamWriter(stream);
                                sw.Write(element.ToString());
                                sw.Flush();
                                sw.Close();
                                break;
                            }
                        }
                        master.Close();
                        docPath = spSite.MakeFullUrl(library.RootFolder.ServerRelativeUrl) + "/Output " + count.ToString("D5") + ".docx";
                        spSite.RootWeb.Files.Add(docPath, docStream, true);
                    }
                    count++;
                }

                // Use Word Automation Services to convert to XPS files
                ConversionJob job = new ConversionJob(WordAutomationServicesName);
                job.UserToken = spSite.UserToken;
                job.Settings.UpdateFields = true;
                job.Settings.OutputFormat = SaveFormat.XPS;
                job.Settings.OutputSaveBehavior = SaveBehavior.AlwaysOverwrite;
                SPList listToConvert = spSite.RootWeb.Lists[docLib];
                job.AddLibrary(listToConvert, listToConvert);
                job.Start();
                for (; ; )
                {
                    Thread.Sleep(5000);
                    ConversionJobStatus status = new ConversionJobStatus(WordAutomationServicesName, job.JobId, null);
                    if (status.Count == status.Succeeded + status.Failed)
                        break;
                }

                // Print output XPS files
                LocalPrintServer srv = new LocalPrintServer();
                PrintQueue pq = srv.GetPrintQueue(printerName);
                for (int num = 1; num < count; num++)
                {
                    XpsDocumentWriter xdw = PrintQueue.CreateXpsDocumentWriter(pq);
                    docPath = spSite.MakeFullUrl(library.RootFolder.ServerRelativeUrl) + "/Output " + num.ToString("D5") + ".xps";
                    SPFile docFile = spSite.RootWeb.GetFile(docPath);
                    package = Package.Open(docFile.OpenBinaryStream(), FileMode.Open, FileAccess.Read);
                    XpsDocument xdoc = new XpsDocument(package);
                    xdoc.Uri = new Uri(docPath);
                    xdw.Write(xdoc.GetFixedDocumentSequence());
                    xdoc.Close();
                }
            }
        }

        static Uri CreateCustomXML(WordprocessingDocument document, XElement customXML, out string guid)
        {
            CustomXmlPart XMLPart = document.MainDocumentPart.AddCustomXmlPart(CustomXmlPartType.CustomXml);
            StreamWriter sw = new StreamWriter(XMLPart.GetStream(FileMode.Create, FileAccess.ReadWrite));
            sw.Write(customXML.ToString());
            sw.Flush();
            sw.Close();
            guid = "{" + Guid.NewGuid().ToString().ToUpper() + "}";
            CustomXmlPropertiesPart propPart = XMLPart.AddNewPart<CustomXmlPropertiesPart>();
            propPart.DataStoreItem = new DataStoreItem(new SchemaReferences()) { ItemId = guid };
            return XMLPart.Uri;
        }

        static void BindControls(WordprocessingDocument document, string guid)
        {
            foreach (SdtProperties item in document.MainDocumentPart.Document.Descendants<SdtProperties>())
            {
                string tag = item.Descendants<Tag>().First<Tag>().Val.ToString();
                item.Append(new DataBinding() { XPath = "/Record/" + tag, StoreItemId = guid });
            }
        }
    }
}
