using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models.Entities
{
    public class EIdentification : EntityBase
    {
        public string type { get; set; }
        public string idStr { get; set; }
    }
}