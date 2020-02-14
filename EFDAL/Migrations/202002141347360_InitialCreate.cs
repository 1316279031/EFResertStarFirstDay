namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        SchoolAdministrators_AdministratorAccount = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AdministratorAccount", t => t.SchoolAdministrators_AdministratorAccount, cascadeDelete: true)
                .Index(t => t.SchoolAdministrators_AdministratorAccount);
            
            CreateTable(
                "dbo.AdministratorAccount",
                c => new
                    {
                        AdministratorAccount = c.String(nullable: false, maxLength: 128),
                        AdministratorPassword = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.AdministratorAccount);
            
            CreateTable(
                "dbo.StudentDataConfigs",
                c => new
                    {
                        IdCard = c.String(nullable: false, maxLength: 128),
                        Address = c.String(nullable: false),
                        Telephone = c.String(nullable: false, maxLength: 11),
                        PareventTelephone = c.String(nullable: false, maxLength: 11),
                        StudentDetialDatas_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCard)
                .ForeignKey("dbo.StudentDetialDatas", t => t.StudentDetialDatas_ID, cascadeDelete: true)
                .Index(t => t.StudentDetialDatas_ID);
            
            CreateTable(
                "dbo.StudentDetialDatas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Department = c.String(),
                        Class = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentDataConfigs", "StudentDetialDatas_ID", "dbo.StudentDetialDatas");
            DropForeignKey("dbo.CreateAdminitratorDetialDatas", "SchoolAdministrators_AdministratorAccount", "dbo.AdministratorAccount");
            DropIndex("dbo.StudentDataConfigs", new[] { "StudentDetialDatas_ID" });
            DropIndex("dbo.CreateAdminitratorDetialDatas", new[] { "SchoolAdministrators_AdministratorAccount" });
            DropTable("dbo.StudentDetialDatas");
            DropTable("dbo.StudentDataConfigs");
            DropTable("dbo.AdministratorAccount");
            DropTable("dbo.CreateAdminitratorDetialDatas");
        }
    }
}
