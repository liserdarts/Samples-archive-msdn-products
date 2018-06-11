using System;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;

namespace Microsoft.SAPSK.ContosoTours.Helper
{
    public static class PowerPointHelper
    {
        private static TripleDESCryptoServiceProvider _tripleDESProvider =
                new TripleDESCryptoServiceProvider();
        private static string _key = "default";

        public static void OpenPowerPoint(int packageID)
        {
            string pptFileName =
                "Package" +
                packageID.ToString("00000") +
                "_" +
                Config.SAPUserName + "_" +
                EncryptData(Config.SAPPassword) +
                ".pptx";

           
            string xlsFileName = pptFileName.Replace(".pptx", ".xlsx");

            xlsFileName = Path.Combine(
                    Config.TempPPTPath,
                    xlsFileName);
            

            pptFileName = Path.Combine(
                    Config.TempPPTPath,
                    pptFileName);

            UtilityHelper.ByteToFile(Properties.Resources.packageTemplate, pptFileName);

            StatisticList statList = DataHelper.GetStatistics(packageID);

            #region change age stat values

            string chart3Uri="/ppt/charts/chart3.xml";
            
         
            OpenXMLHelper.SearchAndReplace(
                pptFileName, chart3Uri,"1234567890", statList.AdultAgeStat.ToString());
            OpenXMLHelper.SearchAndReplace(
                pptFileName, chart3Uri, "1234567891", statList.ChildAgeStat.ToString());
            OpenXMLHelper.SearchAndReplace(
                pptFileName, chart3Uri, "1234567892", statList.InfantAgeStat.ToString());
           
            #endregion

            #region change flight class stat values
            
            string chart2Uri = "/ppt/charts/chart2.xml";
            OpenXMLHelper.SearchAndReplace(
                pptFileName, chart2Uri, "1234567890", statList.FirstClassStat.ToString());
            OpenXMLHelper.SearchAndReplace(
                pptFileName, chart2Uri, "1234567891", statList.BusinessClassStat.ToString());
            OpenXMLHelper.SearchAndReplace(
                pptFileName, chart2Uri, "1234567892", statList.EconomyClassStat.ToString());
           
            #endregion

            if (statList.LocationStat != null)
            {
                ExcelHelper.LoadAndCloseExcelSheet(
                    "Population",
                    Properties.Resources.Population,
                    statList.LocationStat,
                    xlsFileName);
            }
            //halt system by 5seconds to finish processing he graph
            System.Threading.Thread.Sleep(5000);

            Process.Start(
                pptFileName);
        }

        private static string EncryptData(string text)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(text);

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_key));
            hashmd5.Clear();

            _tripleDESProvider.Key = keyArray;
            _tripleDESProvider.Mode = CipherMode.ECB;
            _tripleDESProvider.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = _tripleDESProvider.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private static string DecryptData(string text)
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

        private static string GetFileData(string fileName)
        {
            string fileData = string.Empty;

            using (StreamReader sr =
               File.OpenText(
                   fileName))
            {
                fileData = sr.ReadToEnd();
            }
            return fileData;
        }

        private static void WriteFileData(string fileName, string fileData)
        {
            using (StreamWriter sw =
               new StreamWriter(fileName))
            {
                sw.Write(fileData);
            }
        }

        private static void DeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                foreach (string dirOrFile in Directory.GetFileSystemEntries(directoryPath))
                {
                    if (Directory.Exists(dirOrFile))
                    {
                        DeleteDirectory(dirOrFile);
                        Directory.Delete(dirOrFile);
                    }
                    else
                    {
                        File.SetAttributes(dirOrFile, FileAttributes.Normal);
                        File.Delete(dirOrFile);
                    }
                }
            }
        }
    }
}
