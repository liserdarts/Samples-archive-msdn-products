using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.SharePoint.Diagnostics;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Administration.Claims;
using Microsoft.SharePoint.WebControls;
using Microsoft.IdentityModel.Claims;


namespace SqlClaimsProvider
{
    public class SqlClaims : SPClaimProvider
    {

        public SqlClaims(string displayName)
            : base(displayName)
        {
        }


        internal static string ProviderDisplayName
        {
            get
            {
                return "Basketball Teams";
            }
        }


        internal static string ProviderInternalName
        {
            get
            {
                return "BasketballTeamProvider";
            }
        }


        public override string Name
        {
            get
            {
                return ProviderInternalName;
            }
        }
       
        //Keys for our picker hierarchy.
        private string[] teamKeys = new string[] { "empUS", "empEMEA", "empASIA" };

        //Teams we are using.
        private string[] ourTeams = new string[] { "Consolidated Messenger", "Wingtip Toys", "Tailspin Toys" };

        //Labels for our nodes in the picker.
        private string[] teamLabels = new string[] { "US Teams", "European Teams", "Asian Teams" };

        #region ClaimTypeInfo
        private static string SqlClaimType
        {
            get
            {
                return "http://schema.steve.local/teams";
            }
        }

        private static string SqlClaimValueType
        {
            get
            {
                return Microsoft.IdentityModel.Claims.ClaimValueTypes.String;
            }
        }
        #endregion
        
        protected override void FillClaimsForEntity(Uri context, SPClaim entity, 
            List<SPClaim> claims)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (claims == null)
                throw new ArgumentNullException("claims");

            //Figure out who the user is so we know what team to add to their claim.
            string team = string.Empty;
            string userName = string.Empty;
            int userID = 0;
            int userPipe = entity.Value.LastIndexOf("|");
            
            //Get the part after the final pipe, which corresponds to the user name.
            if (userPipe > -1)
                userName = entity.Value.Substring(userPipe + 1);

            //Get the number part after the user prefix so we know what team they
            //are associated with; use a try in case we are not using the FBA provider
            //or naming convention we are not expecting.
            int.TryParse(userName.Substring(4, userName.Length - 4), out userID);
            
            //See if we got a results.
            if (userID > 0)
            {
                //Plug in the appropriate team.
                if (userID > 30)
                    team = ourTeams[2];
                else if (userID > 15)
                    team = ourTeams[1];
                else
                    team = ourTeams[0];
            }
            else
                team = ourTeams[1];  //If they are not one of our FBA users.

            //Add the claim.
            claims.Add(CreateClaim(SqlClaimType, team, SqlClaimValueType));
        }

        protected override void FillClaimTypes(List<string> claimTypes)
        {
            if (claimTypes == null)
                throw new ArgumentNullException("claimTypes");

            //Add our claim type.
            claimTypes.Add(SqlClaimType);
        }

        protected override void FillClaimValueTypes(List<string> claimValueTypes)
        {
            if (claimValueTypes == null)
                throw new ArgumentNullException("claimValueTypes");

            //Add our claim value type.
            claimValueTypes.Add(SqlClaimValueType);
        }

        protected override void FillEntityTypes(List<string> entityTypes)
        {
            //Return the type of entity claim we are using (only one in this case).
            entityTypes.Add(SPClaimEntityTypes.FormsRole);
        }

        protected override void FillHierarchy(Uri context, string[] entityTypes, string hierarchyNodeID, int numberOfLevels, Microsoft.SharePoint.WebControls.SPProviderHierarchyTree hierarchy)
        {
            //Make sure picker is asking for the type of entity we return; site collection admin won't for example.
            if (!EntityTypesContain(entityTypes, SPClaimEntityTypes.FormsRole))
                return;

            //Check to see if the hierarchyNodeID is null; it will be when the control 
            //is first loaded but if a user clicks on one of the nodes it will return
            //the key of the node that was clicked.
            switch (hierarchyNodeID)
            {
                case null:
                    //When it first loads add all our nodes.
                    hierarchy.AddChild(new 
                        Microsoft.SharePoint.WebControls.SPProviderHierarchyNode(
                            SqlClaims.ProviderInternalName, 
                            teamLabels[0], 
                            teamKeys[0], 
                            true));

                    hierarchy.AddChild(new
                        Microsoft.SharePoint.WebControls.SPProviderHierarchyNode(
                            SqlClaims.ProviderInternalName,
                            teamLabels[1],
                            teamKeys[1],
                            true));

                    hierarchy.AddChild(new
                        Microsoft.SharePoint.WebControls.SPProviderHierarchyNode(
                            SqlClaims.ProviderInternalName,
                            teamLabels[2],
                            teamKeys[2],
                            true));
                    break;
                default:
                    break;
            }
        }


        protected override void FillResolve(Uri context, string[] entityTypes, SPClaim resolveInput, List<Microsoft.SharePoint.WebControls.PickerEntity> resolved)
        {
            //Make sure picker is asking for the type of entity we return; site collection admin won't for example.
            if (!EntityTypesContain(entityTypes, SPClaimEntityTypes.FormsRole))
                return;

            //Same sort of code as in search to validate we have a match.
            foreach (string team in ourTeams)
            {
                if (team.ToLower() == resolveInput.Value.ToLower())
                {
                    //We have a match, create a matching entity.
                    PickerEntity pe = GetPickerEntity(team);

                    //Add it to the return list of picker entries.
                    resolved.Add(pe);
                }
            }

        }

        protected override void FillResolve(Uri context, string[] entityTypes, string resolveInput, List<Microsoft.SharePoint.WebControls.PickerEntity> resolved)
        {
            //Make sure picker is asking for the type of entity we return; site collection admin won't for example.
            if (!EntityTypesContain(entityTypes, SPClaimEntityTypes.FormsRole))
                return;

            //Same sort of code as in search to validate we have a match.
            foreach (string team in ourTeams)
            {
                if (team.ToLower() == resolveInput.ToLower())
                {
                    //We have a match, create a matching entity.
                    PickerEntity pe = GetPickerEntity(team);

                    //Add it to the return list of picker entries.
                    resolved.Add(pe);
                }
            }
        }

        protected override void FillSchema(Microsoft.SharePoint.WebControls.SPProviderSchema schema)
        {
            //Add the schema element we need at a minimum in our picker node.
            schema.AddSchemaElement(new 
                SPSchemaElement(PeopleEditorEntityDataKeys.DisplayName, "Display Name", SPSchemaElementType.Both));
        }

        protected override void FillSearch(Uri context, string[] entityTypes, string searchPattern, string hierarchyNodeID, int maxCount, Microsoft.SharePoint.WebControls.SPProviderHierarchyTree searchTree)
        {
            //Make sure search is asking for the type of entity we return; site collection admin won't for example.
            if (!EntityTypesContain(entityTypes, SPClaimEntityTypes.FormsRole))
                return;

            //Counter to track what node we are in.
            int teamNode = -1;

            //Nodes where we will stick our matches.
            Microsoft.SharePoint.WebControls.SPProviderHierarchyNode matchNode = null;

            //Look to see if the value that is typed in matches any of our teams.
            foreach (string team in ourTeams)
            {
                //Increment team node tracker.
                teamNode += 1;

                if (team.ToLower().StartsWith(searchPattern.ToLower()))
                {
                    //We have a match, create a matching entity.
                    PickerEntity pe = GetPickerEntity(team);

                    //Add the team node where it should be displayed too.
                    if (!searchTree.HasChild(teamKeys[teamNode]))
                    {
                        //Create the node so we can show our match in there too.
                        matchNode = new
                            SPProviderHierarchyNode(SqlClaims.ProviderInternalName,
                            teamLabels[teamNode],
                            teamKeys[teamNode],
                            true);

                        //Add it to the tree.
                        searchTree.AddChild(matchNode);
                    }
                    else
                        //Get the node for this team.
                        matchNode = searchTree.Children.Where(theNode =>
                            theNode.HierarchyNodeID == teamKeys[teamNode]).First();

                    //Add the match to our node.
                    matchNode.AddEntity(pe);
                }
            }
        }


        private PickerEntity GetPickerEntity(string ClaimValue)
        {
            //We have a match, create a matching entity.
            PickerEntity pe = CreatePickerEntity();

            //Set the claim associated with this match.
            pe.Claim = CreateClaim(SqlClaimType, ClaimValue, SqlClaimValueType);

            //Set the tooltip that is displayed when you hover over the resolved claim.
            pe.Description = SqlClaims.ProviderDisplayName + ":" + ClaimValue;

            //Set the text we will display.
            pe.DisplayText = ClaimValue;

            //Store it here too in the hashtable **.
            pe.EntityData[PeopleEditorEntityDataKeys.DisplayName] = ClaimValue;

            //User entity.
            pe.EntityType = SPClaimEntityTypes.FormsRole;

            //Flag the entry as being resolved.
            pe.IsResolved = true;

            //This is the first part of the description that shows
            //up above the matches, like Role: Forms Authentication when
            //you do an FBA search and find a matching role.
            pe.EntityGroupName = "Favorite Team";

            return pe;
        }


        public override bool SupportsEntityInformation
        {
            get 
            { 
                return true; 
            }
        }

        public override bool SupportsHierarchy
        {
            get 
            { 
                return true; 
            }
        }

        public override bool SupportsResolve
        {
            get 
            { 
                return true; 
            }
        }

        public override bool SupportsSearch
        {
            get 
            { 
                return true; 
            }
        }
    }
}
