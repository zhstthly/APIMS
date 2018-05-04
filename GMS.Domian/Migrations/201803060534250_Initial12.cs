namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassProperties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Length = c.Int(nullable: false),
                        DefaultValue = c.String(),
                        ModifyAccount = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        CustomerClass_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CustomerClasses", t => t.CustomerClass_ID)
                .Index(t => t.CustomerClass_ID);
            
            CreateTable(
                "dbo.CustomerClasses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModifyAccount = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassProperties", "CustomerClass_ID", "dbo.CustomerClasses");
            DropIndex("dbo.ClassProperties", new[] { "CustomerClass_ID" });
            DropTable("dbo.CustomerClasses");
            DropTable("dbo.ClassProperties");
        }
    }
}
