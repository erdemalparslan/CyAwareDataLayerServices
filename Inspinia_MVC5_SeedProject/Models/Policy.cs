﻿using Inspinia_MVC5_SeedProject.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class Policy
    {
        public int id { get; set; }
        public DateTime setDate { get; set; }
        [Display(Name = "StatusColHeader", ResourceType = typeof(Resource))]
        public bool isActive { get; set; }
        [Display(Name = "ActivationDateColHeader", ResourceType = typeof(Resource))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? activationDate { get; set; }

        //foreign keys
        public int subscriberId { get; set; }
        
        public int actionId { get; set; }
        public int moduleId { get; set; }

        //navigation properties
        //public virtual Subscriber subscriber { get; set; }
        [Display(Name = "EntityBaseTitle", ResourceType = typeof(Resource))]
        public virtual HashSet<EntityBase> entities { get; set; }
        public virtual List<int> selectedObjects { get; set; }

        //[Display(Name = "ScheduleColumnHeader", ResourceType = typeof(Resource))]
        //public virtual Schedule schedule { get; set; }
        
        [Display(Name = "ModuleColumnHeader", ResourceType = typeof(Resource))]
        public virtual Module module { get; set; }

        [Display(Name = "Action", ResourceType = typeof(Resource))]
        //public virtual Action action { get; set; }


        public int scheduleType { get; set; }
        public bool s_isMonthly { get; set; }
        public bool s_isWeekly { get; set; }
        public bool s_isDaily { get; set; }
        public bool s_isHourly { get; set; }
        public bool s_isPerMinute { get; set; }

        [Display(Name = "PolicySchedulePeriod", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource),
              ErrorMessageResourceName = "SchedulePeriodRequired")]
        [Range(1, 100, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "SchedulePeriodValueCheck")]
        public int s_period { get; set; }

        [Display(Name = "PolicyScheduleStartAt", ResourceType = typeof(Resource))]
        [Range(0, 24, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "ScheduleStartValueCheck")]
        public int s_enableStartTime24Format { get; set; }

        [Display(Name = "PolicyScheduleFinishAt", ResourceType = typeof(Resource))]
        [Range(0, 24, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "ScheduleFinishValueCheck")]
        public int s_enableEndTime24Format { get; set; }
    }
}