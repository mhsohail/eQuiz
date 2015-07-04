namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionUserFieldDataTypeModified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionUsers", "Candidate_Id", "dbo.AspNetUsers");
            DropIndex("dbo.QuestionUsers", new[] { "Candidate_Id" });
            //DropColumn("dbo.QuestionUsers", "ApplicationUserId");
            //RenameColumn(table: "dbo.QuestionUsers", name: "Candidate_Id", newName: "ApplicationUserId");
            DropPrimaryKey("dbo.QuestionUsers");
            AlterColumn("dbo.QuestionUsers", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.QuestionUsers", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.QuestionUsers", new[] { "QuestionId", "ApplicationUserId" });
            CreateIndex("dbo.QuestionUsers", "ApplicationUserId");
            AddForeignKey("dbo.QuestionUsers", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionUsers", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.QuestionUsers", new[] { "ApplicationUserId" });
            DropPrimaryKey("dbo.QuestionUsers");
            AlterColumn("dbo.QuestionUsers", "ApplicationUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.QuestionUsers", "ApplicationUserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.QuestionUsers", new[] { "QuestionId", "ApplicationUserId" });
            RenameColumn(table: "dbo.QuestionUsers", name: "ApplicationUserId", newName: "Candidate_Id");
            AddColumn("dbo.QuestionUsers", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionUsers", "Candidate_Id");
            AddForeignKey("dbo.QuestionUsers", "Candidate_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
