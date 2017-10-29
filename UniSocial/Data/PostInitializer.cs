using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniSocial.Models;

namespace UniSocial.Data
{
    public class PostInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PostContext>
    {

        protected override void Seed(PostContext db)
        {
            var schools = new List<School>
            {
                new School {ID = Guid.NewGuid(), Name = "Uniben"},
                new School {ID = Guid.NewGuid(), Name = "Unilag" }
            };

            schools.ForEach(s => db.Schools.Add(s));
            db.SaveChanges();

            var blogWebsites = new List<BlogWebsite>
            {
                new BlogWebsite {Id = Guid.NewGuid() , SchoolID = db.Schools.Single(s => s.Name == "Uniben").ID, Url = "http://djtech.com.ng"},
                new BlogWebsite {Id = Guid.NewGuid(), SchoolID = db.Schools.Single(s => s.Name == "Unilag").ID, Url = "http://punchng.com"}
            };
            blogWebsites.ForEach(s => db.BlogWebsites.Add(s));
            db.SaveChanges();
        }
    }
}