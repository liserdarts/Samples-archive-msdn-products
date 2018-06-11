using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebProxySampleWeb
{
    public class MyController : ApiController
    {
        // Returns the title of the app web site.
        // POST api/<controller>
        public string Post()
        {
            try
            {
                var authority = this.ControllerContext.Request.RequestUri.Authority;
                var headers = ControllerContext.Request.Headers;
                var contextToken = headers.GetValues("X-SP-AccessToken").FirstOrDefault();
                var spAppWebUrl = headers.GetValues("SPAppWebUrl").FirstOrDefault();

                using (var clientContext = TokenHelper.GetClientContextWithContextToken(spAppWebUrl, contextToken, authority))
                {
                    clientContext.Load(clientContext.Web, web => web.Title);
                    clientContext.ExecuteQuery();

                    return clientContext.Web.Title;
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}