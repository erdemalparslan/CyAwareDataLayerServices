using CyAwareWebApi.Exceptions;


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

            dto.Id = v.Id;
            dto.policyId = v.policyId;
            dto.resultType = v.resultType;

            if (v.resultType == "RModule1")
                dto = new RModule1DTO { ipAddress = ((RModule1)v).ipAddress, tcpPortNumbers = ((RModule1)v).tcpPortNumbers, udpPortNumbers = ((RModule1)v).udpPortNumbers  };
            //else if (v.entityType == "EDomain")
            //    dto = new EDomainDTO { domainName = ((EDomain)v).domainName };
            //else if (v.entityType == "EHostname")
            //    dto = new EHostnameDTO { hostname = ((EHostname)v).hostname };
            //else if (v.entityType == "EIpRange")
            //    dto = new EIpRangeDTO { ip = ((EIpRange)v).ip, range = ((EIpRange)v).range };
            //else if (v.entityType == "EUrl")
            //    dto = new EUrlDTO { url = ((EUrl)v).url };
            //else if (v.entityType == "EInstagramProfile")
            //    dto = new EInstagramProfileDTO
            //    {
            //        bio = ((EInstagramProfile)v).bio
            //                                    ,
            //        dailyMaxCAPITALLETTERRatio = ((EInstagramProfile)v).dailyMaxCAPITALLETTERRatio
            //                                    ,
            //        dailyMaxFalloweeChangeRatio = ((EInstagramProfile)v).dailyMaxFalloweeChangeRatio
            //                                    ,
            //        dailyMaxFollowerChangeRatio = ((EInstagramProfile)v).dailyMaxFollowerChangeRatio
            //                                    ,
            //        dailyMaxPosts = ((EInstagramProfile)v).dailyMaxPosts
            //                                    ,
            //        idStr = ((EInstagramProfile)v).idStr
            //                                    ,
            //        isHacked = ((EInstagramProfile)v).isHacked
            //                                    ,
            //        profilePictureMD5 = ((EInstagramProfile)v).profilePictureMD5
            //                                    ,
            //        screenName = ((EInstagramProfile)v).screenName
            //                                    ,
            //        searchStringForUnusualContent = ((EInstagramProfile)v).searchStringForUnusualContent
            //    };
            //else if (v.entityType == "ETwitterProfile")
            //    dto = new ETwitterProfileDTO
            //    {
            //        dailyMaxCAPITALLETTERRatio = ((ETwitterProfile)v).dailyMaxCAPITALLETTERRatio
            //        ,
            //        dailyMaxFalloweeChangeRatio = ((ETwitterProfile)v).dailyMaxFalloweeChangeRatio
            //        ,
            //        dailyMaxFollowerChangeRatio = ((ETwitterProfile)v).dailyMaxFollowerChangeRatio
            //        ,
            //        idStr = ((ETwitterProfile)v).idStr
            //        ,
            //        isHacked = ((ETwitterProfile)v).isHacked
            //        ,
            //        screenName = ((ETwitterProfile)v).screenName
            //        ,
            //        searchStringForUnusualContent = ((ETwitterProfile)v).searchStringForUnusualContent
            //        ,
            //        dailyMaxTweets = ((ETwitterProfile)v).dailyMaxTweets
            //    };
            else throw new UnknownResultException("Unknown Entity type: " + v.resultType);


            return dto;
        }
    }

    public class ResultBaseDTOEnriched : ResultBaseDTO
    {
        public PolicyDTO policy { get; set; }
    }
}