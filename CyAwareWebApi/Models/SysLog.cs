using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models
{
    public class SysLog
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string severity { get; set; } // exception warning info
        public string apiMethod { get; set; } // GET POST PUT DELETE
        public string source { get; set; } // /front/policies/{id}
        public string message { get; set; }
    }
}