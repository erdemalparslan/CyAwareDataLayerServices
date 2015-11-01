using Inspinia_MVC5_SeedProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class IpController : Controller
    {
        private GenericHttpClient<Ip, int> httpClient = new GenericHttpClient<Ip, int>("http://localhost:50334", "api/ip/");

        public async Task<ActionResult> Details(int id)
        {
            Ip ip = await httpClient.GetByIdAsync(id);
            if (ip == null)
            {
                return HttpNotFound();
            }
            return View(ip);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Ip ip)
        {
            await httpClient.PostAsync(ip);

            return RedirectToAction("Index", "EntityBase");
        }

        public async Task<ActionResult> Edit(int id)
        {
            Ip ip = await httpClient.GetByIdAsync(id);

            if (ip == null)
            {
                return HttpNotFound();
            }
            return View(ip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Ip ip)
        {
            await httpClient.PutAsync(ip.id, ip);
            return RedirectToAction("Index", "EntityBase");
        }

        public async Task<ActionResult> Delete(int id)
        {
            Ip ip = await httpClient.GetByIdAsync(id);
            if (ip == null)
            {
                return HttpNotFound();
            }
            return View(ip);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await httpClient.DeleteAsync(id);
            return RedirectToAction("Index", "EntityBase");
        }
    }
}