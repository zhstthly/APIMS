namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.APIItemExtends",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        InputParameters = c.String(),
                        OutputParameters = c.String(),
                        Result = c.String(),
                        Environment = c.String(),
                        Version = c.String(),
                        Demo = c.String(),
                        ModifyAccount = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.APIItems", "InputParameters");
            DropColumn("dbo.APIItems", "OutputParameters");
            DropColumn("dbo.APIItems", "Result");
            DropColumn("dbo.APIItems", "Demo");
            DropColumn("dbo.APIItems", "Environment");
            DropColumn("dbo.APIItems", "Version");
        }
        
        public override void Down()
        {
            AddColumn("dbo.APIItems", "Version", c => c.String());
            AddColumn("dbo.APIItems", "Environment", c => c.String());
            AddColumn("dbo.APIItems", "Demo", c => c.String());
            AddColumn("dbo.APIItems", "Result", c => c.String());
            AddColumn("dbo.APIItems", "OutputParameters", c => c.String());
            AddColumn("dbo.APIItems", "InputParameters", c => c.String());
            DropTable("dbo.APIItemExtends");
        }
    }
}
