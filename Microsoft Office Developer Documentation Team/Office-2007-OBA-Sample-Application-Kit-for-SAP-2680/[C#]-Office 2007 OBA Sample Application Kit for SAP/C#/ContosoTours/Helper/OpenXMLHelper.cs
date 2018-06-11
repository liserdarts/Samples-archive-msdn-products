using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Packaging;
using System.Text.RegularExpressions;

using Microsoft.Office.DocumentFormat.OpenXml.Packaging;

namespace Microsoft.SAPSK.ContosoTours.Helper
{
    public static class OpenXMLHelper
    {
        public static void SearchAndReplace(string document, string targetURI, string oldValue, string newValue)
        {
            //The string that will store the read excel stream
            string excelText = "";

            using (PresentationDocument pptPres = PresentationDocument.Open(document, true))
            {
                PackagePart pckgPart =
                    pptPres.Package.GetPart(new Uri(targetURI, UriKind.Relative));
                
                using (StreamReader sr = new StreamReader(pckgPart.GetStream()))
                {
                    excelText = sr.ReadToEnd();
                    sr.Close();
                }
                
                Regex regexText = new Regex(oldValue);

                excelText = regexText.Replace(excelText, newValue);

                using (StreamWriter sw = new StreamWriter(pckgPart.GetStream(FileMode.Create)))
                {
                    sw.Write(excelText);
                    sw.Flush();
                    sw.Close();
                }

                pptPres.Close();
            }
        }
    }
}
