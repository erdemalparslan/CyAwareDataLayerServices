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
            return db.policies
                .Include(p => p.subscriber)
                .FirstOrDefault(p => p.Id == id)
                ;
        }

        // GET: front/policies/subscriber/1
        [Route("front/policies/subscriber/{id}")]
        [ResponseType(typeof(Policy))]
        public dynamic GetPolicyBySubscriber(int id)
        {
            return db.policies
                .Include(p => p.subscriber)
                .Where(p => p.subscriber.id == id)
                .ToList();
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

            //int subscriberId = 1;
            //int actionId = 3;
            //int moduleId = 1;

            db.policies.Add(policy);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = policy.Id }, policy);
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