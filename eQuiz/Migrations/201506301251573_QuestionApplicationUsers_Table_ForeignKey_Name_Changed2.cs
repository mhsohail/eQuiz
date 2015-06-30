namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionApplicationUsers_Table_ForeignKey_Name_Changed2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.QuestionApplicationUsers", name: "QuestionId", newName: "Question_QuestionId");
            RenameColumn(table: "dbo.QuestionApplicationUsers", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.QuestionApplicationUsers", name: "IX_QuestionId", newName: "IX_Question_QuestionId");
            RenameIndex(table: "dbo.QuestionApplicationUsers", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.QuestionApplicationUsers", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameIndex(table: "dbo.QuestionApplicationUsers", name: "IX_Question_QuestionId", newName: "IX_QuestionId");
            RenameColumn(table: "dbo.QuestionApplicationUsers", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameColumn(table: "dbo.QuestionApplicationUsers", name: "Question_QuestionId", newName: "QuestionId");
        }
    }
}
