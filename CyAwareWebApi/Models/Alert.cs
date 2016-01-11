using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CyAwareWebApi.Models.Entities;
using CyAwareWebApi.Models.Results;
using System.ComponentModel;

namespace CyAwareWebApi.Models
{
    public class Alert
    {
        public int Id { get; set; }
        //public int scanId { get; set; }
        //public int resultbaseId { get; set; }
        //public DateTime occuringDate { get; set; }
        //public DateTime dismissDate { get; set; }
        //public int incidentEntityId { get; set; }

        //[DefaultValue(false)]
        //public bool isThrown { get; set; }

        //[DefaultValue(ApplicationConstants.DEFAULT_SEVERITY_FOR_INCIDENT)]
        //public int severityLevel { get; set; }

        //public string incident { get; set; }

        //// navigation properties
        //public virtual Scan scan { get; set; }
        //public virtual ResultBase resultbase { get; set; }
    }
}