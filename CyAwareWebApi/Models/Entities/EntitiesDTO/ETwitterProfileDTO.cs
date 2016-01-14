﻿

namespace CyAwareWebApi.Models.EntitiesDTO
{
    public class ETwitterProfileDTO : EntityBaseDTO
    {
        public string idStr { get; set; }
        public string screenName { get; set; }
        public int dailyMaxTweets { get; set; }
        public int dailyMaxCAPITALLETTERRatio { get; set; }
        public int dailyMaxFollowerChangeRatio { get; set; }
        public int dailyMaxFalloweeChangeRatio { get; set; }
        public string searchStringForUnusualContent { get; set; }
        public bool isHacked { get; set; }
    }
}