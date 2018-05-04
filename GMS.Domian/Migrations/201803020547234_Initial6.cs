namespace GMS.Domian.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.APIItems", "Demo", c => c.String());
            AlterColumn("dbo.APIItems", "Visiable", c => c.Boolean(nullable: false));
            DropTable("dbo.APIItemExtends");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.APIItemExtends",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        InputParameters = c.String(),
                        OutputParameters = c.String(),
                        Result = c.String(),
                        Environment = c.String(),
                        Version = c.String(),
                        Demo = c.String(),
                        ModifyAccount = c.String(),
                        ModifyTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.APIItems", "Visiable", c => c.Byte(nullable: false));
            DropColumn("dbo.APIItems", "Demo");
        }
    }
}
