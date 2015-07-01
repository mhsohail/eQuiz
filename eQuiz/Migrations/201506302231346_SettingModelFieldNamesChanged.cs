namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SettingModelFieldNamesChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "Name", c => c.String());
            AddColumn("dbo.Settings", "Value", c => c.String());
            DropColumn("dbo.Settings", "SettingName");
            DropColumn("dbo.Settings", "SettingValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "SettingValue", c => c.String());
            AddColumn("dbo.Settings", "SettingName", c => c.String());
            DropColumn("dbo.Settings", "Value");
            DropColumn("dbo.Settings", "Name");
        }
    }
}
