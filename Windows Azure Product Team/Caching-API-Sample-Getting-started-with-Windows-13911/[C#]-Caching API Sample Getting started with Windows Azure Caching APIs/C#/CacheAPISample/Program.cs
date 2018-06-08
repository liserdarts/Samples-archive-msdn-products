
namespace CacheAPISample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.ApplicationServer.Caching;
    using CacheAPISample.Properties;

    // A Sample class to represent a composite object to be put in cache.
    [Serializable]
    internal class ProductInfo
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Value { get; set; }
        public byte[] Image { get; set; }


        public override string ToString()
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(" Id: {0}, ProductName:{1}, Value:{2}", Id, ProductName, Value);
            if (null != Image
                && 0 != Image.Length)
            {
                strb.Append(", Image:");
                string sep = "";
                for (int i = 0; i < Image.Length; i++)
                {
                    strb.AppendFormat("{0}{1}", sep, Image[i]);
                    sep = ";";
                }
            }

            return strb.ToString();
        }
    }

    class Program
    {
        DataCache myDefaultCache;
        string myObjectForCaching = "This is my Object";

        static void Main(string[] args)
        {
            Program program = new Program();

            // You will only need to encrypt the authentication token once. This will encrypt
            // the token with a random key. The token and key will be stored in application settings.
            //
            // Please set firstRun to false when running this application every subsequent time.
            bool firstRun = true;
            if (firstRun)
            {
                program.EncryptAndStoreAuthenticationToken(); 
            }

            bool sslEnabled = false;
            program.PrepareClient(sslEnabled);
            program.RunSampleTest();

            //Running test Against Ssl Endpoint

            sslEnabled = true;
            program.PrepareClient(sslEnabled);
            program.RunSampleTest();

            Console.WriteLine("Press any key to continue ...");
            Console.ReadLine();
        }

        public void RunSampleTest()
        {
            // TESTING SIMPLE Add/Get on default cache
            AddGetDefaultCache();

            // TESTING SIMPLE Add/GetAndLock
            AddGetAndLock();

            // TESTING SIMPLE Add/Get with Version
            AddGetWithVersion();
        }

        private void AddGetWithVersion()
        {
            //
            // TESTING SIMPLE Add/Get with Version
            //

            DataCacheItemVersion itemVersion;
            DataCacheItem cacheItem1, cacheItem2;
            DataCacheItemVersion cacheItemVersion;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Testing Simple Add/GetCacheItem/Put");
            Console.WriteLine("Cache       = default");
            Console.WriteLine("Tags        = Not Supported");
            Console.WriteLine("Version     = yes");

            string KeyToMyStringWithVersion = "KeyToMyStringWithVersion";

            try
            {
                //First, clear any old value in cache due to some previous run of the sample
                TryClearOldKey(myDefaultCache, KeyToMyStringWithVersion);

                // Add an object to the cache
                itemVersion = myDefaultCache.Add(KeyToMyStringWithVersion, myObjectForCaching);
                WriteSuccess("Add-Object Added to Cache. [key={0}]", KeyToMyStringWithVersion);

                // Get the object added to the Cache
                if ((cacheItem1 = myDefaultCache.GetCacheItem(KeyToMyStringWithVersion)) != null)
                {
                    WriteSuccess("GetCacheItem-Object Get from cache [key={0}]", KeyToMyStringWithVersion);
                }
                else
                {
                    WriteFailure("GetCacheItem-Object did not Get from cache [key={0}]", KeyToMyStringWithVersion);
                }

                // Get another copy of the same object (used to remember the version)
                if ((cacheItem2 = myDefaultCache.GetCacheItem(KeyToMyStringWithVersion)) != null)
                {
                    WriteSuccess("GetCacheItem-Object Get from cache [key={0}]", KeyToMyStringWithVersion);
                }
                else
                {
                    WriteFailure("GetCacheItem-Object did not Get from cache [key={0}]", KeyToMyStringWithVersion);
                }

                // Add a newer version of the object to the cache, supply the version as well to ensure that we are updating
                // the cache only if we have the latest version
                cacheItemVersion = myDefaultCache.Put(KeyToMyStringWithVersion, (object)cacheItem1.Value, cacheItem1.Version);
                WriteSuccess("Put-Object updated successfully [key={0}]", KeyToMyStringWithVersion);
                WriteSuccess("          New version {0} Old version", cacheItemVersion > cacheItem2.Version ? ">" : "<=");

                // Try to add an object when the version of the object in the Cache is newer, it will fail
                try
                {
                    cacheItemVersion = myDefaultCache.Put(KeyToMyStringWithVersion, (object)cacheItem2.Value, cacheItem2.Version);
                    WriteFailure("Put-Object update. Update to new version work.  [key={0}]", KeyToMyStringWithVersion);

                }
                catch (DataCacheException ex)
                {
                    WriteSuccess("Put-Object-Expected behaviour since Object is newer. Exception: {0}", ex.Message);
                }
            }
            catch (DataCacheException ex)
            {
                WriteFailure("Distributed Cache Generated Exception: {0}", ex.Message);
            }
        }

        private void AddGetAndLock()
        {
            //
            // TESTING SIMPLE Add/GetAndLock
            // without version
            //
            // Try this variation
            // - Put a BreakPoint on the second GetAndLock, and hold the execution for 5 seconds.
            //   It will return the object and lock the object for 10 seconds, since the first lock has expired.

            Console.ForegroundColor = ConsoleColor.Gray;
            string item;
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Testing Simple Add/Get/GetAndLock/GetIfNewer/Put/PutAndUnlock");
            Console.WriteLine("Cache       = default");
            Console.WriteLine("Tags        = Not Supported");
            Console.WriteLine("Version     = <none>");

            DataCacheItemVersion myVersionBeforeChange = null, myVersionAfterChange = null, myVersionChangedOnceMore = null;
            DataCacheLockHandle lockHandle;
            string myKey = "KeyToMyStringTryingLock";

            try
            {
                //First, clear any old value in cache due to some previous run of the sample
                TryClearOldKey(myDefaultCache, myKey);

                // Initialize the object with a Add
                myDefaultCache.Add(myKey, myObjectForCaching);
                WriteSuccess("Add-Object Added to Cache [key={0}]", myKey);

                // Do a Simple Get, lock the object for 5 seconds
                item = (string)myDefaultCache.GetAndLock(myKey, new TimeSpan(0, 0, 5), out lockHandle);
                if (item != null)
                {
                    WriteSuccess("GetAndLock-Object Get from cache [key={0}]", myKey);
                }
                else
                {
                    WriteFailure("GetAndLock-Object did not Get from cache [key={0}]", myKey);
                }


                // Do a optimistic Get
                if ((item = (string)myDefaultCache.Get(myKey, out myVersionBeforeChange)) != null)
                {
                    WriteSuccess("Get-Object returned. Get will always pass. Will not wait");
                    WriteSuccess("          on a updating object. Current Version will be returned. [key={0}]", myKey);
                }
                else
                {
                    WriteFailure("Get-Object did not return. [key={0}]", myKey);
                }

                try
                {
                    // Do a one more Simple Get, and attempt lock the object for 10 seconds
                    item = (string)myDefaultCache.GetAndLock(myKey, new TimeSpan(0, 0, 10), out lockHandle);
                    WriteFailure("GetAndLock-Object Get from cache [key={0}]", myKey);
                }
                catch (DataCacheException ex)
                {
                    WriteSuccess("GetAndLock hit a exception, because Object is already locked. [key={0}]", myKey);
                    WriteSuccess("Expected GetAndLock-Distributed Cache Generated Exception: {0}", ex.Message);
                }

                // Get the Object only if the version has changed
                if ((item = (string)myDefaultCache.GetIfNewer(myKey, ref myVersionBeforeChange)) != null)
                {
                    WriteFailure("GetIfNewer-Object changed. Should not return as Object has");
                    WriteFailure("            not been changed. [key={0}]", myKey);
                }
                else
                {
                    WriteSuccess("GetIfNewer-Object has not changed. Hence did not return. [key={0}]", myKey);
                }

                // Now update the object with a Put                
                myVersionAfterChange = myDefaultCache.Put(myKey, myObjectForCaching + "Put1");

                WriteSuccess("Put1-null-version-Object changed. Put will pass even if Object");
                WriteSuccess("          is locked. Object will also be unlocked. [key={0}]", myKey);
                myObjectForCaching += "Put1";

                // Object with older version changed
                if ((item = (string)myDefaultCache.GetIfNewer(myKey, ref myVersionBeforeChange)) != null)
                {
                    WriteSuccess("GetIfNewer-Object has been changed. [key={0}]", myKey);
                }
                else
                {
                    WriteFailure("GetIfNewer-Object did not return. Put ");
                    WriteFailure("            did modify the Object. Should return. [key={0}]", myKey);
                }

                // Object with newer version after Put
                if ((item = (string)myDefaultCache.GetIfNewer(myKey, ref myVersionAfterChange)) != null)
                {
                    WriteFailure("GetIfNewer-Object with newer version not changed.");
                    WriteFailure("            Should not return. [key={0}]", myKey);
                }
                else
                {
                    WriteSuccess("GetIfNewer-Object with newer version not changed. [key={0}]", myKey);
                }

                // Object with newer version after Put
                myVersionChangedOnceMore = myDefaultCache.Put(myKey, myObjectForCaching + "Put2", myVersionBeforeChange);
                WriteSuccess("Put2-version from Put1-Object changed. [key={0}]", myKey);
                myObjectForCaching += "Put2";

                try
                {
                    // Try the above PutAndUnlock                 
                    myVersionChangedOnceMore = myDefaultCache.PutAndUnlock(myKey, myObjectForCaching + "Put3", lockHandle);
                    WriteFailure("[This code should not be hit]PutAndUnlock-Object updated and unlocked. [key={0}]", myKey);
                    myObjectForCaching += "Put3";

                }
                catch (DataCacheException ex)
                {
                    WriteSuccess("PutAndUnlock-Expected exception since Object is already unlocked. [key={0}]", myKey);
                    WriteSuccess("PutAndUnlock-Distributed Cache Generated Exception: {0}", ex.Message);
                }

                // Unlock Object
                try
                {
                    myDefaultCache.Unlock(myKey, lockHandle);
                    WriteFailure("[This code should not be hit]Unlock-Object unlocked. [key={0}]", myKey);
                }
                catch (DataCacheException ex)
                {
                    WriteSuccess("Unlock-Expected exception since Object is already unlocked. [key={0}]", myKey);
                    WriteSuccess("Expected Unlock-Distributed Cache Generated Exception: {0}", ex.Message);
                }

                // Finally, Test the state of object should be "This is my Object.Put1Put2"
                if ((item = (string)myDefaultCache.Get(myKey, out myVersionChangedOnceMore)) == myObjectForCaching)
                {
                    WriteSuccess("Get-Object retrived from cache. [key={0}]", myKey);
                }
                else
                {
                    WriteFailure("Get-Object was not retrived from cache. [key={0}]", myKey);
                }
            }
            catch (DataCacheException ex)
            {
                WriteFailure("Add-Get-GetAndLock-GetIfVersionMismatch-Put-PutAndUnlock-Distributed Cache Generated Exception:");
                WriteFailure(ex.ToString());
            }
        }

        private void AddGetDefaultCache()
        {
            DataCacheItemVersion itemVersion;
            string item;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            //
            // TESTING SIMPLE Add/Get on default cache
            //
            // no regions
            //           
            // Try this variation
            // - Put a BreakPoint at the Get("KeyToMyString") call, and wait for 10 mins when breakpoint is hit.
            // - Get will fail, as the data will have been expired (default TTL is 10 mins).
            Console.WriteLine("----------------------");
            Console.WriteLine("Testing Simple Add/Get");
            Console.WriteLine("Cache       = default");
            Console.WriteLine("Region      = Not Supported");
            Console.WriteLine("Tags        = Not Supported");
            Console.WriteLine("Version     = <none>");

            try
            {
                //First, clear any old value in cache due to some previous run of the sample
                TryClearOldKey(myDefaultCache, "KeyToMyString");

                #region Add-Get String
                // Store the object in the default Cache with a Add
                itemVersion = myDefaultCache.Add("KeyToMyString", myObjectForCaching);
                WriteSuccess("Add-Object Added to Cache [key=KeyToMyString]");

                // Do a Simple Get using valid Key from the default Cache
                if ((item = (string)myDefaultCache.Get("KeyToMyString")) != null)
                {
                    WriteSuccess("Get-Object Get from cache [key=KeyToMyString]");
                }
                else
                {
                    WriteFailure("Get-Object did not Get from cache [key=KeyToMyString]");
                }

                // Do a Simple Get using an invalid Key from the default Cache
                if ((item = (string)myDefaultCache.Get("InCorrectKeySpecified")) == null)
                {
                    WriteSuccess("Get-Object did not Get, since invalid key specified [key=InCorrectKeySpecified]");
                }
                else
                {
                    WriteFailure("Get-Object Get from cache, unexpected result");
                }

                //Remove the key that was added earlier 
                myDefaultCache.Remove("KeyToMyString");
                #endregion


                #region Add-Get Composite object

                ProductInfo productItem = new ProductInfo
                {
                    Id = 101,
                    ProductName = "Contoso Alpha",
                    Value = 202.1,
                    Image = new byte[] { 1, 127, 255, 1, 127, 255 }
                };

                //First, clear any old value in cache due to some previous run of the sample
                TryClearOldKey(myDefaultCache, productItem.ProductName);

                ProductInfo productItemFromCache = null;
                // Store the object in the default Cache with a Add
                itemVersion = myDefaultCache.Add(productItem.ProductName, productItem);
                WriteSuccess("Add-Object Added to Cache [key={0}]", productItem.ProductName);

                // Do a Simple Get using valid Key from the default Cache
                if ((productItemFromCache = (ProductInfo)myDefaultCache.Get(productItem.ProductName)) != null)
                {
                    WriteSuccess("Get-Object Get from cache [key={0}]", productItem.ProductName);
                    WriteSuccess("Got object: {0}", productItemFromCache.ToString());

                }
                else
                {
                    WriteFailure("Get-Object did not Get from cache [key={0}]", productItem.ProductName);
                }

                // Do a Simple Get using an invalid Key from the default Cache
                if ((productItemFromCache = (ProductInfo)myDefaultCache.Get("InCorrectKeySpecified")) == null)
                {
                    WriteSuccess("Get-Object did not Get, since invalid key specified [key=InCorrectKeySpecified]");
                }
                else
                {
                    WriteFailure("Get-Object Get from cache, unexpected result");
                }

                //Remove the key that was added earlier 
                myDefaultCache.Remove(productItem.ProductName);
                #endregion


            }
            catch (DataCacheException ex)
            {
                WriteFailure("Add-Get-This is failing probably because you are running this");
                WriteFailure("          sample test within 10mins (default TTL for the named cache). Please wait for sometime and try again");
                WriteFailure("Expected Distributed Cache Generated Exception: {0}", ex.Message);
            }
        }

        private void EncryptAndStoreAuthenticationToken()
        {
            // Insert the Authentication Token
            string authenticationToken = "[InsertAuthenticationTokenHere]";
            SaveAuthTokenToSettings(authenticationToken); 
            // Save ACS token to the settigs file.
            // Once the ACS token is saved to settings file, the token may be read any number of times by calling this function.
        }

        private void PrepareClient(bool sslEnabled)
        {
            string hostName = "[Cache endpoint without port]";      //Example : "MyCache.cache.appfabric.com";
            int cachePort;

            cachePort = sslEnabled ? 22243 : 22233; // Default port
            List<DataCacheServerEndpoint> server = new List<DataCacheServerEndpoint>();
            server.Add(new DataCacheServerEndpoint(hostName, cachePort));
            DataCacheFactoryConfiguration config = new DataCacheFactoryConfiguration();

            // Uncomment the lines below to use the authentication token in plain text.
            //
            ////string authenticationToken = "[InsertAuthenticationTokenHere]"; 
            ////SecureString secureAuthenticationToken = GetSecureString(authenticationToken);
            
            // Load Auth token from settings
            SecureString secureAuthenticationToken = LoadAuthTokenFromSettings();
            config.SecurityProperties = new DataCacheSecurity(secureAuthenticationToken, sslEnabled);
            config.Servers = server;            

            config.LocalCacheProperties = new DataCacheLocalCacheProperties(10000, new TimeSpan(0, 5, 0), DataCacheLocalCacheInvalidationPolicy.TimeoutBased);
            DataCacheFactory myCacheFactory = new DataCacheFactory(config);

            myDefaultCache = myCacheFactory.GetDefaultCache();
        }

        private static SymmetricAlgorithm GetCryptographyProvider()
        {
            return new AesCryptoServiceProvider();
        }

        // Once the ACS token is saved to settings file, the token may be read any number of times by calling this function.
        //
        // Example :
        //      using (SecureString secureToken = LoadAuthTokenFromSettings(authenticationToken))
        //      {
        //          DataCacheFactoryConfiguration config = new DataCacheFactoryConfiguration();
        //          ...
        //          config.SecurityProperties = new DataCacheSecurity(secureToken);
        //          ...
        //      }
        //
        public static SecureString LoadAuthTokenFromSettings()
        {
            using (SymmetricAlgorithm cryptoProvider = GetCryptographyProvider())
            {
                byte[] cipher = Convert.FromBase64String(Settings.Default.EncryptedToken);

                // For increased security, consider encrypting the key and initialization
                // vector with a certificate.
                byte[] key = Convert.FromBase64String(Settings.Default.EncryptionKey);
                byte[] iv = Convert.FromBase64String(Settings.Default.EncryptionIV);

                using (ICryptoTransform decryptor = cryptoProvider.CreateDecryptor(key, iv))
                {
                    byte[] plainTextBytes = null;
                    char[] plainTextChars = null;
                    try
                    {
                        plainTextBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);
                        plainTextChars = Encoding.UTF8.GetChars(plainTextBytes);

                        SecureString secureString = new SecureString();
                        foreach (var ch in plainTextChars)
                        {
                            secureString.AppendChar(ch);
                        }

                        secureString.MakeReadOnly();
                        return secureString;
                    }
                    catch { throw; }
                    finally
                    {
                        // Zero out all plaintext
                        if (plainTextBytes != null)
                        {
                            Array.Clear(plainTextBytes, 0, plainTextBytes.Length);
                        }
                        if (plainTextChars != null)
                        {
                            Array.Clear(plainTextChars, 0, plainTextChars.Length);
                        }
                    }
                }
            }
        }

        // To save ACS token to settings, please call this function with the ACS token as the plaintext parameter.
        // This function will only need to be called for the first run.
        //
        // Example :
        //      string authenticationToken = @"YWNzOkNhY2hlSU5UM0QzRGVtby5jYWNoZS5pbnQzLndpbmRvd3MtaW50Lm5ldC8=";
        //      SaveAuthTokenToSettings(authenticationToken);
        //
        public static void SaveAuthTokenToSettings(string plainText)
        {
            using (SymmetricAlgorithm cryptoProvider = GetCryptographyProvider())
            {
                // Generate a random key and initialization vector.
                cryptoProvider.GenerateKey();
                cryptoProvider.GenerateIV();

                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                using (ICryptoTransform encryptor = cryptoProvider.CreateEncryptor())
                {
                    byte[] cipherBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                    Settings.Default.EncryptedToken = Convert.ToBase64String(cipherBytes);
                }

                Settings.Default.EncryptionKey = Convert.ToBase64String(cryptoProvider.Key);
                Settings.Default.EncryptionIV = Convert.ToBase64String(cryptoProvider.IV);
            }

            Settings.Default.Save();
        }


        private static void WriteSuccess(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(format, args);
        }

        private static void WriteFailure(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(format, args);
        }

        private static void TryClearOldKey(DataCache cache, string key)
        {
            if (null != cache.Get(key))
            {
                cache.Remove(key);
            }
        }

        public static SecureString GetSecureString(string input)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            var securePassword = new SecureString();
            foreach (char a in input)
            {
                securePassword.AppendChar(a);
            }
            securePassword.MakeReadOnly();
            return securePassword;
        }
    }
}