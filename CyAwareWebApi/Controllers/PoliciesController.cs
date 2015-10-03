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
            return db.policies.Where(p => p.id == id).Include(p => p.entities)
                .Select(p => new {p.id, p.isActive, p.schedule, p.setDate, p.entities });
                
        }

        // PUT: api/Policies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPolicy(int id, Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policy.id)
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

        // POST: api/Policies
        [ResponseType(typeof(Policy))]
        public IHttpActionResult PostPolicy(Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.policies.Add(policy);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = policy.id }, policy);
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
            return db.policies.Count(e => e.id == id) > 0;
        }
    }
}