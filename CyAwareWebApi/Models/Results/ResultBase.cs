using System;
using CyAwareWebApi.Exceptions;
using System.Collections.Generic;

namespace CyAwareWebApi.Models.Results
{
    public class ResultBase
    {
        public int Id { get; set; }
        public string resultType { get; set; }
        // foreign keys
        public int policyId { get; set; }
        // Navigation properties 
        public virtual Policy policy { get; set; }

    }

    public class ResultBaseDTO
    {
        public int Id { get; set; }
        public string resultType { get; set; }
        // foreign keys
        public int policyId { get; set; }


        public static explicit operator ResultBaseDTO(ResultBase v)
        {
            ResultBaseDTO dto = null;
            if (v.resultType == "RModule1")
                dto = new RModule1DTO { ipAddress = ((RModule1)v).ipAddress, tcpPortNumbers = ((RModule1)v).tcpPortNumbers, udpPortNumbers = ((RModule1)v).udpPortNumbers };
            //else if (v.resultType == "RModule2")
            //    dto = new RModule2DTO {  };
            //else if (v.resultType == "RModule3")
            //    dto = new RModule3DTO { };
            //else if (v.resultType == "RModule4")
            //    dto = new RModule4DTO { };
            //else if (v.resultType == "RModule5")
            //    dto = new RModule5DTO { };
            //else if (v.resultType == "RModule6")
            //    dto = new RModule6DTO { };
            else if (v.resultType == "RModule7")
            {
                dto = new RModule7DTO { domain = ((RModule7)v).domain, ip = ((RModule7)v).ip, port = ((RModule7)v).port, information = ((RModule7)v).information };
                //    foreach(var ddto in ((RModule7)v).details)
                //        dto.details.Add(new RModule7DTO.Detail { finding = ((RModule7.Detail)ddto).finding , identifier = ((RModule7.Detail)ddto).finding , severity = ((RModule7.Detail)ddto).severity, type = ((RModule7.Detail)ddto).type});
                //
            }
            //else if (v.resultType == "RModule8")
            //    dto = new RModule8DTO { };
            //else if (v.resultType == "RModule9")
            //    dto = new RModule9DTO { };
            //else if (v.resultType == "RModule10")
            //    dto = new RModule10DTO { };
            else throw new UnknownResultException("Unknown Entity type: " + v.resultType);

            dto.Id = v.Id;
            dto.policyId = v.policyId;
            dto.resultType = v.resultType;

            return dto;
        }
    }

    public class ResultBaseDTOEnriched : ResultBaseDTO
    {
        public PolicyDTO policy { get; set; }
    }
}