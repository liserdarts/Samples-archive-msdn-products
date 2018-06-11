using Windows.UI.Xaml;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Popups;

///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////// Office 365 APIs - Common Functionality. Version 1.1 /////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////

namespace UCourseSelector.Office365
{
    public static class Office365Helper
    {
        private const string OAuthUrl = "https://login.windows.net/{0}";
        private readonly static string LogoutUrl = string.Format(CultureInfo.InvariantCulture,
                OAuthUrl, "{0}/oauth2/logout?post_logout_redirect_uri={1}");

        /// <summary>
        /// Send an HTTP request, with authorization. If the request fails due to an unauthorized exception,
        ///     this method will try to renew the access token in serviceInfo and try again.
        /// </summary>
        public static async Task<HttpResponseMessage> SendRequestAsync(
            Office365ServiceInfo serviceInfo, HttpClient client, Func<HttpRequestMessage> requestCreator)
        {
            using (HttpRequestMessage request = requestCreator.Invoke())
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", serviceInfo.AccessToken);
                request.Headers.Add("X-ClientService-ClientTag", new[] { "Office 365 API Tools", "1.1" });
                HttpResponseMessage response = await client.SendAsync(request);

                // Check if the server responded with "Unauthorized". If so, it might be a real authorization issue, or 
                //     it might be due to an expired access token. To be sure, renew the token and try one more time:
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    string authority = string.Format(CultureInfo.InvariantCulture, OAuthUrl, TenantId ?? "common");
                    AuthenticationContext authContext = new AuthenticationContext(authority);

                    TokenCacheKey[] keysToRemove = (
                        from key in authContext.TokenCacheStore.Keys
                        where (key.Resource == serviceInfo.ResourceId) &&
                              (key.ClientId == (string) Application.Current.Resources["ida:ClientID"])
                        select key).ToArray();
                    foreach (TokenCacheKey key in keysToRemove)
                    {
                        authContext.TokenCacheStore.Remove(key);
                    }

                    serviceInfo.AccessToken = await GetAccessToken(serviceInfo.ResourceId);

                    // Create and send a new request:
                    using (HttpRequestMessage retryRequest = requestCreator.Invoke())
                    {
                        retryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", serviceInfo.AccessToken);
                        retryRequest.Headers.Add("X-ClientService-ClientTag", new[] { "Office 365 API Tools", "1.1" });
                        response = await client.SendAsync(retryRequest);
                    }
                }

                // Return either the original response, or the response from the second attempt:
                return response;
            }
        }

        /// <summary>
        /// Clears any OAuth-related data, such as access and refresh tokens, and logs out.
        /// This method should be called as part of your application's logout routine.
        /// </summary>
        public static async Task ClearSession()
        {
            string authority = string.Format(CultureInfo.InvariantCulture, OAuthUrl, TenantId ?? "common");
            AuthenticationContext authContext = new AuthenticationContext(authority);
            authContext.TokenCacheStore.Clear();

            Office365Cache.RemoveAllFromCache();
            
            string requestUrl = string.Format(CultureInfo.InvariantCulture,
                LogoutUrl,
                TenantId ?? "common",
                Uri.EscapeDataString(WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString()));
            await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.SilentMode, new Uri(requestUrl));
        }

        /// <summary>
        /// A static method that routes errors to a single centralized error-handler.
        /// This method will attempt to extract a human-readable error from the response string,
        /// based on the the format of the data and the error handling scheme of the service.
        /// </summary>
        public static async Task ShowErrorMessageAsync(Office365ServiceInfo serviceInfo, string responseString)
        {
            string message, errorDetails;
            try
            {
                message = serviceInfo.ParseErrorMessage(responseString);
                errorDetails = responseString;
            }
            catch (Exception e)
            {
                message = "An unexpected error has occurred.";
                errorDetails = "Exception when parsing response string: " + e.ToString() +
                    "\n\nResponse string was " + responseString;
            }
            await ShowErrorMessageAsync(message, errorDetails);
        }

        /// <summary>
        /// A common error handler for Platform 365 services.
        /// Message is expected to be a human-readable string that can be displayed to the user.
        /// ErrorDetails can contains any additional details (e.g., for logging purposes).
        /// </summary>
        public static async Task ShowErrorMessageAsync(string message, string errorDetails)
        {
            // TODO: You can customize this method to write the error to a log file, 
            //       and/or to display a different error UI.
            await new MessageDialog(message, "Error").ShowAsync();
        }

        /// <summary>
        /// Returns the Tenant ID of the signed-in user.  If not signed in, returns null.
        /// </summary>
        public static string TenantId
        {
            get { return (string)GetFromCache("TenantId"); }
        }

        /// <summary>
        /// Returns the User ID of the signed-in user.  If not signed in, returns null.
        /// </summary>
        public static string UserId
        {
            get { return (string)GetFromCache("UserId"); }
        }

        /// <summary>
        /// Obtains the access token necessary to call Office 365 APIs. The access token might be obtained anew,
        /// or it might be returned from the Windows Azure Active Directory library cache.
        /// On failure, the method will display a message dialog to the user, and then return null.
        /// </summary>
        internal static async Task<string> GetAccessToken(string resourceId)
        {
            string authority = string.Format(CultureInfo.InvariantCulture, OAuthUrl, TenantId ?? "common");
            AuthenticationContext authContext = new AuthenticationContext(authority);

            AuthenticationResult result = await authContext.AcquireTokenAsync(
                resourceId,
                (string)Application.Current.Resources["ida:ClientID"],
                WebAuthenticationBroker.GetCurrentApplicationCallbackUri(),
                PromptBehavior.Auto);

            if (result.Status == AuthenticationStatus.Succeeded)
            {
                SaveInCache("TenantId", result.TenantId);
                SaveInCache("UserId", result.UserInfo.UserId);
                return result.AccessToken;
            }
            else
            {
                string message = result.Error + ": " + result.ErrorDescription;
                await ShowErrorMessageAsync(message, "Error issuing an access token. " + message);
                return null;
            }
        }

        internal static void SaveInCache(string name, object value)
        {
            Office365Cache.SaveInCache(name, value);
        }

        /// <summary>
        /// If the item exists, returns the saved value; otherwise, returns null.
        /// </summary>
        internal static object GetFromCache(string name)
        {
            return Office365Cache.GetFromCache(name);
        }

        #region Private classes

        /// <summary>
        /// A default cache implementation that uses local settings saving and retrieving data related to
        ///      Office 365 APIs, such as tenant & user IDs, or dynamically-discovered API endpoints.
        /// </summary>
        private class Office365Cache
        {
            private static ApplicationDataContainer settingsContainer = ApplicationData.Current.LocalSettings.CreateContainer(
                "Office365Cache", ApplicationDataCreateDisposition.Always);

            private Office365Cache() { }

            internal static void SaveInCache(string name, object value)
            {
                settingsContainer.Values[name] = value;
            }

            internal static object GetFromCache(string name)
            {
                return settingsContainer.Values[name];
            }

            internal static void RemoveFromCache(string name)
            {
                settingsContainer.Values.Remove(name);
            }

            internal static void RemoveAllFromCache()
            {
                settingsContainer.Values.Clear();
            }
        }

        #endregion
    }
}