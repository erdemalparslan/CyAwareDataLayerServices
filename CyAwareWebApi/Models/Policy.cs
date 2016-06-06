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

        //public bool s_isMonthly { get; set; }
        //public bool s_isWeekly { get; set; }
        //public bool s_isDaily { get; set; }
        //public bool s_isHourly { get; set; }
        //public bool s_isPerMinute { get; set; }
        //public int s_period { get; set; }
        //public int s_enableStartTime24Format { get; set; }
        //public int s_enableEndTime24Format { get; set; }

        //foreign keys
        public int subscriberId { get; set; }
        public int moduleId { get; set; }

        //navigation properties
        public virtual Subscriber subscriber { get; set; }
        public virtual HashSet<EntityBase> entities { get; set; }
        public virtual HashSet<EntityExtraForPolicy> extraInfo { get; set; }
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

        //public bool s_isMonthly { get; set; }
        //public bool s_isWeekly { get; set; }
        //public bool s_isDaily { get; set; }
        //public bool s_isHourly { get; set; }
        //public bool s_isPerMinute { get; set; }
        //public int s_period { get; set; }
        //public int s_enableStartTime24Format { get; set; }
        //public int s_enableEndTime24Format { get; set; }

        public int subscriberId { get; set; }
        public int moduleId { get; set; }
    }

    public class PolicyDTOEnriched : PolicyDTO
    {
        public SubscriberDTO subscriber { get; set; }
        public IEnumerable<EntityBaseDTOEnriched> entities { get; set; }
        public IEnumerable<ActionDTO> actions { get; set; }
        public IEnumerable<EntityExtraForPolicyDTO> extraInfo { get; set; }


        public static explicit operator PolicyDTOEnriched(Policy p)
        {
            CyAwareContext db = new CyAwareContext();
            PolicyDTOEnriched pe = new PolicyDTOEnriched();
            pe.Id = p.Id;
            pe.setDate = p.setDate;
            pe.isActive = p.isActive;
            pe.activationDate = p.activationDate;
            pe.isDeleted = p.isDeleted;
            //CHANGE-MADE: due to the skype call on 06/01/2016 we removed scheduling mechanism from Policy entity
            //pe.s_isMonthly = p.s_isMonthly;
            //pe.s_isWeekly = p.s_isWeekly;
            //pe.s_isDaily = p.s_isDaily;
            //pe.s_isHourly = p.s_isHourly;
            //pe.s_isPerMinute = p.s_isPerMinute;
            //pe.s_period = p.s_period;
            //pe.s_enableStartTime24Format = p.s_enableStartTime24Format;
            //pe.s_enableEndTime24Format = p.s_enableEndTime24Format;
            pe.subscriberId = p.subscriberId;
            pe.moduleId = p.moduleId;
            pe.entities = from n in p.entities select (EntityBaseDTOEnriched)EntityBaseDTOEnriched.EntityBaseDTOEnrichedAll(n,p);
            //e.actions = from a in v.actions select (ActionDTO)a;
            return pe;
        }
    }
}