namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionAppUserModelNameChangedToQuestionUser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuestionAppUsers", newName: "QuestionUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.QuestionUsers", newName: "QuestionAppUsers");
        }
    }
}
