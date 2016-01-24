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
using System.Threading.Tasks;
using System.Web.Http.OData;

namespace CyAwareWebApi.Controllers
{
    public class SubscribersController : ApiController
    {
        private CyAwareContext db;

        SubscribersController()
        {
            db = new CyAwareContext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: front/subscribers
        [ResponseType(typeof(Subscriber))]
        [Route("front/subscribers/")]
        [HttpGet]
        [EnableQuery(PageSize = ApplicationConstants.DEFAULT_PAGING_SIZE)]
        public IQueryable<Subscriber> Getsubscribers()
        {
            //var allRecords = db.subscribers;
            //var queryString = Request.GetQueryNameValuePairs();
            //IQueryable<Subscriber> temp = db.subscribers;
            //foreach (var key in queryString)
            //{
            //    if (key.Key.Equals("name"))
            //       temp = temp.Where(s => s.name == key.Value);
            //    if (key.Key.Equals("entityid"))
            //    {
            //        var entity = db.entities.Find(1);
            //        temp = temp.Include(s => s.entities).Where(s => s.entities.Contains(entity));
            //    }

            //}
            //return temp;
            return db.subscribers;
        }

        
        // GET: front/subscribers/5
        [ResponseType(typeof(Subscriber))]
        [Route("front/subscribers/{id:int}")]
        [HttpGet]
        [EnableQuery(PageSize = ApplicationConstants.DEFAULT_PAGING_SIZE)]
        public dynamic GetSubscriber(int id)
        {
            return db.subscribers.Include(s => s.entities)
                .FirstOrDefault(s => s.id == id)
                //.Select(s => new {  s.id,
                //                    s.name,
                //                    s.subscriptionId,
                //                    s.entities,
                //                    })
                ;
        }


        // GET: front/subscribers/aycell
        [ResponseType(typeof(Subscriber))]
        [Route("front/subscribers/{name}")]
        [HttpGet]
        [EnableQuery(PageSize = ApplicationConstants.DEFAULT_PAGING_SIZE)]
        public dynamic GetSubscriberByName(string name)
        {
            return db.subscribers.Include(s => s.entities)
                .FirstOrDefault(s => s.name == name)
                //.Select(s => new {
                //    s.id,
                //    s.name,
                //    s.subscriptionId,
                //    s.entities,
                //})
                ;
        }

        /*
    [ResponseType(typeof(SubscriberDTO))]
    [Route("front/subscribers/{id}")]
    [HttpGet]
    public async Task<IHttpActionResult> GetSubscriber(int id)
    {

        var retval = await db.subscribers.Include(s => s.entities)
            .Select(s => new SubscriberDTO {
                id = s.id,
                name = s.name,
                entities = s.entities.Select(e => EntityBaseFactory.getEntityBase(e.entityType))
            }).SingleOrDefaultAsync(s => s.id == id);

        if (retval == null)
        {
            return NotFound();
        }

        return Ok(retval);
    }*/

        // PUT: front/Subscribers/5
        [ResponseType(typeof(void))]
        [Route("front/subscribers/{id:int}")]
        public IHttpActionResult PutSubscriber(int id, Subscriber subscriber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subscriber.id)
            {
                return BadRequest();
            }

            db.Entry(subscriber).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriberExists(id))
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

        // POST: front/Subscribers
        [ResponseType(typeof(Subscriber))]
        [Route("front/subscribers")]
        public IHttpActionResult PostSubscriber(Subscriber subscriber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.subscribers.Add(subscriber);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.Accepted);
        }

        // DELETE: front/Subscribers/5
        [ResponseType(typeof(Subscriber))]
        [Route("front/subscribers/{id:int}")]
        public IHttpActionResult DeleteSubscriber(int id)
        {
            Subscriber subscriber = db.subscribers.Find(id);
            if (subscriber == null)
            {
                return NotFound();
            }

            db.subscribers.Remove(subscriber);
            db.SaveChanges();

            return Ok(subscriber);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubscriberExists(int id)
        {
            return db.subscribers.Count(e => e.id == id) > 0;
        }
    }
}