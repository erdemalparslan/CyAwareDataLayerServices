/* 
This module is about Service and Systems Availability checker module
*/

using System.Collections.Generic;

namespace CyAwareWebApi.Models.Results
{
    public class RModule1 : ResultBase
    {
        public string ipAddress { get; set; }
        public string tcpPortNumbers { get; set; }
        public string udpPortNumbers { get; set; }

    }

    public class RModule1DTO : ResultBaseDTO
    {
        public string ipAddress { get; set; }
        public string tcpPortNumbers { get; set; }
        public string udpPortNumbers { get; set; }

    }
}