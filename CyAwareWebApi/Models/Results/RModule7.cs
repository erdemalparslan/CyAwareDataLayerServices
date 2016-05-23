/* 
This result is about SSL certificate details module
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyAwareWebApi.Models.Results
{
    public class RModule7 : ResultBase
    {
        public string domain { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public string information { get; set; }

        public class RModule7Detail : ResultDetail
        {
            public string identifier { get; set; }
            public string type { get; set; }
            public string severity { get; set; }
            public string finding { get; set; }
        }
    }


    public class RModule7DTO : ResultBaseDTO
    {
        public string domain { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public string information { get; set; }
    }

    }


