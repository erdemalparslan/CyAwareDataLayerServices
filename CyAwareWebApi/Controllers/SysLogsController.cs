using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CyAwareWebApi.Models;

namespace CyAwareWebApi.Controllers
{
    public class SysLogsController : Controller
    {
        private CyAwareContext db = new CyAwareContext();

        // GET: SysLogs
        public ActionResult Index()
        {
            return View(db.SysLogs.ToList());
        }

        // GET: SysLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysLog sysLog = db.SysLogs.Find(id);
            if (sysLog == null)
            {
                return HttpNotFound();
            }
            return View(sysLog);
        }

        // GET: SysLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SysLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,severity,apiMethod,source,message")] SysLog sysLog)
        {
            if (ModelState.IsValid)
            {
                db.SysLogs.Add(sysLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysLog);
        }

        // GET: SysLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysLog sysLog = db.SysLogs.Find(id);
            if (sysLog == null)
            {
                return HttpNotFound();
            }
            return View(sysLog);
        }

        // POST: SysLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,severity,apiMethod,source,message")] SysLog sysLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sysLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sysLog);
        }

        // GET: SysLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysLog sysLog = db.SysLogs.Find(id);
            if (sysLog == null)
            {
                return HttpNotFound();
            }
            return View(sysLog);
        }

        // POST: SysLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysLog sysLog = db.SysLogs.Find(id);
            db.SysLogs.Remove(sysLog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
