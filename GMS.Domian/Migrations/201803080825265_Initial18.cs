namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerClasses", "IsCommon", c => c.Byte(nullable: false));
            DropColumn("dbo.CustomerClasses", "ClassTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerClasses", "ClassTypeID", c => c.Int(nullable: false));
            DropColumn("dbo.CustomerClasses", "IsCommon");
        }
    }
}
