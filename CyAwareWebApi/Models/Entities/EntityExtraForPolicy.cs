using System;

namespace CyAwareWebApi.Models.Entities
{
    public class EntityExtraForPolicy
    {
        public int Id { get; set; }
        public string key { get; set; }
        public string value { get; set; }

        public int entityId { get; set; }
        public int ?policyId { get; set; }

        public virtual EntityBase entity { get; set; }
        public virtual Policy policy { get; set; }
    }

    public class EntityExtraForPolicyDTO
    {
        //public int Id { get; set; }
        public string key { get; set; }
        public string value { get; set; }

        //public int entityId { get; set; }
        //public int? policyId { get; set; }

        public static explicit operator EntityExtraForPolicyDTO(EntityExtraForPolicy v)
        {
            EntityExtraForPolicyDTO dto = new EntityExtraForPolicyDTO();
            //dto.entityId = v.entityId;
            //dto.policyId = v.policyId;
            dto.key = v.key;
            dto.value = v.value;
            return dto;
        }
    }


    public class EntityExtraForPolicyDTOEnriched : EntityExtraForPolicyDTO
    {
        public virtual EntityBaseDTO entity { get; set; }
        public virtual PolicyDTO policy { get; set; }
    }
}