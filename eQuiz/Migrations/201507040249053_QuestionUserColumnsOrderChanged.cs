namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionUserColumnsOrderChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionUsers", "IsCorrect", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionUsers", "IsCorrect");
        }
    }
}
