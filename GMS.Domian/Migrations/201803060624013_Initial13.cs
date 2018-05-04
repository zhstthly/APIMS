namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerClasses", "ClassificationID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerClasses", "ClassificationID");
        }
    }
}
