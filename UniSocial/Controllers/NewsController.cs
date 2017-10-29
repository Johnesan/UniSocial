using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UniSocial.Data;
using UniSocial.Models;

namespace UniSocial.Controllers
{
    public class NewsController : Controller
    {
        private PostContext db = new PostContext();

        // GET: News
        public async Task<ActionResult> Index()
        {
            var blogPosts = db.BlogPosts.Include(b => b.BlogWebsite);
            return View( blogPosts.ToList().OrderByDescending(s => s.Date));
              
        }

     

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
