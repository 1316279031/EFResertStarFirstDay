namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenerUserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GenerUserDetials",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        TelePhone = c.String(),
                    })
                .PrimaryKey(t => t.Email)
                .ForeignKey("dbo.GenerUsers", t => t.Email)
                .Index(t => t.Email);
            
            CreateTable(
                "dbo.GenerUsers",
                c => new
                    {
                        User = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.User);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GenerUserDetials", "Email", "dbo.GenerUsers");
            DropIndex("dbo.GenerUserDetials", new[] { "Email" });
            DropTable("dbo.GenerUsers");
            DropTable("dbo.GenerUserDetials");
        }
    }
}
