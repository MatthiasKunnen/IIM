namespace IIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class TypeMigrationOfApplicationUserTypeProperty : DbMigration
    {
        public override void Up()
        {
            Sql($@"UPDATE dbo.""User"" SET ""Type"" = CASE
                WHEN Type = 'personeel' THEN '{(int)Models.Domain.Type.Staff}'
                WHEN Type = 'student' THEN '{(int)Models.Domain.Type.Student}'
                END
                WHERE ""Type"" IN('personeel', 'student')");
            AlterColumn("dbo.User", "Type", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.User", "Type", c => c.String(nullable: false));
            Sql(
                $@"UPDATE dbo.""User"" SET ""Type"" = CASE
                WHEN Type = '{(int)Models.Domain.Type.Staff}' THEN 'personeel'
                WHEN Type = '{(int)Models.Domain.Type.Student}' THEN 'student'
                END");
        }
    }
}
