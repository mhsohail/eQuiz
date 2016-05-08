namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_LoginTime_Field_in_User_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LoginTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LoginTime");
        }
    }
}
