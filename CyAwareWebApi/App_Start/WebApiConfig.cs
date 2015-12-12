using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using CyAwareWebApi.Models.Results;
using CyAwareWebApi.Controllers.JSONConverter;

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
            json.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Services.Replace(typeof(System.Web.Http.Tracing.ITraceWriter), new ExceptionTracer());


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
