
namespace CyAwareWebApi.Models.Entities
{
    public class EInstagramProfile : EntityBase
    {
        public string idStr { get; set; }
        public byte[] profilePictureMD5 { get; set; }
        public string bio { get; set; }
        public string screenName { get; set; }
        public int dailyMaxPosts { get; set; }
        public int dailyMaxCAPITALLETTERRatio { get; set; }
        public int dailyMaxFollowerChangeRatio { get; set; }
        public int dailyMaxFalloweeChangeRatio { get; set; }
        public string searchStringForUnusualContent { get; set; }
        public bool isHacked { get; set; }
    }

    public class EInstagramProfileDTO : EntityBaseDTO
    {
        public string idStr { get; set; }
        public byte[] profilePictureMD5 { get; set; }
        public string bio { get; set; }
        public string screenName { get; set; }
        public int dailyMaxPosts { get; set; }
        public int dailyMaxCAPITALLETTERRatio { get; set; }
        public int dailyMaxFollowerChangeRatio { get; set; }
        public int dailyMaxFalloweeChangeRatio { get; set; }
        public string searchStringForUnusualContent { get; set; }
        public bool isHacked { get; set; }
    }
}