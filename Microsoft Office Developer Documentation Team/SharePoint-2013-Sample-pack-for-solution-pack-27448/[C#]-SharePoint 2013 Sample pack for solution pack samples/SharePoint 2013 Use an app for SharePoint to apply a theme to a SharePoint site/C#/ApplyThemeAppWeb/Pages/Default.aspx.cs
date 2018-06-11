using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using Microsoft.SharePoint.Client;

namespace ApplyThemeAppWeb.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        public string _ContextToken 
        {
            get
            {
                if (ViewState["ContextToken"] == null)
                    return null;
                return ViewState["ContextToken"].ToString();
            }
            set
            {
                ViewState["ContextToken"] = value;
            }
        }

        public string _HostWeb
        {
            get
            {
                if (ViewState["HostWeb"] == null)
                    return null;
                return ViewState["HostWeb"].ToString();
            }
            set
            {
                ViewState["HostWeb"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _ContextToken = TokenHelper.GetContextTokenFromRequest(Page.Request);
                _HostWeb = Page.Request["SPHostUrl"];
            }

            StatusMessage.Text = string.Empty;
        }

        protected void GetThemeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                StatusMessage.Text += GetThemeInfo();
            }
            catch (Exception ex)
            {
                StatusMessage.Text += Environment.NewLine + ex.ToString();
            }
        }

        protected void ApplyTheme_Click(object sender, EventArgs e)
        {
            try
            {
                ApplyTheme();
                StatusMessage.Text += "Theme applied. Click Get Theme Info to see changes." + Environment.NewLine;
            }
            catch (Exception ex)
            {
                StatusMessage.Text += Environment.NewLine + ex.ToString();
            }
        }

        private string GetThemeInfo()
        {
            using (var clientContext = TokenHelper.GetClientContextWithContextToken(_HostWeb, _ContextToken, Request.Url.Authority))
            {

                Web hostWebObj = clientContext.Web;
                ThemeInfo initialThemeInfo = hostWebObj.ThemeInfo;

                // Get the initial theme info for the web. We do not need to load the entire web object here.
                clientContext.Load(hostWebObj, w => w.ThemeInfo, w => w.CustomMasterUrl);

                // Theme component info is available via a method call that must be executed
                var linkShade = initialThemeInfo.GetThemeShadeByName("Hyperlink");
                var titleFont = initialThemeInfo.GetThemeFontByName("title", 1033);

                // Execute
                clientContext.ExecuteQuery();

                // Use the ThemeInfo to show some data about the theme currently applied to the web
                StringBuilder initialInfo = new StringBuilder();
                initialInfo.AppendFormat("Current master page: {0}\r\n", hostWebObj.CustomMasterUrl);
                initialInfo.AppendFormat("Background Image: {0}\r\n", initialThemeInfo.ThemeBackgroundImageUri);
                initialInfo.AppendFormat("The \"Hyperlink\" Color for this theme is: {0}\r\n", linkShade.Value);
                initialInfo.AppendFormat("The \"title\" Font for this theme is: {0}\r\n", titleFont.Value);
                return initialInfo.ToString();
            }
        }

        protected void ApplyTheme()
        {
            using (var clientContext = TokenHelper.GetClientContextWithContextToken(_HostWeb, _ContextToken, Request.Url.Authority))
            {
                // Apply our new theme

                // First, copy theme files to a temporary location (the web's Site Assets Library)
                Web hostWebObj = clientContext.Web;
                Site hostSiteObj = clientContext.Site;
                Web hostRootWebObj = hostSiteObj.RootWeb;
                
                // Get the necessary lists & libraries
                List themeLibrary = hostRootWebObj.Lists.GetByTitle("Theme Gallery");
                Folder themeFolder = themeLibrary.RootFolder.Folders.GetByUrl("15");
                List looksGallery = hostRootWebObj.Lists.GetByTitle("Composed Looks");
                List masterLibrary = hostRootWebObj.Lists.GetByTitle("Master Page Gallery");
                List assetLibrary = hostRootWebObj.Lists.GetByTitle("Site Assets");

                clientContext.Load(themeFolder, f => f.ServerRelativeUrl);
                clientContext.Load(masterLibrary, l => l.RootFolder);
                clientContext.Load(assetLibrary, l => l.RootFolder);

                // First, upload the theme files to the theme gallery
                DirectoryInfo themeDir = new DirectoryInfo(Server.MapPath("/Theme"));
                foreach (var themeFile in themeDir.EnumerateFiles())
                {
                    FileCreationInformation newFile = new FileCreationInformation();
                    newFile.Content = System.IO.File.ReadAllBytes(themeFile.FullName);
                    newFile.Url = themeFile.Name;
                    newFile.Overwrite = true;
                    
                    // Sort by file extension into the correct library 
                    switch (themeFile.Extension)
                    {
                        case ".spcolor":
                        case ".spfont":
                            Microsoft.SharePoint.Client.File uploadTheme = themeFolder.Files.Add(newFile);
                            clientContext.Load(uploadTheme);
                            break;
                        case ".master":
                        case ".html":
                            Microsoft.SharePoint.Client.File updloadMaster = masterLibrary.RootFolder.Files.Add(newFile);
                            clientContext.Load(updloadMaster);
                            break;
                        default:
                            Microsoft.SharePoint.Client.File uploadAsset = assetLibrary.RootFolder.Files.Add(newFile);
                            clientContext.Load(uploadAsset);
                            break;
                    }

                }

                // Execute the file upload
                clientContext.ExecuteQuery();

                // Create a new composed look for our theme
                string themeFolderUrl = themeFolder.ServerRelativeUrl;
                string masterFolderUrl = masterLibrary.RootFolder.ServerRelativeUrl;

                ListItemCreationInformation newLook = new ListItemCreationInformation();
                Microsoft.SharePoint.Client.ListItem newLookItem = looksGallery.AddItem(newLook);
                newLookItem["Title"] = "Theme Sample Look";
                newLookItem["Name"] = "Theme Sample Look";

                FieldUrlValue masterFieldValue = new FieldUrlValue();
                masterFieldValue.Url = masterFolderUrl + "/seattle.master";
                newLookItem["MasterPageUrl"] = masterFieldValue;

                FieldUrlValue colorFieldValue = new FieldUrlValue();
                colorFieldValue.Url = themeFolderUrl + "/ThemeSample.spcolor";
                newLookItem["ThemeUrl"] = colorFieldValue;

                FieldUrlValue fontFieldValue = new FieldUrlValue();
                fontFieldValue.Url = themeFolderUrl + "/ThemeSample.spfont";
                newLookItem["FontSchemeUrl"] = fontFieldValue;

                newLookItem.Update();

                // Apply master page
                hostWebObj.CustomMasterUrl = masterFieldValue.Url;

                // Must update between this step and the next - ApplyTheme errors if master is updated in the same query.
                hostWebObj.Update();
                clientContext.ExecuteQuery();

                // Apply theme
                hostWebObj.ApplyTheme(
                    colorFieldValue.Url, // URL of the Color Palette file (this is the only required theme component),
                    fontFieldValue.Url, // URL to the Font Specification file (optional)
                    null, // Background Image URL (optional, null here),
                    false // false stores the composed theme files in this web only. True would share with the site collection (to which we are not currently granted permissions) 
                );

                // Need to call update to apply the change to the host web
                hostWebObj.Update();

                // Execute the Update Method
                clientContext.ExecuteQuery();
            }
        }
    }
}