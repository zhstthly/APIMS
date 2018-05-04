namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial16 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Results", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Results", "Type", c => c.String());
        }
    }
}
