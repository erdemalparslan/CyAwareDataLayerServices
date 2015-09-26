using CyAwareWebApi.Models;
using CyAwareWebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models
{
    public static class EntityBaseFactory
    {
        public static EntityBase getEntityBase(string type)
        {
            switch (type)
            {
                case "EIpAddress":
                    return new EIpAddress();
                default:
                    return new EIpAddress();
            }
        }
    }
}