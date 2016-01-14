using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class EntityBaseViewModel
    {
        public List<Ip> ipList { get; set; }
        public List<IpRange> ipRangeList { get; set; }
        public List<TwitterProfile> twitterList { get; set; }
        public List<InstagramProfile> instagramList { get; set; }
    }

    public class EditPolicyViewModel : EntityBaseViewModel
    {
        public Policy policy { get; set; }
    }
}