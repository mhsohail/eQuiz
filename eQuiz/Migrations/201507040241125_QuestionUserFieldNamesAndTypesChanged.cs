namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionUserFieldNamesAndTypesChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionUsers", "QuizStartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuestionUsers", "QuizEndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.QuestionUsers", "Something");
            DropColumn("dbo.QuestionUsers", "SomethingElse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionUsers", "SomethingElse", c => c.String());
            AddColumn("dbo.QuestionUsers", "Something", c => c.Int(nullable: false));
            DropColumn("dbo.QuestionUsers", "QuizEndTime");
            DropColumn("dbo.QuestionUsers", "QuizStartTime");
        }
    }
}
