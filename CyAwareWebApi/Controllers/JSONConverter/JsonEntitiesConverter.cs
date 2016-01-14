using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CyAwareWebApi.Models.Entities;
using Newtonsoft.Json.Linq;
using CyAwareWebApi.Exceptions;

namespace CyAwareWebApi.Controllers.JSONConverter
{
    public class JsonEntitiesConverter : JsonCreationConverter<EntityBase>
    {
        protected override EntityBase Create(Type objectType, JObject jsonObject)
        {
            var typeName = jsonObject["entityType"].ToString();
            switch (typeName)
            {
                case "EApplication":
                    return new EApplication();
                case "EDictionary":
                    return new EDictionary();
                case "EDomain":
                    return new EDomain();
                case "EHostname":
                    return new EHostname();
                case "EEMailAddress":
                    return new EEMailAddress();
                case "EIdentification":
                    return new EIdentification();
                case "EIpAddress":
                    return new EIpAddress();
                case "EIpRange":
                    return new EIpRange();
                case "EService":
                    return new EService();
                case "ETemplate":
                    return new ETemplate();
                case "EUrl":
                    return new EUrl();
                case "ETwitterProfile":
                    return new ETwitterProfile();
                case "EInstagramProfile":
                    return new EInstagramProfile();
                default: throw new UnknownEntityException("Unknown Entity type: " + typeName);
            }
        }
    }
}