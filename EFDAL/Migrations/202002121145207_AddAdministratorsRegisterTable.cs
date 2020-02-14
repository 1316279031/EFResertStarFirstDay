namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdministratorsRegisterTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreateAdminitratorDetialDatas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdministratorAuthority = c.String(nullable: false),
                        IsFreeze = c.Boolean(nullable: false),
                        Email = c.String(nullable: false),
                        ValidateCode = c.String(),
                        Message = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AdministratorAccount",
                c => new
                    {
                        AdministratorAccount = c.String(nullable: false, maxLength: 128),
                        AdministratorPassword = c.String(nullable: false, maxLength: 15),
                        CreateAdminitratorDetialData_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdministratorAccount)
                .ForeignKey("dbo.CreateAdminitratorDetialDatas", t => t.CreateAdminitratorDetialData_ID)
                .Index(t => t.CreateAdminitratorDetialData_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdministratorAccount", "CreateAdminitratorDetialData_ID", "dbo.CreateAdminitratorDetialDatas");
            DropIndex("dbo.AdministratorAccount", new[] { "CreateAdminitratorDetialData_ID" });
            DropTable("dbo.AdministratorAccount");
            DropTable("dbo.CreateAdminitratorDetialDatas");
        }
    }
}
