namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentDetialDatas", "Department", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentDetialDatas", "Department", c => c.String());
        }
    }
}
