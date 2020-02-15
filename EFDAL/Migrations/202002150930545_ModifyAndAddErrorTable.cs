namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyAndAddErrorTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ErrorDatabases",
                c => new
                    {
                        ErrorID = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        ErrorMessage = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ErrorID);
            
            AlterColumn("dbo.AdministratorAccount", "AdministratorPassword", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdministratorAccount", "AdministratorPassword", c => c.String(nullable: false, maxLength: 15));
            DropTable("dbo.ErrorDatabases");
        }
    }
}
