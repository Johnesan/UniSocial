using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using UniSocial.Data;
using UniSocial.Models;

namespace UniSocial.Controllers
{
    public class BlogWebsitesController : Controller
    {
        private PostContext db = new PostContext();

        // GET: BlogWebsites
        public async Task<ActionResult> Index()
        {
            var blogWebsites = db.BlogWebsites.Include(b => b.School);
            return View(await blogWebsites.ToListAsync());
        }

        // GET: BlogWebsites/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogWebsite blogWebsite = await db.BlogWebsites.FindAsync(id);
            if (blogWebsite == null)
            {
                return HttpNotFound();
            }
            return View(blogWebsite);
        }

        // GET: BlogWebsites/Create
        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(db.Schools, "ID", "Name");
            return View();
        }

        // POST: BlogWebsites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Name,Url,Logo,SchoolID")] BlogWebsite blogWebsite)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        blogWebsite.Id = Guid.NewGuid();
        //        db.BlogWebsites.Add(blogWebsite);
        //        //string imagePath = blogWebsite.Logo.SaveAs(Server.MapPath("~/Files" + blogWebsite.Name + "jpg"));
        //        //blogWebsite.Logo = imagePath; 
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.SchoolID = new SelectList(db.Schools, "ID", "Name", blogWebsite.SchoolID);
        //    return View(blogWebsite);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BlogWebsite blogWebsite)
        {
            try
            {
                var blogWebsite1 = new BlogWebsite
                {
                    Id = Guid.NewGuid(),
                    Name = blogWebsite.Name,
                    Url = blogWebsite.Url,
                    Logo = blogWebsite.Logo,
                    SchoolID = blogWebsite.SchoolID
                };
                db.BlogWebsites.Add(blogWebsite1);
                db.SaveChanges();
            }

            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to save");
                return View();

            }
            return RedirectToAction("Index");
        }

        // GET: BlogWebsites/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogWebsite blogWebsite = await db.BlogWebsites.FindAsync(id);
            if (blogWebsite == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolID = new SelectList(db.Schools, "ID", "Name", blogWebsite.SchoolID);
            return View(blogWebsite);
        }

        // POST: BlogWebsites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Url,Logo,SchoolID")] BlogWebsite blogWebsite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogWebsite).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolID = new SelectList(db.Schools, "ID", "Name", blogWebsite.SchoolID);
            return View(blogWebsite);
        }

        // GET: BlogWebsites/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogWebsite blogWebsite = await db.BlogWebsites.FindAsync(id);
            if (blogWebsite == null)
            {
                return HttpNotFound();
            }
            return View(blogWebsite);
        }

        // POST: BlogWebsites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            BlogWebsite blogWebsite = await db.BlogWebsites.FindAsync(id);
            db.BlogWebsites.Remove(blogWebsite);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult PostImage()
        {
            System.Threading.Thread.Sleep(3000);
            string _imgname = string.Empty;
            string realImageUrl = "";
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var _comPath = Server.MapPath("~/BlogImage/") + _imgname + _ext;
                    _imgname = _imgname + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    pic.SaveAs(path);
                    realImageUrl = "~/BlogImage/" + Convert.ToString(_imgname);
                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 300)
                    {
                        img.Resize(300, 250);
                        img.Save(_comPath);
                        realImageUrl = "~/BlogImage/" + Convert.ToString(_imgname);
                    }
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);

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
