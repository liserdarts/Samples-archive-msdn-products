/*=====================================================================
  This file is part of the Microsoft Unified Communications Code Samples.

  Copyright (C) 2011 Microsoft Corporation.  All rights reserved.

This source code is intended only as a supplement to Microsoft
Development Tools and/or on-line documentation.  See these other
materials for detailed information regarding Microsoft code samples.

THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
PARTICULAR PURPOSE.
=====================================================================*/

using System;
using System.Net;
using System.Xml;
using System.Text;

namespace MSDNArticleIM
{
    // Bing API 2.0 code sample demonstrating the use of the
    // Spell SourceType over the XML Protocol.
    static class BingSpellChecker
    {
        // Replace the following string with the AppId you received from the
        // Bing Developer Center.
        const string AppId = "DE19D93F664DB1C142E886326F411FC5FC8CF838";
        static void SpellCheckMain()
        {
        }

        public static HttpWebRequest BuildSpellRequest(string textToCheck)
        {
            string requestString = "http://api.bing.net/xml.aspx?"

                // Common request fields (required)
                + "AppId=" + AppId
                + "&Query=" + textToCheck
                + "&Sources=Spell"

                // Common request fields (optional)
                + "&Version=2.0"
                + "&Market=en-us"
                + "&Options=EnableHighlighting";

            // Create and initialize the request.
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(
                requestString);
            

            return request;
        }

        public static string GetSpellResponse(HttpWebResponse response)
        {
            string returnValue = string.Empty;
            // Load the response into an XmlDocument.
            XmlDocument document = new XmlDocument();
            document.Load(response.GetResponseStream());

            // Add the default namespace to the namespace manager.
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(
                document.NameTable);
            nsmgr.AddNamespace(
                "api",
                "http://schemas.microsoft.com/LiveSearch/2008/04/XML/element");

            XmlNodeList errors = document.DocumentElement.SelectNodes(
                "./api:Errors/api:Error",
                nsmgr);

            if (errors.Count > 0)
            {
                // There are errors in the response. Display error details.
                DisplayErrors(errors);
            }
            else
            {
                // There were no errors in the response. Display the
                // Spell results.
               returnValue = DisplaySpellResults(document.DocumentElement, nsmgr);
            }
            return returnValue;
        }

        public static string DisplaySpellResults(XmlNode root, XmlNamespaceManager nsmgr)
        {
            string version = root.SelectSingleNode("./@Version", nsmgr).InnerText;
            string searchTerms = root.SelectSingleNode(
                "./api:Query/api:SearchTerms",
                nsmgr).InnerText;

            // Display the results header.
            StringBuilder sb = new StringBuilder();

            // Add the Spell SourceType namespace to the namespace manager.
            nsmgr.AddNamespace(
                "spl",
                "http://schemas.microsoft.com/LiveSearch/2008/04/XML/spell");

            XmlNodeList results = root.SelectNodes(
                "./spl:Spell/spl:Results/spl:SpellResult",
                nsmgr);

            // Display the Spell results.
            foreach (XmlNode result in results)
            {
                DisplayTextWithHighlighting(
                    result.SelectSingleNode("./spl:Value", nsmgr).InnerText, ref sb);
                sb.Append(System.Environment.NewLine);
            }
            return sb.ToString();
        }

        public static void DisplayTextWithHighlighting(string text, ref StringBuilder sb)
        {
            // Write text to the standard output stream, changing the console
            // foreground color as highlighting characters are encountered.
            foreach (char c in text.ToCharArray())
            {
                if (c == '\uE000')
                {
                    // If the current character is the begin highlighting
                    // character (U+E000), change the console foreground color
                    // to green.
                    Console.ForegroundColor = ConsoleColor.Green;
                   // _ControlToUpdate.fo
                }
                else if (c == '\uE001')
                {
                    // If the current character is the end highlighting
                    // character (U+E001), revert the console foreground color
                    // to gray.
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    //Console.Write(c);
                    sb.Append(c);
                }
            }
        }

        public static void DisplayErrors(XmlNodeList errors)
        {
            // Iterate over the list of errors and display error details.
            Console.WriteLine("Errors:");
            Console.WriteLine();
            foreach (XmlNode error in errors)
            {
                foreach (XmlNode detail in error.ChildNodes)
                {
                    Console.WriteLine(detail.Name + ": " + detail.InnerText);
                }

                Console.WriteLine();
            }
        }
    }
}
