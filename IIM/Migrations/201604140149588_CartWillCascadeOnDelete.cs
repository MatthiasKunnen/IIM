namespace IIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartWillCascadeOnDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cart", "ApplicationUser_Id", "dbo.User");
            AddForeignKey("dbo.Cart", "ApplicationUser_Id", "dbo.User", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cart", "ApplicationUser_Id", "dbo.User");
            AddForeignKey("dbo.Cart", "ApplicationUser_Id", "dbo.User", "Id");
        }
    }
}
