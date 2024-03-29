﻿using System;
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
        public int scanid { get; set; }
        public int ?resultbaseid { get; set; }
        public DateTime occuringdate { get; set; }
        public DateTime ?dismissdate { get; set; }
        public int incidententityid { get; set; }

        [DefaultValue(false)]
        public bool isthrown { get; set; }

        [DefaultValue(ApplicationConstants.DEFAULT_SEVERITY_FOR_INCIDENT)]
        public int severitylevel { get; set; }

        public string incident { get; set; }

        // navigation properties
        public virtual Scan scan { get; set; }
        public virtual ResultBase resultbase { get; set; }

        public Alert()
        {

        }

        public Alert(int _scanid,  int _severitylevel, string _incident)
        {
            occuringdate = DateTime.Now;
            severitylevel = _severitylevel;
            incident = _incident;
            scanid = _scanid;
        }

    }

    public class AlertDTO
    {
        public int Id { get; set; }
        public int scanid { get; set; }
        public int? resultbaseid { get; set; }
        public DateTime occuringdate { get; set; }
        public DateTime? dismissdate { get; set; }
        public int incidententityid { get; set; }
        public bool isthrown { get; set; }
        public int severitylevel { get; set; }
        public string incident { get; set; }
        public int moduleId { get; set; }
    }

    public class AlertDTOEnriched : AlertDTO
    {
        public ScanDTO scan { get; set; }
        public ResultBaseDTO resultbase { get; set; }
    }
}