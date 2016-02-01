using System.Collections.Generic;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.App_LocalResources;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class ResourceController : BaseController
    {
        public JsonResult GetResources()
        {
            return Json(new Dictionary<string, string> {
                {"PolicyStepFollow", Resource.PolicyStepFollow},
                {"PolicySelectEntityInfo", Resource.PolicySelectEntityInfo},
                {"PolicySelectModuleInfo", Resource.PolicySelectModuleInfo},
                {"PolicyFillAllFields", Resource.PolicyFillAllFields}

            }, JsonRequestBehavior.AllowGet);
        }
    }
}