using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace UniSocial.Models
{
    public class School
    {
        
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public string About { get; set; }
    }
}