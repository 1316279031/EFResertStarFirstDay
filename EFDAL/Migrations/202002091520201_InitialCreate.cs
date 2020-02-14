namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                        Name = c.String(nullable: false),
                        Department = c.String(),
                        Class = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentDataConfigs", "StudentDetialDatas_ID", "dbo.StudentDetialDatas");
            DropIndex("dbo.StudentDataConfigs", new[] { "StudentDetialDatas_ID" });
            DropTable("dbo.StudentDetialDatas");
            DropTable("dbo.StudentDataConfigs");
        }
    }
}
