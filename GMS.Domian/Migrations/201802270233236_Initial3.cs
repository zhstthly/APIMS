namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.APIItems", "Environment", c => c.String());
            DropColumn("dbo.APIItems", "Invironment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.APIItems", "Invironment", c => c.String());
            DropColumn("dbo.APIItems", "Environment");
        }
    }
}
