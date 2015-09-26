using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models.Results
{
    public class ResultBase
    {
        public int Id { get; set; }
        public string resultType { get; set; }
        // foreign keys
        public int policyId { get; set; }

        // Navigation properties 
        public virtual Policy policy { get; set; }
    }
}