namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.APIItems", "Visiable", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.APIItems", "Visiable", c => c.Boolean(nullable: false));
        }
    }
}
