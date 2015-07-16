using eQuiz.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eQuiz.Models
{
    public class eQuizContext : IdentityDbContext<ApplicationUser>
    {
        public eQuizContext() : base("eQuizContext")
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<QuestionUser> QuestionUsers { get; set; }
        public DbSet<QuizInfo> QuizInfo { get; set; }
        public DbSet<IdentityUserLogin> UserLogins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<eQuizContext, Configuration>());
            base.OnModelCreating(modelBuilder);

            //  1-*  Video---Answer
            modelBuilder.Entity<Question>()
                .HasMany<Answer>(q => q.Answers)
                .WithRequired(a => a.Question)
                .HasForeignKey(a => a.QuestionId).WillCascadeOnDelete(true);
            
            //  *-*  ApplicationUser---Answer
            modelBuilder.Entity<Question>()
                   .HasMany<ApplicationUser>(q => q.Candidates)
                   .WithMany(au => au.SolvedQuestions)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("Question_QuestionId");
                       cs.MapRightKey("ApplicationUser_Id");
                       cs.ToTable("QuestionApplicationUsers");
                   });
            
        }

        public System.Data.Entity.DbSet<eQuiz.Models.Test> Tests { get; set; }
    }
}