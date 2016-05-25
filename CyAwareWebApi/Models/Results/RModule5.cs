/* 
This result is about Domain expire checker module
*/

namespace CyAwareWebApi.Models.Results
{
    public class RModule5 : ResultBase
    {
        public string domain { get; set; }
        public string expireDate { get; set; }
    }
    public class RModule5DTO : ResultBaseDTO
    {
        public string domain { get; set; }
        public string expireDate { get; set; }
    }
}