using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace eQuiz.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        //[Index(IsUnique=true)]
        //[MaxLength(450)]
        public string MacAddress { get; set; }

        //[Index(IsUnique = true)]
        //[MaxLength(450)]
        public string IpAddress { get; set; }
        
        public virtual ICollection<Question> SolvedQuestions { get; set; }
        public virtual ICollection<QuestionUser> QuestionUsers { get; set; }
        public virtual QuizInfo QuizInfo { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public bool HasSolvedQuestion(Question Question)
        {
            foreach (var SolvedQuestion in this.SolvedQuestions)
            {
                if (SolvedQuestion.QuestionId == Question.QuestionId) { return true; }            
            }
            
            return false;
        }

        public bool IsAheadOfNextUnsolvedQuestion(Question Question)
        {
            eQuizContext db = new eQuizContext();
            var UnsolvedQuestions = this.GetUnsolvedQuestions();

            foreach (Question UnsolvedQuestion in UnsolvedQuestions)
            {
                if (Question.QuestionId > UnsolvedQuestion.QuestionId) { return true; } return false;
            }
            return false;
        }

        public ICollection<Question> GetUnsolvedQuestions()
        { 
            eQuizContext db = new eQuizContext();
            var Questions = db.Questions;
            var UnsolvedQuestions = new List<Question>();

            foreach (var Question in Questions)
            {
                if (!this.HasSolvedQuestion(Question))
                {
                    UnsolvedQuestions.Add(Question);
                }
            }

            return UnsolvedQuestions;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("eQuizContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}