using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models
{
    public class Module
    {
        public int id { get; set; }
        public string moduleName { get; set; }
        public string description { get; set; }
        public bool isDeleted { get; set; }


        //navigation property
        public virtual HashSet<Policy> policies { get; set; }
    }
}