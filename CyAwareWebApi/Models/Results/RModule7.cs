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
        public HashSet<Detail> details { get; set; }

        public class Detail
        {
            public int Id { get; set; }
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
        public HashSet<Detail> details { get; set; }

        public class Detail
        {
            public int Id { get; set; }
            public string identifier { get; set; }
            public string type { get; set; }
            public string severity { get; set; }
            public string finding { get; set; }

            public static explicit operator Detail(RModule7.Detail v)
            {
                Detail d = new Detail();
                d.identifier = v.identifier;
                d.type = v.type;
                d.severity = v.severity;
                d.finding = v.finding;
                return d;
            }
        }
    }

    }


