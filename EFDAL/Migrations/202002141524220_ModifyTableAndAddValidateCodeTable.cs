namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTableAndAddValidateCodeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ValidateCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValidateCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AdministratorAccount", "ValidateCodes_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.CreateAdminitratorDetialDatas", "Email", c => c.String(nullable: false, maxLength: 40));
            CreateIndex("dbo.CreateAdminitratorDetialDatas", "Email", unique: true);
            CreateIndex("dbo.AdministratorAccount", "ValidateCodes_Id");
            AddForeignKey("dbo.AdministratorAccount", "ValidateCodes_Id", "dbo.ValidateCodes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdministratorAccount", "ValidateCodes_Id", "dbo.ValidateCodes");
            DropIndex("dbo.AdministratorAccount", new[] { "ValidateCodes_Id" });
            DropIndex("dbo.CreateAdminitratorDetialDatas", new[] { "Email" });
            AlterColumn("dbo.CreateAdminitratorDetialDatas", "Email", c => c.String(nullable: false));
            DropColumn("dbo.AdministratorAccount", "ValidateCodes_Id");
            DropTable("dbo.ValidateCodes");
        }
    }
}
