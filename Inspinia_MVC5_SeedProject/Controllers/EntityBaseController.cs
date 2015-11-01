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
    public class EntityBaseController : Controller
    {
        /*http://localhost:50334*/
        private GenericHttpClient<EntityBase, int> httpClient = new GenericHttpClient<EntityBase, int>("https://monaware.com", "front/entitybases/");

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //IEnumerable<EntityBase> list = await httpClient.GetAllAsync();
            string result = await httpClient.GetAllAsyncString();
            List<EntityBase> assets = JsonConvert.DeserializeObject<List<EntityBase>>(result, new JsonEntitiesConverter());
            return View(assets);
        }

        /*
        // GET: /EntityBase/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            EntityBase eb = await httpClient.GetByIdAsync(id);
            if (eb == null)
            {
                return HttpNotFound();
            }
            return View(eb);
        }
        */

        // GET: /EntityBase/Create
        public ActionResult Create()
        {
            return PartialView("CreatePartial");
        }

        // POST: /EntityBase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EntityBase eb)
        {
            string entityType = RouteData.Values["type"].ToString();
            if (entityType != null)
            {
                if(entityType.Equals("Ip"))
                {
                    eb.entityType = "EIpAddress";
                }
                else if(entityType.Equals("Email"))
                {
                    eb.entityType = "EEmailAddress";
                }
                else if (entityType.Equals("Port"))
                {
                    eb.entityType = "EPort";
                }
                else if (entityType.Equals("SocNet"))
                {
                    eb.entityType = "ESocNetId";
                }
                else if (entityType.Equals("IpRange"))
                {
                    eb.entityType = "EIpRange";
                }

                await httpClient.PostAsync(eb);
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        // GET: /EntityBase/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            EntityBase eb = await httpClient.GetByIdAsync(id);
            if (eb == null)
            {
                return HttpNotFound();
            }
            return View(eb);
        }

        // POST: /EntityBase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EntityBase eb)
        {
            string entityType = RouteData.Values["type"].ToString();
            if (entityType != null)
            {
                if (entityType.Equals("Ip"))
                {
                    eb.entityType = "EIpAddress";
                }
                else if (entityType.Equals("Email"))
                {
                    eb.entityType = "EEmailAddress";
                }
                else if (entityType.Equals("Port"))
                {
                    eb.entityType = "EPort";
                }
                else if (entityType.Equals("SocNet"))
                {
                    eb.entityType = "ESocNetId";
                }
                else if (entityType.Equals("IpRange"))
                {
                    eb.entityType = "EIpRange";
                }

                await httpClient.PutAsync(eb.id, eb);
                //string resultContent = result.Content.ReadAsStringAsync().Result; 
                return RedirectToAction("Index");
            }

            return HttpNotFound();
        }

        /*
        public EntityBase getElement(int id)
        {
            EntityBase eb = new EntityBase();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            //response = httpClient.GetAsync("http://localhost:50334/api/entitybase/" + id).Result;
            response = httpClient.GetAsync("http://localhost:50334/api/entitybase/" + id).Result;
            response.EnsureSuccessStatusCode();
            eb = response.Content.ReadAsAsync<EntityBase>().Result;
            return eb;
        }

        public List<EntityBase> getElements()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = httpClient.GetAsync("http://localhost:50334/api/entitybase/").Result;
            //response = httpClient.GetAsync("https://monaware.com/api/subscribers/").Result;
            response.EnsureSuccessStatusCode();
            List<EntityBase> list = response.Content.ReadAsAsync<List<EntityBase>>().Result;
            return list;
        }
        */

        // GET: /EntityBase/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            EntityBase eb = await httpClient.GetByIdAsync(id);
            if (eb == null)
            {
                return HttpNotFound();
            }
            return View(eb);
        }

        // POST: /EntityBase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //to be refined
            httpClient = new GenericHttpClient<EntityBase, int>("https://monaware.com", "api/entitybases/");

            await httpClient.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
    
}