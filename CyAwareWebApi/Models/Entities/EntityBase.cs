
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyAwareWebApi.Models.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public string entityType { get; set; }
        // foreign keys
        public int subscriberId { get; set; }
        public int? mainEntityId { get; set; }

        // Navigation properties 
        public virtual Subscriber subscriber { get; set; }
        public virtual HashSet<EntityBase> subentities { get; set; }
        public virtual EntityBase mainEntity { get; set; }
        public virtual HashSet<EntityExtraForPolicy> extraInfo { get; set; }
    }
}