using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using Windows.UI.Core;

namespace WinStoreUcwaAppIM
{
    public class OAuthToken
    {
        public string GrantType { get; private set; }
        public string AccessToken { get; private set;}
        public double ExpirationTime { get; private set; }
        public OAuthToken(string grant_type, string access_token, double expirationTime)
        {
            this.GrantType = grant_type;
            this.AccessToken = access_token;
            this.ExpirationTime = expirationTime;
        }
    }
    public sealed class SignInParameter
    {
        internal string UserName { get; private set; }
        internal string Password { get; private set; }
        internal UcwaAppAuthenticationTypes AuthType { get; private set; }
        public SignInParameter(string name, string pass, UcwaAppAuthenticationTypes authType)
        {
            this.UserName = name;
            this.Password = pass;
            this.AuthType = authType;
        }
    }
    public class UcwaAppUtils
    {
        #region helper methods
        public static IEnumerable<KeyValuePair<string, string>> ConvertWebHeaderCollectionToKeyValuePairs(WebHeaderCollection headerCollection)
        {
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            foreach (var headerName in headerCollection.AllKeys)
            {
                var headerValue = headerCollection[headerName];
                var kvPair = new KeyValuePair<string, string>(headerName, headerValue);
                headers.Add(kvPair);
            }
            return headers.AsEnumerable<KeyValuePair<string, string>>();
        }
        public static string ConvertResponseBodyStreamToString(Stream responseStream)
        {
            string responseBody = null;
            using (StreamReader sr = new StreamReader(responseStream))
                responseBody = sr.ReadToEnd();
            return responseBody;
        }

        public static void ReportError(UcwaAppErrorReportEventHandler errorReporter, Exception e)
        {
            if (errorReporter != null)
                errorReporter(e);
        }
        public static void ReportProgress(UcwaAppProgressReportEventHandler progressReporter, string msg, HttpStatusCode status = HttpStatusCode.OK)
        {
            if (progressReporter != null)
                progressReporter(msg, status);
        }

        public static async Task DispatchEventToUI(CoreDispatcherPriority priority, DispatchedHandler callback)
        {
            if (callback != null)
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(priority, callback);
        }

        #endregion helper methods
    }
}
