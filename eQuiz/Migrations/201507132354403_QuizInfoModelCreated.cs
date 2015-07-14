namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizInfoModelCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuizInfoes",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        QuizStartDateTime = c.DateTime(nullable: false),
                        HasCompletedQuiz = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            DropColumn("dbo.AspNetUsers", "QuizStartDateTime");
            DropColumn("dbo.AspNetUsers", "HasCompletedQuiz");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "HasCompletedQuiz", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "QuizStartDateTime", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.QuizInfoes", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.QuizInfoes", new[] { "ApplicationUserId" });
            DropTable("dbo.QuizInfoes");
        }
    }
}
