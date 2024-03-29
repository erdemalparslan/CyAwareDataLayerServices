﻿using System;
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
using System.Web.Http.OData;
using System.Collections.Generic;
using CyAwareWebApi.Models.Results;

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
        [EnableQuery(PageSize = ApplicationConstants.DEFAULT_PAGING_SIZE)]
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

        // GET: front/Scans/subscriber/1/module/1
        [Route("front/scans/subscriber/{subscriberId}/module/{moduleId}")]
        [ResponseType(typeof(Scan))]
        [EnableQuery(PageSize = ApplicationConstants.DEFAULT_PAGING_SIZE)]
        public dynamic GetFrontScan_Subscriber_Module(int subscriberId, int moduleId)
        {

            var scans = (from s in db.scans.Include("results.details") where s.policy.subscriberId == subscriberId && s.policy.moduleId == moduleId select s).ToList();

            IEnumerable<ScanDTOEnriched> list = from m in scans select (ScanDTOEnriched)m;


            if (list != null && list.Count() > 0)
            {
                return list;
            }
            else
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/Scans/subscriber/1/module/1", "No any scan found!");
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        // GET: front/Scans/subscriber/1/policy/1
        [Route("front/scans/subscriber/{subscriberId}/policy/{policyId}")]
        [ResponseType(typeof(Scan))]
        [EnableQuery(PageSize = ApplicationConstants.DEFAULT_PAGING_SIZE)]
        public dynamic GetFrontScan_Subscriber_Policy(int subscriberId, int policyId)
        {
            var scans = db.scans.Where(s => (s.policy.subscriberId == subscriberId) && (s.policyId == policyId))
                                               .Select(s => new
                                               {
                                                   s.id,
                                                   s.scanRefId,
                                                   s.scanDate,
                                                   s.scanSuccessCode,
                                                   s.results
                                               }).ToList();

            if (scans != null)
            {
                return scans;
            }
            else
            {
                Configuration.Services.GetTraceWriter().Error(Request, "GET: front/Scans/subscriber/1/policy/1", "No any scan found!");
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        // GET: back/Scans/5
        [Route("back/scans/{id}")]
        [ResponseType(typeof(Scan))]
        [EnableQuery(PageSize = ApplicationConstants.DEFAULT_PAGING_SIZE)]
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
                            s.scanDate,
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
                Configuration.Services.GetTraceWriter().Error(Request, "GET: back/scans/{id}",e.Message  + e.InnerException);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        //private void getScanFromDatabase(int id)
        //{
        //    SqlConnection myConnection = new SqlConnection("Data Source=localhost\\SQLEXPRESS; Initial Catalog=cyawaredb;uid=cyaware;pwd=Test12345;MultipleActiveResultSets=true");
        //    try
        //    {
        //        myConnection.Open();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //    try
        //    {
        //        SqlDataReader myReader = null;
        //        SqlCommand myCommand = new SqlCommand("SELECT TOP 1000 [id] ,[scanRefId],[scanSuccessCode],[policyId] FROM [cyawaredb].[dbo].[Scans]", myConnection);
        //        myReader = myCommand.ExecuteReader();
        //        while (myReader.Read())
        //        {
        //            Console.WriteLine(myReader["scanRefId"].ToString());
        //            Console.WriteLine(myReader["policyId"].ToString());
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }



        //    //return null;
        //}
        
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
                db.scans.Add(scan);
                db.SaveChanges();
                AlertsController alerter = new AlertsController();
                alerter.runFilters(scan);
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "POST: back/scans",e.Message  + e.InnerException);
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