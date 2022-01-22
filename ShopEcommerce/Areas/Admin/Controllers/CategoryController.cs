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
    public class CategoryController : AdminBaseController {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Category
        public async Task<ActionResult> Index() {
            return View(await db.ProductCategories
                .Include("UserCreated")
                .Include("UserModified")
                .ToListAsync());
        }


        // GET: Admin/Category/Details/5
        public async Task<ActionResult> Details(Guid? id) {
            if (id == null) {
                ViewBag.ProductCategories = new SelectList(db.ProductCategories.Select(x => new { key = x.Id, value = x.Name }), "key", "value");
                return View(new ProductCategory());
            }
            ProductCategory category = await db.ProductCategories.FindAsync(id);
            if (category == null) {
                return HttpNotFound();
            }
            ViewBag.ProductCategories = new SelectList(db.ProductCategories.Select(x => new { key = x.Id, value = x.Name }), "key", "value", category.ParentId);
            ViewBag.Products = await db.Products.ToListAsync();
            return View(category);
        }

        // POST: Admin/Category/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Details([Bind(Include = "Id,Name,Description,Image,OrderNo,ParentId")] ProductCategory model) {
            if (ModelState.IsValid) {
                if (model.Id != new Guid()) {
                    var category = await db.ProductCategories.SingleOrDefaultAsync(x => x.Id == model.Id);

                    category.ParentId = model.ParentId;
                    category.Name = model.Name;
                    category.Description = model.Description;
                    category.Image = model.Image;
                    category.OrderNo = model.OrderNo;

                    category.ModifiedBy = User.Identity.GetUserId();
                    category.ModifiedDate = DateTime.Now;

                    db.Entry(category).State = EntityState.Modified;
                } else {
                    var category = new ProductCategory();

                    category.Id = Guid.NewGuid();
                    category.ParentId = model.ParentId;
                    category.Name = model.Name;
                    category.Description = model.Description;
                    category.Image = model.Image;
                    category.OrderNo = model.OrderNo;

                    category.CreatedBy = User.Identity.GetUserId();
                    category.CreatedDate = DateTime.Now;
                    db.ProductCategories.Add(category);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategories = new SelectList(db.ProductCategories.Select(x => new { key = x.Id, value = x.Name }), "key", "value", model.ParentId);
            return View(model);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid deletedItemId) {
            ProductCategory category = await db.ProductCategories.FindAsync(deletedItemId);
            db.ProductCategories.Remove(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public JsonResult AddProductToProductCategory(Guid categoryId, string ids) {
            var idItems = ids.Split(',');

            var category = db.ProductCategories.SingleOrDefault(x => x.Id == categoryId);
            category.Products.Clear();
            db.SaveChanges();

            var products = db.Products.Where(x => idItems.Contains(x.Id.ToString())).ToList();
            products.ForEach(x => x.ProductCategories.Add(category));

            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddIsShowOnHomeList(string ids) {
            var idItems = ids.Split(',');
            var products = db.ProductCategories.Where(x => idItems.Contains(x.Id.ToString())).ToList();
            var allProducts = db.ProductCategories.ToList();

            allProducts.ToList().ForEach(x => x.IsShowOnHome = false);

            foreach (var product in products) {
                product.IsShowOnHome = true;
                db.Entry(product).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult AddIsPublishList(string ids) {
            var idItems = ids.Split(',');
            var products = db.ProductCategories.Where(x => idItems.Contains(x.Id.ToString())).ToList();
            var allProducts = db.ProductCategories.ToList();

            allProducts.ToList().ForEach(x => x.IsPublish = false);

            foreach (var product in products) {
                product.IsPublish = true;
                db.Entry(product).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
