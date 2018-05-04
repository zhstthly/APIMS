namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerClasses", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerClasses", "Type");
        }
    }
}
