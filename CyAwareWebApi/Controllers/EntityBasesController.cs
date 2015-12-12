using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CyAwareWebApi.Models;
using CyAwareWebApi.Models.Entities;
using System.Web.Http.Tracing;
using System.Collections.Generic;

namespace CyAwareWebApi.Controllers
{
    public class EntityBasesController : ApiController
    {
        private CyAwareContext db = new CyAwareContext();

        // GET: front/entitybases
        [Route("front/entitybases")]
        [ResponseType(typeof(EntityBase))]
        public dynamic GetEntityBase()
        {
            var entities = db.entities;
            if (entities != null)
            { 
                return entities;
            }
            else
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/entitybases", "No any entity found!");
                return StatusCode(HttpStatusCode.NotFound);
            }
            
        }

        // GET: front/entitybases/5
        [Route("front/entitybases/{id}")]
        [ResponseType(typeof(EntityBase))]
        public dynamic GetEntityBase(int id)
        {
            var entity = db.entities.Include(e => e.subscriber).FirstOrDefault(e => e.Id == id);

            if (entity != null)
            {
                return entity;
            }
            else
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/entitybases/{id}", "Entity with Id: "+id+" not found!");
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        // GET: front/entitybases/subscriber/1
        [Route("front/entitybases/subscriber/{id}")]
        [ResponseType(typeof(EntityBase))]
        public dynamic GetEntityBaseBySubscriber(int id)
        {
            try
            {
                //var entities = db.entities
                //    .Include(e => e.subscriber)
                //    .Where(e => e.subscriber.id == id)
                //    .ToList();
                var entities = (from e in db.entities where e.subscriberId == id select e).ToList();

                List<EntityBase> prunnedList = new List<EntityBase>();

                if (entities != null && entities.Count > 0)
                {
                    foreach (EntityBase entity in entities)
                    {
                        entity.subscriber = null;
                        if (entity.mainEntity != null)
                        {
                            entity.mainEntity.subscriber = null;
                        }
                        if (entity.subentities.Count() > 0)
                        {
                            foreach (EntityBase subentity in entity.subentities)
                            {
                                subentity.subscriber = null; 
                            }
                        }
                        foreach (EntityExtraForPolicy extra in entity.extraInfo)
                            extra.policy = null;

                        prunnedList.Add(entity);
                    }
                    return prunnedList;
                    //TODO Prunned list prun olmuyor, full liste geliyor
                }
                else
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "GET: front/entitybases/subscriber/{id}", "No any entity found FOR SUBSCRIBER ID : " + id + "!");
                    return StatusCode(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/entitybases/subscriber/{id}", e.Message);
                return StatusCode(HttpStatusCode.InternalServerError);
            }

        }

        // PUT: front/EntityBases/5
        [ResponseType(typeof(void))]
        [Route("front/entitybases/{id}")]
        public IHttpActionResult PutEntityBase(int id, EntityBase entityBase)
        {
            if (!ModelState.IsValid)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/entitybases/{id}", "Model is not valid!");
                return BadRequest(ModelState);
            }

            if (id != entityBase.Id)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/entitybases/{id}", "No entity with this Id!");
                return BadRequest();
            }

            db.Entry(entityBase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!EntityBaseExists(id))
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/entitybases/{id}", "Entity does not exists!");
                    return NotFound();
                }
                else
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "PUT: front/entitybases/{id}", e.Message);
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
                Configuration.Services.GetTraceWriter().Error(Request, "POST: front/entitybases", "Model is not valid!");
                return BadRequest(ModelState);
            }

            try
            {
                db.entities.Add(entityBase);
                db.SaveChanges();

                return StatusCode(HttpStatusCode.Accepted);
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "POST: front/entitybases", e.Message);
                return StatusCode(HttpStatusCode.InternalServerError);
            }


        }

        // DELETE: front/EntityBases/5
        [ResponseType(typeof(EntityBase))]
        [Route("front/entitybases/{id}")]
        public IHttpActionResult DeleteEntityBase(int id)
        {
            EntityBase entityBase = db.entities.Find(id);
            if (entityBase == null)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "DELETE: front/entitybases/{id}", "Entity not found!");
                return NotFound();
            }

            try
            {
                db.entities.Add(entityBase);
                db.SaveChanges();

                return Ok(entityBase);
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "DELETE: front/entitybases/{id}", e.Message);
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

        private bool EntityBaseExists(int id)
        {
            return db.entities.Count(e => e.Id == id) > 0;
        }
    }
}