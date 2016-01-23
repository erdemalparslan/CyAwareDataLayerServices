
namespace CyAwareWebApi.Models.Entities
{
    public class EHostname : EntityBase
    {
        public string hostname { get; set; }
    }

    public class EHostnameDTO : EntityBaseDTO
    {
        public string hostname { get; set; }
    }
}