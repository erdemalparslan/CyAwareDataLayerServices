/* 
This result is about Search engines discovery module module
*/

namespace CyAwareWebApi.Models.Results
{
    public class RModule8 : ResultBase
    {
        public string domain { get; set; }
        public string ip { get; set; }
        public string wordFound { get; set; }
        public string engine { get; set; }
        public string url { get; set; }
    }
    public class RModule8DTO : ResultBaseDTO
    {
        public string domain { get; set; }
        public string ip { get; set; }
        public string wordFound { get; set; }
        public string engine { get; set; }
        public string url { get; set; }
    }
}