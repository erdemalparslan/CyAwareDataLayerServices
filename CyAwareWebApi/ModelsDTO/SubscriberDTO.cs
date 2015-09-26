using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CyAwareWebApi.Models.Entities;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CyAwareWebApi.Models
{
    public class SubscriberDTO
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