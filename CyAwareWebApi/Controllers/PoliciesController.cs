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
using System.Web.Http.Tracing;


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
                var policies = db.policies.ToList();
                if (policies != null && policies.Count > 0)
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
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/policies", e.Message);
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
                var policy = (db.policies.Where(p => p.Id == id).Include(p => p.entities)
                        .Select(p => new { p.Id, p.isActive, p.schedule, p.setDate, p.entities })).FirstOrDefault();
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
                Configuration.Services.GetTraceWriter().Error(Request, "GET: back/policies/{id}", e.Message);
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
                                  where p.Id == id
                                  select new
                                  {
                                      policyId = p.Id,
                                      p.isActive,
                                      p.setDate,
                                      p.activationDate,
                                      subscriber = new { subscriberId = p.subscriber.id, p.subscriber.name, },
                                      action = new { actionId = p.action.id, p.action.actionType, p.action.destination },
                                      entities = from e in p.entities select new { entityId = e.Id, e.entityType },
                                      schedule = new { scheduleId = p.schedule.id, p.schedule.isDaily, p.schedule.isMonthly, p.schedule.isHourly, p.schedule.isPerMinute, p.schedule.period, p.schedule.enableStartTime24Format, p.schedule.enableEndTime24Format }
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
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/policies/{id}", e.Message);
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
                                  where p.subscriberId == id
                                  select new
                                  {
                                      policyId = p.Id,
                                      p.isActive,
                                      p.setDate,
                                      p.activationDate,
                                      subscriber = new { subscriberId = p.subscriber.id, p.subscriber.name, },
                                      action = new { actionId = p.action.id, p.action.actionType, p.action.destination },
                                      entities = from e in p.entities select new { entityId = e.Id, e.entityType },
                                      schedule = new { scheduleId = p.schedule.id, p.schedule.isDaily, p.schedule.isMonthly, p.schedule.isHourly, p.schedule.isPerMinute, p.schedule.period, p.schedule.enableStartTime24Format, p.schedule.enableEndTime24Format }
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
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/policies/subscriber/{id}", e.Message);
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

            db.Entry(policy).State = EntityState.Modified;

            try
            {
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
                    Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/Modules/{id}", e.Message);
                    return StatusCode(HttpStatusCode.InternalServerError);
                }
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
                Configuration.Services.GetTraceWriter().Error(Request, "POST: front/policies", e.Message);
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
                db.policies.Remove(policy);
                db.SaveChanges();
                return Ok(policy);
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "DELETE: front/policies/{id}", e.Message);
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