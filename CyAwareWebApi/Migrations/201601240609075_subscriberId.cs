namespace CyAwareWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subscriberId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "subscriberId", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "subscriberId");
        }
    }
}
