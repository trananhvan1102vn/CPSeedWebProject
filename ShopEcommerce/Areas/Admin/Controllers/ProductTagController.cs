using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers {
    public class ProductTagController : AdminBaseController {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductTag
        public async Task<ActionResult> Index() {
            return View(await db.ProductTags
                .ToListAsync());
        }

        // POST: Admin/Category/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Details(Guid? TagId,string TagName) {
            if (TagId.HasValue) {
                var item = await db.ProductTags.SingleOrDefaultAsync(x => x.Id == TagId);
                item.Name = TagName;
                db.Entry(item).State = EntityState.Modified;
            } else {
                var item = new ProductTag();
                item.Id = Guid.NewGuid();
                item.Name = TagName;
                db.ProductTags.Add(item);
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid deletedItemId) {
            ProductTag category = await db.ProductTags.FindAsync(deletedItemId);
            db.ProductTags.Remove(category);
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