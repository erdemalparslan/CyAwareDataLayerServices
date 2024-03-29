﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CyAwareWebApi.Models;
using CyAwareWebApi.Models.Entities;
using CyAwareWebApi.Models.Results;

namespace CyAwareWebApi.Controllers
{
    public class DataController : ApiController
    {
        Module module1, module2, module3, module4, module5, module6, module7, module8;

        Subscriber subscriber;

        EIpAddress ipAddress1, ipAddress2, ipAddress3, ipAddress4;
        EIpRange ipRange1, ipRange2;
        ETwitterProfile twitter1, twitter2;
        EInstagramProfile instagram1, instagram2;
        EDomain domain1, domain2, domain3;
        EHostname hostname1, hostname2, hostname3, hostname4, hostname5, hostname6, hostname7;

        CyAwareWebApi.Models.Action action1, action2;

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

            createActions();
            HashSet<Models.Action> actions = new HashSet<Models.Action>();
            actions.Add(db.actions.Find(action1.id));
            actions.Add(db.actions.Find(action2.id));

            HashSet<EntityBase> policy1Entities = new HashSet<EntityBase>(); // policy1 for module1 (ip-port discovery)
            HashSet<EntityBase> policy2Entities = new HashSet<EntityBase>(); // policy2 for module1 (ip-port discovery)
            HashSet<EntityBase> policy3Entities = new HashSet<EntityBase>(); // policy3 for module2 (twitter)
            HashSet<EntityBase> policy4Entities = new HashSet<EntityBase>(); // policy4 for module3 (instagram)
            HashSet<EntityBase> policy5Entities = new HashSet<EntityBase>(); // policy5 for module4 (dns records discovery)
            HashSet<EntityBase> policy6Entities = new HashSet<EntityBase>(); // policy6 for module5 (domain expire checker)
            HashSet<EntityBase> policy7Entities = new HashSet<EntityBase>(); // policy7 for module6 (SSL expire checker)
            HashSet<EntityBase> policy8Entities = new HashSet<EntityBase>(); // policy8 for module6 (SSL expire checker)
            HashSet<EntityBase> policy9Entities = new HashSet<EntityBase>(); // policy9 for module7 (ssl certificate details)
            HashSet<EntityBase> policy10Entities = new HashSet<EntityBase>(); // policy10 for module7 (ssl certificate details)
            HashSet<EntityBase> policy11Entities = new HashSet<EntityBase>(); // policy11 for module8 (search engine discovery)
            HashSet<EntityBase> policy12Entities = new HashSet<EntityBase>(); // policy12 for module8 (search engine discovery)


            createModule();
            createSubscriber();
            if (type == 1) // flat entities
            {
                createEntities(db.subscribers.Find(1), true);

                int[] policy1SetofEntities = new[] { ipAddress1.Id, ipAddress2.Id, ipAddress3.Id };
                Array.ForEach(policy1SetofEntities, x => policy1Entities.Add(db.entities.Find(x)));
                int[] policy2SetofEntities = new[] { ipAddress4.Id, ipRange1.Id, ipRange2.Id };
                Array.ForEach(policy2SetofEntities, x => policy2Entities.Add(db.entities.Find(x)));

            }
            else if (type == 2) //  hierarchical entities
            {
                createEntities(db.subscribers.Find(1), false);

                int[] policy1SetofEntities = new[] { ipAddress1.Id, ipAddress2.Id };
                Array.ForEach(policy1SetofEntities, x => policy1Entities.Add(db.entities.Find(x)));

                int[] policy2SetofEntities = new[] { ipAddress4.Id, ipRange1.Id, ipRange2.Id };
                Array.ForEach(policy2SetofEntities, x => policy2Entities.Add(db.entities.Find(x)));

                int[] policy3SetofEntities = new[] { twitter1.Id, twitter2.Id };
                Array.ForEach(policy3SetofEntities, x => policy3Entities.Add(db.entities.Find(x)));

                int[] policy4SetofEntities = new[] { instagram1.Id, instagram2.Id };
                Array.ForEach(policy4SetofEntities, x => policy4Entities.Add(db.entities.Find(x)));

                int[] policy5SetofEntities = new[] { domain1.Id, domain2.Id, domain3.Id };
                Array.ForEach(policy5SetofEntities, x => policy5Entities.Add(db.entities.Find(x)));

                int[] policy6SetofEntities = new[] { domain1.Id, domain2.Id };
                Array.ForEach(policy6SetofEntities, x => policy6Entities.Add(db.entities.Find(x)));

                int[] policy7SetofEntities = new[] { domain2.Id, domain3.Id };
                Array.ForEach(policy7SetofEntities, x => policy7Entities.Add(db.entities.Find(x)));

                int[] policy8SetofEntities = new[] { ipAddress1.Id, domain1.Id };
                Array.ForEach(policy8SetofEntities, x => policy8Entities.Add(db.entities.Find(x)));

                int[] policy9SetofEntities = new[] { domain2.Id };
                Array.ForEach(policy9SetofEntities, x => policy9Entities.Add(db.entities.Find(x)));

                int[] policy10SetofEntities = new[] { ipAddress1.Id, domain1.Id };
                Array.ForEach(policy10SetofEntities, x => policy10Entities.Add(db.entities.Find(x)));

                int[] policy11SetofEntities = new[] { domain2.Id };
                Array.ForEach(policy11SetofEntities, x => policy11Entities.Add(db.entities.Find(x)));

                int[] policy12SetofEntities = new[] { ipAddress1.Id, domain1.Id };
                Array.ForEach(policy12SetofEntities, x => policy12Entities.Add(db.entities.Find(x)));
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            int id = 0;
            id = createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "Service and Systems Availability checker module")
                .FirstOrDefault(), policy1Entities, actions);
            db.extras.Add(new EntityExtraForPolicy() { key = "tcp", value = "80, 443, 8080, 22, 3306", entity = ipAddress1, policyId = id });
            db.extras.Add(new EntityExtraForPolicy() { key = "udp", value = "68", entity = ipAddress1, policyId = id });
            //db.extras.Add(new EntityExtraForPolicy() { key = "udp", value = "161,53", entity = ipAddress2, policyId = id });
            db.extras.Add(new EntityExtraForPolicy() { key = "tcp", value = "80, 135, 445, 3389, 5986, 47001, 49152, 49153, 49154, 49155, 49156, 49158", entity = ipAddress2, policyId = id });
            db.extras.Add(new EntityExtraForPolicy() { key = "udp", value = "123, 3389, 5355", entity = ipAddress2, policyId = id });


            id = createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "Service and Systems Availability checker module").FirstOrDefault(), policy2Entities, actions);
            db.extras.Add(new EntityExtraForPolicy() { key = "tcp", value = "8080,80,21,445,23,443", entity = ipAddress4, policyId = id });
            //db.extras.Add(new EntityExtraForPolicy() { key = "udp", value = "161,53", entity = ipAddress4, policyId = id });
            //db.extras.Add(new EntityExtraForPolicy() { key = "udp", value = "161,53", entity = ipRange1, policyId = id });
            db.extras.Add(new EntityExtraForPolicy() { key = "tcp", value = "443,21,80,8080,445,23", entity = ipRange2, policyId = id });
            //db.extras.Add(new EntityExtraForPolicy() { key = "udp", value = "161,53", entity = ipRange2, policyId = id });

            createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "Twitter activity checker module").FirstOrDefault(), policy3Entities, actions);
            createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "Instagram activity checker module").FirstOrDefault(), policy4Entities, actions);

            createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "DNS records discovery module").FirstOrDefault(), policy5Entities, actions);

            createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "Domain expire checker module").FirstOrDefault(), policy6Entities, actions);

            id = createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "SSL expire checker module").FirstOrDefault(), policy7Entities, actions);
            db.extras.Add(new EntityExtraForPolicy() { key = "tcp", value = "443", entity = domain2, policyId = id });
            db.extras.Add(new EntityExtraForPolicy() { key = "tcp", value = "443", entity = domain3, policyId = id });

            id = createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "SSL expire checker module").FirstOrDefault(), policy8Entities, actions);
            db.extras.Add(new EntityExtraForPolicy() { key = "tcp", value = "443", entity = domain1, policyId = id });
            db.extras.Add(new EntityExtraForPolicy() { key = "tcp", value = "443", entity = ipAddress1, policyId = id });

            id = createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "SSL certificate details module").FirstOrDefault(), policy9Entities, actions);
            db.extras.Add(new EntityExtraForPolicy() { key = "tcp", value = "443", entity = domain2, policyId = id });

            id = createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "SSL certificate details module").FirstOrDefault(), policy10Entities, actions);
            db.extras.Add(new EntityExtraForPolicy() { key = "tcp", value = "443", entity = domain1, policyId = id });
            db.extras.Add(new EntityExtraForPolicy() { key = "tcp", value = "443", entity = ipAddress1, policyId = id });

            id = createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "Search engines discovery module").FirstOrDefault(), policy11Entities, actions);
            db.extras.Add(new EntityExtraForPolicy() { key = "searchStrings", value = "point,result,set", entity = domain2, policyId = id });

            id = createPolicyTest(db.subscribers.Where(s => s.name == "aycell").FirstOrDefault(), db.modules.Where(m => m.moduleName == "Search engines discovery module").FirstOrDefault(), policy12Entities, actions);
            db.extras.Add(new EntityExtraForPolicy() { key = "searchStrings", value = "iletisim,servis,3G,fatura", entity = domain1, policyId = id });
            db.extras.Add(new EntityExtraForPolicy() { key = "searchStrings", value = "module,dashboard", entity = ipAddress1, policyId = id });
            //db.extras.Add(new EntityExtraForPolicy() { key = "searchStrings", value = "point,result,set", entity = domain2, policyId = id });

            db.SaveChanges();

            //createScan();

            return StatusCode(HttpStatusCode.NotAcceptable);
        }

        
        private int createModule()
        {
            module1 = new Module { moduleName = "Service and Systems Availability checker module", description = "blah blah blah" };
            module2 = new Module { moduleName = "Twitter activity checker module", description = "breh breh" };
            module3 = new Module { moduleName = "Instagram activity checker module", description = "breh breh" };
            module4 = new Module { moduleName = "DNS records discovery module", description = "breh breh" };
            module5 = new Module { moduleName = "Domain expire checker module", description = "breh breh" };
            module6 = new Module { moduleName = "SSL expire checker module", description = "breh breh" };
            module7 = new Module { moduleName = "SSL certificate details module", description = "breh breh" };
            module8 = new Module { moduleName = "Search engines discovery module", description = "breh breh" };

            db.modules.Add(module1);
            db.modules.Add(module2);
            db.modules.Add(module3);
            db.modules.Add(module4);
            db.modules.Add(module5);
            db.modules.Add(module6);
            db.modules.Add(module7);
            db.modules.Add(module8);
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
            ipAddress1 = new EIpAddress { entityType = "EIpAddress", ip = "52.179.127.117", subscriber = subscriber , Id=-1, mainEntityId = null };
            ipAddress2 = new EIpAddress { entityType = "EIpAddress", ip = "104.45.137.170", subscriber = subscriber, Id=-2, mainEntityId = null };
            ipAddress3 = new EIpAddress { entityType = "EIpAddress", ip = "52.10.180.11", subscriber = subscriber , Id = -3, mainEntityId = null };
            ipAddress4 = new EIpAddress { entityType = "EIpAddress", ip = "46.101.150.106", subscriber = subscriber, Id = -4, mainEntityId = null };
            ipRange1 = new EIpRange { entityType = "EIpRange", ip = "10.12.120.0", range = 31, subscriber = subscriber , Id = -8, mainEntityId = null };
            ipRange2 = new EIpRange { entityType = "EIpRange", ip = "23.15.0.0", range = 31, subscriber = subscriber , Id = -9, mainEntityId = null };

            twitter1 = new ETwitterProfile { entityType = "ETwitterProfile" , idStr = "126571977", screenName = "TURKCELL TWIT1", dailyMaxTweets = 150, dailyMaxCAPITALLETTERRatio = 20, dailyMaxFalloweeChangeRatio = 10, dailyMaxFollowerChangeRatio = 10, searchStringForUnusualContent = "hacked,anonymous,telsim", subscriber = subscriber , Id = -10, mainEntityId = null };
            twitter2 = new ETwitterProfile { entityType = "ETwitterProfile", idStr = "317212937", screenName = "TURKCELL TWIT2", dailyMaxTweets = 150, dailyMaxCAPITALLETTERRatio = 20, dailyMaxFalloweeChangeRatio = 10, dailyMaxFollowerChangeRatio = 10, searchStringForUnusualContent = "vodafone,avea,avea", subscriber = subscriber, Id = -11, mainEntityId = null };

            instagram1 = new EInstagramProfile { entityType = "EInstagramProfile", idStr = "2255540131", screenName = "TURKCELL INST1", dailyMaxPosts = 150, dailyMaxCAPITALLETTERRatio = 20, dailyMaxFalloweeChangeRatio = 10, dailyMaxFollowerChangeRatio = 10, searchStringForUnusualContent = "hacked,anonymous,telsim,vodafone,avea", subscriber = subscriber, Id = -12, mainEntityId = null };
            instagram2 = new EInstagramProfile { entityType = "EInstagramProfile", idStr = "505129896", screenName = "TURKCELL INST2", dailyMaxPosts = 150, dailyMaxCAPITALLETTERRatio = 20, dailyMaxFalloweeChangeRatio = 10, dailyMaxFollowerChangeRatio = 10, searchStringForUnusualContent = "hacked,anonymous,telsim,vodafone,avea", subscriber = subscriber, Id = -13, mainEntityId = null };

            domain1 = new EDomain { entityType = "EDomain", domainName = "google.com", subscriber = subscriber, Id = -14, mainEntityId = null };
            domain2 = new EDomain { entityType = "EDomain", domainName = "monaware.com" , subscriber = subscriber, Id = -15, mainEntityId = null };
            domain3 = new EDomain { entityType = "EDomain", domainName = "yandex.com", subscriber = subscriber, Id = -21, mainEntityId = null };


            hostname1 = new EHostname { entityType = "EHostname", hostname = "www.google.com", subscriber = subscriber, Id = -16, mainEntityId = -14 };
            hostname2 = new EHostname { entityType = "EHostname", hostname = "docs.google.com", subscriber = subscriber, Id = -17, mainEntityId = -14 };
            hostname3 = new EHostname { entityType = "EHostname", hostname = "api.monaware.com", subscriber = subscriber, Id = -18, mainEntityId = -15 };
            hostname4 = new EHostname { entityType = "EHostname", hostname = "db.monaware.com", subscriber = subscriber, Id = -19, mainEntityId = -15 };
            hostname5 = new EHostname { entityType = "EHostname", hostname = "forum.monaware.com", subscriber = subscriber, Id = -20, mainEntityId = -15 };
            hostname6 = new EHostname { entityType = "EHostname", hostname = "maps.yandex.com", subscriber = subscriber, Id = -22, mainEntityId = -21 };
            hostname7 = new EHostname { entityType = "EHostname", hostname = "translate.yandex.com", subscriber = subscriber, Id = -23, mainEntityId = -21 };


            if (!isFlatEntities)
            {

            }

            db.entities.Add(ipAddress1);
            db.entities.Add(ipAddress2);
            db.entities.Add(ipAddress3);
            db.entities.Add(ipAddress4);

            if (isFlatEntities)
            {

            }

            db.entities.Add(ipRange1);
            db.entities.Add(ipRange2);

            db.entities.Add(twitter1);
            db.entities.Add(twitter2);
            db.entities.Add(instagram1);
            db.entities.Add(instagram2);

            db.entities.Add(domain1);
            db.entities.Add(domain2);
            db.entities.Add(domain3);

            db.entities.Add(hostname1);
            db.entities.Add(hostname2);
            db.entities.Add(hostname3);
            db.entities.Add(hostname4);
            db.entities.Add(hostname5);
            db.entities.Add(hostname6);
            db.entities.Add(hostname7);

            db.SaveChanges();
            return 0;
        }

        private int createActions()
        {
            action1 = new Models.Action { actionType = 1, destination = "" };
            action2 = new Models.Action { actionType = 2, destination = "" };
            db.actions.Add(action1);
            db.actions.Add(action2);
            db.SaveChanges();
            return 0;
        }

        private int createPolicyTest(Subscriber subscriber, Module module, HashSet<EntityBase> entities, HashSet<Models.Action> actions)
        {

            Policy policy1 = new Policy
            {
                actions = actions,
                module = module,
                subscriber = subscriber,
                //s_isHourly = true,
                //s_period = 3,
                activationDate = DateTime.Now,
                isActive = true,
                setDate = DateTime.Now,
                entities = entities
            };

            db.policies.Add(policy1);
            db.SaveChanges();

            return policy1.Id;
        }

        private int createScan()
        {
            RModule1 result1 = new RModule1 { ipAddress = "192.145.13.21", tcpPortNumbers = "80,443", resultType = "RModule1", policyId = 1 };
            RModule1 result2 = new RModule1 { ipAddress = "192.145.13.21", udpPortNumbers = "2323,254", resultType = "RModule1", policyId = 1 };
            RModule1 result3 = new RModule1 { ipAddress = "192.145.13.22", tcpPortNumbers = "443", resultType = "RModule1", policyId = 1 };
            RModule1 result4 = new RModule1 { ipAddress = "192.145.13.23", udpPortNumbers = "65,23", resultType = "RModule1", policyId = 1 };
            RModule1 result5 = new RModule1 { ipAddress = "192.145.13.24", udpPortNumbers = "31", resultType = "RModule1", policyId = 1 };
            Scan scan1 = new Scan { policyId = 1, scanRefId = "A1231542", scanDate = DateTime.Now, scanSuccessCode = 1, results = new HashSet<ResultBase> { result1, result2, result3, result4, result5 } };

            db.scans.Add(scan1);
            db.SaveChanges();
            return 0;
        }
    }
}
