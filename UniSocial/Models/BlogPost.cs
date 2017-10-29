using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniSocial.Models
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public DateTime Date { get; set; }
        public string FeaturedImage { get; set; }
        public Guid BlogWebsiteId { get; set; }
        public virtual BlogWebsite BlogWebsite { get; set; }
    }
}