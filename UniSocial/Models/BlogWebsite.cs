using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniSocial.Models
{
    public class BlogWebsite
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Logo { get; set; }
        public Guid SchoolID { get; set; }
        public virtual School School { get; set; }
    }
}