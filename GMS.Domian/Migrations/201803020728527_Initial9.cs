namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.APIItems", "Result_ID", c => c.Int());
            AddColumn("dbo.InputParameters", "APIItem_ID", c => c.Int());
            CreateIndex("dbo.APIItems", "Result_ID");
            CreateIndex("dbo.InputParameters", "APIItem_ID");
            AddForeignKey("dbo.InputParameters", "APIItem_ID", "dbo.APIItems", "ID");
            AddForeignKey("dbo.APIItems", "Result_ID", "dbo.Results", "ID");
            DropColumn("dbo.InputParameters", "ItemID");
            DropColumn("dbo.Results", "ItemID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Results", "ItemID", c => c.Int(nullable: false));
            AddColumn("dbo.InputParameters", "ItemID", c => c.Int(nullable: false));
            DropForeignKey("dbo.APIItems", "Result_ID", "dbo.Results");
            DropForeignKey("dbo.InputParameters", "APIItem_ID", "dbo.APIItems");
            DropIndex("dbo.InputParameters", new[] { "APIItem_ID" });
            DropIndex("dbo.APIItems", new[] { "Result_ID" });
            DropColumn("dbo.InputParameters", "APIItem_ID");
            DropColumn("dbo.APIItems", "Result_ID");
        }
    }
}
