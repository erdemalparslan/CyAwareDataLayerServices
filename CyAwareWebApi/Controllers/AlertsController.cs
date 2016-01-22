using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CyAwareWebApi.Models;
using CyAwareWebApi.Models.Entities;
using CyAwareWebApi.Models.Results;

namespace CyAwareWebApi.Controllers
{
    public class AlertsController : ApiController
    {
        private CyAwareContext db = new CyAwareContext();

        // GET: front/Alerts
        [Route("front/alerts")]
        public dynamic GetAlerts()
        {
            var alerts = from a in db.Alerts
                         select new
                         {
                             a.Id,
                             a.occuringdate,
                             a.dismissdate,
                             a.severitylevel,
                             a.incident,
                             a.scanid
                         };

            return alerts;
        }

        // GET: api/Alerts/5
        [ResponseType(typeof(Alert))]
        public async Task<IHttpActionResult> GetAlert(int id)
        {
            Alert alert = await db.Alerts.FindAsync(id);
            if (alert == null)
            {
                return NotFound();
            }

            return Ok(alert);
        }

        // PUT: api/Alerts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAlert(int id, Alert alert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alert.Id)
            {
                return BadRequest();
            }

            db.Entry(alert).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertExists(id))
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

        // POST: api/Alerts
        [ResponseType(typeof(Alert))]
        public async Task<IHttpActionResult> PostAlert(Alert alert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alerts.Add(alert);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = alert.Id }, alert);
        }

        // DELETE: api/Alerts/5
        [ResponseType(typeof(Alert))]
        public async Task<IHttpActionResult> DeleteAlert(int id)
        {
            Alert alert = await db.Alerts.FindAsync(id);
            if (alert == null)
            {
                return NotFound();
            }

            db.Alerts.Remove(alert);
            await db.SaveChangesAsync();

            return Ok(alert);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlertExists(int id)
        {
            return db.Alerts.Count(e => e.Id == id) > 0;
        }

        public void runFilters(Scan scan)
        {
            int moduleId = db.scans.Find(scan.id).policy.moduleId;

            switch(moduleId)
            {
                case 1:
                    checkModule1(scan);
                    break;
                
            }


        }
        private void checkModule1(Scan actualScan) 
        {
            Alert alert;
            var previousScan = db.scans.Include(s => s.results).Where(s => s.policyId == actualScan.policyId && s.scanSuccessCode == 1 && s.id != actualScan.id).OrderByDescending(s => s.scanDate).First();

            try
            {
                if (previousScan != null)
                {
                    List<KeyValuePair<string, string>> previousIpTcpPorts = new List<KeyValuePair<string, string>>();
                    List<KeyValuePair<string, string>> previousIpUdpPorts = new List<KeyValuePair<string, string>>();
                    List<KeyValuePair<string, string>> actualIpTcpPorts = new List<KeyValuePair<string, string>>();
                    List<KeyValuePair<string, string>> actualIpUdpPorts = new List<KeyValuePair<string, string>>();

                    foreach (RModule1 r in previousScan.results)
                    {
                        if (r.tcpPortNumbers != null) foreach (string tcpPort in r.tcpPortNumbers.Split(',')) previousIpTcpPorts.Add(new KeyValuePair<string, string>(r.ipAddress, tcpPort));
                        if (r.udpPortNumbers != null) foreach (string udpPort in r.udpPortNumbers.Split(',')) previousIpUdpPorts.Add(new KeyValuePair<string, string>(r.ipAddress, udpPort));
                    }

                    foreach (RModule1 r in actualScan.results)
                    {
                        if(r.tcpPortNumbers != null) foreach (string tcpPort in r.tcpPortNumbers.Split(',')) actualIpTcpPorts.Add(new KeyValuePair<string, string>(r.ipAddress, tcpPort));
                        if (r.udpPortNumbers != null) foreach (string udpPort in r.udpPortNumbers.Split(',')) actualIpUdpPorts.Add(new KeyValuePair<string, string>(r.ipAddress, udpPort));
                    }

                    var newIpTcpPorts = actualIpTcpPorts.Except(previousIpTcpPorts);
                    var newIpUdpPorts = actualIpUdpPorts.Except(previousIpUdpPorts);
                    var oldIpTcpPorts = previousIpTcpPorts.Except(actualIpTcpPorts);
                    var oldIpUdpPorts = previousIpUdpPorts.Except(actualIpUdpPorts);

                    foreach (KeyValuePair<string, string> item in newIpTcpPorts)
                    {
                        alert = new Alert();
                        alert.occuringdate = DateTime.Now;
                        alert.severitylevel = 3;
                        alert.scanid = actualScan.id;
                        alert.incident = "NEWIP-TCP;ip:" + item.Key + ";port:" + item.Value;
                        db.Alerts.Add(alert);
                    }

                    foreach (KeyValuePair<string, string> item in newIpUdpPorts)
                    {
                        alert = new Alert();
                        alert.occuringdate = DateTime.Now;
                        alert.severitylevel = 2;
                        alert.scanid = actualScan.id;
                        alert.incident = "NEWIP-UDP;ip:" + item.Key + ";port:" + item.Value;
                        db.Alerts.Add(alert);
                    }

                    foreach (KeyValuePair<string, string> item in oldIpTcpPorts)
                    {
                        alert = new Alert();
                        alert.occuringdate = DateTime.Now;
                        alert.severitylevel = 1;
                        alert.scanid = actualScan.id;
                        alert.incident = "OLDIP-TCP;ip:" + item.Key + ";port:" + item.Value;
                        db.Alerts.Add(alert);
                    }

                    foreach (KeyValuePair<string, string> item in oldIpUdpPorts)
                    {
                        alert = new Alert();
                        alert.occuringdate = DateTime.Now;
                        alert.severitylevel = 1;
                        alert.scanid = actualScan.id;
                        alert.incident = "OLDIP-UDP;ip:" + item.Key + ";port:" + item.Value;
                        db.Alerts.Add(alert);
                    }
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}