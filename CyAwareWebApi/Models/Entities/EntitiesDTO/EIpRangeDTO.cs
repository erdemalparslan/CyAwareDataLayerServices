using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models.EntitiesDTO
{
    public class EIpRangeDTO : EntityBaseDTO
    {
        public string ip { get; set; }
        public int range { get; set; }
    }
}