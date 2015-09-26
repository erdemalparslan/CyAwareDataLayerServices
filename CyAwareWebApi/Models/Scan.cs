using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CyAwareWebApi.Models.Results;

namespace CyAwareWebApi.Models
{
    public class Scan
    {
        public int id { get; set; }
        public string scanRefId { get; set; }
        public int scanSuccessCode { get; set; } 
        
        // foreign key
        public int policyId { get; set; }

        //navigation property
        public virtual Policy policy { get; set; }
        public virtual HashSet<ResultBase> results { get; set; }
    }
}