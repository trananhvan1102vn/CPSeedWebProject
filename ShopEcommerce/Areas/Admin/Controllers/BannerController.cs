using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CPSeed.Models;
using Microsoft.AspNet.Identity;

namespace CPSeed.Areas.Admin.Controllers {
    public class BannerController : AdminBaseController {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Banner
        public async Task<ActionResult> Index() {
            return View(await db.Banners
                .Include("UserCreated")
                .Include("UserModified")
                .ToListAsync());
        }


        // GET: Admin/Banner/Details/5
        public async Task<ActionResult> Details(Guid? id) {
            ViewBag.BannerCategories = new SelectList(db.BannerCategories.ToList(), "Id", "Name");
            if (id == null) {
                return View(new Banner());
            }
            Banner Banner = await db.Banners.FindAsync(id);
            if (Banner == null) {
                return HttpNotFound();
            }
            return View(Banner);
        }

        // POST: Admin/Banner/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Details([Bind(Include = "Id,BannerCategoryId,Name,Link,Image,OrderNo")] Banner model) {
            if (ModelState.IsValid) {
                if (model.Id != new Guid()) {
                    var Banner = await db.Banners.SingleOrDefaultAsync(x => x.Id == model.Id);

                    Banner.BannerCategoryId = model.BannerCategoryId;
                    Banner.Name = model.Name;
                    Banner.Link = model.Link;
                    Banner.Image = model.Image;
                    Banner.OrderNo = model.OrderNo;

                    Banner.ModifiedBy = User.Identity.GetUserId();
                    Banner.ModifiedDate = DateTime.Now;

                    db.Entry(Banner).State = EntityState.Modified;
                } else {
                    var Banner = new Banner();

                    Banner.Id = Guid.NewGuid();
                    Banner.BannerCategoryId = model.BannerCategoryId;
                    Banner.Name = model.Name;
                    Banner.Link = model.Link;
                    Banner.Image = model.Image;
                    Banner.OrderNo = model.OrderNo;

                    Banner.CreatedBy = User.Identity.GetUserId();
                    Banner.CreatedDate = DateTime.Now;
                    db.Banners.Add(Banner);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Admin/Banner/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid deletedItemId) {
            Banner Banner = await db.Banners.FindAsync(deletedItemId);
            db.Banners.Remove(Banner);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
