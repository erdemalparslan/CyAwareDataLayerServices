
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CyAwareWebApi.Models.Entities;
using CyAwareWebApi.Exceptions;

namespace CyAwareWebApi.Models.EntitiesDTO
{
    public class EntityBaseDTO
    {
        public int Id { get; set; }
        public string entityType { get; set; }
        public int subscriberId { get; set; }
        public int? mainEntityId { get; set; }

        public static explicit operator EntityBaseDTO(EntityBase v)
        {
            EntityBaseDTO dto = null;

            if(v.entityType == "EIpAddress")
                dto = new EIpAddressDTO { ip = ((EIpAddress)v).ip };
            else if (v.entityType == "EDomain")
                dto = new EDomainDTO { domainName = ((EDomain)v).domainName };
            else if (v.entityType == "EHostname")
                dto = new EHostnameDTO { hostname = ((EHostname)v).hostname };
            else if (v.entityType == "EIpRange")
                dto = new EIpRangeDTO { ip = ((EIpRange)v).ip, range = ((EIpRange)v).range};
            else if (v.entityType == "EUrl")
                dto = new EUrlDTO { url = ((EUrl)v).url };
            else if (v.entityType == "EInstagramProfile")
                dto = new EInstagramProfileDTO { bio = ((EInstagramProfile)v).bio 
                                                , dailyMaxCAPITALLETTERRatio = ((EInstagramProfile)v).dailyMaxCAPITALLETTERRatio
                                                , dailyMaxFalloweeChangeRatio = ((EInstagramProfile)v).dailyMaxFalloweeChangeRatio
                                                , dailyMaxFollowerChangeRatio = ((EInstagramProfile)v).dailyMaxFollowerChangeRatio
                                                , dailyMaxPosts = ((EInstagramProfile)v).dailyMaxPosts
                                                , idStr = ((EInstagramProfile)v).idStr
                                                , isHacked = ((EInstagramProfile)v).isHacked
                                                , profilePictureMD5 = ((EInstagramProfile)v).profilePictureMD5
                                                , screenName = ((EInstagramProfile)v).screenName
                                                , searchStringForUnusualContent = ((EInstagramProfile)v).searchStringForUnusualContent};
            else if (v.entityType == "ETwitterProfile")
                dto = new ETwitterProfileDTO
                {
                    dailyMaxCAPITALLETTERRatio = ((ETwitterProfile)v).dailyMaxCAPITALLETTERRatio
                    ,dailyMaxFalloweeChangeRatio = ((ETwitterProfile)v).dailyMaxFalloweeChangeRatio
                    ,dailyMaxFollowerChangeRatio = ((ETwitterProfile)v).dailyMaxFollowerChangeRatio
                    ,idStr = ((ETwitterProfile)v).idStr
                    ,isHacked = ((ETwitterProfile)v).isHacked
                    ,screenName = ((ETwitterProfile)v).screenName
                    ,searchStringForUnusualContent = ((ETwitterProfile)v).searchStringForUnusualContent
                    , dailyMaxTweets = ((ETwitterProfile)v).dailyMaxTweets
                };
            else throw new UnknownEntityException("Unknown Entity type: " + v.entityType);

            dto.entityType = v.entityType;
            dto.Id = v.Id;
            dto.mainEntityId = v.mainEntityId;
            dto.subscriberId = v.subscriberId;
            return dto;
        }
    }
}