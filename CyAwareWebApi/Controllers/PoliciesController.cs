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
    public class PoliciesController : ApiController
    {
        private CyAwareContext db = new CyAwareContext();

        // GET: api/Policies
        public IQueryable<Policy> Getpolicies()
        {
            return db.policies;
        }

        // GET: back/policies/5
        [Route("back/policies/{id}")]
        [HttpGet]
        [ResponseType(typeof(Policy))]
        public dynamic GetPolicy(int id)
        {
            return db.policies.Where(p => p.Id == id).Include(p => p.entities)
                .Select(p => new {p.Id, p.isActive, p.schedule, p.setDate, p.entities });
                
        }

        // GET: front/policies/5
        [Route("front/policies/{id}")]
        [ResponseType(typeof(Policy))]
        public dynamic GetEntityBase(int id)
        {
            var policyList = from p in db.policies.Include("entities") where p.Id == id select new
            {
                policyId = p.Id,
                p.isActive,
                p.setDate,
                p.activationDate,
                subscriber = new { subscriberId = p.subscriber.id, p.subscriber.name, },
                action = new { actionId = p.action.id, p.action.actionType, p.action.destination },
                entities = from e in p.entities select new { entityId = e.Id, e.entityType},
                schedule = new { scheduleId = p.schedule.id, p.schedule.isDaily, p.schedule.isMonthly, p.schedule.isHourly, p.schedule.isPerMinute, p.schedule.period, p.schedule.enableStartTime24Format, p.schedule.enableEndTime24Format }
            };
            return policyList.FirstOrDefault();
        }

        // GET: front/policies/subscriber/1
        [Route("front/policies/subscriber/{id}")]
        [ResponseType(typeof(Policy))]
        public dynamic GetPolicyBySubscriber(int id)
        {
            var policyList = from p in db.policies.Include("entities")
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
                             };
            return policyList;
        }

        // PUT: api/Policies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPolicy(int id, Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policy.Id)
            {
                return BadRequest();
            }

            db.Entry(policy).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
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

        // POST: front/policies
        [ResponseType(typeof(Policy))]
        [Route("front/policies")]
        public IHttpActionResult PostPolicy(Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            policy.setDate = DateTime.Now;
            if (policy.isActive)
                policy.activationDate = DateTime.Now;

            List<int> permenantIds = new List<int>();
            foreach(var entity in policy.entities)
                permenantIds.Add(entity.Id);

            policy.entities.Clear();

            foreach(int permenantId in permenantIds)
            {
                var actualEntity = (from e in db.entities where e.Id == permenantId select e).FirstOrDefault();
                policy.entities.Add(actualEntity);
            }

            db.policies.Add(policy);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.Accepted);
        }

        // DELETE: api/Policies/5
        [ResponseType(typeof(Policy))]
        public IHttpActionResult DeletePolicy(int id)
        {
            Policy policy = db.policies.Find(id);
            if (policy == null)
            {
                return NotFound();
            }

            db.policies.Remove(policy);
            db.SaveChanges();

            return Ok(policy);
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