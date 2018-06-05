namespace PunchIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pendingmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeClocks", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TimeClocks", "ApplicationUserId");
            AddForeignKey("dbo.TimeClocks", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeClocks", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.TimeClocks", new[] { "ApplicationUserId" });
            DropColumn("dbo.TimeClocks", "ApplicationUserId");
        }
    }
}
