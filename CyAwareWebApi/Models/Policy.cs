using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CyAwareWebApi.Models.Entities;

namespace CyAwareWebApi.Models
{
    public class Policy
    {
        public int Id { get; set; }
        public DateTime setDate { get; set; }
        public bool isActive { get; set; }
        public DateTime activationDate { get; set; }

        //foreign keys
        public int subscriberId { get; set; }
        public int moduleId { get; set; }
        public int scheduleId { get; set; }
        public int actionId { get; set; }

        //navigation properties
        public virtual Subscriber subscriber { get; set; }
        public virtual HashSet<EntityBase> entities { get; set; }
        public virtual Module module { get; set; }
        public virtual Schedule schedule { get; set; }
        public virtual Action action { get; set; }

        public virtual HashSet<Scan> scans { get; set; }
    }
}