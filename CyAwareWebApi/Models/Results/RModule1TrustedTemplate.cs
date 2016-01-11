using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models.Results
{
    public class RModule1TrustedTemplate
    {
        public string ipAddress { get; set; }
        public string tcpPortNumbers { get; set; }
        public string udpPortNumbers { get; set; }

        public int subscriberId { get; set; }

        public Subscriber subscriber { get; set; }
    }
}