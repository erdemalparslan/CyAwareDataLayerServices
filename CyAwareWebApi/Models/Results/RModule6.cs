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
    }
}