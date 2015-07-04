namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionUserFielNameChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionUsers", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuestionUsers", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.QuestionUsers", "QuizStartTime");
            DropColumn("dbo.QuestionUsers", "QuizEndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionUsers", "QuizEndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuestionUsers", "QuizStartTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.QuestionUsers", "EndTime");
            DropColumn("dbo.QuestionUsers", "StartTime");
        }
    }
}
