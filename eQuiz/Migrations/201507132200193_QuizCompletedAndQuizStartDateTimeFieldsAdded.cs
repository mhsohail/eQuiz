namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizCompletedAndQuizStartDateTimeFieldsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "QuizStartDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "HasCompletedQuiz", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "HasCompletedQuiz");
            DropColumn("dbo.AspNetUsers", "QuizStartDateTime");
        }
    }
}
