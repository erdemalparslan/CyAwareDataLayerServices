using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CyAwareWebApi.Models;

namespace CyAwareWebApi.Controllers
{
    public class ModulesController : ApiController
    {
        private CyAwareContext db;

        ModulesController()
        {
            db = new CyAwareContext();
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Modules
        public IQueryable<Module> Getmodules()
        {
            return db.modules;
        }

        // GET: back/modules/5
        [Route("back/modules/{id}")]
        [HttpGet]
        [ResponseType(typeof(Module))]
        public dynamic GetModule(int id)
        {

            var result = db.modules
                .Where(m => m.id == id)
                .Include(m => m.policies/*.Select(p => p.subscriber)*/)
                .Select(m => new {
                    m.id,
                    m.moduleName,
                    m.description,
                    policies = m.policies
                    .Select(p => new
                                    {
                                        p.id,
                                        p.setDate,
                                        p.isActive,
                                        p.activationDate,
                                        subscriber = new
                                        {
                                            p.subscriber.id,
                                            p.subscriber.name,
                                            p.subscriber.subscriptionId,
                                        },
                                        p.entities,
                                        schedule = new
                                        {
                                            p.schedule.id,
                                            p.schedule.isMonthly,
                                            p.schedule.isWeekly,
                                            p.schedule.isDaily,
                                            p.schedule.isHourly,
                                            p.schedule.isPerMinute,
                                            //p.schedule.numberOfOccurences,
                                            p.schedule.period,
                                            //p.schedule.startDate,
                                            //p.schedule.tillDate,
                                            p.schedule.enableStartTime24Format,
                                            p.schedule.enableEndTime24Format
                                        }
                                    })
                });
            return result;
        }

        // PUT: api/Modules/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutModule(int id, Module module)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != module.id)
            {
                return BadRequest();
            }

            db.Entry(module).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Modules
        [ResponseType(typeof(Module))]
        public IHttpActionResult PostModule(Module module)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.modules.Add(module);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = module.id }, module);
        }

        // DELETE: api/Modules/5
        [ResponseType(typeof(Module))]
        public IHttpActionResult DeleteModule(int id)
        {
            Module module = db.modules.Find(id);
            if (module == null)
            {
                return NotFound();
            }

            db.modules.Remove(module);
            db.SaveChanges();

            return Ok(module);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModuleExists(int id)
        {
            return db.modules.Count(e => e.id == id) > 0;
        }
    }
}