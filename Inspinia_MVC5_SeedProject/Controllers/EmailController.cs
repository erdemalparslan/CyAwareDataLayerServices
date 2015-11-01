using Inspinia_MVC5_SeedProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class EmailController : Controller
    {

        private GenericHttpClient<Email, int> httpClient = new GenericHttpClient<Email, int>("http://localhost:50334", "api/email/");

        // GET: Email/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Email email = await httpClient.GetByIdAsync(id);
            if(email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        // GET: Email/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Email/Create
        [HttpPost]
        public async Task<ActionResult> Create(Email email)
        {
            await httpClient.PostAsync(email);

            return RedirectToAction("Index", "EntityBase");
        }

        // GET: Email/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Email email = await httpClient.GetByIdAsync(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        // POST: Email/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Email email)
        {
            await httpClient.PutAsync(email.id, email);
            return RedirectToAction("Index", "EntityBase");
        }

        // GET: Email/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Email email = await httpClient.GetByIdAsync(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        // POST: Email/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await httpClient.DeleteAsync(id);
            return RedirectToAction("Index", "EntityBase");
        }
    }
}
