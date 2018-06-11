using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXMLHtmlConverter.Helper;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;

namespace OpenXMLHtmlConverter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Use the code included to learn more about OpenXML";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Demo on how to customize and use OpenXML";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Write your comments in MSDN";
            return View();
        }

        /// <summary>
        /// Uploads a File and converts the cell content into Html
        /// </summary>
        /// <param name="File">Selected docx file from View</param>
        /// <returns>View and ViewBag with wordML, Html content from cell and hyperlinks information</returns>
        [HttpPost]
        public ActionResult UploadDoc(HttpPostedFileBase File)
        {
            if (File == null)
                throw new Exception("Please, browse to UploadTemplate.docx in the Template folder");
            if (File.InputStream.Length == 0)
                throw new Exception("Invalid file");

            BinaryReader b = new BinaryReader(File.InputStream);
            byte[] binData = b.ReadBytes(File.ContentLength);

            // Write the byte array in a memory stream
            using (MemoryStream streamFile = new MemoryStream())
            {
                streamFile.Write(binData, 0, binData.Length);
                // Read the stream package in a word processing document
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(streamFile, true))
                {
                    ViewBag.WordML = wordDoc.MainDocumentPart.Document.Body.InnerXml;
                    TableCell myTableCell = wordDoc.MainDocumentPart.Document.Body.Elements<Table>().FirstOrDefault().Elements<TableRow>().ElementAt(1).Elements<TableCell>().FirstOrDefault();
                    ViewBag.HtmlContent = HTMLConverterHelper.ConvertToHtml(wordDoc, myTableCell);
                    ViewBag.Hyperlinks = HTMLConverterHelper.GetHyperlinks(wordDoc, String.Empty);
                    wordDoc.Close();
                }
            }
            return View();
        }

        /// <summary>
        /// Gets cell wordML and hyperlinks info from template. Then creates a docx based on DownloadTemplate.docx
        /// </summary>
        /// <returns>Docx file</returns>
        [HttpGet]
        public FileContentResult DownloadConvertedDoc()
        {
            //Get WordML from TableCell and hyperlinks
            string wordMlAndLinks = GetWordMLAndHyperlinks();
            //Get bytes after replacing TableCell data and fix hyperlinks in Template 2
            byte[] byteDownload = GetDownload(wordMlAndLinks);
            return File(byteDownload, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        }

        /// <summary>
        /// Reads UploadTemplate.docx to get cell's wordML and hyperlinks info
        /// </summary>
        /// <returns>cell's wordML and hyperlinks info</returns>
        private string GetWordMLAndHyperlinks()
        {
            string wordMlOriginal = String.Empty;
            string hyperlinks = String.Empty;
            //Read template
            string templatePath = GetRootPath() + @"\Template\UploadTemplate.docx";
            using (var templateFile = System.IO.File.Open(templatePath, FileMode.Open, FileAccess.Read)) //read our template
            {
                using (var stream = new MemoryStream())
                {
                    templateFile.CopyTo(stream);
                    using (var wordDoc = WordprocessingDocument.Open(stream, true))
                    {
                        TableCell myTableCell = wordDoc.MainDocumentPart.Document.Body.Elements<Table>().FirstOrDefault().Elements<TableRow>().ElementAt(1).Elements<TableCell>().FirstOrDefault();
                        //Get TableCell wordML
                        wordMlOriginal = myTableCell.InnerXml;
                        //Get Document Hyperlinks
                        wordMlOriginal = HTMLConverterHelper.UpdateHyperlinkIds(wordDoc, wordMlOriginal, "MyId");
                        hyperlinks = HTMLConverterHelper.GetHyperlinks(wordDoc, "MyId");
                    }
                }
            }
            return wordMlOriginal + hyperlinks;
        }

        /// <summary>
        /// Reads DownloadTemplate.docx to create a new docx. It copies the wordML and hyperlinks information passed
        /// </summary>
        /// <param name="wordMlAndLinks">wordML and hyperlinks info</param>
        /// <returns>New docx</returns>
        private byte[] GetDownload(string wordMlAndLinks)
        {
            //Create/read another word doc and save wordML and links
            string templatePath = GetRootPath() + @"\Template\DownloadTemplate.docx";
            //split WordML and Hyperlinks
            string hyperlinks = wordMlAndLinks.Substring(wordMlAndLinks.IndexOf("<hyperlinks>"));
            string tableCellWordML = wordMlAndLinks.Substring(0, wordMlAndLinks.IndexOf("<hyperlinks>"));
            //Update Hyperlinks
            using (var templateFile = System.IO.File.Open(templatePath, FileMode.Open, FileAccess.Read))
            {
                using (var stream = ConvertStreamToMemoryStream(templateFile))
                {
                    using (var wordDoc = WordprocessingDocument.Open(stream, true))
                    {
                        TableCell DownloadTemplateTableCell = wordDoc.MainDocumentPart.Document.Body.Elements<Table>().FirstOrDefault().Elements<TableRow>().ElementAt(1).Elements<TableCell>().FirstOrDefault();
                        DownloadTemplateTableCell.InnerXml = tableCellWordML;
                        //Update Hyperlinks
                        HTMLConverterHelper.AddHyperlinks(wordDoc, hyperlinks);
                        wordDoc.MainDocumentPart.Document.Save();
                        wordDoc.Close();

                        return stream.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Gets application root path
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            string outputDir = System.Web.HttpContext.Current.Server.MapPath(@"~");
            return new DirectoryInfo(outputDir + @"\Template\").Parent.Parent.FullName;
        }

        /// <summary>
        /// Converts a stream to MemoryStream
        /// </summary>
        /// <param name="stream">stream</param>
        /// <returns></returns>
        public MemoryStream ConvertStreamToMemoryStream(Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();
            if (stream != null)
            {
                byte[] buffer = ReadBytesFromStream(stream); if (buffer != null)
                {
                    var binaryWriter = new BinaryWriter(memoryStream);
                    binaryWriter.Write(buffer);
                }
            }
            return memoryStream;
        }

        /// <summary>
        /// Reads Bytes from Stream
        /// </summary>
        /// <param name="input">input of stream type</param>
        /// <returns></returns>
        public byte[] ReadBytesFromStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024]; using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
