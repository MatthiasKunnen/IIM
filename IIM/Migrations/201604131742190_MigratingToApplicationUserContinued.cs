namespace IIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigratingToApplicationUserContinued : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserTemp", newName: "User");
            AddForeignKey("dbo.Reservation", "User_id", "dbo.User");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.User", newName: "UserTemp");
            DropForeignKey("dbo.Reservation", "User_id", "dbo.User");
        }
    }
}
