namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_LoginTime_Field_to_LoginTimeUtc : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.AspNetUsers", "LoginTime", "LoginTimeUtc");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.AspNetUsers", "LoginTimeUtc", "LoginTime");
        }
    }
}
