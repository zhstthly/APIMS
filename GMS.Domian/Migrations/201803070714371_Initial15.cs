namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial15 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClassProperties", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClassProperties", "Type", c => c.String());
        }
    }
}
