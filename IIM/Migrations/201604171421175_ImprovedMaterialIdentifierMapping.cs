namespace IIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImprovedMaterialIdentifierMapping : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ReservationDetail", name: "MaterialIdentifierId", newName: "MaterialIdentifier_Id");
            RenameIndex(table: "dbo.ReservationDetail", name: "IX_MaterialIdentifierId", newName: "IX_MaterialIdentifier_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ReservationDetail", name: "IX_MaterialIdentifier_Id", newName: "IX_MaterialIdentifierId");
            RenameColumn(table: "dbo.ReservationDetail", name: "MaterialIdentifier_Id", newName: "MaterialIdentifierId");
        }
    }
}
