using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CyAwareWebApi.Models;
using CyAwareWebApi.Models.Entities;
using System.Data.Entity;

namespace CyAwareWebApi.Controllers
{
    public class DataController : ApiController
    {
        Module module1, module2;

        private CyAwareContext db;

        DataController()
        {
            db = new CyAwareContext();
            db.Configuration.ProxyCreationEnabled = false;
        }

        // PUT: api/Data
        [ResponseType(typeof(void))]
        public IHttpActionResult GetData()
        {
            createModule();
            createSubscriber();
            createEntities(db.subscribers.Find(1));

            createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "IP scan").FirstOrDefault(), new HashSet<EntityBase> { db.entities.Find(1), db.entities.Find(7), db.entities.Find(8), db.entities.Find(9) });
            createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "IP scan").FirstOrDefault(), new HashSet<EntityBase> { db.entities.Find(4), db.entities.Find(8), db.entities.Find(1) });
            return StatusCode(HttpStatusCode.NotAcceptable);

        }
        
        private int createModule()
        {
            module1 = new Module { moduleName = "IP scan", description = "blah blah blah" };
            module2 = new Module { moduleName = "Social network ids hacked", description = "breh breh" };
            db.modules.Add(module1);
            db.modules.Add(module2);
            db.SaveChanges();
            return 0;
        }

        private int createSubscriber()
        {
            Subscriber subscriber = new Subscriber { name = "aycell", subscriptionId = 1 };
            db.subscribers.Add(subscriber);
            db.SaveChanges();

            return 0;

        }

        private int createEntities(Subscriber subscriber)
        {
            EIpAddress ipAddress = new EIpAddress { entityType = "EIpAddress", ip = "104.209.43.4", subscriber = subscriber };
            EIpAddress ipAddress2 = new EIpAddress { entityType = "EIpAddress", ip = "52.25.28.149", subscriber = subscriber };
            EIpAddress ipAddress3 = new EIpAddress { entityType = "EIpAddress", ip = "66.6.44.4", subscriber = subscriber };
            EIpAddress ipAddress4 = new EIpAddress { entityType = "EIpAddress", ip = "66.6.44.5", subscriber = subscriber };
            ESocNetId socNetId1 = new ESocNetId { entityType = "ESocNetId", type = "twitter", idStr = "twitAycell" ,subscriber = subscriber };
            ESocNetId socNetId2 = new ESocNetId { entityType = "ESocNetId", type = "facebook", idStr = "faceAycell", subscriber = subscriber };
            EPort port1 = new EPort { entityType = "EPort", type = "tcp", port = 22, subscriber = subscriber };
            EPort port2 = new EPort { entityType = "EPort", type = "tcp", port = 80, subscriber = subscriber };
            EIpRange ipRange1 = new EIpRange { entityType = "EIpRange", ip = "10.12.120.0", range = 24, subscriber = subscriber };
            EIpRange ipRange2 = new EIpRange { entityType = "EIpRange", ip = "23.15.0.0", range = 16, subscriber = subscriber };

            ipAddress.subentities = new HashSet<EntityBase> { port1, port2 };
            ipRange1.subentities = new HashSet<EntityBase> { socNetId1, socNetId2 };

            db.entities.Add(ipAddress);
            db.entities.Add(ipAddress2);
            db.entities.Add(ipAddress3);
            db.entities.Add(ipAddress4);
            //db.entities.Add(socNetId1);
            //db.entities.Add(socNetId2);
            //db.entities.Add(port1);
            //db.entities.Add(port2);
            db.entities.Add(ipRange1);
            db.entities.Add(ipRange2);

            /*


            db.entities.Add(ipAddress);
            db.entities.Add(ipAddress2);
            db.entities.Add(ipAddress3);
            db.entities.Add(ipAddress4);
            db.entities.Add(socNetId1);
            db.entities.Add(socNetId2);

            db.entities.Add(ipRange1);
            db.entities.Add(ipRange2);*/

            db.SaveChanges();

            return 0;
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
