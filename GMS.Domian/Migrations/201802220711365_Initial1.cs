namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.APITypes", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.APITypes", "Value", c => c.Int(nullable: false));
        }
    }
}
