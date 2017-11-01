using Nasa.Mission.Mars.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;

namespace Nasa.Mission.Mars.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //AddApiVersioning(config);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetRobotJobById",
                routeTemplate: "api/robots/{robotId}/jobs/{jobId}",
                defaults: new { }
            );
        }

        private static void AddApiVersioning(HttpConfiguration config) =>
            config.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                }
            );
    }
}
