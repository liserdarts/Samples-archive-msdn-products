namespace ExchangeRateCalculatorMVCWeb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Net;
    using System.Xml;
    public class Common
    {
        public static XmlDocument getXMLDocumentFromXMLTemplate(string inURL)
        {
            HttpWebRequest myHttpWebRequest = null;     //Declare an HTTP-specific implementation of the WebRequest class.
            HttpWebResponse myHttpWebResponse = null;   //Declare an HTTP-specific implementation of the WebResponse class
            XmlDocument myXMLDocument = null;           //Declare XMLResponse document
            XmlTextReader myXMLReader = null;           //Declare XMLReader

            try
            {
                //Create Request
                myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(inURL);
                myHttpWebRequest.Method = "GET";
                myHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";

                //Get Response
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                //Now load the XML Document
                myXMLDocument = new XmlDocument();

                //Load response stream into XMLReader
                myXMLReader = new XmlTextReader(myHttpWebResponse.GetResponseStream());
                myXMLDocument.Load(myXMLReader);
            }
            catch (Exception myException)
            {
                return null;
            }
            finally
            {
                myHttpWebRequest = null;
                myHttpWebResponse = null;
                myXMLReader = null;
            }
            return myXMLDocument;
        }
    }
}