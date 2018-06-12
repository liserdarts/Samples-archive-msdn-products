using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using System.Net.Http;

namespace WinStoreUcwaAppIM
{
    public  class Transport
    {
        public UcwaAppErrorReportEventHandler OnErrorReported;
        public UcwaAppProgressReportEventHandler OnProgressReported;

        public Transport()
        {
            this.ContentType = "application/xml";
        }
        #region public properties
        public string ContentType { get; set; }
        public string Host { get; set; }
        public string OAuthToken { get; set; }
        #endregion public properties

        #region public-facing method-specific asynchronous request submissions, including GET, POST, PUT and DELETE
        public HttpWebResponse PGetRequest(string uri)
        {
            var fullUrl = this.ConvertToFullHttpUrl(uri);
            var request = HttpWebRequest.CreateHttp(fullUrl);

            request.Method = "GET";
            request.Accept = this.ContentType;
            request.ContentType = this.ContentType;
            if (!string.IsNullOrEmpty(this.OAuthToken))
                request.Headers["Authorization"] = this.OAuthToken;

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.EndGetResponse(request.BeginGetResponse(null, null));
            }
            catch(WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            return response;
        }
        public async Task<HttpWebResponse> GetRequest(string uri, params KeyValuePair<string, string>[] requestHeaders)
        {
            return await this.SubmitRequest(uri, "GET", this.ContentType, this.ContentType, requestHeaders, null);
        }
        public async Task<HttpWebResponse> PostRequest(string uri, string requestData, params KeyValuePair<string, string>[] requestHeaders)
        {
            return await this.SubmitRequest(uri, "POST", this.ContentType, this.ContentType, requestHeaders, requestData);
        }
        public async Task<HttpWebResponse> PutRequest(string uri, string requestData, params KeyValuePair<string, string>[] requestHeaders)
        {
            return await this.SubmitRequest(uri, "PUT", this.ContentType, this.ContentType, requestHeaders, requestData);
        }
        public async Task<HttpWebResponse> DeleteRequest(string uri)
        {
            return await this.SubmitRequest(uri, "DELETE", this.ContentType, this.ContentType, null, null);
        }
        public async Task<HttpWebResponse> GetRequest(string uri, string accept, string contentType, params KeyValuePair<string, string>[] requestHeaders)
        {
            return await SubmitRequest(uri, "GET", accept, contentType, requestHeaders, null);
        }
        public async Task<HttpWebResponse> PostRequest(string uri, string accept, string contentType, string requestData, params KeyValuePair<string, string>[] requestHeaders)
        {
            return await this.SubmitRequest(uri, "POST", accept, contentType, requestHeaders, requestData);
        }
        public async Task<HttpWebResponse> PutRequest(string uri, string accept, string contentType, string requestData, params KeyValuePair<string, string>[] requestHeaders)
        {
            return await this.SubmitRequest(uri, "PUT", accept, contentType, requestHeaders, requestData);
        }
        #endregion method-specific request submissions

        #region private asynchronous request submission and related helper methods.
        private async Task<HttpWebResponse> SubmitRequest(string uri, string method, string accept, string contentType, 
             IEnumerable<KeyValuePair<string, string>> requestHeaders, string requestData)
        {
            var request = await this.CreateRequest(uri, method, accept, contentType, requestHeaders, requestData);
            var response = await this.GetResponse(request);
            return response;
        }
        //void CreateRequest(string url)
        //{
        //    HttpClient client = new HttpClient()
        //}
        private async Task<HttpWebRequest> CreateRequest(string url, string method, string accept, string contentType, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders, string requestBody)
        {
            var fullUrl = this.ConvertToFullHttpUrl(url);
            var request = HttpWebRequest.CreateHttp(fullUrl);

            request.Method = method;
            request.Accept = accept;
            request.ContentType = contentType;
            if (!string.IsNullOrEmpty(this.OAuthToken))
                request.Headers["Authorization"] = this.OAuthToken;

            // Set supplied headers, if any.
            if (requestHeaders != null)
                foreach (var header in requestHeaders)
                    request.Headers[header.Key] = header.Value;

            // Set supplied body, if any.
            if (!string.IsNullOrEmpty(requestBody))
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] body = encoding.GetBytes(requestBody);
                var reqStream = await this.GetRequestStream(request);
                reqStream.Write(body, 0, body.Length);
                reqStream.Flush();
            }
            return request;
        }
        // Asynchronous method to get request stream
        private Task<Stream> GetRequestStream(HttpWebRequest request)
        {
            Stream requestStream;
            var taskComplete = new TaskCompletionSource<Stream>();
            request.BeginGetRequestStream(asyncRequest =>
            {
                try
                {
                    HttpWebRequest responseRequest = (HttpWebRequest)asyncRequest.AsyncState;
                    requestStream = (Stream)responseRequest.EndGetRequestStream(asyncRequest);
                    taskComplete.TrySetResult(requestStream);
                }
                catch (WebException ex)
                { 
                    if (OnErrorReported != null)
                        UcwaAppUtils.ReportError(OnErrorReported, ex);
                }
            }, request);
            return taskComplete.Task;
        }
        // Asynchronous method to get a request's response.
        private Task<HttpWebResponse> GetResponse(HttpWebRequest request)
        {
            HttpWebResponse response;
            var taskComplete = new TaskCompletionSource<HttpWebResponse>();
            request.BeginGetResponse(asyncResponse =>
            {
                try
                {
                    HttpWebRequest responseRequest = (HttpWebRequest)asyncResponse.AsyncState;
                    response = (HttpWebResponse)responseRequest.EndGetResponse(asyncResponse);
                    taskComplete.TrySetResult(response);
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                    taskComplete.TrySetResult(response);
                }
            }, request);
            return taskComplete.Task;
        }
        private string ConvertToFullHttpUrl(string uri)
        {
            return uri == null || uri.StartsWith("http") ? uri : "https://" + this.Host + uri;
        }
        #endregion prvate asynchronous request submission and related helper methods.
    }
}
