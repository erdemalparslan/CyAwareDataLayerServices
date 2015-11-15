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
    public class EntityBasesController : ApiController
    {
        private CyAwareContext db = new CyAwareContext();

        // GET: front/entitybases
        [Route("front/entitybases")]
        [ResponseType(typeof(EntityBase))]
        public IQueryable<EntityBase> GetEntityBase()
        {
            return db.entities;
        }

        // GET: front/entitybases/5
        [Route("front/entitybases/{id}")]
        [ResponseType(typeof(EntityBase))]
        public dynamic GetEntityBase(int id)
        {
            return db.entities
                .Include(e => e.subscriber)
                .FirstOrDefault(e => e.Id == id)
                ;
        }

        // GET: front/entitybases/subscriber/1
        [Route("front/entitybases/subscriber/{id}")]
        [ResponseType(typeof(EntityBase))]
        public dynamic GetEntityBaseBySubscriber(int id)
        {
            return db.entities
                .Include(e => e.subscriber)
                .Where(e => e.subscriber.id == id)
                .ToList();
        }

        // PUT: front/EntityBases/5
        [ResponseType(typeof(void))]
        [Route("front/entitybases/{id}")]
        public IHttpActionResult PutEntityBase(int id, EntityBase entityBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entityBase.Id)
            {
                return BadRequest();
            }

            db.Entry(entityBase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityBaseExists(id))
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

        // POST: front/EntityBases
        [ResponseType(typeof(EntityBase))]
        [Route("front/entitybases")]
        public IHttpActionResult PostEntityBase(EntityBase entityBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.entities.Add(entityBase);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.Accepted);
        }

        // DELETE: front/EntityBases/5
        [ResponseType(typeof(EntityBase))]
        [Route("front/entitybases/{id}")]
        public IHttpActionResult DeleteEntityBase(int id)
        {
            EntityBase entityBase = db.entities.Find(id);
            if (entityBase == null)
            {
                return NotFound();
            }

            db.entities.Remove(entityBase);
            db.SaveChanges();

            return Ok(entityBase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntityBaseExists(int id)
        {
            return db.entities.Count(e => e.Id == id) > 0;
        }
    }
}