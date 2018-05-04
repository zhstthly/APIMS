namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InputParameters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        Name = c.String(),
                        Type = c.String(),
                        IsNeed = c.Boolean(nullable: false),
                        ModifyAccount = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        Name = c.String(),
                        Type = c.String(),
                        ModifyAccount = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Results");
            DropTable("dbo.InputParameters");
        }
    }
}
