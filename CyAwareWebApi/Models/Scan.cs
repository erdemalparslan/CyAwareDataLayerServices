using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CyAwareWebApi.Models.Results;

namespace CyAwareWebApi.Models
{
    public class Scan
    {
        public int id { get; set; }
        public string scanRefId { get; set; }
        public int scanSuccessCode { get; set; }
        public bool isDeleted { get; set; }
        public DateTime scanDate { get; set; }


        // foreign key
        public int policyId { get; set; }

        //navigation property
        public virtual Policy policy { get; set; }
        public virtual HashSet<ResultBase> results { get; set; }
    }

    public class ScanDTO
    {
        public int id { get; set; }
        public string scanRefId { get; set; }
        public int scanSuccessCode { get; set; }
        public bool isDeleted { get; set; }
        public DateTime scanDate { get; set; }

        public int policyId { get; set; }

        public static explicit operator ScanDTO(Scan v)
        {
            ScanDTO dto = new ScanDTO();
            dto.id = v.id;
            dto.isDeleted = v.isDeleted;
            dto.scanDate = v.scanDate;
            dto.scanRefId = v.scanRefId;
            dto.scanSuccessCode = v.scanSuccessCode;
            return dto;
        }
    }

    public class ScanDTOEnriched : ScanDTO
    {
        //public virtual PolicyDTO policy { get; set; }
        public virtual IEnumerable<ResultBaseDTO> results { get; set; }

        public static explicit operator ScanDTOEnriched(Scan v)
        {
            ScanDTOEnriched dto = new ScanDTOEnriched();
            dto.id = v.id;
            dto.isDeleted = v.isDeleted;
            dto.scanDate = v.scanDate;
            dto.scanRefId = v.scanRefId;
            dto.scanSuccessCode = v.scanSuccessCode;
            dto.results = from r in v.results select (ResultBaseDTO)r;
            return dto;
        }
    }
}