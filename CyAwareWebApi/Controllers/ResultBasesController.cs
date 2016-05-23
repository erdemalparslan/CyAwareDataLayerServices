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
using CyAwareWebApi.Models.Results;

namespace CyAwareWebApi.Controllers
{
    public class ResultBasesController : ApiController
    {
        private CyAwareContext db = new CyAwareContext();

        // GET: api/ResultBases
        public IQueryable<ResultBase> Getresults()
        {
            return db.results;
        }

        // GET: api/ResultBases/5
        [ResponseType(typeof(ResultBase))]
        public IHttpActionResult GetResultBase(int id)
        {
            ResultBase resultBase = db.results.Find(id);
            if (resultBase == null)
            {
                return NotFound();
            }

            return Ok(resultBase);
        }

        // PUT: api/ResultBases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResultBase(int id, ResultBase resultBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resultBase.Id)
            {
                return BadRequest();
            }

            db.Entry(resultBase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultBaseExists(id))
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

        // POST: api/ResultBases
        [ResponseType(typeof(ResultBase))]
        public IHttpActionResult PostResultBase(ResultBase resultBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.results.Add(resultBase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = resultBase.Id }, resultBase);
        }

        // DELETE: api/ResultBases/5
        [ResponseType(typeof(ResultBase))]
        public IHttpActionResult DeleteResultBase(int id)
        {
            ResultBase resultBase = db.results.Find(id);
            if (resultBase == null)
            {
                return NotFound();
            }

            db.results.Remove(resultBase);
            db.SaveChanges();

            return Ok(resultBase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResultBaseExists(int id)
        {
            return db.results.Count(e => e.Id == id) > 0;
        }
    }
}