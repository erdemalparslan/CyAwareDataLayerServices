using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using CyAwareWebApi.Models.Results;
using CyAwareWebApi.Controllers.JSONConverter;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using System.Web.Http.Tracing;

namespace CyAwareWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.Re‌​ferenceLoopHandling = ReferenceLoopHandling.Ignore; ;
            json.SerializerSettings.Converters.Add(new JsonEntitiesConverter());
            json.SerializerSettings.Converters.Add(new JSONResultsConverter());
            //json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            json.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            SystemDiagnosticsTraceWriter traceWriter = config.EnableSystemDiagnosticsTracing();
            traceWriter.IsVerbose = true;
            traceWriter.MinimumLevel = TraceLevel.Debug;
            config.Services.Replace(typeof(System.Web.Http.Tracing.ITraceWriter), new ExceptionTracer());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ////// New code:
            //ODataModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<AlertDTO>("Alerts");
            //config.MapODataServiceRoute(
            //    routeName: "ODataRoute",
            //    routePrefix: null,
            //    model: builder.GetEdmModel());

        }
    }
}
