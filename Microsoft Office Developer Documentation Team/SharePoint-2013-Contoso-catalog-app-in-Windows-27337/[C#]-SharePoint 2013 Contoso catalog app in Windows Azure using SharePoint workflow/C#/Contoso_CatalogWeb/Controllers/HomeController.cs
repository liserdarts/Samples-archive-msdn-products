/*
 * Developed by:    Martin Harwar, www.Point8020.com
 * Developed for:   MSDN and SharePoint Product group
 * First released:  14th February, 2014
 */ 

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SharePoint.Client;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace Contoso_CatalogWeb.Controllers
{
    public class HomeController : Controller
    {

        /*
        * The following declarations are class-level variables for 
        * holding various bits of information from talking to SharePoint.
        */
        private bool isProductDirector = false;
        private bool isProductManager = false;
        private int productDirectorCount = 0;
        private int productManagerCount = 0;
        private string ownersGroupAlias = string.Empty;


        /*
         * The Index method uses the Model classes to retrieve data from the database.
         * The data retrieved from the Model classes represents Categories.
         * The method then builds various members of the ViewBag object, so that the Index view can render the data.
         */ 
        [SharePointContextFilter]
        public ActionResult Index()
        {
            List<string> statusChecks = checkStatus();
            ViewBag.DatabaseCheck = statusChecks[0];
            if (ViewBag.DatabaseCheck != "Success!")
            {
                return (RedirectToAction("Setup", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
            getUserName();
            ViewBag.Title = "Categories";
            Models.CatalogData cData = new Models.CatalogData();
            List<Models.Category> categories = cData.CategoryList();
            List<string> categoryIDs = new List<string>();
            List<string> categoryNames = new List<string>();
            List<string> categoryDescriptions = new List<string>();
            List<string> categoryImages = new List<string>();
            foreach (Models.Category category in categories)
            {
                categoryIDs.Add(category.CategoryID);
                categoryNames.Add(category.CategoryName);
                categoryDescriptions.Add(category.CategoryDescription);
                categoryImages.Add(category.CategoryImageURL);
            }
            ViewBag.CategoryIDs = categoryIDs;
            ViewBag.CategoryNames = categoryNames;
            ViewBag.CategoryDescriptions = categoryDescriptions;
            ViewBag.CategoryImages = categoryImages;
            return View();
        }

        /*
         * The CategoryDetails method uses the Model classes to retrieve data from the database.
         * The data retrieved from the Model classes represents products in a given category (as specified by the CategoryID parameter).
         * The method then builds various members of the ViewBag object, so that the CategoryDetails view can render the data.
         */
        [SharePointContextFilter]
        public ActionResult CategoryDetails(string CategoryID, string CategoryName)
        {
            getUserName();
            ViewBag.Title = CategoryName;
            Models.CatalogData cData = new Models.CatalogData();
            List<Models.Product> products = cData.ProductList(CategoryID);
            List<string> productIDs = new List<string>();
            List<string> productNames = new List<string>();
            List<decimal> productPrices = new List<decimal>();
            List<string> productDescriptions = new List<string>();
            List<string> productImages = new List<string>();
            List<bool> productHasWorkflow = new List<bool>();
            foreach (Models.Product product in products)
            {
                productIDs.Add(product.ProductID);
                productNames.Add(product.ProductName);
                productPrices.Add(product.ProductPrice);
                productDescriptions.Add(product.ProductDescription);
                productImages.Add(product.ProductImageURL);
                productHasWorkflow.Add(DoesProductHaveProposal(product.ProductID));
            }
            ViewBag.CategoryID = CategoryID;
            ViewBag.ProductIDs = productIDs;
            ViewBag.ProductNames = productNames;
            ViewBag.ProductPrices = productPrices;
            ViewBag.ProductDescriptions = productDescriptions;
            ViewBag.ProductImages = productImages;
            ViewBag.ProductHasWorkflow = productHasWorkflow;
            if (productIDs.Count > 0)
            {
                ViewBag.Message = "Click a product to view details";
            }
            else
            {
                ViewBag.Message = "This category contains no products. You can add new products from here.";
            }

            return View();
        }

        /*
         * The DoesProductHaveProposal helper function uses the SharePoint client-side object model to query the Proposals list that was created when the app was installed.
         * Specifically, the function determines whether the product identified byt he ProductID parameter has any existing proposals.
         * The return boolean value is used to control whether the Workflow icon is shown to Product Directors.
         */
        private bool DoesProductHaveProposal(string ProductID)
        {
            bool hasProposal = false;
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var appContext = spContext.CreateUserClientContextForSPAppWeb())
            {
                if (appContext != null)
                {
                    Web appWeb = appContext.Web;
                    appContext.Load(appWeb);
                    List proposalList = appWeb.Lists.GetByTitle("Proposals");
                    CamlQuery camlQuery = new CamlQuery();
                    camlQuery.ViewXml = "<View><Query><Where><And><Eq><FieldRef Name='ProductID'/><Value Type='Text'>" + ProductID + "</Value></Eq><Eq><FieldRef Name='Status'/><Value Type='Text'>Pending</Value></Eq></And></Where></Query></View>";
                    ListItemCollection proposals = proposalList.GetItems(camlQuery);
                    appContext.Load(proposalList);
                    appContext.Load(proposals);
                    appContext.ExecuteQuery();
                    if (proposals.Count > 0)
                    {
                        hasProposal = true;
                    }
                }
            }
            return (hasProposal);
        }

        /*
         * The Approve method uses the Model classes to update data in the database.
         * It also calls the setApprovalStatus helper function to update the Proposals list in SharePoint.
         */
        public ActionResult Approve(string proposalID, string productID, string newPrice)
        {
            Models.CatalogData cData = new Models.CatalogData();
            bool success = cData.UpdateProductPrice(productID, double.Parse(newPrice));
            if (success)
            {
                setApprovalStatus(proposalID);
            }
            return (RedirectToAction("Proposals", "Home", new { ProductID = productID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
        }

        /*
         * The Reject method calls the setApprovalStatus helper function to update the Proposals list in SharePoint.
         */
        public ActionResult Reject(string proposalID, string productID)
        {
            setApprovalStatus(proposalID);
            return (RedirectToAction("Proposals", "Home", new { ProductID = productID, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
        }

        /*
        * The setApprovalStatus helper function updates the Proposals list in SharePoint.
        */
        private void setApprovalStatus(string ProposalID)
        {
            getUserName();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var appContext = spContext.CreateUserClientContextForSPAppWeb())
            {
                if (appContext != null)
                {
                    Web appWeb = appContext.Web;
                    appContext.Load(appWeb);
                    List proposalList = appWeb.Lists.GetByTitle("Proposals");
                    ListItem proposal = proposalList.GetItemById(ProposalID);
                    proposal["Status"] = "Reviewed";
                    proposal["ReviewedBy"] = ViewBag.UserName;
                    proposal.Update();
                    appContext.ExecuteQuery();
                }
            }
        }

        /*
        * The Proposals method retrieves a list of price change proposals that are
        * still pending from the SharePoint Proposals list. The CAML query includes
        * a clause to limit the results to the product identified by the ProductID parameter.
        */
        public ActionResult Proposals(string ProductID)
        {
            getUserName();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var appContext = spContext.CreateUserClientContextForSPAppWeb())
            {
                if (appContext != null)
                {
                    Web appWeb = appContext.Web;
                    appContext.Load(appWeb);
                    List proposalList = appWeb.Lists.GetByTitle("Proposals");
                    CamlQuery camlQuery = new CamlQuery();
                    camlQuery.ViewXml = "<View><Query><Where><And><Eq><FieldRef Name='ProductID'/><Value Type='Text'>" + ProductID + "</Value></Eq><Eq><FieldRef Name='Status'/><Value Type='Text'>Pending</Value></Eq></And></Where></Query></View>";
                    ListItemCollection proposals = proposalList.GetItems(camlQuery);
                    appContext.Load(proposalList);
                    appContext.Load(proposals);
                    appContext.ExecuteQuery();
                    List<string> proposalIDs = new List<string>();
                    List<string> productIDs = new List<string>();
                    List<string> productNames = new List<string>();
                    List<string> productCurrentPrices = new List<string>();
                    List<string> productProposedPrices = new List<string>();
                    List<string> productProposedBys = new List<string>();
                    if (proposals.Count == 0)
                    {
                        return (RedirectToAction("Index", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
                    }
                    foreach (ListItem proposal in proposals)
                    {
                        ViewBag.NameTitle = proposal.FieldValues["ProductName"].ToString();
                        proposalIDs.Add(proposal.Id.ToString());
                        productIDs.Add(proposal.FieldValues["ProductID"].ToString());
                        productNames.Add(proposal.FieldValues["ProductName"].ToString());
                        productCurrentPrices.Add(proposal.FieldValues["ExistingPrice"].ToString());
                        //string price = "$" + String.Format("{0:C}", proposal.FieldValues["ProposedPrice"].ToString());
                        productProposedPrices.Add(proposal.FieldValues["ProposedPrice"].ToString());
                        productProposedBys.Add(proposal.FieldValues["ProposedBy"].ToString());
                    }
                    ViewBag.ProposalIDs = proposalIDs;
                    ViewBag.ProductIDs = productIDs;
                    ViewBag.ProductNames = productNames;
                    ViewBag.ProductCurrentPrices = productCurrentPrices;
                    ViewBag.ProductProposedPrices = productProposedPrices;
                    ViewBag.ProductProposedBys = productProposedBys;
                    return View();
                }
                return (RedirectToAction("Index", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
            }
        }

        /*
        * The CreateCategory method uses the Model classes to add data for a new category to the database.
        */
        [SharePointContextFilter]
        public ActionResult CreateCategory(string CategoryName, string CategoryDescription, string CategoryImageURL)
        {
            Models.CatalogData cData = new Models.CatalogData();
            Models.Category category = cData.AddCategory(CategoryName, CategoryDescription, CategoryImageURL);
            return (RedirectToAction("CategoryDetails", "Home", new { CategoryID = category.CategoryID, CategoryName = category.CategoryName, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
        }

        /*
        * The SaveCategory method uses the Model classes to update data for an existing category in the database.
        */
        [SharePointContextFilter]
        public ActionResult SaveCategory(string CategoryID, string CategoryName, string CategoryDescription, string CategoryImageURL)
        {
            Models.CatalogData cData = new Models.CatalogData();
            Models.Category category = cData.UpdateCategory(CategoryID, CategoryName, CategoryDescription, CategoryImageURL);
            return (RedirectToAction("Index", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
        }

        /*
        * The DeleteCategory method uses the Model classes to delete data for an existing category in the database.
        */
        public ActionResult DeleteCategory(string CategoryID)
        {
            Models.CatalogData cData = new Models.CatalogData();
            bool result = cData.DeleteCategory(CategoryID);
            return (RedirectToAction("Index", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
        }

        /*
        * The DeleteProduct method uses the Model classes to delete data for an existing category in the database.
        */
        public ActionResult DeleteProduct(string ProductID, string CategoryID, string CategoryName)
        {
            Models.CatalogData cData = new Models.CatalogData();
            bool result = cData.DeleteProduct(ProductID);
            return (RedirectToAction("CategoryDetails", "Home", new { CategoryID = CategoryID, CategoryName = CategoryName, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
        }


        /*
        * The CreateProduct method uses the Model classes to add data for a new product to the database.
        */
        [SharePointContextFilter]
        public ActionResult CreateProduct(string CategoryID, string ProductName, string ProductDescription, string ProductImageURL, string CategoryName)
        {
            Models.CatalogData cData = new Models.CatalogData();
            Models.Product product = cData.AddProduct(CategoryID, ProductName, ProductDescription, ProductImageURL);
            return (RedirectToAction("CategoryDetails", "Home", new { CategoryID = CategoryID, CategoryName = CategoryName, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
        }

        /*
        * The SaveProduct method uses the Model classes to update data for an existing product in the database.
        */
        [SharePointContextFilter]
        public ActionResult SaveProduct(string CategoryID, string ProductID, string ProductName, string ProductPrice, string ProposedPrice, string ProductDescription, string ProductImageURL, string CategoryName)
        {
            getUserName();
            Models.CatalogData cData = new Models.CatalogData();
            Models.Product product = cData.UpdateProduct(CategoryID, ProductID, ProductName, ProductDescription, ProductImageURL);
            if (!string.IsNullOrEmpty(ProposedPrice))
            {
                string appUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                if (appUrl.EndsWith(":444/"))
                {
                    appUrl = appUrl.Replace(":444/", "/");
                }
                var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
                using (var appContext = spContext.CreateUserClientContextForSPAppWeb())
                {
                    if (appContext != null)
                    {
                        Web appWeb = appContext.Web;
                        appContext.Load(appWeb);
                        List proposalList = appWeb.Lists.GetByTitle("Proposals");
                        appContext.Load(proposalList);
                        appContext.ExecuteQuery();
                        ListItemCreationInformation itemInfo = new ListItemCreationInformation();
                        ListItem newProposal = proposalList.AddItem(itemInfo);
                        newProposal["Title"] = ProductName + ": New Price Change Proposal";
                        newProposal["ProductID"] = ProductID;
                        newProposal["ProductName"] = ProductName;
                        newProposal["ExistingPrice"] = ProductPrice;
                        newProposal["ProposedPrice"] = ProposedPrice;
                        newProposal["ProposedBy"] = ViewBag.UserName;
                        newProposal["Status"] = "Pending";
                        newProposal["ApproversAlias"] = ownersGroupAlias;
                        newProposal["AppURL"] = appUrl;
                        newProposal.Update();
                        appContext.ExecuteQuery();
                    }
                }
            }
            return (RedirectToAction("CategoryDetails", "Home", new { CategoryID = CategoryID, CategoryName = CategoryName, SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri }));
        }

        /*
        * The Setup method checks for database connectivity. It also checks for the existence of the 'Proposals' list
        */
        [SharePointContextFilter]
        public ActionResult Setup()
        {
            getUserName();
            ViewBag.Title = "Contoso Catalog";
            List<string> statusChecks = checkStatus();
            ViewBag.DatabaseCheck = statusChecks[0];
            ViewBag.DirectorsCheck = statusChecks[1];
            ViewBag.ManagersCheck = statusChecks[2];
            try
            {
                var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
                using (var appContext = spContext.CreateUserClientContextForSPAppWeb())
                {
                    if (appContext != null)
                    {
                        Web appWeb = appContext.Web;
                        appContext.Load(appWeb);
                        List proposalList = appWeb.Lists.GetByTitle("Proposals");
                        appContext.Load(proposalList);
                        appContext.ExecuteQuery();
                        ViewBag.ListCheck = "Success!";
                    }
                }
            }
            catch
            {
                ViewBag.ListCheck = "Warning: The 'Proposals' list in the AppWeb does not exist";
            }
            return View();
        }


        /*
        * The getUserName method uses the SharePoint client-side object model to:
         * 1. Retrive the currently logged-on user's Email address for display in the app
         * 2. Determine whether the currently logged-on user should be considered to be a Product Director
         * 3. Determine whether the currently logged-on user should be considered to be a Product Manager
         * 4. Retreive a count of all site users who should be considered to be a Product Director or Product Manager
        */
        [SharePointContextFilter]
        private void getUserName()
        {
            User spUser = null;
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    spUser = clientContext.Web.CurrentUser;
                    clientContext.Load(spUser);
                    clientContext.Load(clientContext.Web.CurrentUser.Groups);
                    clientContext.Load(clientContext.Web.SiteGroups);
                    clientContext.ExecuteQuery();
                    ViewBag.UserName = spUser.Email;

                    foreach (Group group in clientContext.Web.CurrentUser.Groups)
                    {
                        if (group.Title.Contains("Owners"))
                        {
                            isProductDirector = true;
                            break;
                        }
                    }
                    if (!isProductDirector)
                    {
                        foreach (Group group in clientContext.Web.CurrentUser.Groups)
                        {
                            if (group.Title.Contains("Members"))
                            {
                                isProductManager = true;
                                break;
                            }
                        }
                    }
                    ViewBag.UserName = spUser.Email;
                    ViewBag.IsProductDirector = isProductDirector;
                    ViewBag.IsProductManager = isProductManager;
                    //if (isProductDirector || isProductManager)
                    //{
                    foreach (Group group in clientContext.Web.SiteGroups)
                    {
                        if (group.Title.Contains("Owners"))
                        {
                            ownersGroupAlias = group.LoginName;
                            clientContext.Load(group.Users);
                            clientContext.ExecuteQuery();
                            productDirectorCount = group.Users.Count;
                        }
                        if (group.Title.Contains("Members"))
                        {
                            clientContext.Load(group.Users);
                            clientContext.ExecuteQuery();
                            productManagerCount = group.Users.Count;
                        }
                    }
                    //}
                }
            }
        }

        /*
         * The checkStatus method uses ADO.NET directly to determine whether the database is contactable.
         * It also prepares the ViewBag data with previosuly-retrieved data that will be used to populate the
         * UI in the Setup view
        */
        [SharePointContextFilter]
        private List<string> checkStatus()
        {
            List<string> statusList = new List<string>();
            string connStr = string.Empty;
            if (ConfigurationManager.ConnectionStrings["ContosoCatalogDB"] != null)
            {
                SqlConnection sqlCon = new SqlConnection();
                try
                {
                    sqlCon.ConnectionString = ConfigurationManager.ConnectionStrings["ContosoCatalogDB"].ConnectionString;
                    sqlCon.Open();
                    // Database Connection
                    statusList.Add("Success!");
                    sqlCon.Close();

                    // Product Director Role Check
                    if (productDirectorCount > 0)
                    {
                        statusList.Add("Success!");
                    }
                    else
                    {
                        statusList.Add("Warning: No Product Directors have yet been defined. Product Directors should be added to the 'Owners' group in the host site.");
                    }

                    // Product Manager Role Check
                    if (productManagerCount > 0)
                    {
                        statusList.Add("Success!");
                    }
                    else
                    {
                        statusList.Add("Warning: No Product Managers have yet been defined. Product Managers should be added to the 'Members' group in the host site.");
                    }

                }
                catch
                {
                    statusList = new List<string>();

                    // Database Connection
                    statusList.Add("The data source referred to by the ContosoCatalogDB connectionString cannot be contacted or is incorrectly configured. Please refer to the readme file that accompanies this app for instructions on how to set up the app.");

                    // Product Director Role Check
                    statusList.Add("Not Checked");

                    // Product Manager Role Check
                    statusList.Add("Not Checked");

                }
                finally
                {
                    sqlCon.Dispose();
                }
                return (statusList);
            }
            statusList = new List<string>();

            // Database Connection
            statusList.Add("The ContosoCatalogDB connectionString element has not been set in Web.config. Please refer to the readme file that accompanies this app for instructions on how to set up the app.");

            // Product Director Role Check
            statusList.Add("Not Checked");

            // Product Manager Role Check
            statusList.Add("Not Checked");

            return (statusList);
        }
    }
}
