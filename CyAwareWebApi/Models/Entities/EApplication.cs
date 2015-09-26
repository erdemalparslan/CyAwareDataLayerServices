using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models.Entities
{
    public class EApplication : EntityBase
    {
        public string platform { get; set; }
        public string name { get; set; }
    }
}