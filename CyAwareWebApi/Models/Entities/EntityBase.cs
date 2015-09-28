using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CyAwareWebApi.Models;

namespace CyAwareWebApi.Models.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public string entityType { get; set; }
        // foreign keys
        public int subscriberId { get; set; }

        // Navigation properties 
        public virtual Subscriber subscriber { get; set; }
        //public virtual HashSet<Policy> policies { get; set; }
        public virtual HashSet<EntityBase> subentities { get; set; }
    }
}