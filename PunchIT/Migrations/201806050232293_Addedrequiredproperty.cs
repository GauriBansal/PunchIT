namespace PunchIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedrequiredproperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeClocks", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.TimeClocks", new[] { "ApplicationUserId" });
            AlterColumn("dbo.TimeClocks", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TimeClocks", "ApplicationUserId");
            AddForeignKey("dbo.TimeClocks", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeClocks", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.TimeClocks", new[] { "ApplicationUserId" });
            AlterColumn("dbo.TimeClocks", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TimeClocks", "ApplicationUserId");
            AddForeignKey("dbo.TimeClocks", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
