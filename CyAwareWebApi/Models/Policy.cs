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

        //navigation properties
        public virtual Subscriber subscriber { get; set; }
        public virtual HashSet<EntityBase> entities { get; set; }
        public virtual Module module { get; set; }
        public virtual HashSet<Action> actions { get; set; }
        public virtual HashSet<Scan> scans { get; set; }

    }

    public class PolicyDTO
    {
        public int Id { get; set; }
        public DateTime setDate { get; set; }
        public bool isActive { get; set; }
        public DateTime? activationDate { get; set; }
        public bool isDeleted { get; set; }

        public bool s_isMonthly { get; set; }
        public bool s_isWeekly { get; set; }
        public bool s_isDaily { get; set; }
        public bool s_isHourly { get; set; }
        public bool s_isPerMinute { get; set; }
        public int s_period { get; set; }
        public int s_enableStartTime24Format { get; set; }
        public int s_enableEndTime24Format { get; set; }

        public int subscriberId { get; set; }
        public int moduleId { get; set; }
    }

    public class PolicyDTOEnriched : PolicyDTO
    {
        public SubscriberDTO subscriber { get; set; }
        public IEnumerable<EntityBaseDTO> entities { get; set; }
        public IEnumerable<ActionDTO> actions { get; set; }

        public static explicit operator PolicyDTOEnriched(Policy v)
        {
            PolicyDTOEnriched e = new PolicyDTOEnriched();
            e.Id = v.Id;
            e.setDate = v.setDate;
            e.isActive = v.isActive;
            e.activationDate = v.activationDate;
            e.isDeleted = v.isDeleted;
            e.s_isMonthly = v.s_isMonthly;
            e.s_isWeekly = v.s_isWeekly;
            e.s_isDaily = v.s_isDaily;
            e.s_isHourly = v.s_isHourly;
            e.s_isPerMinute = v.s_isPerMinute;
            e.s_period = v.s_period;
            e.s_enableStartTime24Format = v.s_enableStartTime24Format;
            e.s_enableEndTime24Format = v.s_enableEndTime24Format;
            e.subscriberId = v.subscriberId;
            e.moduleId = v.moduleId;
            e.entities = from n in v.entities select (EntityBaseDTO)n;
            //e.actions = from a in v.actions select (ActionDTO)a;
            return e;
        }
    }
}