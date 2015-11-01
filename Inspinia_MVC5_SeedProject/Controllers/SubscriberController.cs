using Inspinia_MVC5_SeedProject.Models;
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
    public class SubscriberController : Controller
    {
        private GenericHttpClient<Subscriber, int> httpClient = new GenericHttpClient<Subscriber, int>("https://monaware.com", "front/subscribers/");

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<Subscriber> list = await httpClient.GetAllAsync();
            //List<EntityBase> list = getElements();
            return View(list);
        }

        // GET: /Subscriber/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            Subscriber model = await httpClient.GetByIdAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /Subscriber/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Subscriber/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Subscriber model)
        {
            await httpClient.PostAsync(model);
            //string resultContent = result.Content.ReadAsStringAsync().Result; 
            return RedirectToAction("Index");
        }

        // GET: /Subscriber/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            /*
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            Subscriber model = await httpClient.GetByIdAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Subscriber/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Subscriber model)
        {
            await httpClient.PutAsync(model.id, model);
            //string resultContent = result.Content.ReadAsStringAsync().Result; 
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            Subscriber model = await httpClient.GetByIdAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Subscriber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await httpClient.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}