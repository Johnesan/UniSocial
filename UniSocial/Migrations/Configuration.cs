using System.Collections.Generic;
using UniSocial.Models;

namespace UniSocial.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UniSocial.Data.PostContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UniSocial.Data.PostContext db)
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


            var schools = new List<School>
            {
                new School {SchoolName = "Uniben"},
                new School { SchoolName = "Unilag" }
            };

            schools.ForEach(s => db.Schools.Add(s));
            db.SaveChanges();
           

            var blogWebsites = new List<BlogWebsite>
            {
                new BlogWebsite {SchoolID = db.Schools.Single(s => s.SchoolName == "Uniben").ID, Url = "djtech.com.ng"},
                new BlogWebsite {SchoolID = db.Schools.Single(s => s.SchoolName == "Unilag").ID, Url = "punchng.com"}
            };
            blogWebsites.ForEach(s => db.BlogWebsites.Add(s));
            db.SaveChanges();
            
        }
    }
}
