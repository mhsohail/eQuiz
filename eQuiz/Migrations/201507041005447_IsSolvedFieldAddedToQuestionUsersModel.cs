namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsSolvedFieldAddedToQuestionUsersModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionUsers", "IsSolved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionUsers", "IsSolved");
        }
    }
}
