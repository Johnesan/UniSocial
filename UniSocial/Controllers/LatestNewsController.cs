using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniSocial.Data;
using UniSocial.Models;

namespace UniSocial.Controllers
{
    public class LatestNewsController : Controller
    {
        private PostContext db = new PostContext();

        // GET: LatestNews
        public async Task<ActionResult> Index()
        {
            var blogPosts = db.BlogPosts.Include(b => b.BlogWebsite);
            return View(await blogPosts.ToListAsync());
        }

        // GET: LatestNews/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = await db.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: LatestNews/Create
        public ActionResult Create()
        {
            ViewBag.BlogWebsiteId = new SelectList(db.BlogWebsites, "Id", "Name");
            return View();
        }

        // POST: LatestNews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Link,Title,Excerpt,Date,FeaturedImage,BlogWebsiteId")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.Id = Guid.NewGuid();
                db.BlogPosts.Add(blogPost);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BlogWebsiteId = new SelectList(db.BlogWebsites, "Id", "Name", blogPost.BlogWebsiteId);
            return View(blogPost);
        }

        // GET: LatestNews/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = await db.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogWebsiteId = new SelectList(db.BlogWebsites, "Id", "Name", blogPost.BlogWebsiteId);
            return View(blogPost);
        }

        // POST: LatestNews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Link,Title,Excerpt,Date,FeaturedImage,BlogWebsiteId")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogPost).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BlogWebsiteId = new SelectList(db.BlogWebsites, "Id", "Name", blogPost.BlogWebsiteId);
            return View(blogPost);
        }

        // GET: LatestNews/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = await db.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: LatestNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            BlogPost blogPost = await db.BlogPosts.FindAsync(id);
            db.BlogPosts.Remove(blogPost);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
