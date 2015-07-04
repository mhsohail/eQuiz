namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionAppUsersTableCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionAppUsers",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        ApplicationUserId = c.Int(nullable: false),
                        Something = c.Int(nullable: false),
                        SomethingElse = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.ApplicationUserId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionAppUsers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionAppUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.QuestionAppUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.QuestionAppUsers", new[] { "QuestionId" });
            DropTable("dbo.QuestionAppUsers");
        }
    }
}
