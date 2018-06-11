using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using CustomerOData.Models;
using Microsoft.Data.Edm;

namespace CustomerOData
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Customer>("Customers");
            IEdmModel model = builder.GetEdmModel();
            config.Routes.MapODataRoute("CustomersRoute", "api", model);
        }
    }
}
