using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using CyAwareWebApi.Models.Results;
using CyAwareWebApi.Exceptions;

namespace CyAwareWebApi.Controllers.JSONConverter
{
    public class JSONResultsConverter : JsonCreationConverter<ResultBase>
    {
        protected override ResultBase Create(Type objectType, JObject jsonObject)
        {
            var typeName = jsonObject["resultType"].ToString();
            switch (typeName)
            {
                case "RModule1":
                    return new RModule1();
                case "RModule2":
                    return new RModule2();
                case "RModule3":
                    return new RModule3();
                /*case "RModule4":
                    return new RModule1();
                case "RModule5":
                    return new RModule1();
                case "RModule6":
                    return new RModule1();
                case "RModule7":
                    return new RModule1();
                case "RModule8":
                    return new RModule1();
                case "RModule9":
                    return new RModule1();
                case "RModule10":
                    return new RModule1();
                case "RModule11":
                    return new RModule1();
                case "RModule12":
                    return new RModule1();
                case "RModule13":
                    return new RModule1();
                case "RModule14":
                    return new RModule1();
                case "RModule15":
                    return new RModule1();
                case "RModule16":
                    return new RModule1();*/
                default: throw new UnknownResultException("Unknown Result type: " + typeName);
            }
        }
    }
}