// 
// (c) Microsoft Corporation. All rights reserved.
// 
namespace Microsoft.ServiceBus.AccessControlExtensions
{
    using System;
    using System.Data.Services.Client;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using AccessControlManagement;

    public class AccessControlServiceIdentity
    {
        readonly AccessControlSettings settings;

        AccessControlServiceIdentity(AccessControlSettings settings)
        {
            this.settings = settings;
        }

        public string Name { get; set; }
        public byte[] Key { get; set; }

        public string Password
        {
            get { return this.GetKeyAsBase64(); }
        }

        public string Description { get; set; }

        public void Save()
        {
            ManagementService serviceClient = ManagementServiceHelper.CreateManagementServiceClient(this.settings);
            ServiceIdentity serviceId = serviceClient.GetServiceIdentityByName(this.Name);
            if (serviceId == null)
            {
                serviceId = serviceClient.CreateServiceIdentity(this.Name, Encoding.UTF8.GetBytes(this.Password), ServiceIdentityKeyType.Password,
                                                                ServiceIdentityKeyUsage.Password);

                ServiceIdentityKey key = new ServiceIdentityKey
                                             {
                                                 EndDate = DateTime.MaxValue.AddDays(-1).ToUniversalTime(),
                                                 StartDate = DateTime.UtcNow.ToUniversalTime(),
                                                 Type = ServiceIdentityKeyType.Symmetric.ToString(),
                                                 Usage = ServiceIdentityKeyUsage.Signing.ToString(),
                                                 Value = this.Key,
                                                 DisplayName = String.Format(CultureInfo.InvariantCulture, "Symmetric key for {0}", this.Name)
                                             };
                serviceClient.AddRelatedObject(serviceId, "ServiceIdentityKeys", key);
            }
            else
            {
                if (serviceId.Description != this.Description)
                {
                    serviceId.Description = this.Description;
                    serviceClient.UpdateObject(serviceId);
                }
                serviceClient.UpdateServiceIdentityKey(this.Name, Encoding.UTF8.GetBytes(this.Password), ServiceIdentityKeyType.Password);
                serviceClient.UpdateServiceIdentityKey(this.Name, this.Key, ServiceIdentityKeyType.Symmetric);
            }
            serviceClient.SaveChanges(SaveChangesOptions.Batch);
        }

        public void Delete()
        {
            ManagementService serviceClient = ManagementServiceHelper.CreateManagementServiceClient(this.settings);
            serviceClient.DeleteServiceIdentityIfExists(this.Name);
        }

        public void RegenerateKey()
        {
            byte[] key = new byte[32];
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            rngCryptoServiceProvider.GetBytes(key);
            this.Key = key;
        }

        public string GetKeyAsBase64()
        {
            if (this.Key != null)
            {
                return Convert.ToBase64String(this.Key);
            }
            else
            {
                return null;
            }
        }

        public static AccessControlServiceIdentity Create(AccessControlSettings accessControlSettings, string name)
        {
            AccessControlServiceIdentity identity = new AccessControlServiceIdentity(accessControlSettings);
            identity.RegenerateKey();
            identity.Name = name;
            return identity;
        }
    }
}