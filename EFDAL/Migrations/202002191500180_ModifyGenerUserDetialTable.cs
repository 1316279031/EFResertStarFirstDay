namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyGenerUserDetialTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GenerUserDetials", "Email", "dbo.GenerUsers");
            DropIndex("dbo.GenerUserDetials", new[] { "Email" });
            DropPrimaryKey("dbo.GenerUserDetials");
            AddColumn("dbo.GenerUserDetials", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.GenerUserDetials", "Users_User", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.GenerUserDetials", "Email", c => c.String());
            AddPrimaryKey("dbo.GenerUserDetials", "ID");
            CreateIndex("dbo.GenerUserDetials", "Users_User");
            AddForeignKey("dbo.GenerUserDetials", "Users_User", "dbo.GenerUsers", "User", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GenerUserDetials", "Users_User", "dbo.GenerUsers");
            DropIndex("dbo.GenerUserDetials", new[] { "Users_User" });
            DropPrimaryKey("dbo.GenerUserDetials");
            AlterColumn("dbo.GenerUserDetials", "Email", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.GenerUserDetials", "Users_User");
            DropColumn("dbo.GenerUserDetials", "ID");
            AddPrimaryKey("dbo.GenerUserDetials", "Email");
            CreateIndex("dbo.GenerUserDetials", "Email");
            AddForeignKey("dbo.GenerUserDetials", "Email", "dbo.GenerUsers", "User");
        }
    }
}
