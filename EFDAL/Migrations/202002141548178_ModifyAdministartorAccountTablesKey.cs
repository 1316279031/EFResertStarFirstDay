namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyAdministartorAccountTablesKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ValidateCodes", "SchoolAdministrators_AdministratorAccount", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ValidateCodes", "SchoolAdministrators_AdministratorAccount");
            AddForeignKey("dbo.ValidateCodes", "SchoolAdministrators_AdministratorAccount", "dbo.AdministratorAccount", "AdministratorAccount", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValidateCodes", "SchoolAdministrators_AdministratorAccount", "dbo.AdministratorAccount");
            DropIndex("dbo.ValidateCodes", new[] { "SchoolAdministrators_AdministratorAccount" });
            DropColumn("dbo.ValidateCodes", "SchoolAdministrators_AdministratorAccount");
        }
    }
}
