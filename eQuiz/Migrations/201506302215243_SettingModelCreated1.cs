namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SettingModelCreated1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "SettingName", c => c.String());
            AddColumn("dbo.Settings", "SettingValue", c => c.String());
            DropColumn("dbo.Settings", "Name");
            DropColumn("dbo.Settings", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "Value", c => c.String());
            AddColumn("dbo.Settings", "Name", c => c.String());
            DropColumn("dbo.Settings", "SettingValue");
            DropColumn("dbo.Settings", "SettingName");
        }
    }
}
