using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class EntityBase
    {
        public int id { get; set; }
        //public string entityType { get; set; }

        [Display(Name = "Subscriber ID")]
        public int subscriberId { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Type")]
        public string entityType { get; set; }

        [Display(Name = "Ip Address")]
        public string ip { get; set; }

        [Display(Name = "Email Address")]
        public string emailAddress { get; set; }

        [Display(Name = "Port Number")]
        public int port { get; set; }

        /*[Display(Name = "Port Type")]
        public int type { get; set; }*/
    }

    public class Ip : EntityBase
    {
        /*[Display(Name = "Ip Address")]
        public string ipAddress { get; set; }*/
    }

    public class Email : EntityBase
    {
        /*[Display(Name = "Email Address")]
        public string emailAddress { get; set; }*/
    }

    public class Url : EntityBase
    {
        public string urlAddress { get; set; }
    }

    public class MobilePhone : EntityBase
    {
        public string mobilePhone { get; set; }
    }

    public class PortNumber : EntityBase
    {
        /*public int portNumber { get; set; }*/
    }
}