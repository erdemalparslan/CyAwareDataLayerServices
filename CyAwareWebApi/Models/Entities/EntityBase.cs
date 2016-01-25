
using System.Collections.Generic;
using CyAwareWebApi.Exceptions;
namespace CyAwareWebApi.Models.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public string entityType { get; set; }
        public bool isDeleted { get; set; }

        // foreign keys
        public int subscriberId { get; set; }
        public int? mainEntityId { get; set; }

        // Navigation properties 
        public virtual Subscriber subscriber { get; set; }
        public virtual HashSet<EntityBase> subentities { get; set; }
        public virtual EntityBase mainEntity { get; set; }
        public virtual HashSet<EntityExtraForPolicy> extraInfo { get; set; }
        public virtual HashSet<Policy> policies { get; set; }
    }

    public class EntityBaseDTO
    {
        public int Id { get; set; }
        public string entityType { get; set; }
        public bool isDeleted { get; set; }
        public int subscriberId { get; set; }
        public int? mainEntityId { get; set; }

        public static explicit operator EntityBaseDTO(EntityBase v)
        {
            EntityBaseDTO dto = null;

            if (v.entityType == "EIpAddress")
                dto = new EIpAddressDTO { ip = ((EIpAddress)v).ip };
            else if (v.entityType == "EDomain")
                dto = new EDomainDTO { domainName = ((EDomain)v).domainName };
            else if (v.entityType == "EHostname")
                dto = new EHostnameDTO { hostname = ((EHostname)v).hostname };
            else if (v.entityType == "EIpRange")
                dto = new EIpRangeDTO { ip = ((EIpRange)v).ip, range = ((EIpRange)v).range };
            else if (v.entityType == "EUrl")
                dto = new EUrlDTO { url = ((EUrl)v).url };
            else if (v.entityType == "EInstagramProfile")
                dto = new EInstagramProfileDTO
                {
                    bio = ((EInstagramProfile)v).bio
                                                ,
                    dailyMaxCAPITALLETTERRatio = ((EInstagramProfile)v).dailyMaxCAPITALLETTERRatio
                                                ,
                    dailyMaxFalloweeChangeRatio = ((EInstagramProfile)v).dailyMaxFalloweeChangeRatio
                                                ,
                    dailyMaxFollowerChangeRatio = ((EInstagramProfile)v).dailyMaxFollowerChangeRatio
                                                ,
                    dailyMaxPosts = ((EInstagramProfile)v).dailyMaxPosts
                                                ,
                    idStr = ((EInstagramProfile)v).idStr
                                                ,
                    isHacked = ((EInstagramProfile)v).isHacked
                                                ,
                    profilePictureMD5 = ((EInstagramProfile)v).profilePictureMD5
                                                ,
                    screenName = ((EInstagramProfile)v).screenName
                                                ,
                    searchStringForUnusualContent = ((EInstagramProfile)v).searchStringForUnusualContent
                };
            else if (v.entityType == "ETwitterProfile")
                dto = new ETwitterProfileDTO
                {
                    dailyMaxCAPITALLETTERRatio = ((ETwitterProfile)v).dailyMaxCAPITALLETTERRatio
                    ,
                    dailyMaxFalloweeChangeRatio = ((ETwitterProfile)v).dailyMaxFalloweeChangeRatio
                    ,
                    dailyMaxFollowerChangeRatio = ((ETwitterProfile)v).dailyMaxFollowerChangeRatio
                    ,
                    idStr = ((ETwitterProfile)v).idStr
                    ,
                    isHacked = ((ETwitterProfile)v).isHacked
                    ,
                    screenName = ((ETwitterProfile)v).screenName
                    ,
                    searchStringForUnusualContent = ((ETwitterProfile)v).searchStringForUnusualContent
                    ,
                    dailyMaxTweets = ((ETwitterProfile)v).dailyMaxTweets
                };
            else throw new UnknownEntityException("Unknown Entity type: " + v.entityType);

            dto.entityType = v.entityType;
            dto.Id = v.Id;
            dto.mainEntityId = v.mainEntityId;
            dto.subscriberId = v.subscriberId;
            dto.isDeleted = v.isDeleted;
            return dto;
        }
    }

    public class EntityBaseDTOEnriched : EntityBaseDTO
    {
        public SubscriberDTO subscriber { get; set; }
        public IEnumerable<EntityBaseDTO> subentities { get; set; }
        public EntityBaseDTO mainEntity { get; set; }
        public IEnumerable<EntityExtraForPolicyDTO> extraInfo { get; set; }
        public IEnumerable<PolicyDTO> policies { get; set; }
    }
}