using System;
using Microsoft.SAPSK.ContosoTours.SAPServices;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.SAPSK.ContosoTours.PPT
{
    public static class AuthenticateSAP
    {
        private static TripleDESCryptoServiceProvider _tripleDESProvider =
            new TripleDESCryptoServiceProvider();
        private static string _key = "default";

        public static bool ValidateUser(string username, string password)
        {
            Config.SAPUserName = username;
            Config.SAPPassword = password;
            return SAPCredential.ValidateUser(username, password);
        }

        public static string DecryptData(string text)
        {
            try
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(text);

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_key));
                hashmd5.Clear();

                _tripleDESProvider.Key = keyArray;
                _tripleDESProvider.Mode = CipherMode.ECB;
                _tripleDESProvider.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = _tripleDESProvider.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                _tripleDESProvider.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
