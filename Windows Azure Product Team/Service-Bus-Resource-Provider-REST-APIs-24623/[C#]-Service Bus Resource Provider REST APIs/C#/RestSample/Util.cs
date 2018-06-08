namespace SBRestSample
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;

    class Util
    {
        /// <summary>
        /// Util function to Get Certificate
        /// </summary>
        public static X509Certificate2 GetManagementCertificate(string thumbprint)
        {
            List<StoreLocation> locations = new List<StoreLocation> 
            { 
                StoreLocation.LocalMachine ,
                StoreLocation.CurrentUser
            };

            foreach (var location in locations)
            {
                X509Store store = new X509Store(StoreName.My, location);

                try
                {
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
                    if (certificates.Count > 0)
                    {
                        return certificates[0];
                    }
                }
                finally
                {
                    store.Close();
                }
            }

            throw new ArgumentException(string.Format(
                "A Certificate with Thumbprint '{0}' could not be located.",
                thumbprint));
        }
    }
}
