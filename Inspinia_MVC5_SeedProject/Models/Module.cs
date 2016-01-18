using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Inspinia_MVC5_SeedProject.App_LocalResources;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class Module
    {
        public int id { get; set; }
        [Display(Name = "ModuleColumnHeader", ResourceType = typeof(Resource))]
        public string moduleName {
            get {
                if (id == 1)
                {
                    return Resource.ModuleName1;
                }
                else if(id == 2)
                {
                    return Resource.ModuleName2;
                }
                else if(id == 3)
                {
                    return Resource.ModuleName3;
                }
                return "";
            }
        }
        public string description { get; set; }
    }
}