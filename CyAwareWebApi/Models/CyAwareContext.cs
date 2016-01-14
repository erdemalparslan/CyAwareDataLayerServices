using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CyAwareWebApi.Models.Entities;
using CyAwareWebApi.Models.Results;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CyAwareWebApi.Models
{
    public class CyAwareContext : DbContext
    {
        public DbSet<Subscriber> subscribers { get; set; }

        public DbSet<EntityBase> entities { get; set; }

        public DbSet<Module> modules { get; set; }

        public DbSet<Policy> policies { get; set; }

        public DbSet<Schedule> schedules { get; set; }

        public DbSet<Action> actions { get; set; }

        public DbSet<Scan> scans { get; set; }

        public DbSet<ResultBase> results { get; set; }

        public DbSet<EntityExtraForPolicy> extras { get; set; }

        public DbSet<SysLog> SysLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

        }

    }
}
