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
                case "EApplication":
                    return new Application();
                case "EDictionary":
                    return new Dictionary();
                case "EDomain":
                    return new Domain();
                case "EEMailAddress":
                    return new Email();
                case "EIdentification":
                    return new Identificaiton();
                case "EHostname":
                    return new EHostname();
                case "EInstagramProfile":
                    return new InstagramProfile();
                case "EIpAddress":
                    return new Ip();
                case "EIpRange":
                    return new IpRange();
                case "EService":
                    return new Service();
                case "ETemplate":
                    return new Template();
                case "ETwitterProfile":
                    return new TwitterProfile();
                case "EUrl":
                    return new Url();
                default:
                    return new EntityBase();
            }
        }
    }
}