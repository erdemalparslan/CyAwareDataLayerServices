using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models.Entities
{
    public class EUrl : EntityBase
    {
        public string url { get; set; }

    }

    public class EUrlDTO : EntityBaseDTO
    {
        public string url { get; set; }

    }
}