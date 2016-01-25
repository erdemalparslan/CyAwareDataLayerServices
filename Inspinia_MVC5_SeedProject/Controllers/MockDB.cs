using Inspinia_MVC5_SeedProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class MockDB
    {
        static private List<EntityBase> entityBases = new List<EntityBase>();
        static private List<Policy> policies = new List<Policy>();
        static private List<Scan> scans = new List<Scan>();
        static private List<Alert> alerts = new List<Alert>();
        static int id = 100;

        public MockDB()
        {
            mockEntityBases();
            mockPolicies();
        }

        private void mockEntityBases()
        {
            Ip ip1 = new Ip();
            ip1.id = 1;
            ip1.ip = "192.168.5.45";
            ip1.subscriberId = 1;
            ip1.entityType = "EIpAddress";

            Ip ip2 = new Ip();
            ip2.id = 2;
            ip2.ip = "192.168.4.65";
            ip2.subscriberId = 1;
            ip2.entityType = "EIpAddress";

            IpRange ipRange1 = new IpRange();
            ipRange1.id = 3;
            ipRange1.ip = "192.168.6.54";
            ipRange1.range = 24;
            ipRange1.subscriberId = 1;
            ipRange1.entityType = "EIpRange";

            TwitterProfile tw1 = new TwitterProfile();
            tw1.id = 4;
            tw1.idStr = "deneme_twit1";
            tw1.screenName = "Deneme twit1";
            tw1.subscriberId = 1;
            tw1.entityType = "ETwitterProfile";
            tw1.searchStringForUnusualContent = "denemeT1,hack";

            TwitterProfile tw2 = new TwitterProfile();
            tw2.id = 5;
            tw2.idStr = "deneme_twit2";
            tw2.screenName = "Deneme twit2";
            tw2.subscriberId = 1;
            tw2.entityType = "ETwitterProfile";
            tw2.searchStringForUnusualContent = "denemeT2,hack";

            InstagramProfile ins1 = new InstagramProfile();
            ins1.id = 6;
            ins1.idStr = "deneme_ins1";
            ins1.screenName = "Deneme Ins1";
            ins1.subscriberId = 1;
            ins1.entityType = "EInstagramProfile";
            ins1.searchStringForUnusualContent = "denemeI1,hack";
            
            entityBases.Add(ip1);
            entityBases.Add(ip2);
            entityBases.Add(ipRange1);
            entityBases.Add(tw1);
            entityBases.Add(tw2);
            entityBases.Add(ins1);
        }

        public List<EntityBase> getEntityBases()
        {
            return entityBases;
        }

        public EntityBase getEntityBaseById(int id)
        {
            for (int i = 0; i < entityBases.Count; i++)
            {
                if (entityBases[i].id == id)
                {
                    return entityBases.ElementAt(i);
                }
            }
            return null;
        }

        public void addEntityBase(EntityBase eb)
        {
            eb.id = id;
            entityBases.Add(eb);
            id++;
        }

        public void editEntityBase(EntityBase eb)
        {
            for(int i = 0; i<entityBases.Count;i++)
            {
                if(entityBases[i].id == eb.id)
                {
                    entityBases[i] = eb;
                    break;
                }
            }
        }

        public void deleteEntityBase(int id)
        {
            int remove_index = 0;
            for (int i = 0; i<entityBases.Count;i++)
            {
                if(entityBases[i].id == id)
                {
                    remove_index = i;
                    break;
                }
            }
            entityBases.RemoveAt(remove_index);
        }

        private void mockPolicies()
        {
            /*
            Schedule s = new Schedule();
            s.isDaily = true;
            s.period = 3;
            s.enableEndTime24Format = 0;
            s.enableStartTime24Format = 0;*/

            DateTime dt1 = new DateTime(2015, 11, 23, 21, 19, 55);
            DateTime dt2 = new DateTime(2016, 1, 21, 15, 29, 45);

            HashSet<EntityBase> listIp = new HashSet<EntityBase>();
            listIp.Add(entityBases.ElementAt(0));
            listIp.Add(entityBases.ElementAt(2));

            HashSet<EntityBase> listIp2 = new HashSet<EntityBase>();
            listIp2.Add(entityBases.ElementAt(1));

            Policy p1 = new Policy();
            p1.id = 21;
            p1.actionId = 1;
            p1.isActive = true;
            p1.moduleId = 1;
            p1.activationDate = dt1;
            //p1.subscriberId = 1;
            //p1.schedule = s;
            p1.s_isDaily = true;
            p1.s_period = 3;
            p1.s_enableEndTime24Format = 0;
            p1.s_enableStartTime24Format = 0;
            p1.entities = listIp;

            Policy p2 = new Policy();
            p2.id = 22;
            p2.actionId = 1;
            p2.isActive = true;
            p2.moduleId = 1;
            p2.activationDate = dt2;
            //p2.subscriberId = 1;
            //p2.schedule = s;
            p2.s_isDaily = true;
            p2.s_period = 3;
            p2.s_enableEndTime24Format = 0;
            p2.s_enableStartTime24Format = 0;
            p2.entities = listIp2;

            policies.Add(p1);
            policies.Add(p2);
        }

        public List<Policy> getPolicies()
        {
            return policies;
        }

        public Policy getPolicyById(int id)
        {
            for (int i = 0; i < policies.Count; i++)
            {
                if (policies[i].id == id)
                {
                    return policies.ElementAt(i);
                }
            }
            return null;
        }

        public void addPolicy(Policy policy)
        {
            policy.id = id;
            policies.Add(policy);
            id++;
        }

        public void editPolicy(Policy policy)
        {
            for (int i = 0; i < policies.Count; i++)
            {
                if (policies[i].id == policy.id)
                {
                    policies[i] = policy;
                    break;
                }
            }
        }

        public void deletePolicy(int id)
        {
            int remove_index = 0;
            for (int i = 0; i < policies.Count; i++)
            {
                if (policies[i].id == id)
                {
                    remove_index = i;
                    break;
                }
            }
            policies.RemoveAt(remove_index);
        }

        public void mockScans()
        {
            DateTime dt1 = new DateTime(2015, 11, 23, 21, 19, 55);
            DateTime dt2 = new DateTime(2016, 4, 24, 12, 22, 25);
            DateTime dt3 = new DateTime(2016, 5, 23, 11, 44, 25);

            RModule1 rb1 = new RModule1();
            rb1.Id = 3001;
            rb1.ipAddress = "192.145.13.21";
            rb1.tcpPortNumbers = "80,443";
            rb1.resultType = "RModule1";
            rb1.policyId = 1;

            RModule1 rb2 = new RModule1();
            rb2.Id = 3002;
            rb2.ipAddress = "192.145.13.21";
            rb2.udpPortNumbers= "2523,254";
            rb2.resultType = "RModule1";
            rb2.policyId = 1;

            RModule1 rb3 = new RModule1();
            rb3.Id = 3003;
            rb3.ipAddress = "192.145.13.22";
            rb3.tcpPortNumbers = "443";
            rb3.resultType = "RModule1";
            rb3.policyId = 1;

            RModule1 rb4 = new RModule1();
            rb4.Id = 3004;
            rb4.ipAddress = "192.145.13.23";
            rb4.tcpPortNumbers = "65,23";
            rb4.resultType = "RModule1";
            rb4.policyId = 1;

            RModule1 rb5 = new RModule1();
            rb5.Id = 3005;
            rb5.ipAddress = "192.145.13.24";
            rb5.tcpPortNumbers = "31";
            rb5.resultType = "RModule1";
            rb5.policyId = 1;

            RModule1 rb6 = new RModule1();
            rb2.Id = 3006;
            rb2.ipAddress = "192.145.13.21";
            rb2.udpPortNumbers = "2523,254";
            rb2.resultType = "RModule1";
            rb2.policyId = 1;

            RModule1 rb7 = new RModule1();
            rb2.Id = 3007;
            rb2.ipAddress = "192.145.13.21";
            rb2.udpPortNumbers = "2523,254";
            rb2.resultType = "RModule1";
            rb2.policyId = 1;

            HashSet<ResultBase> results1 = new HashSet<ResultBase>();
            results1.Add(rb1);
            results1.Add(rb2);

            HashSet<ResultBase> results2 = new HashSet<ResultBase>();
            results2.Add(rb3);
            results2.Add(rb4);
            results2.Add(rb5);

            HashSet<ResultBase> results3 = new HashSet<ResultBase>();
            results3.Add(rb6);
            results3.Add(rb7);

            Scan scan1 = new Scan();
            scan1.id = 301;
            scan1.isDeleted = false;
            scan1.policyId = 21;
            scan1.scanDate = dt1;
            scan1.scanRefId = "A321442";
            scan1.scanSuccessCode = 1;
            scan1.results = results1;

            Scan scan2 = new Scan();
            scan1.id = 302;
            scan1.isDeleted = false;
            scan1.policyId = 21;
            scan1.scanDate = dt2;
            scan1.scanRefId = "A321656";
            scan1.scanSuccessCode = 1;
            scan1.results = results2;

            Scan scan3 = new Scan();
            scan1.id = 303;
            scan1.isDeleted = false;
            scan1.policyId = 22;
            scan1.scanDate = dt3;
            scan1.scanRefId = "A321823";
            scan1.scanSuccessCode = 1;
            scan1.results = results3;

            scans.Add(scan1);
            scans.Add(scan2);
            scans.Add(scan3);
        }

        public List<Scan> getScans()
        {
            return scans;
        }

        public Scan getScanById(int id)
        {
            for (int i = 0; i < scans.Count; i++)
            {
                if (scans[i].id == id)
                {
                    return scans.ElementAt(i);
                }
            }
            return null;
        }

        public void addScan(Scan scan)
        {
            scan.id = id;
            scans.Add(scan);
            id++;
        }

        public void editScan(Scan scan)
        {
            for (int i = 0; i < scans.Count; i++)
            {
                if (scans[i].id == scan.id)
                {
                    scans[i] = scan;
                    break;
                }
            }
        }

        public void deleteScan(int id)
        {
            int remove_index = 0;
            for (int i = 0; i < scans.Count; i++)
            {
                if (scans[i].id == id)
                {
                    remove_index = i;
                    break;
                }
            }
            scans.RemoveAt(remove_index);
        }

        public void mockAlerts()
        {
            DateTime dt1 = new DateTime(2015, 11, 23, 21, 19, 55);
            DateTime dt2 = new DateTime(2016, 4, 24, 10, 22, 25);
            DateTime dt3 = new DateTime(2016, 7, 14, 12, 9, 5);
            DateTime dt4 = new DateTime(2016, 8, 21, 11, 2, 8);

            Alert a1 = new Alert();
            a1.Id = 401;
            a1.occuringDate = dt1;
            a1.scanId = 2;
            a1.severityLevel = 2;
            a1.incident = "NEWIP-TCP;ip:192.145.13.21;port:22";

            Alert a2 = new Alert();
            a2.Id = 402;
            a2.occuringDate = dt2;
            a2.scanId = 2;
            a2.severityLevel = 2;
            a2.incident = "NEWIP-UDP;ip:192.145.13.21;port:255";

            Alert a3 = new Alert();
            a3.Id = 403;
            a3.occuringDate = dt3;
            a3.scanId = 2;
            a3.severityLevel = 2;
            a3.incident = "NEWIP-UDP;ip:192.145.13.24;port:32";

            Alert a4 = new Alert();
            a4.Id = 404;
            a4.occuringDate = dt4;
            a4.scanId = 2;
            a4.severityLevel = 2;
            a4.incident = "NEWIP-TCP;ip:192.145.13.21;port:254";

            alerts.Add(a1);
            alerts.Add(a2);
            alerts.Add(a3);
            alerts.Add(a4);
        }

        public List<Alert> getAlerts()
        {
            return alerts;
        }

        public Alert getAlertById(int id)
        {
            for (int i = 0; i < alerts.Count; i++)
            {
                if (alerts[i].Id == id)
                {
                    return alerts.ElementAt(i);
                }
            }
            return null;
        }



        public void addScan(Scan scan)
        {
            scan.id = id;
            scans.Add(scan);
            id++;
        }

        public void editScan(Scan scan)
        {
            for (int i = 0; i < scans.Count; i++)
            {
                if (scans[i].id == scan.id)
                {
                    scans[i] = scan;
                    break;
                }
            }
        }

        public void deleteScan(int id)
        {
            int remove_index = 0;
            for (int i = 0; i < scans.Count; i++)
            {
                if (scans[i].id == id)
                {
                    remove_index = i;
                    break;
                }
            }
            scans.RemoveAt(remove_index);
        }
    }
}