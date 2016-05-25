/* 
This module is about Instagram activity checker module
*/

namespace CyAwareWebApi.Models.Results
{
    public class RModule3 : ResultBase
    {
        public string idStr { get; set; }
        public byte[] actualProfilePictureMD5 { get; set; }
        public string actualScreenName { get; set; }
        public int actualPosts { get; set; }
        public int actualCAPITALLETTERRatio { get; set; }
        public int actualFollowerNumber { get; set; }
        public int actualFalloweeNumber { get; set; }
        public string unusualContentFound { get; set; }
    }
    public class RModule3DTO : ResultBaseDTO
    {
        public string idStr { get; set; }
        public byte[] actualProfilePictureMD5 { get; set; }
        public string actualScreenName { get; set; }
        public int actualPosts { get; set; }
        public int actualCAPITALLETTERRatio { get; set; }
        public int actualFollowerNumber { get; set; }
        public int actualFalloweeNumber { get; set; }
        public string unusualContentFound { get; set; }
    }
}