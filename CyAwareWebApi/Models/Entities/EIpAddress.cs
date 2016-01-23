using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models.Entities
{
    public partial class EIpAddress : EntityBase
    {
        public string ip { get; set; }

    }

    public partial class EIpAddressDTO : EntityBaseDTO
    {
        public string ip { get; set; }
    }
}