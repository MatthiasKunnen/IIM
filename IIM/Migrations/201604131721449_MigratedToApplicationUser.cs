namespace IIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigratedToApplicationUser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "UserTemp");
            DropForeignKey("dbo.Cart", "CartId", "dbo.user");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservation", "User_id", "dbo.user");
            DropIndex("dbo.Reservation", new[] { "User_Id" }); //Drop
            DropIndex("dbo.Cart", new[] { "CartId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" }); 
            DropIndex("dbo.UserTemp", "UserNameIndex");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            AddColumn("dbo.AspNetUserRoles", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserClaims", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserLogins", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Reservation", "User_Id", c => c.String(nullable: false, maxLength: 128)); //Alter
            AlterColumn("dbo.Cart", "CartId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserTemp", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.UserTemp", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.UserTemp", "Faculty", c => c.String(nullable: false));
            AlterColumn("dbo.UserTemp", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.UserTemp", "Email", c => c.String());
            AlterColumn("dbo.UserTemp", "UserName", c => c.String());
            AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.String());
            CreateIndex("dbo.Reservation", "User_Id");
            CreateIndex("dbo.AspNetUserClaims", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUserLogins", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUserRoles", "ApplicationUser_Id");
            CreateIndex("dbo.Cart", "CartId");
            AddForeignKey("dbo.Cart", "CartId", "dbo.UserTemp", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "ApplicationUser_Id", "dbo.UserTemp", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "ApplicationUser_Id", "dbo.UserTemp", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.UserTemp", "Id");
            DropTable("dbo.user");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.user",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Faculty = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        TelNumber = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.UserTemp");
            DropForeignKey("dbo.AspNetUserLogins", "ApplicationUser_Id", "dbo.UserTemp");
            DropForeignKey("dbo.AspNetUserClaims", "ApplicationUser_Id", "dbo.UserTemp");
            DropForeignKey("dbo.Cart", "CartId", "dbo.UserTemp");
            DropIndex("dbo.Cart", new[] { "CartId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Reservation", new[] { "User_Id" });
            AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserTemp", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.UserTemp", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.UserTemp", "Type", c => c.String());
            AlterColumn("dbo.UserTemp", "Faculty", c => c.String());
            AlterColumn("dbo.UserTemp", "FirstName", c => c.String());
            AlterColumn("dbo.UserTemp", "LastName", c => c.String());
            AlterColumn("dbo.Cart", "CartId", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservation", "User_Id", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUserLogins", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUserClaims", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUserRoles", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.UserTemp", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            CreateIndex("dbo.Cart", "CartId");
            CreateIndex("dbo.Reservation", "User_Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cart", "CartId", "dbo.user", "Id");
            AddForeignKey("dbo.Reservation", "User_id", "dbo.user");
            RenameTable(name: "dbo.UserTemp", newName: "AspNetUsers");
        }
    }
}
