namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyAdministartorAccountTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdministratorAccount", "ValidateCodes_Id", "dbo.ValidateCodes");
            DropIndex("dbo.AdministratorAccount", new[] { "ValidateCodes_Id" });
            DropColumn("dbo.AdministratorAccount", "ValidateCodes_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdministratorAccount", "ValidateCodes_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AdministratorAccount", "ValidateCodes_Id");
            AddForeignKey("dbo.AdministratorAccount", "ValidateCodes_Id", "dbo.ValidateCodes", "Id");
        }
    }
}
