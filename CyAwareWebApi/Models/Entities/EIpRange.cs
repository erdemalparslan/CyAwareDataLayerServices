﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models.Entities
{
    public class EIpRange : EntityBase
    {
        public string ip { get; set; }
        public int range { get; set; }
    }
}