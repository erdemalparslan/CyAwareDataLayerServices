/* 
This module is about Twitter activity checker module
*/

namespace CyAwareWebApi.Models.Results
{
    public class RModule2 : ResultBase
    {
        public string idStr { get; set; }
        public string actualScreenName { get; set; }
        public int actualTweets { get; set; }
        public int actualCAPITALLETTERRatio { get; set; }
        public int actualFollowerChangeRatio { get; set; }
        public int actualFalloweeChangeRatio { get; set; }
        public string unusualContentFound { get; set; }

    }
}