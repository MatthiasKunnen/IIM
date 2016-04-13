namespace IIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Material",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleNr = c.String(nullable: false),
                        Description = c.String(),
                        Encoding = c.String(),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        FirmId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Firm", t => t.FirmId)
                .Index(t => t.FirmId);
            
            CreateTable(
                "dbo.Curricular",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Firm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialIdentifier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Place = c.String(nullable: false),
                        Visibility = c.Int(nullable: false),
                        Material_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Material", t => t.Material_Id, cascadeDelete: true)
                .Index(t => t.Material_Id);
            
            CreateTable(
                "dbo.TargetGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.user", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.user", t => t.User_Id1)
                .Index(t => t.UserId)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.ReservationDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BroughtBackDate = c.DateTime(),
                        PickUpDate = c.DateTime(),
                        MaterialIdentifierId = c.Int(nullable: false),
                        Reservation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MaterialIdentifier", t => t.MaterialIdentifierId, cascadeDelete: true)
                .ForeignKey("dbo.Reservation", t => t.Reservation_Id, cascadeDelete: true)
                .Index(t => t.MaterialIdentifierId)
                .Index(t => t.Reservation_Id);
            
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
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        CartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.user", t => t.CartId)
                .Index(t => t.CartId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Faculty = c.String(),
                        Type = c.String(),
                        TelNumber = c.String(),
                        Base64Photo = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MaterialCurricular",
                c => new
                    {
                        MaterialId = c.Int(nullable: false),
                        CurricularId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MaterialId, t.CurricularId })
                .ForeignKey("dbo.Material", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Curricular", t => t.CurricularId, cascadeDelete: true)
                .Index(t => t.MaterialId)
                .Index(t => t.CurricularId);
            
            CreateTable(
                "dbo.MaterialTargetgroup",
                c => new
                    {
                        MaterialId = c.Int(nullable: false),
                        TargetgroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MaterialId, t.TargetgroupId })
                .ForeignKey("dbo.Material", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.TargetGroup", t => t.TargetgroupId, cascadeDelete: true)
                .Index(t => t.MaterialId)
                .Index(t => t.TargetgroupId);
            
            CreateTable(
                "dbo.CartMaterial",
                c => new
                    {
                        CartId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CartId, t.MaterialId })
                .ForeignKey("dbo.Cart", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.Material", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.CartId)
                .Index(t => t.MaterialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reservation", "User_Id1", "dbo.user");
            DropForeignKey("dbo.Cart", "CartId", "dbo.user");
            DropForeignKey("dbo.CartMaterial", "MaterialId", "dbo.Material");
            DropForeignKey("dbo.CartMaterial", "CartId", "dbo.Cart");
            DropForeignKey("dbo.Reservation", "UserId", "dbo.user");
            DropForeignKey("dbo.ReservationDetail", "Reservation_Id", "dbo.Reservation");
            DropForeignKey("dbo.ReservationDetail", "MaterialIdentifierId", "dbo.MaterialIdentifier");
            DropForeignKey("dbo.MaterialTargetgroup", "TargetgroupId", "dbo.TargetGroup");
            DropForeignKey("dbo.MaterialTargetgroup", "MaterialId", "dbo.Material");
            DropForeignKey("dbo.MaterialIdentifier", "Material_Id", "dbo.Material");
            DropForeignKey("dbo.Material", "FirmId", "dbo.Firm");
            DropForeignKey("dbo.MaterialCurricular", "CurricularId", "dbo.Curricular");
            DropForeignKey("dbo.MaterialCurricular", "MaterialId", "dbo.Material");
            DropIndex("dbo.CartMaterial", new[] { "MaterialId" });
            DropIndex("dbo.CartMaterial", new[] { "CartId" });
            DropIndex("dbo.MaterialTargetgroup", new[] { "TargetgroupId" });
            DropIndex("dbo.MaterialTargetgroup", new[] { "MaterialId" });
            DropIndex("dbo.MaterialCurricular", new[] { "CurricularId" });
            DropIndex("dbo.MaterialCurricular", new[] { "MaterialId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Cart", new[] { "CartId" });
            DropIndex("dbo.ReservationDetail", new[] { "Reservation_Id" });
            DropIndex("dbo.ReservationDetail", new[] { "MaterialIdentifierId" });
            DropIndex("dbo.Reservation", new[] { "User_Id1" });
            DropIndex("dbo.Reservation", new[] { "UserId" });
            DropIndex("dbo.MaterialIdentifier", new[] { "Material_Id" });
            DropIndex("dbo.Material", new[] { "FirmId" });
            DropTable("dbo.CartMaterial");
            DropTable("dbo.MaterialTargetgroup");
            DropTable("dbo.MaterialCurricular");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Cart");
            DropTable("dbo.user");
            DropTable("dbo.ReservationDetail");
            DropTable("dbo.Reservation");
            DropTable("dbo.TargetGroup");
            DropTable("dbo.MaterialIdentifier");
            DropTable("dbo.Firm");
            DropTable("dbo.Curricular");
            DropTable("dbo.Material");
        }
    }
}
