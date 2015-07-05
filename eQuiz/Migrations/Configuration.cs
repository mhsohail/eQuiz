namespace eQuiz.Migrations
{
    using eQuiz.Helpers;
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
            AutomaticMigrationsEnabled = false;
            ContextKey = "eQuiz.Models.eQuizContext";
            AutomaticMigrationDataLossAllowed = false;
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

            DbInitializer.Initialize();
        }
    }
}
