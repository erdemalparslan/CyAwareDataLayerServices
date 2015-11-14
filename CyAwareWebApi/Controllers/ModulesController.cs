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
using CyAwareWebApi.Models.Entities;

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

            var requestedModules = (from m in db.modules.Include("policies.entities.subentities")
                          where m.id == id
                          select new
                          {
                              moduleId = m.id,
                              moduleName = m.moduleName,
                              description = m.description,
                              policies = from p in m.policies
                                         select new { policyId = p.Id, p.schedule
                                                        ,entities = from e in p.entities
                                                                 select e 
                                                                  }
                          }).ToList();

            foreach (var m in requestedModules)
            {
                foreach(var p in m.policies)
                {
                    foreach (EntityBase e in p.entities)
                    {

                        var subs = (from se in db.entities where se.mainEntityId == e.Id select se).ToList();

                        foreach (var sub in subs)
                        {
                            e.subentities.Add(sub);
                        }
                    }
                }
            }

            //foreach(var m in result2)
            //{
            //    foreach (var p in m.policies)
            //    {
            //        p.entities = db.entities.Where(e => e.policyId == p.id)
            //    }
            //}

            /*var result = db.modules
                .Where(m => m.id == id)
                .Include(m => m.policies)
                .Select(m => new
                {
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
                        entities = p.entities,
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

                }).ToList();*/

            //var result = (from eb in db.entities
            //              where eb.Id == eb.entitybaseId && eb.policyid == 1
            //              select eb).tolist();


            //foreach (var mod in result)
            //{
            //    var policies = mod.policies;
            //    foreach (var pol in policies)
            //    {
            //        var entities = pol.entities;
            //        foreach (var ent in entities)
            //        {
            //            //ent.subentities = db.entities.Where(e => e.E == id)
            //        }
            //    }
            //}

            return requestedModules;
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