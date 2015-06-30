namespace eQuiz.Migrations
{
    using eQuiz.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<eQuiz.Models.eQuizContext>
    {
        eQuizContext db = new eQuizContext();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "eQuiz.Models.eQuizContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(eQuiz.Models.eQuizContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            
            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // question 1
                    var q1 = new Question();
                    q1.Text = "Which Helper Method Returns binary output to write to the response?";
                    db.Questions.Add(q1);
                    db.SaveChanges();

                    var a1 = new Answer();
                    a1.Text = "Content";
                    a1.IsCorrect = true;
                    a1.QuestionId = q1.QuestionId;
                    db.Answers.Add(a1);

                    var a2 = new Answer();
                    a2.Text = "File";
                    a2.IsCorrect = false;
                    a2.QuestionId = q1.QuestionId;
                    db.Answers.Add(a2);

                    var a3 = new Answer();
                    a3.Text = "JavaScript";
                    a3.IsCorrect = false;
                    a3.QuestionId = q1.QuestionId;
                    db.Answers.Add(a3);

                    var a4 = new Answer();
                    a4.Text = "Json";
                    a4.IsCorrect = false;
                    a4.QuestionId = q1.QuestionId;
                    db.Answers.Add(a4);

                    db.SaveChanges();

                    // question 2
                    var q2 = new Question();
                    q2.Text = "Which Action Result Renders a partial view, which defines a section of a view that can be rendered inside another view?";
                    db.Questions.Add(q2);
                    db.SaveChanges();

                    var q2a1 = new Answer();
                    q2a1.Text = "ContentResult";
                    q2a1.IsCorrect = false;
                    q2a1.QuestionId = q2.QuestionId;
                    db.Answers.Add(q2a1);

                    var q2a2 = new Answer();
                    q2a2.Text = "RedirectResult";
                    q2a2.IsCorrect = true;
                    q2a2.QuestionId = q2.QuestionId;
                    db.Answers.Add(q2a2);

                    var q2a3 = new Answer();
                    q2a3.Text = "PartialViewResult";
                    q2a3.IsCorrect = false;
                    q2a3.QuestionId = q2.QuestionId;
                    db.Answers.Add(q2a3);

                    var q2a4 = new Answer();
                    q2a4.Text = "None of above.";
                    q2a4.IsCorrect = false;
                    q2a4.QuestionId = q2.QuestionId;
                    db.Answers.Add(q2a4);

                    db.SaveChanges();

                    // question 3
                    var q3 = new Question();
                    q3.Text = "The Controller class is responsible for the following processing stages:";
                    db.Questions.Add(q3);
                    db.SaveChanges();

                    var q3a1 = new Answer();
                    q3a1.Text = "Locating the appropriate action method to call and validating that it can be called";
                    q3a1.IsCorrect = false;
                    q3a1.QuestionId = q3.QuestionId;
                    db.Answers.Add(q3a1);

                    var q3a2 = new Answer();
                    q3a2.Text = "Getting the values to use as the action method's arguments";
                    q3a2.IsCorrect = false;
                    q3a2.QuestionId = q3.QuestionId;
                    db.Answers.Add(q3a2);

                    var q3a3 = new Answer();
                    q3a3.Text = "Handling all errors that might occur during the execution of the action method";
                    q3a3.IsCorrect = false;
                    q3a3.QuestionId = q3.QuestionId;
                    db.Answers.Add(q3a3);

                    var q3a4 = new Answer();
                    q3a4.Text = "All of the above.";
                    q3a4.IsCorrect = true;
                    q3a4.QuestionId = q3.QuestionId;
                    db.Answers.Add(q3a4);

                    db.SaveChanges();

                    //this is for creating user
                    var userStore = new UserStore<ApplicationUser>(db);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    if (!(db.Users.Any(u => u.UserName == "sohailx2x@yahoo.com")))
                    {
                        var userToInsert = new ApplicationUser { UserName = "sohailx2x@yahoo.com", Email = "sohailx2x@yahoo.com", PhoneNumber = "03035332033", LockoutEnabled = true };
                        userManager.Create(userToInsert, "Sohail@2");
                    }

                    //this is for creating role
                    RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                    var role = new IdentityRole("Administrator");
                    RoleManager.Create(role);

                    //this is for assigning role that is created above
                    ApplicationUser user = userManager.FindByNameAsync("sohailx2x@yahoo.com").Result;
                    userManager.AddToRole(user.Id, role.Name);

                    Transaction.Commit();
                }
                catch(Exception exc)
                {
                    Transaction.Rollback();
                }
            }
            
        }
    }
}
