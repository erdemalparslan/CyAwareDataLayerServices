using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models
{
    public class Schedule
    {
        [Key, ForeignKey("policy")]
        public int id { get; set; }
        public bool isMonthly { get; set; }
        public bool isWeekly { get; set; }
        public bool isDaily { get; set; }
        public bool isHourly { get; set; }
        public bool isPerMinute { get; set; }
        public int period { get; set; }
        public int enableStartTime24Format { get; set; }
        public int enableEndTime24Format { get; set; }

        //navigation property
        public virtual Policy policy { get; set; }
    }
}