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
using System.Data.Common;
using Newtonsoft.Json;
using CyAwareWebApi.Controllers.JSONConverter;

namespace CyAwareWebApi.Controllers
{
    public class ScansController : ApiController
    {
        private CyAwareContext db;

        ScansController()
        {
            db = new CyAwareContext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Scans
        public IQueryable<Scan> Getscans()
        {
            return db.scans;
        }

        // GET: back/Scans/5
        [Route("back/scans/{id}")]
        [ResponseType(typeof(Scan))]
        public dynamic GetScan(int id)
        {
            /*var res = db.scans
                .Where(s => s.id == id)
                .Include(s => s.policy)
                .Select(s => new
                {
                    s.id,
                    s.scanRefId,
                    policy = new
                    {
                        s.policy.id,
                        s.policy.entities
                    }
                }
                );

            return res;*/

            return db.scans
                .Where(s => s.id == id)
                .Include(s => s.results)
                .Select(s => new
                {
                    s.id,
                    s.scanRefId,
                    s.scanSuccessCode,
                    s.results
                }
                );
        }
        
        // PUT: api/Scans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutScan(int id, Scan scan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scan.id)
            {
                return BadRequest();
            }

            db.Entry(scan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScanExists(id))
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

        // POST: back/Scans
        [Route("back/scans")]
        [ResponseType(typeof(Scan))]
        public IHttpActionResult PostScan(Scan scan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Policy policy = db.policies.Find(scan.policyId);
            scan.policy = policy;
            db.scans.Add(scan);
            db.SaveChanges();


            return StatusCode(HttpStatusCode.Accepted);
        }

        // DELETE: api/Scans/5
        [ResponseType(typeof(Scan))]
        public IHttpActionResult DeleteScan(int id)
        {
            Scan scan = db.scans.Find(id);
            if (scan == null)
            {
                return NotFound();
            }

            db.scans.Remove(scan);
            db.SaveChanges();

            return Ok(scan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScanExists(int id)
        {
            return db.scans.Count(e => e.id == id) > 0;
        }
    }
}