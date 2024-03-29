﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models.Entities
{
    public class EDomain : EntityBase
    {
        public string domainName { get; set; }
    }

    public class EDomainDTO : EntityBaseDTO
    {
        public string domainName { get; set; }
    }

    public class EDomainDTOEnriched : EntityBaseDTOEnriched
    {
        public string domainName { get; set; }
    }
}