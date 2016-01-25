using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class Scan
    {
        public int id { get; set; }
        public string scanRefId { get; set; }
        public int scanSuccessCode { get; set; }
        public bool isDeleted { get; set; }
        public DateTime scanDate { get; set; }


        // foreign key
        public int policyId { get; set; }

        //navigation property
        public virtual Policy policy { get; set; }
        public virtual HashSet<ResultBase> results { get; set; }
    }
}