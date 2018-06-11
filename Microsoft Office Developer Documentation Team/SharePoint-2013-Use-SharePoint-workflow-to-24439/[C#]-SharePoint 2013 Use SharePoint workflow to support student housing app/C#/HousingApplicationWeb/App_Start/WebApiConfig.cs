using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using HousingApplicationWeb.Models;
using Microsoft.Data.Edm;

namespace HousingApplicationWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Student>("Student");
            IEdmModel model = builder.GetEdmModel();
            config.Routes.MapODataRoute(routeName: "API", routePrefix: "api", model: model);
        }
    }
}