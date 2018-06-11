using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using Windows.UI.Core;

namespace UcwaWinStoreHello
{
    public enum AuthenticationTypes { Password, Windows, Passive, Annonymous }

    public class UcwaApp
    {
        private string discoverUrl = null;
        bool discoverFromInternalDomain = false;
        AuthenticationTypes authenticationType = AuthenticationTypes.Password;

        string applicationsUrl;
        string userName, password;
        string oAuth20Token;
        string appSettingsFormatter =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<input xmlns=\"http://schemas.microsoft.com/rtc/2012/03/ucwa\">" +
            "   <property name=\"culture\">{0}</property>" +
            "   <property name=\"endpointId\">{1}</property>" +
            "   <property name=\"userAgent\">{2}</property>" +
            "</input>";

        public UcwaAppProgressReportEventHandler OnProgressReported; // delegate for reporting progress on the calling UI thread
        public UcwaAppErrorReportEventHandler OnErrorReported; // delegate for reporting errors on the callling UI thread
        public Transport Transport { get; private set; }
        public bool IsSignedIn { get; set; }
        public UcwaResource ApplicationResource { get; private set; }
        public string Host { get { return this.Transport.Host; } }
        public UcwaAppMe Me { get; private set; }
        public UcwaApp(bool discoverFromInternalDomain = true)
        {
            Transport = new Transport();
            this.IsSignedIn = false;
            this.discoverFromInternalDomain = discoverFromInternalDomain;
        }

        public async Task<HttpStatusCode> SignIn(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            this.authenticationType = AuthenticationTypes.Password;
            try
            {
                var opResult = await DiscoverRootResource(this.discoverFromInternalDomain);
                if (opResult.Resource == null)
                {
                    UcwaAppUtils.ReportProgress(OnProgressReported, "GetRootResource returns null result.", opResult.HttpStatusCode);
                    return opResult.HttpStatusCode;
                }

                opResult = await GetUserResource(opResult.Resource.GetLinkUri("user"), userName, password, this.authenticationType);
                if (opResult.Resource == null)
                {
                    UcwaAppUtils.ReportProgress(OnProgressReported,
                        userName + " cannot be authenticated, with the " + this.authenticationType.ToString() + " grant_type.",
                        opResult.HttpStatusCode);
                    return opResult.HttpStatusCode;
                }
                // Create the UCWA application bound to the specified user
                opResult = await GetApplicationResource(opResult.Resource);

                if (opResult.Resource == null)
                {
                    UcwaAppUtils.ReportProgress(OnProgressReported, "Failed to create the UCWA application resource.", opResult.HttpStatusCode);
                    return opResult.HttpStatusCode;
                }

                this.ApplicationResource = opResult.Resource;
                UcwaAppUtils.ReportProgress(OnProgressReported, "Succeded in creating the application resource: " + this.ApplicationResource.Uri);


                // Make me available to receive incoming alerts
                this.Me = new UcwaAppMe(this);
                var statusCode = await this.Me.PostMakeMeAvailable("4255552222", "Online",
                    new string[] { "Plain", "Html" }, new string[] { "PhoneAudio", "Messaging" });
                if (statusCode != HttpStatusCode.NoContent)
                {
                    UcwaAppUtils.ReportProgress(OnProgressReported, "Failed to post to makeMeAvailable resource.", statusCode);
                    return statusCode;
                }

                // Get application resource again to receive any updates triggered by the POST request to making me available
                opResult = await GetApplicationResource(this.ApplicationResource.Uri);
                if (opResult.Resource == null)
                {
                    UcwaAppUtils.ReportProgress(OnProgressReported, "Failed to get the updated application resource", opResult.HttpStatusCode);
                    return opResult.HttpStatusCode;
                }
                this.ApplicationResource = opResult.Resource;
                statusCode = await this.Me.Refresh();

            }
            catch (Exception ex)
            {
                UcwaAppUtils.ReportError(OnErrorReported, ex);
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.OK;
        }

        public bool InternalDomain;
        #region Auto-discovery routines
        private async Task<UcwaHttpOperationResult> DiscoverRootResource(bool discoverFromInternalDomain = false)
        {
            this.discoverFromInternalDomain = discoverFromInternalDomain;
            string domain = this.userName.Contains("@") ? this.userName.Split('@')[1] : null;
            this.discoverUrl = "https://lyncdiscoverinternal." + domain;
            if (!this.discoverFromInternalDomain)
                this.discoverUrl = "https://lyncdiscover." + domain;

            var opResult = await GetRootResource(this.discoverUrl, maxDiscoverTrials);
            if (opResult.Resource == null)
            {
                if (this.discoverFromInternalDomain)
                {
                    this.discoverUrl = "https://lyncdiscover." + domain;
                    this.InternalDomain = false;
                    opResult = await GetRootResource(this.discoverUrl, maxDiscoverTrials);
                }
                if (opResult.Resource == null)
                    return opResult;
            }
            if (discoverUrl.ToLower().Contains("lyncdiscoverinternal"))
                this.InternalDomain = true;
            else
                this.InternalDomain = false;

            string redirectUrl = opResult.Resource.GetLinkUri("redirect");
            if (!string.IsNullOrEmpty(redirectUrl) && RedirectUrlSecurityCheckPassed(redirectUrl))
            {
                opResult = await GetRedirectResource(redirectUrl);
            }
            return opResult;
        }

        int maxDiscoverTrials = 3;
        private async Task<UcwaHttpOperationResult> GetRootResource(string url, int maxTrials = 3)
        {
            HttpWebResponse response;
            UcwaResource resource = null;
            int trials = 0;
            while (trials < maxTrials)
            {
                trials++;
                response = await Transport.GetRequest(url);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    resource = new UcwaResource(response.GetResponseStream());
                    return new UcwaHttpOperationResult(response.StatusCode, null, resource);
                }
            }
            return new UcwaHttpOperationResult(HttpStatusCode.NotFound, "Failed to get root resource of " + url);
        }

        private async Task<UcwaHttpOperationResult> GetRedirectResource(string redirectUrl, bool checkRedirectUrl = true)
        {
            if (checkRedirectUrl && !RedirectUrlSecurityCheckPassed(redirectUrl))
            {
                return new UcwaHttpOperationResult(HttpStatusCode.Redirect, "Failed to pass secury check on redirect of " + redirectUrl);
            }
            var response = await Transport.GetRequest(redirectUrl);
            if (response.StatusCode != HttpStatusCode.OK)
                return new UcwaHttpOperationResult(response.StatusCode, UcwaAppUtils.ConvertResponseBodyStreamToString(response.GetResponseStream()));
            try
            {
                var res = new UcwaResource(response.GetResponseStream());
                return new UcwaHttpOperationResult(response.StatusCode, null, res);
            }
            catch (Exception e)
            {
                return new UcwaHttpOperationResult(response.StatusCode, e.Message, null, e);
            }
        }
        bool RedirectUrlSecurityCheckPassed(string redirectUrl)
        {
            // See the Security check section in http://ucwa.lync.com/documentation/GettingStarted-RootURL
            // do security verification of the supplied redirectUrl, if (not valid) return false;
            bool isHttps;
            var domain = ParseDomainFromUrl(redirectUrl, out isHttps);
            if (!isHttps) return true;

            if (IsGlobalTrustedDomain(domain)) return true;

            // Prompt user and manage approved list of host names for specific sign-in address domain
            // to do ...

            // Check if host name in the redirect URL in he approved list for specific sign-in address?
            // to do ... if so, return true;


            return false;
        }
        private string ParseDomainFromUrl(string url, out bool isHttps)
        {
            string pattern = @"(http[s]?)://[\w|\d|-]+.([\w|\d|-]+.[\w|\d|-]+)/";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
            var match = regex.Match(url);
            isHttps = match.Groups[1].Value.ToLower() == "s";
            var domain = match.Groups[2].Value;
            return domain;
        }
        bool IsGlobalTrustedDomain(string domain)
        {
            // to do
            return true;
        }
        #endregion Auto-discovery routines

        #region Create or refresh application resource
        /// <summary>
        /// Get an application resource bound to the user's local endpoint
        /// </summary>
        /// <param name="resUser">The authenticated user resource</param>
        /// <param name="userAgent">The name of this application</param>
        /// <param name="culture">The locale of this application</param>
        /// <returns>The application resoure as part of UcwaHttpOperationResult</returns>
        async Task<UcwaHttpOperationResult> GetApplicationResource(UcwaResource resUser,
            string userAgent = "ContosoApp/1.0 (WinStore)", string culture = "en-us")
        {
            applicationsUrl = resUser.GetLinkUri("applications");
            Transport.Host = applicationsUrl.Split('/')[2];

            var endpointId = Guid.NewGuid().ToString();
            string appSettings = string.Format(appSettingsFormatter, culture, endpointId, userAgent);
            var response = await Transport.PostRequest(applicationsUrl, appSettings);
            if (response.StatusCode != HttpStatusCode.Created)
                return new UcwaHttpOperationResult(response.StatusCode, "Failed to PostRequest on " + applicationsUrl);

            var res = new UcwaResource(response.GetResponseStream());
            return new UcwaHttpOperationResult(response.StatusCode, "CreateApplicationResource", res);
        }

        /// <summary>
        /// An overloaded member to tet updated application resource, given the application uri.
        /// </summary>
        /// <param name="appUri">previously returned application uri</param>
        /// <returns>application resource as part of the UcwaHttpOperationResult</returns>
        public async Task<UcwaHttpOperationResult> GetApplicationResource(string appUri)
        {
            var response = await Transport.GetRequest(appUri);
            var res = new UcwaResource(response.GetResponseStream());
            return new UcwaHttpOperationResult(response.StatusCode, "GetUpdatedApplicationResource", res);
        }

        #endregion Create or refresh application resource

        #region authenticate user routine
        private async Task<UcwaHttpOperationResult> GetUserResource(string userResUri, string userName, string password, AuthenticationTypes authType = AuthenticationTypes.Password)
        {
            this.IsSignedIn = false;
            //
            // First GET user resource to retrieve oAuthToken href. 
            // Expect 401 Unauthorized response as an HTML payload
            var response = await Transport.GetRequest(userResUri);
            if (response.StatusCode != HttpStatusCode.Unauthorized && response.StatusCode != HttpStatusCode.OK)
            {
                return new UcwaHttpOperationResult(response.StatusCode, "Failed to GetRequest on " + userResUri);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Get OAuth resource for a Web ticket
                var authHeader = UcwaAppUtils.ConvertWebHeaderCollectionToKeyValuePairs(response.Headers)
                    .Where(a => a.Key == "WWW-Authenticate" && a.Value.Contains("MsRtcOAuth href"))
                    .FirstOrDefault().Value;
                var oAuthHref = authHeader.Split(',').Where(s => s.Contains("MsRtcOAuth")).FirstOrDefault()
                    .Split('=')[1].Replace("\"", "").Trim();
                string requestBody = GetAuthenticationRequestBody(userName, password, authType);

                // Note: the following PostRequest returns a json payload in the responseData, containing the access token, 
                var cType = "application/x-www-form-urlencoded;charset='utf-8'";
                var aType = "application/x-www-form-urlencoded;charset='utf-8'";

                response = await Transport.PostRequest(oAuthHref, aType, cType, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                    return new UcwaHttpOperationResult(response.StatusCode, "PostRequest on " + oAuthHref + " with " + requestBody);

                string responseData = UcwaAppUtils.ConvertResponseBodyStreamToString(response.GetResponseStream());

                if (authType == AuthenticationTypes.Passive && response.StatusCode == HttpStatusCode.BadRequest &&
                    responseData.Contains("ms_rtc_passiveauthuri"))
                {
                    // get ms_rtc_passiveauthuri to obtain an ADFS cookie and do another POST request (above) to obtain UCWA oAuth token
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\"ms_rtc_passiveauthuri\":\"(.)\"");
                    var match = regex.Match(responseData);
                    var passiveauthuri = match.Groups[1].Value;
                    // to do: obtain a token from ADFS
                    //    ... .// omitted here

                    // repost on oAuthHref, once a new ADFS token is had
                    response = await Transport.PostRequest(oAuthHref, aType, cType, requestBody);
                    if (response.StatusCode != HttpStatusCode.OK)
                        return new UcwaHttpOperationResult(response.StatusCode, "PostRequest on " + oAuthHref + " with " + requestBody);
                    responseData = UcwaAppUtils.ConvertResponseBodyStreamToString(response.GetResponseStream());
                }

                // Extract the access token from the response body to construct the oAuth token
                oAuth20Token = GetOAuthToken(responseData);
                if (oAuth20Token != null)
                {
                    Transport.OAuthToken = oAuth20Token;
                    // Second GET userHref, supplying the required compact-web-ticket (cwt) in an Authorization header
                    response = await Transport.GetRequest(userResUri);
                    if (response.StatusCode != HttpStatusCode.OK)
                        return new UcwaHttpOperationResult(response.StatusCode, "GetRequest on " + userResUri + " with oAuth token of " + oAuth20Token);
                }
                else
                {
                    return new UcwaHttpOperationResult(response.StatusCode, "PostRequest on " + oAuthHref + " returns " + responseData);
                }
            }
            this.IsSignedIn = true;
            var res = new UcwaResource(response.GetResponseStream());
            return new UcwaHttpOperationResult(response.StatusCode, null, res);
        }
        private string GetOAuthToken(string responseData)
        {
            string oAuth20Token = null;
            Windows.Data.Json.JsonObject json;
            if (Windows.Data.Json.JsonObject.TryParse(responseData, out json))
                if (json.ContainsKey("access_token") && json.ContainsKey("token_type"))
                {
                    var at = json.GetNamedValue("access_token");
                    var tt = json.GetNamedValue("token_type");
                    if (at != null && tt != null)
                        oAuth20Token = tt.GetString() + " " + at.GetString();
                }            
            return oAuth20Token;
        }
        string GetAuthenticationRequestBody(string userName, string password, AuthenticationTypes authType)
        {
            string requestBody = null;
            switch (authType)
            {
                case AuthenticationTypes.Windows:
                    requestBody = "grant_type=urn:microsoft.rtc:windows&username=" + userName;
                    break;
                case AuthenticationTypes.Annonymous:
                    requestBody = "grant_type=urn:microsoft.rtc:anonmeeting&password=" + password + "&msrtc_conferenceuri=" + userName;
                    break;
                case AuthenticationTypes.Passive:
                    requestBody = "grant_type=urn:Microsoft.rtc:passive";
                    break;
                default:  // password
                    requestBody = "grant_type=password&username=" + userName + "&password=" + password;
                    break;
            }
            return requestBody;
        }
        void SetTotRefreshOAuthTokenOnExpiration(string expiresInSeconds)
        {
            int intSeconds;
            if (!int.TryParse(expiresInSeconds, out intSeconds))
                return;
            int hours = intSeconds / 3600;
            int minutes = (intSeconds - 3600 * hours) / 60;
            int seconds = (intSeconds - 3600 * hours - 60 * minutes);

            var asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
                (work) =>
                {
                    var timer = Windows.System.Threading.ThreadPoolTimer.CreateTimer(
                        (handler) =>
                        {
                            // submit a token-refresh request
                        },
                        new TimeSpan(hours, minutes, seconds));
                },
                Windows.System.Threading.WorkItemPriority.Normal
            );
        }

        #endregion authenticate user routine
    }
}
