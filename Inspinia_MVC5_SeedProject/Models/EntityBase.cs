using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Inspinia_MVC5_SeedProject.App_LocalResources;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class EntityBase
    {
        public int id { get; set; }
        public string entityType { get; set; }
        [Display(Name = "Subscriber Id")]
        public int subscriberId { get; set; }
        public int? mainEntityId { get; set; }

        // Navigation properties 
        public virtual Subscriber subscriber { get; set; }
        public virtual EntityBase mainEntity { get; set; }
        public virtual HashSet<EntityBase> subentities { get; set; }
        public virtual HashSet<EntityExtraForPolicy> extraInfo { get; set; }
    }

    public class Application : EntityBase
    {
        public string platform { get; set; }
        public string name { get; set; }
    }

    public class Dictionary : EntityBase
    {
        public string filePath { get; set; }
    }

    public class Domain : EntityBase
    {
        [Display(Name = "Domain Name")]
        public string domainName { get; set; }
    }

    public class Email : EntityBase
    {
        [Display(Name = "Email Address")]
        public string address { get; set; }
    }

    public class EHostname : EntityBase
    {
        public string hostname { get; set; }
    }

    public class Identificaiton : EntityBase
    {
        public string type { get; set; }
        public string idStr { get; set; }
    }

    public class InstagramProfile : SocialNetwork
    {
        public byte[] profilePictureMD5 { get; set; }
        public string bio { get; set; }
        public int dailyMaxPosts { get; set; }
    }

    public class Ip : EntityBase
    {
        [Display(Name = "IpAddress", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource),
              ErrorMessageResourceName = "IpRequired")]
        [RegularExpression(@"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$",
              ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "IpValidCheck")]
        public string ip { get; set; }
    }

    public class IpRange : Ip
    {
        [Display(Name = "IpRange", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource),
              ErrorMessageResourceName = "IpRangeRequired")]
        [Range(0,32, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "IpRangeLengthCheck")]
        public int range { get; set; }
    }

    public class Port : EntityBase
    {
        [Display(Name = "Port Number")]
        public int port { get; set; }
        [Display(Name = "Port Type")]
        public string type { get; set; }
    }

    public class Service : EntityBase
    {
        public string service { get; set; }
    }

    public class SocialNetwork : EntityBase
    {
        [Display(Name = "ProfileId", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource),
              ErrorMessageResourceName = "ProfileIdRequired")]
        public string idStr { get; set; }
        public int dailyMaxCAPITALLETTERRatio { get; set; }
        public int dailyMaxFollowerChangeRatio { get; set; }
        public int dailyMaxFalloweeChangeRatio { get; set; }
        public string screenName { get; set; }
        [Display(Name = "SocNetSearchStr", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource),
              ErrorMessageResourceName = "SocNetSearchStrRequired")]
        public string searchStringForUnusualContent { get; set; }
        public bool isHacked { get; set; }
    }

    public class Template : EntityBase
    {
        public string type { get; set; }
        public string content { get; set; }
    }

    public class TwitterProfile : SocialNetwork
    {
        public int dailyMaxTweets { get; set; }
    }

    public class Url : EntityBase
    {
        [Display(Name = "Url")]
        public string url { get; set; }
    }

    public class EntityExtraForPolicy
    {
        public int Id { get; set; }
        public string key { get; set; }
        public string value { get; set; }

        public int entityId { get; set; }
        public int? policyId { get; set; }
    }
}