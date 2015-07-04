namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldNameUpdated : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.QuestionAppUsers", name: "ApplicationUser_Id", newName: "Candidate_Id");
            RenameIndex(table: "dbo.QuestionAppUsers", name: "IX_ApplicationUser_Id", newName: "IX_Candidate_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.QuestionAppUsers", name: "IX_Candidate_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.QuestionAppUsers", name: "Candidate_Id", newName: "ApplicationUser_Id");
        }
    }
}
