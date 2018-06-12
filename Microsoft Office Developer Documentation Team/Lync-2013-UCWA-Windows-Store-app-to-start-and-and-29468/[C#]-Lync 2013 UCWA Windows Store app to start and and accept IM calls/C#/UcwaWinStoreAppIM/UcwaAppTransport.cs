using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace WinStoreUcwaAppIM
{
    public class UcwaAppTransport
    {
        public UcwaAppErrorReportEventHandler OnErrorReported;
        public UcwaAppProgressReportEventHandler OnProgressReported;
        public HttpClient Client { get; private set; }
        public UcwaAppAuthenticationTypes AuthenticationType { get; private set; }
        public UcwaAppTransport(Uri baseAddress = null, string accept = "application/xml", AuthenticationHeaderValue authorization = null)
        {
            this.Initialize(accept, authorization, baseAddress);
        }
        public void Initialize(HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> accept, AuthenticationHeaderValue authorization, 
            Uri baseAddress, UcwaAppAuthenticationTypes authType = UcwaAppAuthenticationTypes.Password)
        {
            Client = new HttpClient(new HttpClientHandler { UseDefaultCredentials = authType == UcwaAppAuthenticationTypes.Windows });
            Client.Timeout = new TimeSpan(0, 30, 0);  // initialize the app timeout to be 30 minutes.
            this.AuthenticationType = authType;
            this.SetAccept(accept);
            if (baseAddress != null) this.Client.BaseAddress = baseAddress; // must be set before any request is made
            if (authorization != null)
            {
                this.SetAuthorization(authorization);
                this.Client.DefaultRequestHeaders.Authorization = authorization;
            }
        }
        public void Initialize(string accept, AuthenticationHeaderValue authorization,
            Uri baseAddress, UcwaAppAuthenticationTypes authType = UcwaAppAuthenticationTypes.Password)
        {
            Client = new HttpClient(new HttpClientHandler { UseDefaultCredentials = authType == UcwaAppAuthenticationTypes.Windows });
            Client.Timeout = new TimeSpan(0, 30, 0);  // initialize the app timeout to be 30 minutes.
            this.AuthenticationType = authType;
            this.SetAccept(accept);
            if (baseAddress != null) this.Client.BaseAddress = baseAddress; // must be set before any request is made
            if (authorization != null)
            {
                this.SetAuthorization(authorization);
                this.Client.DefaultRequestHeaders.Authorization = authorization;
            }
        }
        public void SetAuthenticationType(UcwaAppAuthenticationTypes authType)
        {
            var accept = this.Accept;
            var authorization = this.Authorization;
            var baseAddress = this.BaseAddress;
            Client.Dispose();
            Client = null;
            Initialize(accept, authorization, baseAddress, authType);
            this.AuthenticationType = authType;
        }
        public Uri BaseAddress { get { return this.Client.BaseAddress; } }
        public void SetBaseAddress(Uri baseAddress)
        {
            {
                var accept = this.Accept;
                var authorization = this.Authorization;
                Client.Dispose();
                Client = null;
                Initialize(accept, authorization, baseAddress);
            }
        }
        public HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> Accept { get { return this.Client.DefaultRequestHeaders.Accept; } }
        public void SetAccept(string accept)
        {

            if (!string.IsNullOrEmpty(accept)) this.Client.DefaultRequestHeaders.Add("Accept", accept);

        }
        public void SetAccept(HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> accept)
        {
            if (accept != null)
            {
                var acceptValues = accept.Select(v => v.MediaType);
                if (accept != null) this.Client.DefaultRequestHeaders.Add("Accept", acceptValues);
            }
        }
        public AuthenticationHeaderValue Authorization
        {
            get { return this.Client.DefaultRequestHeaders.Authorization; }
        }
        public void SetAuthorization(AuthenticationHeaderValue authorization)
        {
            if (authorization != null) Client.DefaultRequestHeaders.Authorization = authorization;
        }
        public void SetAuthorization(string scheme, string parameter)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);
        }
        public async Task<UcwaAppOperationResult> GetPhotoStreamAsync(string uri)
        {
            HttpResponseMessage response;
            //string responseContent;
            //byte[] bytes; 
            Stream contentStream;
            try
            {
                response = await Client.GetAsync(uri);
                contentStream = await response.Content.ReadAsStreamAsync();
                //bytes = await response.Content.ReadAsByteArrayAsync(); 
                // responseContent = response.Content.ReadAsStringAsync();
                return new UcwaAppOperationResult(response.StatusCode, response.Headers, contentStream, null);
            }
            catch (TaskCanceledException tcex)
            {
                return new UcwaAppOperationResult(HttpStatusCode.RequestTimeout, tcex);
            }
            catch (Exception ex)
            {
                return new UcwaAppOperationResult(HttpStatusCode.BadRequest, ex);
            }

        }

        public async Task<UcwaAppOperationResult> GetResourceAsync(string uri)
        {
            HttpResponseMessage response;
            string responseContent;
            try
            {
                response = await Client.GetAsync(uri);
                responseContent = await response.Content.ReadAsStringAsync();
                try
                {
                    var resource = new UcwaResource(responseContent);
                    return new UcwaAppOperationResult(response.StatusCode, response.Headers, resource);
                }
                catch (Exception ex)
                {
                    return new UcwaAppOperationResult(response.StatusCode, response.Headers, responseContent, ex);
                }
            }
            catch (TaskCanceledException tcex)
            {
                return new UcwaAppOperationResult(HttpStatusCode.RequestTimeout, tcex);
            }
            catch (Exception ex)
            {
                return new UcwaAppOperationResult(HttpStatusCode.BadRequest, ex);
            }

        }

        public async Task<UcwaAppOperationResult> PostResourceAsync(string uri, IEnumerable<KeyValuePair<string, string>> nameValuePairs, string contentType="text/html")
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(nameValuePairs);
            return await PostResourceAsync(uri, content);
        }
        public async Task<UcwaAppOperationResult> PostResourceAsync(string uri, string requestData, string contentType = "application/xml")
        {
            HttpContent content = new ByteArrayContent(UTF8Encoding.UTF8.GetBytes(requestData));
            content.Headers.Add("Content-Type", contentType);
            return await PostResourceAsync(uri, content);
        }

        public async Task<UcwaAppOperationResult> PostResourceAsync(string uri, HttpContent content)
        {

            var response = await Client.PostAsync(uri, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseContent))
                return new UcwaAppOperationResult(response.StatusCode, response.Headers);
            try
            {
                var resource = new UcwaResource(responseContent);
                return new UcwaAppOperationResult(response.StatusCode, response.Headers, resource);
            }
            catch (Exception ex)
            {
                return new UcwaAppOperationResult(response.StatusCode, response.Headers, responseContent, ex);
            }
        }

        public async Task<UcwaAppOperationResult> PutResourceAsync(string uri, HttpContent requestContent)
        {
            var response = await Client.PutAsync(uri, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseContent.Trim()))
                return new UcwaAppOperationResult(response.StatusCode, response.Headers);

            try
            {
                UcwaResource resource = new UcwaResource(responseContent);
                return new UcwaAppOperationResult(response.StatusCode, response.Headers, resource);
            }
            catch (Exception ex)
            {
                return new UcwaAppOperationResult(response.StatusCode, response.Headers, responseContent, ex);
            }
        }

        public async Task<UcwaAppOperationResult> DeleteResourceAsync(string uri)
        {
            var response = await Client.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
            return new UcwaAppOperationResult(response.StatusCode, response.Headers);
        }
    }
}
