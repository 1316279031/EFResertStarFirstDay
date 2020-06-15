namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LibData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LibrayManagent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Author = c.String(nullable: false),
                        DataAdded = c.DateTime(nullable: false),
                        PublishingHouse = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LibrayManagent");
        }
    }
}
