/* 
This result is about SSL certificate details module
*/

namespace CyAwareWebApi.Models.Results
{
    public class RModule7 : ResultBase
    {
        public string domain { get; set; }
        public string ip { get; set; }
        public bool hasHeartBleed { get; set; }
        public string securityScore { get; set; }
        public string information { get; set; }
    }
}