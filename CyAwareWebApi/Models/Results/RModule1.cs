using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Models.Results
{
    public class RModule1 : ResultBase
    {
        public string ipAddress { get; set; }
        public int portNumber { get; set; }
    }
}