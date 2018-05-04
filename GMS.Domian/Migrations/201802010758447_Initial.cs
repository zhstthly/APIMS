namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.APIClassifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BelongSystem = c.String(),
                        Visiable = c.Boolean(nullable: false),
                        ModifyAccount = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.APIItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassificationID = c.Int(nullable: false),
                        Name = c.String(),
                        TypeID = c.Int(nullable: false),
                        InputParameters = c.String(),
                        OutputParameters = c.String(),
                        Result = c.String(),
                        Demo = c.String(),
                        Version = c.String(),
                        Visiable = c.Boolean(nullable: false),
                        ModifyAccount = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.APITypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Int(nullable: false),
                        Color = c.String(),
                        ModifyAccount = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.APITypes");
            DropTable("dbo.APIItems");
            DropTable("dbo.APIClassifications");
        }
    }
}
