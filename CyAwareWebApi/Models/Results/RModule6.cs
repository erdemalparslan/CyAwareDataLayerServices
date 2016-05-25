/* 
This result is about SSL expire checker module
*/

namespace CyAwareWebApi.Models.Results
{
    public class RModule6 : ResultBase
    {
        public string domain { get; set; }
        public string ip { get; set; }
        public string expireDate { get; set; }
        public string fingerprint { get; set; }
        public string subject { get; set; }
        public string issuer { get; set; }
    }
    public class RModule6DTO : ResultBaseDTO
    {
        public string domain { get; set; }
        public string ip { get; set; }
        public string expireDate { get; set; }
        public string fingerprint { get; set; }
        public string subject { get; set; }
        public string issuer { get; set; }
    }
}