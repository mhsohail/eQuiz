namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionApplicationUsers_Table_ForeignKey_Name_Changed : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.QuestionApplicationUsers", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.QuestionApplicationUsers", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.QuestionApplicationUsers", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.QuestionApplicationUsers", name: "ApplicationUserId", newName: "ApplicationUser_Id");
        }
    }
}
