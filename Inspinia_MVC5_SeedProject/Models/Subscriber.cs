using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class Subscriber
    {
        public int id { get; set; }
        public string name { get; set; }
        public int subscriptionId { get; set; }

        // Navigation property 
        public virtual HashSet<EntityBase> entities { get; set; }

        //public virtual HashSet<ENetworks> networks { get; set; }
        //public virtual HashSet<ESocNetId> socNetIds { get; set; }

        //public virtual HashSet<Policy> policies { get; set; }
    }
}