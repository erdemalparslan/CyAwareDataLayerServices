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
using CyAwareWebApi.Models.EntitiesDTO;

namespace CyAwareWebApi.Controllers
{
    public class PoliciesController : ApiController
    {
        private CyAwareContext db = new CyAwareContext();

        // GET: front/policies
        [Route("front/policies")]
        [ResponseType(typeof(Policy))]
        public dynamic Getpolicies()
        {
            try
            {
                List<EntityBaseDTO> dtos = new List<EntityBaseDTO>();
                var policies = (from p in db.policies.Where(p => p.isDeleted == false).Include("entities")
                              select new
                              {
                                  Id = p.Id,
                                  p.isActive,
                                  p.setDate,
                                  p.moduleId,
                                  p.activationDate,
                                  subscriber = new { Id = p.subscriber.id, p.subscriber.name, },
                                  action = from a in p.actions select new { Id = a.id, a.actionType, a.destination },
                                  entities = from e in p.entities select new { Id = e.Id, e.entityType },
                                  p.s_isMonthly,
                                  p.s_isWeekly,
                                  p.s_isDaily,
                                  p.s_isHourly,
                                  p.s_isPerMinute,
                                  p.s_period,
                                  p.s_enableStartTime24Format,
                                  p.s_enableEndTime24Format
                              });


                if (policies != null)
                {
                    return policies;
                }
                else
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "GET: front/policies", "No any policy found!");
                    return StatusCode(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/policies",e.Message  + e.InnerException);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // GET: back/policies/5
        [Route("back/policies/{id}")]
        [HttpGet]
        [ResponseType(typeof(Policy))]
        public dynamic GetBackPolicy(int id)
        {
            try
            {
                var policy = (db.policies.Where(p => (p.Id == id && p.isDeleted==false)).Include(p => p.entities)
                        .Select(p => new { p.Id, p.isActive, p.setDate, p.entities, p.moduleId,
                            p.s_isMonthly,
                            p.s_isWeekly,
                            p.s_isDaily,
                            p.s_isHourly,
                            p.s_isPerMinute,
                            p.s_period,
                            p.s_enableStartTime24Format,
                            p.s_enableEndTime24Format
                        })).FirstOrDefault();
                if (policy != null)
                {
                    return policy;
                }
                else
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "GET: front/policies/{id}", "No any policy found!");
                    return StatusCode(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: back/policies/{id}",e.Message  + e.InnerException);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // GET: front/policies/5
        [Route("front/policies/{id}")]
        [ResponseType(typeof(Policy))]
        public dynamic GetFrontPolicy(int id)
        {
            try
            {
                var policy = (from p in db.policies.Include("entities")
                                  where (p.Id == id && p.isDeleted == false)
                                  select new
                                  {
                                      Id = p.Id,
                                      p.isActive,
                                      p.setDate,
                                      p.moduleId,
                                      p.activationDate,
                                      subscriber = new { Id = p.subscriber.id, p.subscriber.name, },
                                      action = from a in p.actions select new { Id = a.id, a.actionType, a.destination },
                                      entities = from e in p.entities select new { Id = e.Id, e.entityType },
                                      p.s_isMonthly,
                                      p.s_isWeekly,
                                      p.s_isDaily,
                                      p.s_isHourly,
                                      p.s_isPerMinute,
                                      p.s_period,
                                      p.s_enableStartTime24Format,
                                      p.s_enableEndTime24Format
                                  }).FirstOrDefault();
                if (policy != null)
                {
                    return policy;
                }
                else
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "GET: front/policies/{id}", "No any policy found!");
                    return StatusCode(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/policies/{id}",e.Message  + e.InnerException);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // GET: front/policies/subscriber/1
        [Route("front/policies/subscriber/{id}")]
        [ResponseType(typeof(Policy))]
        public dynamic GetPolicyBySubscriber(int id)
        {
            try
            {
                var policyList = (from p in db.policies.Include("entities")
                                  where p.subscriberId == id && p.isDeleted == false
                                  select new
                                  {
                                      Id = p.Id,
                                      p.isActive,
                                      p.setDate,
                                      p.moduleId,
                                      p.activationDate,
                                      subscriber = new {Id = p.subscriber.id, p.subscriber.name, },
                                      action = from a in p.actions select new { Id = a.id, a.actionType, a.destination },
                                      entities = from e in p.entities select new { Id = e.Id, e.entityType },
                                      p.s_isMonthly, p.s_isWeekly, p.s_isDaily, p.s_isHourly, p.s_isPerMinute, p.s_period, p.s_enableStartTime24Format,p.s_enableEndTime24Format
                                  }).ToList();
                if (policyList != null && policyList.Count > 0)
                {
                    return policyList;
                }
                else
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "GET: front/policies/subscriber/{id}", "No any policy found!");
                    return StatusCode(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/policies/subscriber/{id}",e.Message  + e.InnerException);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: front/Policies/5
        [ResponseType(typeof(void))]
        [Route("front/policies/{id}")]
        public IHttpActionResult PutPolicy(int id, Policy policy)
        {
            if (!ModelState.IsValid)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/policies/{id}", "Model is not valid!");
                return BadRequest(ModelState);
            }

            if (id != policy.Id)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/policies/{id}", "No policy with this Id!");
                return BadRequest();
            }
            
            try
            {
                //db.Entry(policy).State = EntityState.Modified;
                var policyInDb = db.policies.Include(p => p.entities).Single(p => p.Id == policy.Id);
                db.Entry(policyInDb).CurrentValues.SetValues(policy);
                // Remove types
                foreach (var typeInDb in policyInDb.entities.ToList())
                    if (!policy.entities.Any(t => t.Id == typeInDb.Id))
                        policyInDb.entities.Remove(typeInDb);

                // Add new types
                foreach (var type in policy.entities)
                    if (!policyInDb.entities.Any(t => t.Id == type.Id))
                    {
                        db.entities.Attach(type);
                        policyInDb.entities.Add(type);
                    }

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!PolicyExists(id))
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/policies/{id}", "Policy does not exists!");
                    return NotFound();
                }
                else
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/Modules/{id}", e.Message + e.InnerException);
                    return StatusCode(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/policies/{id}", e.Message + e.InnerException);
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.Accepted);
        }

        // POST: front/policies
        [ResponseType(typeof(Policy))]
        [Route("front/policies")]
        public IHttpActionResult PostPolicy(Policy policy)
        {
            if (!ModelState.IsValid)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "POST: front/policies", "Model is not valid!");
                return BadRequest(ModelState);
            }

            try
            {
                policy.setDate = DateTime.Now;
                if (policy.isActive)
                    policy.activationDate = DateTime.Now;

                List<int> permenantIds = new List<int>();
                foreach (var entity in policy.entities)
                    permenantIds.Add(entity.Id);

                policy.entities.Clear();

                foreach (int permenantId in permenantIds)
                {
                    var actualEntity = (from e in db.entities where e.Id == permenantId select e).FirstOrDefault();
                    policy.entities.Add(actualEntity);
                }

                db.policies.Add(policy);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "POST: front/policies",e.Message  + e.InnerException);
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return StatusCode(HttpStatusCode.Accepted);
        }

        // DELETE: front/policies/5
        [ResponseType(typeof(Policy))]
        [Route("front/policies/{id}")]
        public IHttpActionResult DeletePolicy(int id)
        {
            Policy policy = db.policies.Find(id);
            if (policy == null)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "DELETE: front/policies/{id}", "Policy not found!");
                return NotFound();
            }

            try
            {
                policy.isDeleted = true;
                db.SaveChanges();
                return StatusCode(HttpStatusCode.Accepted);
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "DELETE: front/policies/{id}",e.Message  + e.InnerException);
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

        private bool PolicyExists(int id)
        {
            return db.policies.Count(e => e.Id == id) > 0;
        }
    }
}