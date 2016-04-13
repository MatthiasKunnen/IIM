namespace IIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedApplicationUserPropertyIsLocal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsLocal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsLocal");
        }
    }
}
