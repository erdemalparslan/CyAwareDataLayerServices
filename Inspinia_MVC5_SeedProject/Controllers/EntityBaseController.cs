using Inspinia_MVC5_SeedProject.Models;
using Inspinia_MVC5_SeedProject.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class EntityBaseController : BaseController
    {
        static public string session_str;
        static public MockDB mockDb;
        static public GenericHttpClient<EntityBase, int> httpClient;

        public EntityBaseController()
        {
            /*http://localhost:50334*/
            if (httpClient == null)
            {
                httpClient = new GenericHttpClient<EntityBase, int>("http://monaware.com", "front/entitybases/");
            }

            if(mockDb == null)
            {
                mockDb = new MockDB();
            }
            
            if(session_str == null)
            {
                session_str = "true";
            }
        }

        public string getSessionStr()
        {
            return session_str;
        }

        public MockDB getMockDB()
        {
            return mockDb;
        }

        [HttpGet]
        public ActionResult Mock()
        {
            Session["mock"] = "true";
            session_str = "true";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Unmock()
        {
            Session["mock"] = null;
            session_str = "";
            return RedirectToAction("Index");
        }

        public void eb_viewbag_set(List<EntityBase> assets)
        {

            List<Application> allApplications = new List<Application>();
            List<Dictionary> allDictionaries = new List<Dictionary>();
            List<Domain> allDomains = new List<Domain>();
            List<Email> allEmails = new List<Email>();
            List<Identificaiton> allIdentifications = new List<Identificaiton>();
            List<InstagramProfile> allInstagramProfiles = new List<InstagramProfile>();
            List<Ip> allIpAddresses = new List<Ip>();
            List<IpRange> allIpRanges = new List<IpRange>();
            List<Port> allPortNumbers = new List<Port>();
            List<Service> allServices = new List<Service>();
            List<Template> allTemplates = new List<Template>();
            List<TwitterProfile> allTwitterProfiles = new List<TwitterProfile>();
            List<Url> allUrls = new List<Url>();

            foreach (var entity in assets)
            {
                if (entity.entityType == "EApplication")
                {
                    allApplications.Add((Application)entity);
                }
                else if (entity.entityType == "EDictionary")
                {
                    allDictionaries.Add((Dictionary)entity);
                }
                else if (entity.entityType == "EDomain")
                {
                    allDomains.Add((Domain)entity);
                }
                else if (entity.entityType == "EEMailAddress")
                {
                    allEmails.Add((Email)entity);
                }
                else if (entity.entityType == "EIdentification")
                {
                    allIdentifications.Add((Identificaiton)entity);
                }
                else if (entity.entityType == "EInstagramProfile")
                {
                    allInstagramProfiles.Add((InstagramProfile)entity);
                }
                else if (entity.entityType == "EIpAddress")
                {
                    allIpAddresses.Add((Ip)entity);
                }
                else if (entity.entityType == "EIpRange")
                {
                    allIpRanges.Add((IpRange)entity);
                }
                else if (entity.entityType == "EPort")
                {
                    allPortNumbers.Add((Port)entity);
                }
                else if (entity.entityType == "EService")
                {
                    allServices.Add((Service)entity);
                }
                else if (entity.entityType == "ETemplate")
                {
                    allTemplates.Add((Template)entity);
                }
                else if (entity.entityType == "ETwitterProfile")
                {
                    allTwitterProfiles.Add((TwitterProfile)entity);
                }
                else if (entity.entityType == "EUrl")
                {
                    allUrls.Add((Url)entity);
                }
            }

            ViewBag.Applications = allApplications;
            ViewBag.Dictionaries = allDictionaries;
            ViewBag.Domains = allDomains;
            ViewBag.Emails = allEmails;
            ViewBag.Identifications = allIdentifications;
            ViewBag.InstagramProfiles = allInstagramProfiles;
            ViewBag.IpAddresses = allIpAddresses;
            ViewBag.IpRanges = allIpRanges;
            ViewBag.PortNumbers = allPortNumbers;
            ViewBag.Services = allServices;
            ViewBag.TwitterProfiles = allTwitterProfiles;
            ViewBag.Templates = allTemplates;
            ViewBag.Urls = allUrls;

        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<EntityBase> assets;
            if (session_str == "true")
            {
                assets = mockDb.getEntityBases();
            }
            else
            {
                //IEnumerable<EntityBase> list = await httpClient.GetAllAsync();
                string result = await httpClient.GetAllAsyncString();
                assets = JsonConvert.DeserializeObject<List<EntityBase>>(result, new JsonEntitiesConverter());
            }
            
            eb_viewbag_set(assets);

            return View();
        }

        // GET: /EntityBase/Create
        public ActionResult Create()
        {
            return PartialView("CreatePV/Main");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateInstagram(InstagramProfile instagramEntity)
        {
            if (session_str == "true")
            {
                mockDb.addEntityBase(instagramEntity);
            }
            else
            {
                await httpClient.PostAsync(instagramEntity);
            }
                
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateIp(Ip ipEntity)
        {
            if (session_str == "true")
            {
                mockDb.addEntityBase(ipEntity);
            }
            else
            {
                await httpClient.PostAsync(ipEntity);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateIpRange(IpRange ipRangeEntity)
        {
            if (session_str == "true")
            {
                mockDb.addEntityBase(ipRangeEntity);
            }
            else
            {
                await httpClient.PostAsync(ipRangeEntity);
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePort(Port portEntity)
        {
            await httpClient.PostAsync(portEntity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateEmail(Email emailEntity)
        {
            await httpClient.PostAsync(emailEntity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTwitter(TwitterProfile twitterEntity)
        {
            if (session_str == "true")
            {
                mockDb.addEntityBase(twitterEntity);
            }
            else
            {
                await httpClient.PostAsync(twitterEntity);
            }
            
            return RedirectToAction("Index");
        }

        // GET: /EntityBase/Edit/Entity/5
        public async Task<ActionResult> Edit(int id)
        {
            EntityBase entity;
            if (session_str == "true")
            {
                entity = mockDb.getEntityBaseById(id);
            }
            else
            {
                string result = await httpClient.GetByIdAsyncString(id);
                entity = JsonConvert.DeserializeObject<EntityBase>(result, new JsonEntitiesConverter());
            }

            if (entity.entityType == "EApplication")
            {
                return PartialView("EditPV/Application", (Application)entity);
            }
            else if (entity.entityType == "EDictionary")
            {
                return PartialView("EditPV/Dictionary", (Dictionary)entity);
            }
            else if (entity.entityType == "EDomain")
            {
                return PartialView("EditPV/Domain", (Domain)entity);
            }
            else if (entity.entityType == "EEMailAddress")
            {
                return PartialView("EditPV/Email", (Email)entity);
            }
            else if (entity.entityType == "EIdentification")
            {
                return PartialView("EditPV/Identificaiton", (Identificaiton)entity);
            }
            else if (entity.entityType == "EInstagramProfile")
            {
                return PartialView("EditPV/Instagram", (InstagramProfile)entity);
            }
            else if (entity.entityType == "EIpAddress")
            {
                return PartialView("EditPV/Ip", (Ip)entity);
            }
            else if (entity.entityType == "EIpRange")
            {
                return PartialView("EditPV/IpRange", (IpRange)entity);
            }
            else if (entity.entityType == "EPort")
            {
                return PartialView("EditPV/Port", (Port)entity);
            }
            else if (entity.entityType == "EService")
            {
                return PartialView("EditPV/Service", (Service)entity);
            }
            else if (entity.entityType == "ETemplate")
            {
                return PartialView("EditPV/Template", (Template)entity);
            }
            else if (entity.entityType == "ETwitterProfile")
            {
                return PartialView("EditPV/Twitter", (TwitterProfile)entity);
            }
            else if (entity.entityType == "EUrl")
            {
                return PartialView("EditPV/Url", (Url)entity);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditIp(Ip ipEntity)
        {
            if (session_str == "true")
            {
                mockDb.editEntityBase(ipEntity);
            }
            else
            {
                await httpClient.PutAsync(ipEntity.id, ipEntity);
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditIpRange(IpRange ipRangeEntity)
        {
            if (session_str == "true")
            {
                mockDb.editEntityBase(ipRangeEntity);
            }
            else
            {
                await httpClient.PutAsync(ipRangeEntity.id, ipRangeEntity);
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPort(Port portEntity)
        {
            await httpClient.PutAsync(portEntity.id, portEntity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTwitter(TwitterProfile twitterEntity)
        {
            if (session_str == "true")
            {
                mockDb.editEntityBase(twitterEntity);
            }
            else
            {
                await httpClient.PutAsync(twitterEntity.id, twitterEntity);
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditInstagram(InstagramProfile instagramEntity)
        {
            if (session_str == "true")
            {
                mockDb.editEntityBase(instagramEntity);
            }
            else
            {
                await httpClient.PutAsync(instagramEntity.id, instagramEntity);
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEmail(Email emailEntity)
        {
            await httpClient.PutAsync(emailEntity.id, emailEntity);
            return RedirectToAction("Index");
        }


        // GET: /EntityBase/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            EntityBase entity;
            if (session_str == "true")
            {
                entity = mockDb.getEntityBaseById(id);
            }
            else
            {
                string result = await httpClient.GetByIdAsyncString(id);
                entity = JsonConvert.DeserializeObject<EntityBase>(result, new JsonEntitiesConverter());
            }

            if (entity.entityType == "EApplication")
            {
                return PartialView("DeletePV/Application", (Application)entity);
            }
            else if (entity.entityType == "EDictionary")
            {
                return PartialView("DeletePV/Dictionary", (Dictionary)entity);
            }
            else if (entity.entityType == "EDomain")
            {
                return PartialView("DeletePV/Domain", (Domain)entity);
            }
            else if (entity.entityType == "EEMailAddress")
            {
                return PartialView("DeletePV/Email", (Email)entity);
            }
            else if (entity.entityType == "EIdentification")
            {
                return PartialView("DeletePV/Identificaiton", (Identificaiton)entity);
            }
            else if (entity.entityType == "EInstagramProfile")
            {
                return PartialView("DeletePV/Instagram", (InstagramProfile)entity);
            }
            else if (entity.entityType == "EIpAddress")
            {
                return PartialView("DeletePV/Ip", (Ip)entity);
            }
            else if (entity.entityType == "EIpRange")
            {
                return PartialView("DeletePV/IpRange", (IpRange)entity);
            }
            else if (entity.entityType == "EPort")
            {
                return PartialView("DeletePV/Port", (Port)entity);
            }
            else if (entity.entityType == "EService")
            {
                return PartialView("DeletePV/Service", (Service)entity);
            }
            else if (entity.entityType == "ETemplate")
            {
                return PartialView("DeletePV/Template", (Template)entity);
            }
            else if (entity.entityType == "ETwitterProfile")
            {
                return PartialView("DeletePV/Twitter", (TwitterProfile)entity);
            }
            else if (entity.entityType == "EUrl")
            {
                return PartialView("DeletePV/Url", (Url)entity);
            }

            return HttpNotFound();
        }

        // POST: /EntityBase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (session_str == "true")
            {
                mockDb.deleteEntityBase(id);
            }
            else
            {
                await httpClient.DeleteAsync(id);
            }

            return RedirectToAction("Index");
        }

        public async Task<string> TestRemote()
        {
            string result = "";

            IEnumerable<EntityBase> list = await httpClient.GetAllAsync();

            if (list.Count() != 0)
            {
                result = "Entity list cekme başarılı \n";
            }
            else
            {
                result = "Entity list cekme başarılı degil \n";
                return result;
            }

            Ip testIp = new Ip();
            testIp.entityType = "EIpAddress";
            testIp.ip = "192.168.3.3";
            testIp.subscriberId = 1;

            await httpClient.PostAsync(testIp);

            IEnumerable<EntityBase> list2 = await httpClient.GetAllAsync();

            if(list.Count()+1 == list2.Count())
            {
                result = "Entity ekleme başarılı \n";
            }
            else
            {
                result = "Entity ekleme başarılı değil \n";
                return result;
            }

            Ip testIpEdit = (Ip)list2.Last();
            testIpEdit.ip = "192.168.3.4";

            await httpClient.PutAsync(testIpEdit.id, testIpEdit);

            Ip testIpEditReturn = (Ip) await httpClient.GetByIdAsync(testIpEdit.id);

            if(testIpEdit.ip == testIpEditReturn.ip)
            {
                result = "Entity update başarılı \n";
            }
            else
            {
                result = "Entity update başarılı değil\n";
                return result;
            }

            await httpClient.DeleteAsync(testIpEdit.id);

            IEnumerable<EntityBase> list3 = await httpClient.GetAllAsync();

            if (list3.Count() + 1 == list2.Count())
            {
                result = "Entity silme başarılı \n";
            }
            else
            {
                result = "Entity silme başarılı değil \n";
                return result;
            }


            return result;
        }
    }
    
}