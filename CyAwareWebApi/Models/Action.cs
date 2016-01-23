using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models
{
    public class Action
    {
        public int id { get; set; }
        public int actionType { get; set; }
        public string destination { get; set; }

        //navigation property
        public virtual HashSet<Policy> policies { get; set; }
    }

    public class ActionDTO
    {
        public int id { get; set; }
        public int actionType { get; set; }
        public string destination { get; set; }
    }

    public class ActionDTOEnriched : ActionDTO
    {
        public IEnumerable<PolicyDTO> policies { get; set; }

    }
}