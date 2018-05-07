namespace NotificationApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Notifications", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}
