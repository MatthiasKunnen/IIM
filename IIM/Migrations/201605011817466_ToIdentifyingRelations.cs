namespace IIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToIdentifyingRelations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReservationDetail", "MaterialIdentifier_Id", "dbo.MaterialIdentifier");
            DropIndex("dbo.ReservationDetail", new[] { "MaterialIdentifier_Id" });
            RenameColumn(table: "dbo.MaterialIdentifier", name: "Material_Id", newName: "MaterialId");
            RenameIndex(table: "dbo.MaterialIdentifier", name: "IX_Material_Id", newName: "IX_MaterialId");
            DropPrimaryKey("dbo.MaterialIdentifier");
            AddColumn("dbo.ReservationDetail", "MaterialIdentifier_MaterialId", c => c.Int(nullable: false));
            AlterColumn("dbo.MaterialIdentifier", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MaterialIdentifier", new[] { "Id", "MaterialId" });
            CreateIndex("dbo.ReservationDetail", new[] { "MaterialIdentifier_Id", "MaterialIdentifier_MaterialId" });
            AddForeignKey("dbo.ReservationDetail", new[] { "MaterialIdentifier_Id", "MaterialIdentifier_MaterialId" }, "dbo.MaterialIdentifier", new[] { "Id", "MaterialId" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationDetail", new[] { "MaterialIdentifier_Id", "MaterialIdentifier_MaterialId" }, "dbo.MaterialIdentifier");
            DropIndex("dbo.ReservationDetail", new[] { "MaterialIdentifier_Id", "MaterialIdentifier_MaterialId" });
            DropPrimaryKey("dbo.MaterialIdentifier");
            AlterColumn("dbo.MaterialIdentifier", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.ReservationDetail", "MaterialIdentifier_MaterialId");
            AddPrimaryKey("dbo.MaterialIdentifier", "Id");
            RenameIndex(table: "dbo.MaterialIdentifier", name: "IX_MaterialId", newName: "IX_Material_Id");
            RenameColumn(table: "dbo.MaterialIdentifier", name: "MaterialId", newName: "Material_Id");
            CreateIndex("dbo.ReservationDetail", "MaterialIdentifier_Id");
            AddForeignKey("dbo.ReservationDetail", "MaterialIdentifier_Id", "dbo.MaterialIdentifier", "Id", cascadeDelete: true);
        }
    }
}
