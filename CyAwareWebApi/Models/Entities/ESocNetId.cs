using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models.Entities
{
    public partial class ESocNetId : EntityBase
    {
        public string type { get; set; }
        public string idStr { get; set; }
    }
}