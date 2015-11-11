
using System.Collections.Generic;


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
        public virtual HashSet<EntityBase> subentities { get; set; }
        public virtual EntityBase mainEntity { get; set; }
        public virtual Policy policy { get; set; }
    }
}