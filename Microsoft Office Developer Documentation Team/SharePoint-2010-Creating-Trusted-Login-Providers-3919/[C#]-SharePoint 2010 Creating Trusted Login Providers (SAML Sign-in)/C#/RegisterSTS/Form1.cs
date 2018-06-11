using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.IdentityModel.Claims;
using Microsoft.SharePoint.Administration.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace RegisterSTS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<SPTrustedClaimTypeInformation> claimMapping = new List<SPTrustedClaimTypeInformation>();
            List<string> strClaimMapping = new List<string>();

            SPTrustedClaimTypeInformation idClaim = new SPTrustedClaimTypeInformation("EmailAddress", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            SPTrustedClaimTypeInformation titleClaim = new SPTrustedClaimTypeInformation("Title", "http://schemas.wingtip.com/sharepoint/2009/08/claims/title", "http://schemas.wingtip.com/sharepoint/2009/08/claims/title");

            titleClaim.AcceptOnlyKnownClaimValues = true;

            idClaim.AddKnownClaimValue("user1@wingtip.com");
            idClaim.AddKnownClaimValue("user2@wingtip.com");
            idClaim.AddKnownClaimValue("user3@wingtip.com");

            titleClaim.AddKnownClaimValue("Engineer");
            titleClaim.AddKnownClaimValue("Manager");
            titleClaim.AddKnownClaimValue("CEO");

            //creating the string[] for all claims, this is required for the construction of SPTrustedLoginProvider
            strClaimMapping.Add(idClaim.InputClaimType);
            strClaimMapping.Add(titleClaim.InputClaimType);


            X509Certificate2 ImportTrustCertificate = new X509Certificate2(@"C:\StudentFiles\LabFiles\Module_6\Resources\STSTestCertPub.cer");

            claimMapping.Add(idClaim);
            claimMapping.Add(titleClaim);

            SPSecurityTokenServiceManager manager = SPSecurityTokenServiceManager.Local;
            SPTrustedLoginProvider provider = new SPTrustedLoginProvider(
                                                                        manager,
                                                                        "WingtipSTS",
                                                                        "WingtipSTS",
                                                                        new Uri("http://localhost:48924/WingtipSTS/default.aspx"),
                                                                        "https://spdev0223/_trust/",
                                                                        strClaimMapping.ToArray(),
                                                                        idClaim);



            foreach (SPTrustedClaimTypeInformation claimTypeInfo in claimMapping)
            {
                if (claimTypeInfo.InputClaimType == provider.IdentityClaimTypeInformation.InputClaimType)
                {
                    continue;
                }
                provider.AddClaimTypeInformation(claimTypeInfo);
            }

            if (ImportTrustCertificate != null)
            {
                provider.SigningCertificate = ImportTrustCertificate;
            }


            //provider.ClaimProviderName = "ContosoCRMClaimProvider";

            provider.UseWReplyParameter = true;

            manager.TrustedLoginProviders.Add(provider);
            manager.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SPSecurityTokenServiceManager manager = SPSecurityTokenServiceManager.Local;
            SPTrustedLoginProviderCollection providers = manager.TrustedLoginProviders;
            
            foreach(SPTrustedLoginProvider provider in providers)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(provider.Name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SPSecurityTokenServiceManager manager = SPSecurityTokenServiceManager.Local;

            string providerName = (string)listBox1.SelectedItem;

            SPTrustedLoginProvider provider = manager.TrustedLoginProviders[providerName];

            manager.TrustedLoginProviders.Remove(provider.Id);
            manager.Update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SPSecurityTokenServiceManager manager = SPSecurityTokenServiceManager.Local;

            string providerName = (string)listBox1.SelectedItem;

            SPTrustedLoginProvider provider = manager.TrustedLoginProviders[providerName];
            provider.UseWReplyParameter = true;
            provider.ProviderRealm = "https://spdev0223/_trust/";
            provider.ProviderUri = new Uri("http://localhost:48924/WingtipSTS/default.aspx");


            provider.Update();

            manager.Update();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SPSecurityTokenServiceManager manager = SPSecurityTokenServiceManager.Local;

            string providerName = (string)listBox1.SelectedItem;

            SPTrustedLoginProvider provider = manager.TrustedLoginProviders[providerName];
            manager.TrustedLoginProviders.Remove(provider.Id);
            manager.Update();
        }
    }
}
