namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityIdUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionApplicationUsers", "QuestionId", "dbo.Questions");
            DropPrimaryKey("dbo.Answers");
            DropPrimaryKey("dbo.Questions");
            //AddColumn("dbo.Answers", "AnswerId", c => c.Int(nullable: false, identity: true));
            //AddColumn("dbo.Questions", "QuestionId", c => c.Int(nullable: false, identity: true));
            RenameColumn("dbo.Answers", "Id", "AnswerId");
            RenameColumn("dbo.Questions", "Id", "QuestionId");
            AddPrimaryKey("dbo.Answers", "AnswerId");
            AddPrimaryKey("dbo.Questions", "QuestionId");
            AddForeignKey("dbo.Answers", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: true);
            AddForeignKey("dbo.QuestionApplicationUsers", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: true);
            //DropColumn("dbo.Answers", "Id");
            //DropColumn("dbo.Questions", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Answers", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.QuestionApplicationUsers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropPrimaryKey("dbo.Questions");
            DropPrimaryKey("dbo.Answers");
            DropColumn("dbo.Questions", "QuestionId");
            DropColumn("dbo.Answers", "AnswerId");
            AddPrimaryKey("dbo.Questions", "Id");
            AddPrimaryKey("dbo.Answers", "Id");
            AddForeignKey("dbo.QuestionApplicationUsers", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Answers", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
        }
    }
}
