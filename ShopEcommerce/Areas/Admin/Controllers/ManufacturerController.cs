using Microsoft.AspNet.Identity;
using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers {
    public class ManufacturerController : AdminBaseController {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Vendor
        public async Task<ActionResult> Index() {
            return View(await db.Vendors
                .Include("UserCreated")
                .Include("UserModified")
                .ToListAsync());
        }


        // GET: Admin/Vendor/Details/5
        public async Task<ActionResult> Details(Guid? id) {
            if (id == null) {
                return View(new Vendor());
            }
            Vendor Vendor = await db.Vendors.FindAsync(id);
            if (Vendor == null) {
                return HttpNotFound();
            }
            return View(Vendor);
        }

        // POST: Admin/Vendor/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Details([Bind(Include = "Id,Name,Image,Description,OrderNo")] Vendor model) {
            if (ModelState.IsValid) {
                if (model.Id != new Guid()) {
                    var Vendor = await db.Vendors.SingleOrDefaultAsync(x => x.Id == model.Id);

                    Vendor.Name = model.Name;
                    Vendor.Description = model.Description;
                    Vendor.Image = model.Image;
                    Vendor.OrderNo = model.OrderNo;

                    Vendor.ModifiedBy = User.Identity.GetUserId();
                    Vendor.ModifiedDate = DateTime.Now;

                    db.Entry(Vendor).State = EntityState.Modified;
                } else {
                    var Vendor = new Vendor();

                    Vendor.Id = Guid.NewGuid();
                    Vendor.Name = model.Name;
                    Vendor.Description = model.Description;
                    Vendor.Image = model.Image;
                    Vendor.OrderNo = model.OrderNo;

                    Vendor.CreatedBy = User.Identity.GetUserId();
                    Vendor.CreatedDate = DateTime.Now;
                    db.Vendors.Add(Vendor);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Admin/Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid deletedItemId) {
            Vendor Vendor = await db.Vendors.FindAsync(deletedItemId);
            db.Vendors.Remove(Vendor);
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