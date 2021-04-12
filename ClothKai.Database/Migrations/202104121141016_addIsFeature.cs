namespace ClothKai.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsFeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "isFeature", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "isFeature");
        }
    }
}
