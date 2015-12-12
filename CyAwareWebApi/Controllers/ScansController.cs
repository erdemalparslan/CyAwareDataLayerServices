using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CyAwareWebApi.Models;
using System.Data.SqlClient;
using System.Web.Http.Tracing;

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

        // GET: front/Scans
        [Route("front/scans")]
        [ResponseType(typeof(Scan))]
        public dynamic Getscans()
        {
            var scans = db.scans;
            if (scans != null)
            {
                return scans;
            }
            else
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/scans", "No any scan found!");
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        // GET: back/Scans/5
        [Route("back/scans/{id}")]
        [ResponseType(typeof(Scan))]
        public dynamic GetBackScan(int id)
        {
            //getScanFromDatabase(id);

            try
            {
                var scan = (db.scans
                        .Where(s => s.id == id)
                        .Include(s => s.results)
                        .Select(s => new
                        {
                            s.id,
                            s.scanRefId,
                            s.scanSuccessCode,
                            s.results
                        })).ToList();
                if (scan != null)
                {
                    return scan;
                }
                else
                {
                    Configuration.Services.GetTraceWriter().Error(Request, "GET: front/scans/{id}", "No any scan found!");
                    return StatusCode(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: back/scans/{id}", e.Message);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        private void getScanFromDatabase(int id)
        {
            SqlConnection myConnection = new SqlConnection("Data Source=localhost\\SQLEXPRESS; Initial Catalog=cyawaredb;uid=cyaware;pwd=Test12345;MultipleActiveResultSets=true");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT TOP 1000 [id] ,[scanRefId],[scanSuccessCode],[policyId] FROM [cyawaredb].[dbo].[Scans]", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Console.WriteLine(myReader["scanRefId"].ToString());
                    Console.WriteLine(myReader["policyId"].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }



            //return null;
        }
        
        //// PUT: api/Scans/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutScan(int id, Scan scan)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != scan.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(scan).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ScanExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: back/Scans
        [Route("back/scans")]
        [ResponseType(typeof(Scan))]
        public IHttpActionResult PostScan(Scan scan)
        {
            if (!ModelState.IsValid)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "POST: back/scans", "Model is not valid!");
                return BadRequest(ModelState);
            }
            try
            {
                Policy policy = db.policies.Find(scan.policyId);
                scan.policy = policy;
                db.scans.Add(scan);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "POST: back/scans", e.Message);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            return StatusCode(HttpStatusCode.Accepted);
        }

        //// DELETE: api/Scans/5
        //[ResponseType(typeof(Scan))]
        //public IHttpActionResult DeleteScan(int id)
        //{
        //    Scan scan = db.scans.Find(id);
        //    if (scan == null)
        //    {
        //        return NotFound();
        //    }

        //    db.scans.Remove(scan);
        //    db.SaveChanges();

        //    return Ok(scan);
        //}

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