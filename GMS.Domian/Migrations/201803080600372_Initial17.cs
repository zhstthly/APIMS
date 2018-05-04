namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InputParameters", "ClassTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.Results", "ClassTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.ClassProperties", "ClassTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerClasses", "ClassTypeID", c => c.Int(nullable: false));
            DropColumn("dbo.InputParameters", "Type");
            DropColumn("dbo.Results", "Type");
            DropColumn("dbo.ClassProperties", "Type");
            DropColumn("dbo.CustomerClasses", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerClasses", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.ClassProperties", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Results", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.InputParameters", "Type", c => c.String());
            DropColumn("dbo.CustomerClasses", "ClassTypeID");
            DropColumn("dbo.ClassProperties", "ClassTypeID");
            DropColumn("dbo.Results", "ClassTypeID");
            DropColumn("dbo.InputParameters", "ClassTypeID");
        }
    }
}
