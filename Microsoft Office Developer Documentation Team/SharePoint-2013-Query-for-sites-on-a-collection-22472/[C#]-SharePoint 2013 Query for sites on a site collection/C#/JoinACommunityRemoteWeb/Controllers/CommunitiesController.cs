using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search;
using Microsoft.SharePoint.Client.Search.Query;
using JoinACommunityRemoteWeb.Models;

namespace JoinACommunityRemoteWeb.Controllers
{
    public class CommunitiesController : Controller
    {

        public ActionResult Index(string SPHostUrl,string keyword)
        {

            Uri hostWebUri = new Uri(SPHostUrl);
            string loginName = string.Empty;
            List<Community> communities = new List<Community>();

            //Search for the Top 25 communities based on membership count
            using (var ctx = TokenHelper.GetS2SClientContextWithWindowsIdentity(hostWebUri, Request.LogonUserIdentity))
            {
                //Get the user's login Name
                User currentUser = ctx.Web.CurrentUser;
                ctx.Load(currentUser, u => u.LoginName);
                ctx.ExecuteQuery();
                loginName = currentUser.LoginName;

                //Limit search to the current site collection
                Site siteCollection = ctx.Site;
                ctx.Load(siteCollection, s => s.Url);
                ctx.ExecuteQuery();
                
                //Prepare the query
                KeywordQuery keywordQuery = new KeywordQuery(ctx);
                keywordQuery.QueryText = keyword + " WebTemplate=COMMUNITY +Path:" + siteCollection.Url;
                keywordQuery.SelectProperties.Add("Title");
                keywordQuery.SelectProperties.Add("Description");
                keywordQuery.SelectProperties.Add("CommunityMembersCount");
                keywordQuery.SelectProperties.Add("Path");
                keywordQuery.SortList.Add("CommunityMembersCount", SortDirection.Descending);
                keywordQuery.RowLimit = 25;

                //Execute the query
                SearchExecutor searchExecutor = new SearchExecutor(ctx);
                ClientResult<ResultTableCollection> searchResults = searchExecutor.ExecuteQuery(keywordQuery);
                ctx.ExecuteQuery();

                //Collect the results
                ResultTable resultTable = searchResults.Value.Where<ResultTable>(r => r.TableType == "RelevantResults").FirstOrDefault();
                foreach (var resultRow in resultTable.ResultRows)
                {
                    communities.Add(new Community()
                    {
                        Title = resultRow["Title"].ToString(),
                        Description = resultRow["Description"].ToString(),
                        Membership = int.Parse(resultRow["CommunityMembersCount"].ToString()),
                        SiteRelativeUrl = resultRow["Path"].ToString().Substring(siteCollection.Url.Length),
                        FullUrl = resultRow["Path"].ToString()
                    });
                }

            }

            //Determine if the current user is a member of each community
            //This is done with app-only access to read the membership lists
            string appOnlyAccessTokenString = TokenHelper.GetS2SAccessTokenWithWindowsIdentity(hostWebUri, null);
            using (ClientContext ctx = TokenHelper.GetClientContextWithAccessToken(SPHostUrl, appOnlyAccessTokenString))
            {

                //Check each community
                foreach (Community community in communities)
                {
                    List memberList = ctx.Site.OpenWeb(community.SiteRelativeUrl).Lists.GetByTitle("Community Members");
                    ctx.Load(memberList);

                    StringBuilder caml = new StringBuilder();
                    caml.Append("<View><Query><Where><Eq><FieldRef Name='Name'/><Value Type='Text'>");
                    caml.Append(loginName);
                    caml.Append("</Value></Eq></Where></Query>");
                    caml.Append("<Joins><Join Type='Inner' ListAlias='Users'><Eq><FieldRef Name='Member' RefType='Id'/>");
                    caml.Append("<FieldRef List='Users' Name='Id'/></Eq></Join></Joins>");
                    caml.Append("<ProjectedFields><Field Name='Name' Type='Lookup' List='Users' ShowField='Name'/></ProjectedFields>");
                    caml.Append("<ViewFields><FieldRef Name='Name'/></ViewFields></View>");
                    CamlQuery query = new CamlQuery();
                    query.ViewXml = caml.ToString();

                    ListItemCollection members = memberList.GetItems(query);
                    ctx.Load(members,m=>m.Include(i=>i["Name"]));
                    ctx.ExecuteQuery();

                    if (members.Count == 1)
                        community.CurrentUserIsMember = true;

                }

            }

            //Display communities
            ViewBag.Url = Request.Url.AbsolutePath;
            ViewBag.SPHostUrl = SPHostUrl;
            ViewBag.Communities = communities;
            if (keyword==null)
                return View();
            else
                return PartialView();
        }

        public ActionResult Questions(string SPHostUrl, string siteRelativeUrl)
        {
            Uri hostWebUri = new Uri(SPHostUrl);
            List<Question> questions = new List<Question>();

            //Get the top 10 questions from the community site
            //This is done with app-only access to read the membership lists
            string appOnlyAccessTokenString = TokenHelper.GetS2SAccessTokenWithWindowsIdentity(hostWebUri, null);
            using (ClientContext ctx = TokenHelper.GetClientContextWithAccessToken(SPHostUrl, appOnlyAccessTokenString))
            {
                List discussionList = ctx.Site.OpenWeb(siteRelativeUrl).Lists.GetByTitle("Discussions List");
                ctx.Load(discussionList);

                StringBuilder caml = new StringBuilder();
                caml.Append("<View><Query>");
                caml.Append("<Where><Eq><FieldRef Name='IsQuestion'/><Value Type='Numeric'>1</Value></Eq></Where>");
                caml.Append("<OrderBy><FieldRef Name='Popularity' Ascending='false'/></OrderBy>");
                caml.Append("</Query><ViewFields><FieldRef Name='Title'/><FieldRef Name='Body'/><FieldRef Name='Created'/></ViewFields></View>");
                CamlQuery query = new CamlQuery();
                query.ViewXml = caml.ToString();

                ListItemCollection discussionItems = discussionList.GetItems(query);
                ctx.Load(discussionItems, m => m.Include(i => i["Title"], i => i["Body"], i => i["Created"]));
                ctx.ExecuteQuery();

                foreach (ListItem discussionItem in discussionItems)
                {
                    questions.Add(new Question()
                    {
                        Title = discussionItem["Title"].ToString(),
                        Body = discussionItem["Body"].ToString(),
                        Created = DateTime.Parse(discussionItem["Created"].ToString())
                    });
                }

            }

            ViewBag.Questions = questions;
            return PartialView();
        }
    }
}
