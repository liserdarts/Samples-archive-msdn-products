using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.Claims;
using Microsoft.IdentityModel.Claims;
using System.Collections;
using Microsoft.SharePoint.WebControls;

namespace ContosoClaimProviders
{
    public class CRMClaimProvider : SPClaimProvider
    {

        public CRMClaimProvider(string displayName)
            : base(displayName)
        {
        }

        public override string Name
        {
            get { return "ContosoCRMClaimProvider"; }
        }

        /// <summary>
        /// Return true if you are doing claim augmentation.
        /// </summary>
        public override bool SupportsEntityInformation
        {
            get { return true; }
        }

        /// <summary>
        /// Return true if you support claims resolution in the People Picker.
        /// </summary>
        public override bool SupportsResolve
        {
            get { return true; }
        }

        /// <summary>
        /// Return true if you support searching claims in the People Picker.
        /// </summary>
        public override bool SupportsSearch
        {
            get { return true; }
        }

        /// <summary>
        /// Return true if you support hierarchy display in the People Picker.
        /// </summary>
        public override bool SupportsHierarchy
        {
            get { return false; }
        }

        
        public override bool SupportsUserSpecificHierarchy
        {
            get
            {
                return base.SupportsUserSpecificHierarchy;
            }
        }

        /// <summary>
        /// Implement this method if the claims provider supports claims augmentation.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity"></param>
        /// <param name="claims"></param>
        protected override void FillClaimsForEntity(Uri context, SPClaim entity, List<SPClaim> claims)
        {
            if (null == entity)
            {
                throw new ArgumentNullException("entity");
            }
            if (null == claims)
            {
                throw new ArgumentNullException("claims");
            }

            //Add the role claim.
            SPClaim userIdClaim = SPClaimProviderManager.DecodeUserIdentifierClaim(entity);

            //Add claims for users.
            List<string> allCRMUsers = CRMUserInfo.GetAllUsers();
            
            if(allCRMUsers.Contains(userIdClaim.Value.ToLower()))
            {
                List<Claim> userClaims = CRMUserInfo.GetClaimsForUser(userIdClaim.Value.ToLower());
                foreach(Claim claim in userClaims)
                {
                    claims.Add(CreateClaim(claim.ClaimType, claim.Value, claim.ValueType));
                }

            }

        }

        /// <summary>
        /// Returns all the claim types that are supported by this claims provider.
        /// </summary>
        /// <param name="claimTypes"></param>
        protected override void FillClaimTypes(List<string> claimTypes)
        {
            if (null == claimTypes)
            {
                throw new ArgumentNullException("claimTypes");
            }

            // Add the claim types that will be added by this claims provider.  
            claimTypes.Add(CRMClaimType.Role);
            claimTypes.Add(CRMClaimType.Region);
        }

        /// <summary>
        /// Return all claim value types that correspond to the claim types.
        /// You have to return the values in the same order as in the FillClaimTypes method.
        /// </summary>
        /// <param name="claimValueTypes"></param>
        protected override void FillClaimValueTypes(List<string> claimValueTypes)
        {

            if (null == claimValueTypes)
            {
                throw new ArgumentNullException("claimValueTypes");
            }

            claimValueTypes.Add(Microsoft.IdentityModel.Claims.ClaimValueTypes.String);
            claimValueTypes.Add(Microsoft.IdentityModel.Claims.ClaimValueTypes.String);
        }

        /// <summary>
        /// Required for the People Picker. This tells the People Picker what information are available
        /// for the entity.
        /// </summary>
        /// <param name="schema"></param>
        protected override void FillSchema(SPProviderSchema schema)
        {
            schema.AddSchemaElement(new SPSchemaElement(PeopleEditorEntityDataKeys.DisplayName,
                                                        "DisplayName",
                                                         SPSchemaElementType.TableViewOnly));
        }

        /// <summary>
        /// Returns the entity type for the claims returned from the claims provider.
        /// </summary>
        /// <param name="entityTypes"></param>
        protected override void FillEntityTypes(List<string> entityTypes)
        {
            entityTypes.Add(SPClaimEntityTypes.FormsRole);
            entityTypes.Add(SPClaimEntityTypes.FormsRole);
        }


        /// <summary>
        /// Required if you implement claims search in the People Picker.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entityTypes"></param>
        /// <param name="searchPattern"></param>
        /// <param name="hierarchyNodeID"></param>
        /// <param name="maxCount"></param>
        /// <param name="searchTree"></param>
        protected override void FillSearch(Uri context, string[] entityTypes, string searchPattern, string hierarchyNodeID, int maxCount, SPProviderHierarchyTree searchTree)
        {
            string keyword = searchPattern.ToLower();
            Hashtable knownClaims = CRMUserInfo.GetAllClaims();
            List<string> knownClaimsList = new List<string>();

            //Convert the knownClaims.Key into a List<string> for LINQ query.
            foreach (string claim in knownClaims.Keys)
            {
                knownClaimsList.Add(claim);
            }
            

            var claimQuery = knownClaimsList.Where(claim => claim.IndexOf(keyword) >= 0).Select(claim => claim);

            foreach (string claimValue in claimQuery)
            {
                //Get the ClaimType for the claim type.
                //For example, if you search "SalesManager", the ClaimType will be CRMClaimType.Role.

                string claimType = CRMUserInfo.GetClaimTypeForRole((string)knownClaims[claimValue]);

                PickerEntity entity = CreatePickerEntity();
                entity.Claim = CreateClaim(claimType, claimValue, Microsoft.IdentityModel.Claims.ClaimValueTypes.String);
                entity.Description = claimValue;
                entity.DisplayText = claimValue;
                entity.EntityData[PeopleEditorEntityDataKeys.DisplayName] = claimValue;
                entity.EntityType = SPClaimEntityTypes.FormsRole;
                entity.IsResolved = true;
                searchTree.AddEntity(entity);
            }

        }

        /// <summary>
        /// Resolve a single claim using exact match. This method is required for both claim search
        /// and resolve.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entityTypes"></param>
        /// <param name="resolveInput"></param>
        /// <param name="resolved"></param>
        protected override void FillResolve(Uri context, string[] entityTypes, SPClaim resolveInput, List<PickerEntity> resolved)
        {
            string keyword = resolveInput.Value.ToLower();
            Hashtable knownClaims = CRMUserInfo.GetAllClaims();

            if (knownClaims.ContainsKey(keyword))
            {
                //Get the ClaimType for the claim type.
                //For example, if you are search "SalesManager", the ClaimType will be CRMClaimType.Role.
                //In this case, the keyword is the value of the claim.
                string claimValue = keyword;
                string claimType = CRMUserInfo.GetClaimTypeForRole((string)knownClaims[keyword]);

                PickerEntity entity = CreatePickerEntity();
                entity.Claim = CreateClaim(claimType, claimValue, Microsoft.IdentityModel.Claims.ClaimValueTypes.String);
                entity.Description = claimValue;
                entity.DisplayText = claimValue;
                entity.EntityData[PeopleEditorEntityDataKeys.DisplayName] = claimValue;
                entity.EntityType = SPClaimEntityTypes.FormsRole;
                entity.IsResolved = true;
                resolved.Add(entity);
            }
        }


        /// <summary>
        /// Required if you implement claim resolve in the People Picker.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entityTypes"></param>
        /// <param name="resolveInput"></param>
        /// <param name="resolved"></param>
        protected override void FillResolve(Uri context, string[] entityTypes, string resolveInput, List<PickerEntity> resolved)
        {

            string keyword = resolveInput.ToLower();
            Hashtable knownClaims = CRMUserInfo.GetAllClaims();
            List<string> knownClaimsList = new List<string>();

            //Convert the knownClaims.Key into a List<string> for LINQ query.
            foreach (string claim in knownClaims.Keys)
            {
                knownClaimsList.Add(claim);
            }

            var claimQuery = knownClaimsList.Where(claim => claim.IndexOf(keyword) >= 0).Select(claim => claim);

            foreach (string claimValue in claimQuery)
            {
                //Get the ClaimType for the claim type.
                //For example, if you are search "SalesManager", the ClaimType will be CRMClaimType.Role.

                string claimType = CRMUserInfo.GetClaimTypeForRole((string)knownClaims[claimValue]);

                PickerEntity entity = CreatePickerEntity();
                entity.Claim = CreateClaim(claimType, claimValue, Microsoft.IdentityModel.Claims.ClaimValueTypes.String);
                entity.Description = claimValue;
                entity.DisplayText = claimValue;
                entity.EntityData[PeopleEditorEntityDataKeys.DisplayName] = claimValue;
                entity.EntityType = SPClaimEntityTypes.FormsRole;
                entity.IsResolved = true;
                resolved.Add(entity);
            }

        }

        protected override void FillHierarchy(Uri context, string[] entityTypes, string hierarchyNodeID, int numberOfLevels, SPProviderHierarchyTree hierarchy)
        {
            throw new NotImplementedException();
        }

        
    }
}
