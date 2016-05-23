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
using System.Web.Http.Tracing;
using System.Web.Http.OData;
using System.Web.Http.OData.Extensions;

using System.Web.Http.OData.Query;
using CyAwareWebApi.Utils;
using System.Collections;

namespace CyAwareWebApi.Controllers
{
    public class AlertsController : ApiController
    {
        private Alert alert;
        private CyAwareContext db = new CyAwareContext();

        // GET: front/alerts/subscriberId/1/moduleId/1      
        [Route("front/alerts/subscriberId/{subscriberId}/moduleId/{moduleId}")]
        [EnableQuery(PageSize = ApplicationConstants.DEFAULT_PAGING_SIZE)]
        public IQueryable<AlertDTO> GetAlerts(int subscriberId, int moduleId)
        {
            IQueryable<AlertDTO> alerts = from a in db.Alerts where (a.scan.policy.subscriberId == subscriberId && a.scan.policy.moduleId == moduleId)
                         select new AlertDTO
                         {
                             Id = a.Id,
                             occuringdate = a.occuringdate,
                             dismissdate = a.dismissdate,
                             severitylevel = a.severitylevel,
                             incident = a.incident,
                             scanid = a.scanid,
                             isthrown = a.isthrown,
                             incidententityid = a.incidententityid,
                             resultbaseid = a.resultbaseid,
                             moduleId = a.scan.policy.moduleId
                         };

            return alerts;
        }

        // GET: front/alerts/subscriberId/1     
        [Route("front/alerts/subscriberId/{subscriberId}")]
        [EnableQuery(PageSize = ApplicationConstants.DEFAULT_PAGING_SIZE)]
        public IQueryable<AlertDTO> GetAlerts(int subscriberId)
        {
            IQueryable<AlertDTO> alerts = from a in db.Alerts
                                          where (a.scan.policy.subscriberId == subscriberId)
                                          select new AlertDTO
                                          {
                                              Id = a.Id,
                                              occuringdate = a.occuringdate,
                                              dismissdate = a.dismissdate,
                                              severitylevel = a.severitylevel,
                                              incident = a.incident,
                                              scanid = a.scanid,
                                              isthrown = a.isthrown,
                                              incidententityid = a.incidententityid,
                                              resultbaseid = a.resultbaseid,
                                              moduleId = a.scan.policy.moduleId
                                          };

            return alerts;
        }



        // GET: front/alerts/1/subscriberId/1/moduleId/1      
        [Route("front/alerts/{lastAlert}/subscriberId/{subscriberId}/moduleId/{moduleId}")]
        [EnableQuery(PageSize = ApplicationConstants.DEFAULT_PAGING_SIZE)]
        public PageResult<AlertDTO> GetAlertsOdata(int lastAlert, int subscriberId, int moduleId, ODataQueryOptions<AlertDTO> options)
        {
            IQueryable<AlertDTO> alerts = from a in db.Alerts
                                          where (a.Id > lastAlert && a.scan.policy.subscriberId == subscriberId && a.scan.policy.moduleId == moduleId)
                                          select new AlertDTO
                                          { 
                                              Id = a.Id,
                                              occuringdate = a.occuringdate,
                                              dismissdate = a.dismissdate,
                                              severitylevel = a.severitylevel,
                                              incident = a.incident,
                                              scanid = a.scanid,
                                              isthrown = a.isthrown,
                                              incidententityid = a.incidententityid,
                                              resultbaseid = a.resultbaseid,
                                              moduleId = a.scan.policy.moduleId
                                          };
            return new PageResult<AlertDTO>(
                    alerts as IEnumerable<AlertDTO>,
                    Request.ODataProperties().NextLink,
                    Request.ODataProperties().TotalCount);
        }
    
        // GET: front/alerts/1/subscriberId    
        [Route("front/alerts/{lastAlert}/subscriberId/{subscriberId}")]
        [EnableQuery(PageSize = ApplicationConstants.DEFAULT_PAGING_SIZE)]
        public IQueryable<AlertDTO> GetAlertsOdata(int lastAlert, int subscriberId, ODataQueryOptions<AlertDTO> options)
        {
            IQueryable<AlertDTO> alerts = from a in db.Alerts
                                          where (a.Id > lastAlert && a.scan.policy.subscriberId == subscriberId)
                                          select new AlertDTO
                                          {
                                              Id = a.Id,
                                              occuringdate = a.occuringdate,
                                              dismissdate = a.dismissdate,
                                              severitylevel = a.severitylevel,
                                              incident = a.incident,
                                              scanid = a.scanid,
                                              isthrown = a.isthrown,
                                              incidententityid = a.incidententityid,
                                              resultbaseid = a.resultbaseid,
                                              moduleId = a.scan.policy.moduleId
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

            try
            {
                switch (moduleId)
                {
                    case 1:
                        checkModule1(scan);
                        break;

                    case 2:
                        checkModule2(scan);
                        break;

                    case 3:
                        checkModule3(scan);
                        break;

                    case 4:
                        checkModule4(scan);
                        break;

                    case 5:
                        checkModule5(scan);
                        break;

                    case 6:
                        checkModule6(scan);
                        break;

                    case 7:
                        checkModule7(scan);
                        break;

                    case 8:
                        checkModule8(scan);
                        break;
                }
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error(Request, "Filter running for alerts is failed!", e.Message + e.InnerException);
            }
        }

        private void checkModule1(Scan actualScan) 
        {
            var previousScan = db.scans.Include(s => s.results).Where(s => s.policyId == actualScan.policyId && s.scanSuccessCode == 1 && s.id != actualScan.id).OrderByDescending(s => s.scanDate).FirstOrDefault();
            List<Alert> newAlerts = new List<Alert>();
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
                        newAlerts.Add(alert);
                    }

                    foreach (KeyValuePair<string, string> item in newIpUdpPorts)
                    {
                        alert = new Alert();
                        alert.occuringdate = DateTime.Now;
                        alert.severitylevel = 2;
                        alert.scanid = actualScan.id;
                        alert.incident = "NEWIP-UDP;ip:" + item.Key + ";port:" + item.Value;
                        newAlerts.Add(alert);
                    }

                    foreach (KeyValuePair<string, string> item in oldIpTcpPorts)
                    {
                        alert = new Alert();
                        alert.occuringdate = DateTime.Now;
                        alert.severitylevel = 1;
                        alert.scanid = actualScan.id;
                        alert.incident = "OLDIP-TCP;ip:" + item.Key + ";port:" + item.Value;
                        newAlerts.Add(alert);
                    }

                    foreach (KeyValuePair<string, string> item in oldIpUdpPorts)
                    {
                        alert = new Alert();
                        alert.occuringdate = DateTime.Now;
                        alert.severitylevel = 1;
                        alert.scanid = actualScan.id;
                        alert.incident = "OLDIP-UDP;ip:" + item.Key + ";port:" + item.Value;
                        newAlerts.Add(alert);
                    }
                    db.Alerts.AddRange(newAlerts);
                    foreach (Alert alert in newAlerts)
                        checkForActions(alert);
                    db.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        private void checkModule2(Scan actualScan)
        {
            var previousScan = db.scans.Include(s => s.results).Where(s => s.policyId == actualScan.policyId && s.scanSuccessCode == 1 && s.id != actualScan.id).OrderByDescending(s => s.scanDate).FirstOrDefault();
            List<Alert> newAlerts = new List<Alert>();

            foreach (RModule2 result in actualScan.results)
            {
                try
                {
                    var policyOfResult = (from p in db.policies where (p.Id == result.policyId) select p).First();
                    foreach (ETwitterProfile profile in policyOfResult.entities)
                    {
                        if (profile.idStr == result.idStr)
                        {
                            A2checkForChangedScreenName(newAlerts, result, profile, actualScan);
                            A2checkForDailyMaxTweetsExceeded(newAlerts, result, profile, actualScan);
                            if (previousScan != null)
                            {
                                A2checkForDailyMaxFollowerChangeRatioExceeded(newAlerts, result, profile, actualScan, previousScan);
                                A2checkForDailyMaxFolloweeChangeRatioExceeded(newAlerts, result, profile, actualScan, previousScan);
                            }
                            A2checkForDailyMaxCAPITALLETTERRatioExceeded(newAlerts, result, profile, actualScan);
                            A2unusualContentFound(newAlerts, result, profile, actualScan);
                            db.Alerts.AddRange(newAlerts);
                            foreach (Alert alert in newAlerts)
                                checkForActions(alert);
                            db.SaveChangesAsync();
                        }
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        private void A2checkForChangedScreenName(List<Alert> newAlerts, RModule2 actual, ETwitterProfile expected, Scan actualScan)
        {
            if(!actual.actualScreenName.Equals(expected.screenName))
            {
                alert = new Alert(actualScan.id,2, "Changed Twitter Screenname;expected:" + expected.screenName + ";found:" + actual.actualScreenName);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void A2checkForDailyMaxTweetsExceeded(List<Alert> newAlerts, RModule2 actual, ETwitterProfile expected, Scan actualScan)
        {
            if (actual.actualTweets > expected.dailyMaxTweets)
            {
                alert = new Alert(actualScan.id, 2, "More than daily max tweets;expected:" + expected.dailyMaxTweets + ";found:" + actual.actualTweets);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void A2checkForDailyMaxCAPITALLETTERRatioExceeded(List<Alert> newAlerts, RModule2 actual, ETwitterProfile expected, Scan actualScan)
        {
            if (actual.actualCAPITALLETTERRatio > expected.dailyMaxCAPITALLETTERRatio)
            {
                alert = new Alert(actualScan.id, 2, "More than daily max CAPITAL LETTER Ratio;expected:" + expected.dailyMaxCAPITALLETTERRatio + ";found:" + actual.actualCAPITALLETTERRatio);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void A2checkForDailyMaxFollowerChangeRatioExceeded(List<Alert> newAlerts, RModule2 actual, ETwitterProfile expected, Scan actualScan, Scan previousScan)
        {
            int previousFollowernumber = (from p in previousScan.results where actual.idStr == ((RModule2)p).idStr select ((RModule2)p).actualFollowerNumber).FirstOrDefault();
            if (previousFollowernumber == 0)
                return;
            double actualChangeRatio = ((actual.actualFollowerNumber / previousFollowernumber) - 1);
            if (actualChangeRatio > expected.dailyMaxFollowerChangeRatio)
            {
                alert = new Alert(actualScan.id, 1, "More than daily max change in the number followers;expected:" + expected.dailyMaxFollowerChangeRatio + ";found:" + actualChangeRatio);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void A2checkForDailyMaxFolloweeChangeRatioExceeded(List<Alert> newAlerts, RModule2 actual, ETwitterProfile expected, Scan actualScan, Scan previousScan)
        {
            int previousFolloweenumber = (from p in previousScan.results where actual.idStr == ((RModule2)p).idStr select ((RModule2)p).actualFolloweeNumber).FirstOrDefault();
            if (previousFolloweenumber == 0)
                return;
            double actualChangeRatio = ((actual.actualFolloweeNumber / previousFolloweenumber) - 1);
            if (actualChangeRatio > expected.dailyMaxFalloweeChangeRatio)
            {
                alert = new Alert(actualScan.id, 1, "More than daily max change in the number followees;expected:" + expected.dailyMaxFalloweeChangeRatio + ";found:" + actualChangeRatio);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void A2unusualContentFound(List<Alert> newAlerts, RModule2 actual, ETwitterProfile expected, Scan actualScan)
        {
            if (!(actual.unusualContentFound == null) && !actual.unusualContentFound.Equals(""))
            {
                alert = new Alert(actualScan.id, 3, "Unusual content found;" + actual.unusualContentFound);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void checkModule3(Scan actualScan)
        {
            var previousScan = db.scans.Include(s => s.results).Where(s => s.policyId == actualScan.policyId && s.scanSuccessCode == 1 && s.id != actualScan.id).OrderByDescending(s => s.scanDate).FirstOrDefault();
            List<Alert> newAlerts = new List<Alert>();

            foreach (RModule3 result in actualScan.results)
            {
                try
                {
                    var policyOfResult = (from p in db.policies where (p.Id == result.policyId) select p).First();
                    foreach (EInstagramProfile profile in policyOfResult.entities)
                    {
                        if (profile.idStr == result.idStr)
                        {
                            A3checkForChangedScreenName(newAlerts, result, profile, actualScan);
                            A3checkForChangedProfilePicture(newAlerts, result, profile, actualScan);
                            A3checkForDailyMaxPostsExceeded(newAlerts, result, profile, actualScan);
                            if (previousScan != null)
                            {
                                A3checkForDailyMaxFollowerChangeRatioExceeded(newAlerts, result, profile, actualScan, previousScan);
                                A3checkForDailyMaxFolloweeChangeRatioExceeded(newAlerts, result, profile, actualScan, previousScan);
                            }
                            A3checkForDailyMaxCAPITALLETTERRatioExceeded(newAlerts, result, profile, actualScan);
                            A3unusualContentFound(newAlerts, result, profile, actualScan);
                            db.Alerts.AddRange(newAlerts);
                            foreach (Alert alert in newAlerts)
                                checkForActions(alert);
                            db.SaveChangesAsync();
                        }
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        private void A3checkForChangedScreenName(List<Alert> newAlerts, RModule3 actual, EInstagramProfile expected, Scan actualScan)
        {
            if (!actual.actualScreenName.Equals(expected.screenName))
            {
                alert = new Alert(actualScan.id, 3, "Changed Instagram Screenname;expected:" + expected.screenName + ";found:" + actual.actualScreenName);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void A3checkForChangedProfilePicture(List<Alert> newAlerts, RModule3 actual, EInstagramProfile expected, Scan actualScan)
        {
            if (!actual.actualProfilePictureMD5.Equals(expected.profilePictureMD5))
            {
                alert = new Alert(actualScan.id, 3, "Changed Instagram profile picture;expected:" + expected.profilePictureMD5 + ";found:" + actual.actualProfilePictureMD5);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void A3checkForDailyMaxPostsExceeded(List<Alert> newAlerts, RModule3 actual, EInstagramProfile expected, Scan actualScan)
        {
            if (actual.actualPosts > expected.dailyMaxPosts)
            {
                alert = new Alert(actualScan.id, 2, "More than daily max posts;expected:" + expected.dailyMaxPosts + ";found:" + actual.actualPosts);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void A3checkForDailyMaxCAPITALLETTERRatioExceeded(List<Alert> newAlerts, RModule3 actual, EInstagramProfile expected, Scan actualScan)
        {
            if (actual.actualCAPITALLETTERRatio > expected.dailyMaxCAPITALLETTERRatio)
            {
                alert = new Alert(actualScan.id, 2, "More than daily max CAPITAL LETTER Ratio;expected:" + expected.dailyMaxCAPITALLETTERRatio + ";found:" + actual.actualCAPITALLETTERRatio);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void A3checkForDailyMaxFollowerChangeRatioExceeded(List<Alert> newAlerts, RModule3 actual, EInstagramProfile expected, Scan actualScan, Scan previousScan)
        {
            int previousFollowernumber = (from p in previousScan.results where actual.idStr == ((RModule3)p).idStr select ((RModule3)p).actualFollowerNumber).FirstOrDefault();
            if (previousFollowernumber == 0)
                return;
            double actualChangeRatio = ((actual.actualFollowerNumber / previousFollowernumber) - 1);
            if (actualChangeRatio > expected.dailyMaxFollowerChangeRatio)
            {
                alert = new Alert(actualScan.id, 1, "More than daily max change in the number followers;expected:" + expected.dailyMaxFollowerChangeRatio + ";found:" + actualChangeRatio);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void A3checkForDailyMaxFolloweeChangeRatioExceeded(List<Alert> newAlerts, RModule3 actual, EInstagramProfile expected, Scan actualScan, Scan previousScan)
        {
            int previousFolloweenumber = (from p in previousScan.results where actual.idStr == ((RModule3)p).idStr select ((RModule3)p).actualFalloweeNumber).FirstOrDefault();
            if (previousFolloweenumber == 0)
                return;
            double actualChangeRatio = ((actual.actualFalloweeNumber / previousFolloweenumber) - 1);
            if (actualChangeRatio > expected.dailyMaxFalloweeChangeRatio)
            {
                alert = new Alert(actualScan.id, 1, "More than daily max change in the number followees;expected:" + expected.dailyMaxFalloweeChangeRatio + ";found:" + actualChangeRatio);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void A3unusualContentFound(List<Alert> newAlerts, RModule3 actual, EInstagramProfile expected, Scan actualScan)
        {
            if (!(actual.unusualContentFound == null) && !actual.unusualContentFound.Equals(""))
            {
                alert = new Alert(actualScan.id, 3, "Unusual content found;" + actual.unusualContentFound);
                alert.resultbaseid = actual.Id;
                alert.incidententityid = expected.Id;
                newAlerts.Add(alert);
            }
        }

        private void checkModule4(Scan actualScan)
        {

        }

        private void checkModule5(Scan actualScan)
        {

        }

        private void checkModule6(Scan actualScan)
        {

        }

        private void checkModule7(Scan actualScan)
        {

        }

        private void checkModule8(Scan actualScan)
        {

        }
        private void checkForActions(Alert alert)
        {
            SMTPClient.Instance.send(alert.incident, "ealparslan@gmail.com");
            SMSClient.Instance.send(alert.incident);
        }

    }
}