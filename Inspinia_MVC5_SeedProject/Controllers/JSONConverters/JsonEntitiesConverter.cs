using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Inspinia_MVC5_SeedProject.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class JsonEntitiesConverter : JsonCreationConverter<EntityBase>
    {
        protected override EntityBase Create(Type objectType, JObject jsonObject)
        {
            var typeName = jsonObject["entityType"].ToString();
            switch (typeName)
            {
                case "EEMailAddress":
                    return new Email();
                case "EIpAddress":
                    return new Ip();
                case "EUrl":
                    return new Url();
                default:
                    return new Ip();
            }
        }
    }
}