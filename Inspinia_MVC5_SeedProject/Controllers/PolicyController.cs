using Inspinia_MVC5_SeedProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class PolicyController : BaseController
    {
        /*http://localhost:50334*/
        static private GenericHttpClient<Policy, int> httpClient;
        static private EntityBaseController ebc;

        public PolicyController()
        {
            if(httpClient == null)
            {
                httpClient = new GenericHttpClient<Policy, int>("http://monaware.com", "front/policies/");
            }

            if(ebc == null)
            {
                ebc = new EntityBaseController();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<Policy> policies;
            if (ebc.getSessionStr() == "true")
            {
                policies = ebc.getMockDB().getPolicies();
            }
            else
            {
                //IEnumerable<Policy> list = await httpClient.GetAllAsync();
                string result = await httpClient.GetAllAsyncString();
                policies = JsonConvert.DeserializeObject<List<Policy>>(result, new JsonEntitiesConverter());
            }



            foreach (var policy in policies)
            {
                policy.module.id = policy.moduleId;

                HashSet<EntityBase> entityList = policy.entities;
                foreach(var entity in entityList)
                {
                    string key = entity.id.ToString();
                    EntityBase test = (EntityBase)ViewData[key];
                    if (test == null)
                    {
                        EntityBase ebView;
                        if (ebc.getSessionStr() == "true") {
                            ebView = entity;
                        }
                        else
                        {
                            string result = await EntityBaseController.httpClient.GetByIdAsyncString(entity.id);
                            ebView = JsonConvert.DeserializeObject<EntityBase>(result, new JsonEntitiesConverter());
                        }

                        if (entity.entityType == "EIpAddress")
                        {
                            ViewData.Add(key, (Ip)ebView);
                        }
                        else if (entity.entityType == "EIpRange")
                        {
                            ViewData.Add(key, (IpRange)ebView);
                        }
                        else if (entity.entityType == "EInstagramProfile")
                        {
                            ViewData.Add(key, (InstagramProfile)ebView);
                        }
                        else if (entity.entityType == "ETwitterProfile")
                        {
                            ViewData.Add(key, (TwitterProfile)ebView);
                        }
                    }
                }
            }
            
            return View(policies);
        }

        // GET: /EntityBase/Create
        public async Task<ActionResult> Create()
        {
            List<EntityBase> assets;
            if (ebc.getSessionStr() == "true")
            {
                assets = ebc.getMockDB().getEntityBases();
            }
            else
            {
                GenericHttpClient<EntityBase, int> httpClientx = new GenericHttpClient<EntityBase, int>("http://monaware.com", "front/entitybases/subscriber/");

                string result = await httpClientx.GetByIdAsyncString(1);

                assets = JsonConvert.DeserializeObject<List<EntityBase>>(result, new JsonEntitiesConverter());
            }
            

            List<Ip> ipList = new List<Ip>();
            List<IpRange> ipRangeList = new List<IpRange>();
            List<TwitterProfile> tList = new List<TwitterProfile>();
            List<InstagramProfile> insList = new List<InstagramProfile>();

            foreach (var entity in assets)
            {
                if (entity.entityType == "EIpAddress")
                {
                    ipList.Add((Ip)entity);
                }
                else if (entity.entityType == "EIpRange")
                {
                    ipRangeList.Add((IpRange)entity);
                }
                else if (entity.entityType == "EInstagramProfile")
                {
                    insList.Add((InstagramProfile)entity);
                }
                else if (entity.entityType == "ETwitterProfile")
                {
                    tList.Add((TwitterProfile)entity);
                }
            }
            EntityBaseViewModel ebvm = new EntityBaseViewModel();
            ebvm.ipList = ipList;
            ebvm.ipRangeList = ipRangeList;
            ebvm.twitterList = tList;
            ebvm.instagramList = insList;
            Session["ebvm"] = ebvm;
            return PartialView(ebvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Policy policy)
        {
            EntityBaseViewModel ebvm = Session["ebvm"] as EntityBaseViewModel;
            HashSet<EntityBase> enList = new HashSet<EntityBase>();
            if (policy.module.id == 1)
            {
                foreach (int selectedId in policy.selectedObjects)
                {
                    foreach(Ip tmpIp in ebvm.ipList)
                    {
                        if(tmpIp.id == selectedId)
                        {
                            enList.Add(tmpIp);
                        }
                    }

                    foreach (IpRange tmpIpRange in ebvm.ipRangeList)
                    {
                        if (tmpIpRange.id == selectedId)
                        {
                            enList.Add(tmpIpRange);
                        }
                    }
                }
            }
            else if (policy.module.id == 2)
            {
                foreach (int selectedId in policy.selectedObjects)
                {
                    foreach (TwitterProfile tmpTp in ebvm.twitterList)
                    {
                        if (tmpTp.id == selectedId)
                        {
                            enList.Add(tmpTp);
                        }
                    }
                }
            }
            else if (policy.module.id == 3)
            {
                foreach (int selectedId in policy.selectedObjects)
                {
                    foreach (InstagramProfile tmpIp in ebvm.instagramList)
                    {
                        if (tmpIp.id == selectedId)
                        {
                            enList.Add(tmpIp);
                        }
                    }
                }
            }

            if(policy.scheduleType == 1)
            {
                policy.s_isMonthly = true;
            }
            else if (policy.scheduleType == 2)
            {
                policy.s_isWeekly = true;
            }
            else if (policy.scheduleType == 3)
            {
                policy.s_isDaily = true;
            }
            else if (policy.scheduleType == 4)
            {
                policy.s_isHourly = true;
            }
            else if (policy.scheduleType == 5)
            {
                policy.s_isPerMinute = true;
            }

            if(policy.s_period == 0)
            {
                policy.s_period = 1; // default
            }

            policy.entities = enList;

            policy.actionId = 1;
            //policy.subscriberId = 1;
            policy.isActive = true;

            if(ebc.getSessionStr() == "true")
            {
                ebc.getMockDB().addPolicy(policy);
            }
            else
            {
                await httpClient.PostAsync(policy);
            }

            Session["ebvm"] = null;

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            List<EntityBase> assets;
            if (ebc.getSessionStr() == "true")
            {
                assets = ebc.getMockDB().getEntityBases();
            }
            else
            {
                GenericHttpClient<EntityBase, int> httpClientx = new GenericHttpClient<EntityBase, int>("http://monaware.com", "front/entitybases/subscriber/");
                string result = await httpClientx.GetByIdAsyncString(1);
                assets = JsonConvert.DeserializeObject<List<EntityBase>>(result, new JsonEntitiesConverter());
            }

            List<Ip> ipList = new List<Ip>();
            List<IpRange> ipRangeList = new List<IpRange>();
            List<TwitterProfile> tList = new List<TwitterProfile>();
            List<InstagramProfile> insList = new List<InstagramProfile>();

            foreach (var entity in assets)
            {
                if (entity.entityType == "EIpAddress")
                {
                    ipList.Add((Ip)entity);
                }
                else if (entity.entityType == "EIpRange")
                {
                    ipRangeList.Add((IpRange)entity);
                }
                else if (entity.entityType == "EInstagramProfile")
                {
                    insList.Add((InstagramProfile)entity);
                }
                else if (entity.entityType == "ETwitterProfile")
                {
                    tList.Add((TwitterProfile)entity);
                }
            }

            Policy policy;
            if (ebc.getSessionStr() == "true")
            {
                policy = ebc.getMockDB().getPolicyById(id);
            }
            else
            {
                policy = await httpClient.GetByIdAsync(id);
            }

            if (policy.s_isMonthly)
            {
                policy.scheduleType = 1;
            } else if (policy.s_isWeekly)
            {
                policy.scheduleType = 2;
            } else if (policy.s_isDaily)
            {
                policy.scheduleType = 3;
            } else if (policy.s_isHourly)
            {
                policy.scheduleType = 4;
            } else if (policy.s_isPerMinute)
            {
                policy.scheduleType = 5;
            }

            EditPolicyViewModel epvm = new EditPolicyViewModel();
            epvm.ipList = ipList;
            epvm.ipRangeList = ipRangeList;
            epvm.twitterList = tList;
            epvm.instagramList = insList;
            epvm.policy = policy;
            Session["epvm"] = epvm;

            return PartialView(epvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Policy policy)
        {
            EditPolicyViewModel epvm = Session["epvm"] as EditPolicyViewModel;
            HashSet<EntityBase> enList = new HashSet<EntityBase>();
            if (policy.module.id == 1)
            {
                foreach (int selectedId in policy.selectedObjects)
                {
                    foreach (Ip tmpIp in epvm.ipList)
                    {
                        if (tmpIp.id == selectedId)
                        {
                            enList.Add(tmpIp);
                        }
                    }

                    foreach (IpRange tmpIpRange in epvm.ipRangeList)
                    {
                        if (tmpIpRange.id == selectedId)
                        {
                            enList.Add(tmpIpRange);
                        }
                    }
                }
            }
            else if (policy.module.id == 2)
            {
                foreach (int selectedId in policy.selectedObjects)
                {
                    foreach (TwitterProfile tmpTp in epvm.twitterList)
                    {
                        if (tmpTp.id == selectedId)
                        {
                            enList.Add(tmpTp);
                        }
                    }
                }
            }
            else if(policy.module.id == 3)
            {
                foreach (int selectedId in policy.selectedObjects)
                {
                    foreach (InstagramProfile tmpIp in epvm.instagramList)
                    {
                        if (tmpIp.id == selectedId)
                        {
                            enList.Add(tmpIp);
                        }
                    }
                }
            }

            if (policy.scheduleType == 1)
            {
                policy.s_isMonthly = true;
            }
            else if (policy.scheduleType == 2)
            {
                policy.s_isWeekly = true;
            }
            else if (policy.scheduleType == 3)
            {
                policy.s_isDaily = true;
            }
            else if (policy.scheduleType == 4)
            {
                policy.s_isHourly = true;
            }
            else if (policy.scheduleType == 5)
            {
                policy.s_isPerMinute = true;
            }

            if(policy.s_period == 0)
            {
                policy.s_period = 1; // default value
            }

            policy.entities = enList;

            if (ebc.getSessionStr() == "true")
            {
                ebc.getMockDB().editPolicy(policy);
            }
            else
            {
                await httpClient.PutAsync(policy.id, policy);
            }

            Session["epvm"] = null;

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Delete(int id)
        {
            Policy policy;
            if (ebc.getSessionStr() == "true")
            {
                policy = ebc.getMockDB().getPolicyById(id);
            }
            else
            {
                policy = await httpClient.GetByIdAsync(id);
            }
            return PartialView(policy);
        }

        // POST: /EntityBase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (ebc.getSessionStr() == "true")
            {
                ebc.getMockDB().deletePolicy(id);
            }
            else
            {
                await httpClient.DeleteAsync(id);
            }
            return RedirectToAction("Index");
        }
    }
    
}