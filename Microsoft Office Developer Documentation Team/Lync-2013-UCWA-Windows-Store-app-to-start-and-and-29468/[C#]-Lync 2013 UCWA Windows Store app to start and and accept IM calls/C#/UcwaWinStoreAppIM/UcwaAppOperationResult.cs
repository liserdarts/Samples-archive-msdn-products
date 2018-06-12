using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace WinStoreUcwaAppIM
{
    public class UcwaAppOperationResult
    {
        public UcwaResource Resource { get; private set; }
        public string ResponseBody { get; private set; }
        public byte[] ResponseBodyBytes { get; private set; }
        public System.IO.Stream ResponseBodyStream { get; private set; }
        public HttpResponseHeaders ResponseHeaders { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public Exception Exception { get; private set; }

        public UcwaAppOperationResult(HttpStatusCode status)
        {
            this.StatusCode = status;
        }
        public UcwaAppOperationResult(HttpStatusCode status, HttpResponseHeaders httpHeaders, UcwaResource res)
        {
            this.Resource = res;
            this.ResponseHeaders = httpHeaders;
            this.StatusCode = status;
        }

        public UcwaAppOperationResult(HttpStatusCode status, HttpResponseHeaders httpHeaders, string httpContent, Exception e)
        {
            this.StatusCode = status;
            this.Exception = e;
            this.ResponseHeaders = httpHeaders;
            this.ResponseBody = httpContent;
        }
        public UcwaAppOperationResult(HttpStatusCode status, HttpResponseHeaders httpHeaders, byte[] httpContent, Exception e)
        {
            this.StatusCode = status;
            this.Exception = e;
            this.ResponseHeaders = httpHeaders;
            this.ResponseBodyBytes = httpContent;
        }
        public UcwaAppOperationResult(HttpStatusCode status, HttpResponseHeaders httpHeaders, System.IO.Stream httpContent, Exception e)
        {
            this.StatusCode = status;
            this.Exception = e;
            this.ResponseHeaders = httpHeaders;
            this.ResponseBodyStream = httpContent;
        }
        public UcwaAppOperationResult(HttpStatusCode status, HttpResponseHeaders httpHeaders)
        {
            this.StatusCode = status;
            this.ResponseHeaders = httpHeaders;
        }
        public UcwaAppOperationResult(HttpStatusCode status, Exception e)
        {
            this.StatusCode = status;
            this.Exception = e;
        }
        
    }
}
