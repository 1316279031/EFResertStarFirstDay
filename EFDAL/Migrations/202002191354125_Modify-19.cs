namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify19 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentDetialDatas", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.StudentDetialDatas", "Department", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentDetialDatas", "Department", c => c.String());
            AlterColumn("dbo.StudentDetialDatas", "Name", c => c.String());
        }
    }
}
