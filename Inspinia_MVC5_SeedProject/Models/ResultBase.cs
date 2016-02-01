using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class ResultBase
    {
        public int Id { get; set; }
        public string resultType { get; set; }
        
        // foreign keys
        public int policyId { get; set; }

        // Navigation properties 
        public virtual Policy policy { get; set; }
    }

    public class RModule1 : ResultBase
    {
        public string ipAddress { get; set; }
        public string tcpPortNumbers { get; set; }
        public string udpPortNumbers { get; set; }
    }

    public class RModule2 : ResultBase
    {
        public string idStr { get; set; }
        public string actualScreenName { get; set; }
        public int actualTweets { get; set; }
        public int actualCAPITALLETTERRatio { get; set; }
        public int actualFollowerNumber { get; set; }
        public int actualFalloweeNumber { get; set; }
        public string unusualContentFound { get; set; }
    }

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
}