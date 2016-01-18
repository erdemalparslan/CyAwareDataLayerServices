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
        public DateTime ?activationDate { get; set; }
        public bool isDeleted { get; set; }

        public bool s_isMonthly { get; set; }
        public bool s_isWeekly { get; set; }
        public bool s_isDaily { get; set; }
        public bool s_isHourly { get; set; }
        public bool s_isPerMinute { get; set; }
        public int s_period { get; set; }
        public int s_enableStartTime24Format { get; set; }
        public int s_enableEndTime24Format { get; set; }

        //foreign keys
        public int subscriberId { get; set; }
        public int moduleId { get; set; }
        public int scheduleId { get; set; }

        //navigation properties
        public virtual Subscriber subscriber { get; set; }
        public virtual HashSet<EntityBase> entities { get; set; }
        public virtual Module module { get; set; }
        public virtual HashSet<Action> actions { get; set; }
        public virtual HashSet<Scan> scans { get; set; }

    }
}