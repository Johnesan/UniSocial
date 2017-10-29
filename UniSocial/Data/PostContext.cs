using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UniSocial.Models;

namespace UniSocial.Data
{
    public class PostContext : DbContext
    {
        public PostContext() : base("PostContext")
        {
           
            Database.SetInitializer(new PostInitializer());
            
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<BlogWebsite> BlogWebsites { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }

    }
}
