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

            Module m1 = new Module();
            m1.id = 1;

            HashSet<EntityBase> listIp = new HashSet<EntityBase>();
            listIp.Add(entityBases.ElementAt(0));
            listIp.Add(entityBases.ElementAt(2));

            HashSet<EntityBase> listIp2 = new HashSet<EntityBase>();
            listIp2.Add(entityBases.ElementAt(1));

            Policy p1 = new Policy();
            p1.id = 21;
            p1.actionId = 1;
            p1.isActive = true;
            p1.module = m1;
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
            p2.module = m1;
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
    }
}