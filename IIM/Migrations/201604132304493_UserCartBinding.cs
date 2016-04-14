namespace IIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCartBinding : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Cart", name: "CartId", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Cart", name: "IX_CartId", newName: "IX_ApplicationUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Cart", name: "IX_ApplicationUser_Id", newName: "IX_CartId");
            RenameColumn(table: "dbo.Cart", name: "ApplicationUser_Id", newName: "CartId");
        }
    }
}
