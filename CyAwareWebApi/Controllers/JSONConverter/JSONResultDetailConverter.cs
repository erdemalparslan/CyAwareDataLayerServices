using CyAwareWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using CyAwareWebApi.Models.Results;
using CyAwareWebApi.Exceptions;

namespace CyAwareWebApi.Controllers.JSONConverter
{
    public class JSONResultDetailConverter : JsonCreationConverter<ResultDetail>
    {
        protected override ResultDetail Create(Type objectType, JObject jsonObject)
        {
            var typeName = jsonObject["resultType"].ToString();
            switch (typeName)
            {
                /*case "RModule1":
                    return new RModule1.RModule1Detail();
                case "RModule2":
                    return new RModule2();
                case "RModule3":
                    return new RModule3();
                case "RModule4":
                    return new RModule4();
                case "RModule5":
                    return new RModule5();
                case "RModule6":
                    return new RModule6();*/
                case "RModule7":
                    return new RModule7.RModule7Detail();
                /*case "RModule8":
                    return new RModule8();
                case "RModule9":
                    return new RModule9();
                case "RModule10":
                    return new RModule10();
                case "RModule11":
                    return new RModule11();
                case "RModule12":
                    return new RModule12();
                case "RModule13":
                    return new RModule13();
                case "RModule14":
                    return new RModule14();
                case "RModule15":
                    return new RModule15();
                case "RModule16":
                    return new RModule16();*/
                default: throw new UnknownResultException("Unknown Result type: " + typeName);
            }
        }
    }
}