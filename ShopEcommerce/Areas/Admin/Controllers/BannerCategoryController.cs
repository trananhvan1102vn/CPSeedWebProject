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
    public class BannerCategoryController : AdminBaseController {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Category
        public async Task<ActionResult> Index() {
            return View(await db.BannerCategories
                .Include("UserCreated")
                .Include("UserModified")
                .ToListAsync());
        }


        // GET: Admin/Category/Details/5
        public async Task<ActionResult> Details(Guid? id) {
            if (id == null) {
                return View(new BannerCategory());
            }
            BannerCategory category = await db.BannerCategories.FindAsync(id);
            if (category == null) {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Details([Bind(Include = "Id,Name,Description,OrderNo")] BannerCategory model) {
            if (ModelState.IsValid) {
                if (model.Id != new Guid()) {
                    var category = await db.BannerCategories.SingleOrDefaultAsync(x => x.Id == model.Id);

                    category.Name = model.Name;
                    category.Description = model.Description;
                    category.OrderNo = model.OrderNo;

                    category.ModifiedBy = User.Identity.GetUserId();
                    category.ModifiedDate = DateTime.Now;

                    db.Entry(category).State = EntityState.Modified;
                } else {
                    var category = new BannerCategory();

                    category.Id = Guid.NewGuid();
                    category.Name = model.Name;
                    category.Description = model.Description;
                    category.OrderNo = model.OrderNo;

                    category.CreatedBy = User.Identity.GetUserId();
                    category.CreatedDate = DateTime.Now;
                    db.BannerCategories.Add(category);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid deletedItemId) {
            BannerCategory category = await db.BannerCategories.FindAsync(deletedItemId);
            db.BannerCategories.Remove(category);
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
