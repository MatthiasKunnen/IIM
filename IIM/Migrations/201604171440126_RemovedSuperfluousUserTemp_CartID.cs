namespace IIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSuperfluousUserTemp_CartID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cart", "FK_dbo.Cart_dbo.UserTemp_CartId");
        }
        
        public override void Down()
        {
            AddForeignKey("dbo.Cart", "ApplicationUser_Id", "dbo.ApplicationUser", name: "FK_dbo.Cart_dbo.UserTemp_CartId");
        }
    }
}
