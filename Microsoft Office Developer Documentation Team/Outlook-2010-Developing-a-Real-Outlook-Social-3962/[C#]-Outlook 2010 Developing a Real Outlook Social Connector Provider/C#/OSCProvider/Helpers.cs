using System;
using System.Xml;

namespace OSCProvider
{
    internal abstract partial class XmlHelper
    {
        private const string SCHEMA_URL = 
            "http://schemas.microsoft.com/office/outlook/socialprovider.xsd";
        private const string SCHEMA_URL_V1_1 = 
            "http://schemas.microsoft.com/office/outlook/2010/06/socialprovider.xsd";
        internal static XmlDocument GetXmlDoc(ProviderSchemaVersion schemaVersion)
        {
            XmlNameTable xnt = new System.Xml.NameTable();
            XmlDocument xdoc = new XmlDocument(xnt);
            XmlNamespaceManager nsm = new XmlNamespaceManager(xdoc.NameTable);
            
            nsm.AddNamespace("", GetSchemaUrl(schemaVersion));
            xdoc.CreateProcessingInstruction(@"version", @"1.0");
            xdoc.CreateProcessingInstruction(@"encoding", @"utf-8");
            return xdoc;
        }
        internal static string GetSchemaUrl(ProviderSchemaVersion schemaVersion)
        {
            if (schemaVersion == ProviderSchemaVersion.v1_0)
                return SCHEMA_URL;
            if (schemaVersion == ProviderSchemaVersion.v1_1)
                return SCHEMA_URL_V1_1;
            return null;
        }
        internal static XmlElement AddStringElement(XmlElement parentElement, string elementName, object value,bool createIfNull = false,string dateFormat="s")
        {
            if (!createIfNull && value == null) return null;
            if (!createIfNull && value.GetType() == typeof(DateTime) && (DateTime)value == DateTime.MinValue) return null;

            XmlElement xel = parentElement.OwnerDocument.CreateElement(elementName, parentElement.NamespaceURI);

            if (value == null)
            {
                parentElement.AppendChild(xel);
                return xel;
            }

            string innerXml = string.Empty;
            if (value.GetType() == typeof(bool))
            {
                    innerXml = value.ToString().ToLower();
            }
            else if(value.GetType() == typeof(DateTime))
            {
                    innerXml = ((DateTime)value).ToString(dateFormat);
            }
            else if (value.GetType() == typeof(Uri))
            {
                innerXml = Uri.EscapeUriString(value.ToString());
            }
            else
            {
                innerXml = HtmlEncode(value.ToString());
            }
            xel.InnerXml = innerXml;
            parentElement.AppendChild(xel);
            return xel;
        }
        

    }
    public static class Helpers{
        public static string Hash(string input, Schema.HashFunction hashFunction)
        {
            string retVal = null;
            string md5Hash = null;
            string sha1Hash = null;
            string crc32md5Hash = null;


            if(hashFunction == Schema.HashFunction.SHA1)
            {
                
               System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
               foreach(byte b in sha1.ComputeHash(System.Text.Encoding.Default.GetBytes(input)))
                        sha1Hash += b.ToString(@"x2").ToLower();

                retVal = sha1Hash;
            }
            if( hashFunction == Schema.HashFunction.MD5 |
                hashFunction == Schema.HashFunction.CRC32MD5)
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                foreach(byte b in md5.ComputeHash(System.Text.Encoding.Default.GetBytes(input)))
                    md5Hash += b.ToString(@"x2").ToLower();

                retVal = md5Hash;
            } 
            if(hashFunction== Schema.HashFunction.CRC32MD5){

                Int64 crc32 = Crc32.Compute(System.Text.Encoding.Default.GetBytes(input));
                crc32md5Hash = string.Format(@"{0}_{1}",crc32,md5Hash);
                retVal = crc32md5Hash;
            }
            return retVal;
        }
        internal static bool IsOSCException(Exception ex)
        {
            bool isOSCException = false;
            if (ex is OSCException)
            {
                OSCException cex = ex as OSCException;
                switch (System.Runtime.InteropServices.Marshal.GetHRForException(cex))
                {
                    case (int)OSCExceptions.OSC_E_AUTH_ERROR:
                    case (int)OSCExceptions.OSC_E_COULDNOTCONNECT:
                    case (int)OSCExceptions.OSC_E_INTERNAL_ERROR:
                    case (int)OSCExceptions.OSC_E_INVALIDARG:
                    case (int)OSCExceptions.OSC_E_NO_CHANGES:
                    case (int)OSCExceptions.OSC_E_NOT_FOUND:
                    case (int)OSCExceptions.OSC_E_NOT_IMPLEMENTED:
                    case (int)OSCExceptions.OSC_E_OUT_OF_MEMORY:
                    case (int)OSCExceptions.OSC_E_PERMISSION_DENIED:
                    case (int)OSCExceptions.OSC_E_VERSION:
                        isOSCException = true;
                        break;
                    default:
                        isOSCException = false;
                        break;
                }
            }
            return isOSCException;
        }
        public static byte[] GetBytesFromUrl(Uri url)
        {
            System.Net.WebRequest webReq = System.Net.WebRequest.Create(url);
            webReq.Method = @"GET";
            System.Net.WebResponse webResp = webReq.GetResponse();
            System.IO.Stream stream = webResp.GetResponseStream();
            System.IO.BinaryReader br = new System.IO.BinaryReader(stream);
            byte[] buffer = br.ReadBytes((int)webResp.ContentLength);
            webResp.Close();
            return buffer;
        }
        internal static bool IsNE(string txt)
        {
            bool retVal = true;
            if (txt != null && txt.Length > 0)
                retVal = false;
            return retVal;
        }
    }
}
