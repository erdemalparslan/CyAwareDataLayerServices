using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CyAwareWebApi.Models;
using CyAwareWebApi.Models.Entities;

namespace CyAwareWebApi.Controllers
{
    public class DataController : ApiController
    {
        Module module1, module2, module3;

        Subscriber subscriber;

        EIpAddress ipAddress, ipAddress2, ipAddress3, ipAddress4;
        EPort port1, port2, port3;
        EIpRange ipRange1, ipRange2;
        ETwitterProfile twitter1, twitter2;
        EInstagramProfile instagram1, instagram2;

        private CyAwareContext db;

        DataController()
        {
            db = new CyAwareContext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        // PUT: api/Data/
        [ResponseType(typeof(void))]
        [Route("api/Data/{type}")]
        [HttpGet]
        public IHttpActionResult GetDataByType(int type)
        {
           
            HashSet<EntityBase> policy1Entities = new HashSet<EntityBase>();
            HashSet<EntityBase> policy2Entities = new HashSet<EntityBase>();
            createModule();
            createSubscriber();
            if(type == 1) // flat entities
            {
                createEntities(db.subscribers.Find(1),true);

                int[] policy1SetofEntities = new[] { ipAddress.Id, ipAddress2.Id, ipAddress3.Id, port1.Id };
                Array.ForEach(policy1SetofEntities, x => policy1Entities.Add(db.entities.Find(x)));
                int[] policy2SetofEntities = new[] { ipAddress4.Id, ipRange1.Id, ipRange2.Id, port2.Id, port3.Id };
                Array.ForEach(policy2SetofEntities, x => policy2Entities.Add(db.entities.Find(x)));

            }
            else if (type == 2) //  hierarchical entities
            {
                createEntities(db.subscribers.Find(1), false);

                int[] policy1SetofEntities = new[] { ipAddress.Id, ipAddress2.Id, ipAddress3.Id };
                Array.ForEach(policy1SetofEntities, x => policy1Entities.Add(db.entities.Find(x)));
                int[] policy2SetofEntities = new[] { ipAddress4.Id, ipRange1.Id, ipRange2.Id};
                Array.ForEach(policy2SetofEntities, x => policy2Entities.Add(db.entities.Find(x)));

            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "Service and Systems Availability checker module").FirstOrDefault(), policy1Entities);
            createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "Service and Systems Availability checker module").FirstOrDefault(), policy2Entities);
            return StatusCode(HttpStatusCode.NotAcceptable);

        }


        
        private int createModule()
        {
            module1 = new Module { moduleName = "Service and Systems Availability checker module", description = "blah blah blah" };
            module2 = new Module { moduleName = "Twitter activity checker module", description = "breh breh" };
            module3 = new Module { moduleName = "Instagram activity checker module", description = "breh breh" };
            db.modules.Add(module1);
            db.modules.Add(module2);
            db.modules.Add(module3);
            db.SaveChanges();
            return 0;
        }

        private int createSubscriber()
        {
            subscriber = new Subscriber { name = "aycell", subscriptionId = 1 };
            db.subscribers.Add(subscriber);
            db.SaveChanges();

            return 0;
        }

        private int createEntities(Subscriber subscriber, bool isFlatEntities)
        {

            ipAddress = new EIpAddress { entityType = "EIpAddress", ip = "104.209.43.4", subscriber = subscriber };
            ipAddress2 = new EIpAddress { entityType = "EIpAddress", ip = "52.25.28.149", subscriber = subscriber };
            ipAddress3 = new EIpAddress { entityType = "EIpAddress", ip = "52.10.180.11", subscriber = subscriber };
            ipAddress4 = new EIpAddress { entityType = "EIpAddress", ip = "66.6.44.5", subscriber = subscriber };
            port1 = new EPort { entityType = "EPort", type = "tcp", port = 22, subscriber = subscriber };
            port2 = new EPort { entityType = "EPort", type = "tcp", port = 80, subscriber = subscriber };
            port3 = new EPort { entityType = "EPort", type = "udp", port = 53, subscriber = subscriber };
            ipRange1 = new EIpRange { entityType = "EIpRange", ip = "10.12.120.0", range = 24, subscriber = subscriber };
            ipRange2 = new EIpRange { entityType = "EIpRange", ip = "23.15.0.0", range = 24, subscriber = subscriber };
            twitter1 = new ETwitterProfile { entityType = "ETwitterProfile" , idStr = "TCTWIT1", screenName = "TURKCELL TWIT1", dailyMaxTweets = 150, dailyMaxCAPITALLETTERRatio = 20, dailyMaxFalloweeChangeRatio = 10, dailyMaxFollowerChangeRatio = 10, searchStringForUnusualContent = "hacked,anonymous,telsim", subscriber = subscriber };
            twitter2 = new ETwitterProfile { entityType = "ETwitterProfile", idStr = "TCTWIT2", screenName = "TURKCELL TWIT2", dailyMaxTweets = 150, dailyMaxCAPITALLETTERRatio = 20, dailyMaxFalloweeChangeRatio = 10, dailyMaxFollowerChangeRatio = 10, searchStringForUnusualContent = "vodafone,avea,avea", subscriber = subscriber };
            instagram1 = new EInstagramProfile { entityType = "ETwitterProfile", idStr = "TCINST1", screenName = "TURKCELL INST1", dailyMaxPosts = 150, dailyMaxCAPITALLETTERRatio = 20, dailyMaxFalloweeChangeRatio = 10, dailyMaxFollowerChangeRatio = 10, searchStringForUnusualContent = "hacked,anonymous,telsim,vodafone,avea", subscriber = subscriber };
            instagram2 = new EInstagramProfile { entityType = "ETwitterProfile", idStr = "TCINST2", screenName = "TURKCELL INST2", dailyMaxPosts = 150, dailyMaxCAPITALLETTERRatio = 20, dailyMaxFalloweeChangeRatio = 10, dailyMaxFollowerChangeRatio = 10, searchStringForUnusualContent = "hacked,anonymous,telsim,vodafone,avea", subscriber = subscriber };

            if (!isFlatEntities)
            {
                ipAddress.subentities = new HashSet<EntityBase> { port1, port2 };
                ipAddress2.subentities = new HashSet<EntityBase> { port1, port3 };
            }

            db.entities.Add(ipAddress);
            db.entities.Add(ipAddress2);
            db.entities.Add(ipAddress3);
            db.entities.Add(ipAddress4);

            if (isFlatEntities)
            {
                db.entities.Add(port1);
                db.entities.Add(port2);
                db.entities.Add(port3);
            }

            db.entities.Add(ipRange1);
            db.entities.Add(ipRange2);

            db.entities.Add(twitter1);
            db.entities.Add(twitter2);
            db.entities.Add(instagram1);
            db.entities.Add(instagram2);

            db.SaveChanges();

            return 0;
            //return new List<int> {ipAddress.Id , ipAddress2 .Id, ipAddress3.Id, ipAddress4.Id};
        }

        private int createPolicyTest(Subscriber subscriber, Module module, HashSet<EntityBase> entities)
        {

            Schedule schedule1 = new Schedule { isHourly = true, period = 3};

            Models.Action action1 = new Models.Action { actionType = 1, destination = "" };
            Models.Action action2 = new Models.Action { actionType = 2, destination = "" };
            
            Policy policy1 = new Policy
            {
                action = action1,
                module = module,
                subscriber = subscriber,
                schedule = schedule1,
                activationDate = DateTime.Now,
                isActive = true,
                setDate = DateTime.Now,
                entities = entities
            };

            db.schedules.Add(schedule1);
            db.actions.Add(action1);
            db.actions.Add(action2);
            db.policies.Add(policy1);
            db.SaveChanges();

            return 0;
        }
    }
}
