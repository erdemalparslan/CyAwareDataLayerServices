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

        public static explicit operator ActionDTO(Action v)
        {
            ActionDTO a = new ActionDTO();
            a.id = v.id;
            a.actionType = v.actionType;
            a.destination = v.destination;
            return a;
        }
    }

    public class ActionDTOEnriched : ActionDTO
    {
        public IEnumerable<PolicyDTO> policies { get; set; }

    }
}