using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SSU.Reception.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.EnrolleeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "SSU.Reception.Models.EnrolleeContext";
        }

        protected override void Seed(Models.EnrolleeContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
