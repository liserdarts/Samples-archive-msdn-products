using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterviewManagement.BdcNetModel;
using Microsoft.SharePoint.Client;
using Microsoft.BusinessData.SystemSpecific;
using Microsoft.BusinessData.MetadataModel;
using Microsoft.BusinessData.Runtime;
using Microsoft.SharePoint;
using System.IO;
using System.Data;
using Microsoft.BusinessData.Infrastructure.SecureStore;
using System.Runtime.InteropServices;

namespace InterviewManagement.BdcModel1 
{
    public class CandidateService : IContextProperty
    {
        public CandidateService()
        {
        }

        #region Private Fields
        private  IMethodInstance methodInstance;
        private  ILobSystemInstance lobSystemInstance;
        private  IExecutionContext executionContext;
        private const string connString = @"Data Source=lobiditstserv;Initial Catalog=TestDB;User ID={0};Password={1};";
        #endregion

        #region IContextProperty implementation

        public IMethodInstance MethodInstance
        {
            get { return this.methodInstance; }
            set { this.methodInstance = value; }
        }

        public  ILobSystemInstance LobSystemInstance
        {
            get { return this.lobSystemInstance; }
            set { this.lobSystemInstance = value; }
        }

        public  IExecutionContext ExecutionContext
        {
            get { return this.executionContext; }
            set { this.executionContext = value; }
        }


        #endregion


        public Candidate ReadItem(int id, string username, string password)
        {
            if (username == null || password == null)
                throw new LobBusinessErrorException("the credentials were not retrieved correctly");

            Candidate candidate = new Candidate();                    
            CandidateInfo candidateInfo;
            String conn = String.Format(connString, username, password);

            candidate.CandidateId = id;
            using (DataClasses1DataContext db = new DataClasses1DataContext(conn))
                {
                    candidateInfo = (from candidateDB in db.GetTable<CandidateInfo>()
                                     where candidateDB.CandidateId == candidate.CandidateId
                                     select candidateDB).SingleOrDefault();


                    FillInDataFromDb(candidate, candidateInfo);
                }
            
            return candidate;
        }

        public Int32[] IdEnumerator(int maxCandidates)
        {

            List<Int32> candidataIds = new List<Int32> ();
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext(connString))
                {
                   var dbCandidateIds = (from candidateDB in db.GetTable<CandidateInfo>()
                                    select candidateDB.CandidateId);

                   foreach (Int32 id in dbCandidateIds)
                   {
                       candidataIds.Add(id);
                   }

                }
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return candidataIds.ToArray();

        }

        public Candidate[] ReadList(string username, string password)
        {
            if (username == null || password == null)
                throw new LobBusinessErrorException("the credentials were not retrieved correctly");

            String conn = String.Format(connString,username,password);
            List<Candidate> candidateList = new List<Candidate>();
            
            using (DataClasses1DataContext db = new DataClasses1DataContext(conn))
                {
                    var allCandidatesInfo = (from candidateDB in db.GetTable<CandidateInfo>()
                                                           select candidateDB);

                     foreach (CandidateInfo candidateInfo in allCandidatesInfo)
                     {
                         Candidate candidate = new Candidate();
                         FillInDataFromDb(candidate, candidateInfo);
                         candidateList.Add(candidate);
                     }
                }
            

           return candidateList.ToArray();
        }

        public Stream GetResumeDetails(Int32 candidateId)
        {
            
             String foldername = this.LobSystemInstance.GetProperties()["FolderName"] as string;
             if (foldername == null)
                 throw new LobBusinessErrorException("the folder information is missing to retrive the resume file");

            SPContext context = GetSPContext();
            if (context == null)
                throw new LobBusinessErrorException("The SPServiceContext is null to get the resume file");

            SPWeb site = context.Web;
            

            string fileName = "Candidate" + candidateId +".docx";
           SPFolder resumeFolder = site.GetFolder(foldername);
           
            if (resumeFolder == null)
                       throw new LobBusinessErrorException("The folder cannot be found. Hence exiting....");

           SPFile file = resumeFolder.Files[fileName];
           Stream resumeStream = file.OpenBinaryStream(SPOpenBinaryOptions.SkipVirusScan);
            if(!resumeStream.CanRead)
            {
                throw new LobBusinessErrorException("The resume file cannot be read");
            }

            return resumeStream;

         }

        private SPContext GetSPContext()
        {
          //  String siteUrl = this.LobSystemInstance.GetProperties()["SiteUrl"] as string;
            SPContext spContext = SPContext.Current;

           
            if (spContext == null)
                throw new LobBusinessErrorException("No service context was found to get the document library info");

            return spContext;
        }
      

        private SPListItemCollection ExecuteSharePointQueryOnServer (SPQuery query, SPContext context, string listName)
        {
            SPWeb mySite = context.Web;
            SPList list = mySite.Lists[listName];
            SPListItemCollection listItems = list.GetItems(query);
            return listItems;




        }

        private void FillInDataFromDb(Candidate candidate, CandidateInfo candidateInfo)
        {

            candidate.CandidateId = candidateInfo.CandidateId;
            candidate.FirstName = candidateInfo.FirstName;
            candidate.LastName = candidateInfo.LastName;
            candidate.MiddleName = candidateInfo.MiddleName;
            candidate.PrimaryEmail = candidateInfo.PrimaryEmail;
            candidate.PrimaryContactNo = candidateInfo.PrimaryContactNo;


        }

        private ISecureStoreProvider GetSecureStoreProvider()
        {
                string ssoProvider = this.LobSystemInstance.GetProperties()["SsoProviderImplementation"] as string;
                if (ssoProvider == null)
                {
                    throw new LobBusinessErrorException("The SSO provider information is missing to retrieve the credentials");
                }

                Type providerType = Type.GetType(ssoProvider);
                return Activator.CreateInstance(providerType)as ISecureStoreProvider;
        }


        private string[] GetCredentialsFromSSS()
        {
            string[] Credentials = new String[2];
             
            //Get the provider info
             string ssoProvider = this.LobSystemInstance.GetProperties()["SsoProviderImplementation"] as string;
                if (ssoProvider == null)
                {
                    throw new LobBusinessErrorException("The SSO provider information is missing to retrieve the credentials");
                }

                Type providerType = Type.GetType(ssoProvider);            
            ISecureStoreProvider provider = (ISecureStoreProvider)Activator.CreateInstance(providerType);
            
            //Get the credentials
            string appTargetName = this.LobSystemInstance.GetProperties()["SecondarySsoApplicationId"] as string;
           
            SecureStoreCredentialCollection credentials = provider.GetCredentials(appTargetName);

            foreach (ISecureStoreCredential cred in credentials)
            {
                if (cred.CredentialType == SecureStoreCredentialType.UserName)
                {
                    Credentials[0] = GetString(cred.Credential);
                }
                else if (cred.CredentialType == SecureStoreCredentialType.Password)
                {
                    Credentials[1] = GetString(cred.Credential);
                }
            }
            
            return Credentials;
        }

        private string GetString(System.Security.SecureString secureString)
        {
            string str = null;
            IntPtr pStr = IntPtr.Zero;

            try
            {
                pStr = Marshal.SecureStringToBSTR(secureString);
                str = Marshal.PtrToStringBSTR(pStr);
            }
            finally
            {
                Marshal.FreeBSTR(pStr);
            }

            return str;
        }
       

       

        
       
    }
}
