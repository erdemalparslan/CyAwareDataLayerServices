﻿using System;
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
using System.Web.Http.Tracing;

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
        // GET: front/modules
        [Route("front/modules")]
        [ResponseType(typeof(Module))]
        public dynamic Getmodules()
        {
            var modules = db.modules;
            if (modules != null)
            {
                return modules;
            }
            else
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/modules", "No any module found!");
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        // GET: back/modules/5
        [Route("back/modules/{id}")]
        [HttpGet]
        [ResponseType(typeof(Module))]
        public dynamic GetModule(int id)
        {

            try
            {
                var requestedModules = (from m in db.modules.Include("policies.entities.subentities")
                                        where m.id == id
                                        select new
                                        {
                                            moduleId = m.id,
                                            moduleName = m.moduleName,
                                            description = m.description,
                                            policies = from p in m.policies
                                                       select new { policyId = p.Id, p.schedule, entities = from e in p.entities select e }
                                        }).ToList();

                foreach (var m in requestedModules)
                {
                    foreach (var p in m.policies)
                    {
                        foreach (EntityBase e in p.entities)
                        {

                            var subs = (from se in db.entities where se.mainEntityId == e.Id select se).ToList();
                            var extras = (from ex in db.extras where ex.entityId == e.Id && ex.policyId == p.policyId select ex).ToList();

                            foreach (var sub in subs)
                                e.subentities.Add(sub);
                            foreach (var ex in extras)
                                e.extraInfo.Add(ex);
                        }
                    }
                }

                if(requestedModules != null && requestedModules.Count > 0)
                {
                    return requestedModules;
                }
                else
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "GET: back/modules/{id}", "No any module found with Id: " + id +"!");
                    return StatusCode(HttpStatusCode.NotFound);
                }
                
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: back/modules/{id}", e.Message);
                return StatusCode(HttpStatusCode.InternalServerError);
            }


        }

        // PUT: front/Modules/5
        [ResponseType(typeof(void))]
        [Route("front/modules/{id}")]
        public IHttpActionResult PutModule(int id, Module module)
        {
            if (!ModelState.IsValid)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/Modules/{id}", "Model is not valid!");
                return BadRequest(ModelState);
            }

            if (id != module.id)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/Modules/{id}", "No module with this Id!");
                return BadRequest();
            }

            db.Entry(module).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!ModuleExists(id))
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/Modules/{id}", "Module does not exists!");
                    return NotFound();
                }
                else
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/Modules/{id}", e.Message);
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.Accepted);
        }

        // POST: front/Modules
        [ResponseType(typeof(Module))]
        [Route("front/modules")]
        public IHttpActionResult PostModule(Module module)
        {
            if (!ModelState.IsValid)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "POST: front/modules", "Model is not valid!");
                return BadRequest(ModelState);
            }

            try
            {

                db.modules.Add(module);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "POST: front/modules", e.Message);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            return StatusCode(HttpStatusCode.Accepted);
        }

        // DELETE: front/Modules/5
        [ResponseType(typeof(Module))]
        [Route("front/modules/{id}")]
        public IHttpActionResult DeleteModule(int id)
        {
            Module module = db.modules.Find(id);
            if (module == null)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "DELETE: front/modules/{id}", "Module not found!");
                return NotFound();
            }

            try
            {
                db.modules.Remove(module);
                db.SaveChanges();
                return Ok(module);
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "DELETE: front/modules/{id}", e.Message);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
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