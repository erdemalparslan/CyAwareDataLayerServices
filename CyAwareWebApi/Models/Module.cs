using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models
{
    public class Module
    {
        public int id { get; set; }
        public string moduleName { get; set; }
        public string description { get; set; }
        public bool isDeleted { get; set; }


        //navigation property
        public virtual HashSet<Policy> policies { get; set; }
    }

    public class ModuleDTO
    {
        public int id { get; set; }
        public string moduleName { get; set; }
        public string description { get; set; }
        public bool isDeleted { get; set; }
    }

    public class ModuleDTOEnriched : ModuleDTO
    {
        public IEnumerable<PolicyDTOEnriched> policies { get; set; }

        public static explicit operator ModuleDTOEnriched(Module v)
        {
            ModuleDTOEnriched e = new ModuleDTOEnriched();
            e.id = v.id;
            e.moduleName = v.moduleName;
            e.description = v.description;
            e.isDeleted = v.isDeleted;
            e.policies = from p in v.policies select (PolicyDTOEnriched)p;
            return e;
        }
    }
}