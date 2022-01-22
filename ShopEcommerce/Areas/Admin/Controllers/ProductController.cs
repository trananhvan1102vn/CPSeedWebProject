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
using CPSeed.Areas.Admin.Models;

namespace CPSeed.Areas.Admin.Controllers {
    public class ProductController : AdminBaseController {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Product
        //public async Task<ActionResult> Index() {

        //    return View(await db.Products
        //        .Include("UserCreated")
        //        .Include("UserModified")
        //        .ToListAsync());
        //}
        public ActionResult Index() {
            ProductViewModel searchCriteria = new ProductViewModel();
            if (Session["ProductViewModel"] != null) {
                searchCriteria = (ProductViewModel)Session["ProductViewModel"];
            }
            searchCriteria = Search(searchCriteria);
            ViewBag.ProductCategories = db.ProductCategories.Where(x => !x.ParentId.HasValue).ToList();
            return View(searchCriteria);
        }
        [HttpPost]
        public ActionResult Index(ProductViewModel searchCriteria, string[] CategoryId) {
            foreach (var cateId in CategoryId.Where(x => x != "false")) {
                searchCriteria.CategoryIds.Add(Guid.Parse(cateId));
            }
            searchCriteria = Search(searchCriteria);
            ViewBag.ProductCategories = db.ProductCategories.Where(x => !x.ParentId.HasValue).ToList();
            return View(searchCriteria);
        }
        private ProductViewModel Search(ProductViewModel searchCriteria) {
            DateTime? endRange = null;
            if (searchCriteria.DateTo.HasValue) {
                endRange = searchCriteria.DateTo.Value.AddDays(1).AddMinutes(-1);
            }

            List<Product> items = db.Products
                                .Include("UserCreated")
                                .Include("UserModified")
                    .Where(x => (string.IsNullOrEmpty(searchCriteria.SKU) || x.SKU == searchCriteria.SKU)
                             && (string.IsNullOrEmpty(searchCriteria.Name) || x.Name == searchCriteria.Name)
                             && (!searchCriteria.DateFrom.HasValue || x.CreatedDate >= searchCriteria.DateFrom)
                             && (!searchCriteria.DateTo.HasValue || x.CreatedDate <= searchCriteria.DateTo)
                             && (searchCriteria.CategoryIds.Count == 0 || x.ProductCategories.Any(z => searchCriteria.CategoryIds.Contains(z.Id)))
                    ).OrderByDescending(x => x.CreatedDate).ToList();
            searchCriteria.Results = items;
            Session["ProductViewModel"] = searchCriteria;
            return searchCriteria;
        }

        // GET: Admin/Product/Details/5
        public async Task<ActionResult> Details(Guid? id) {
            ViewBag.ProductCategories = new SelectList(db.ProductCategories.ToList(), "Id", "Name");
            if (id == null) {
                return View(new Product());
            }
            Product product = await db.Products
                .Include("ProductTags")
                .Include("CartItems")
                .Include("CartItems.Product")
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) {
                return HttpNotFound();
            }
            ViewBag.Tags = db.ProductTags.ToList();
            return View(product);
        }

        // POST: Admin/Product/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Details([Bind(Include = "Id,SKU,Name,Thumnail,Price,Manufacturer,Warranty,Description,Color,Delivery,Quantity,Size,DetailOverview,OrderNo,MetaKeywords,MetaDescription,MetaTitle,SeName")] Product model
            , Guid[] ProductCategories, string btnSubmit) {
            if (ModelState.IsValid) {
                if (model.Id != new Guid()) {
                    var product = await db.Products.SingleOrDefaultAsync(x => x.Id == model.Id);

                    product.SKU = model.SKU;
                    product.Name = model.Name;
                    product.Thumnail = model.Thumnail;
                    product.Price = model.Price;
                    product.Manufacturer = model.Manufacturer;
                    product.Warranty = model.Warranty;
                    product.Description = model.Description;
                    product.Color = model.Color;
                    product.Delivery = model.Delivery;
                    product.Quantity = model.Quantity;
                    product.Size = model.Size;
                    product.DetailOverview = model.DetailOverview;
                    product.OrderNo = model.OrderNo;

                    product.MetaKeywords = model.MetaKeywords;
                    product.MetaDescription = model.MetaDescription;
                    product.MetaTitle = model.MetaTitle;
                    product.SeName = model.SeName;

                    product.ModifiedBy = User.Identity.GetUserId();
                    product.ModifiedDate = DateTime.Now;
                    product.ProductCategories.Clear();
                    if (ProductCategories != null) {
                        foreach (var categoryId in ProductCategories) {
                            var category = await db.ProductCategories.SingleOrDefaultAsync(x => x.Id == categoryId);
                            product.ProductCategories.Add(category);
                        }
                    }
                    db.Entry(product).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    if (btnSubmit == "Continue") {
                        return RedirectToAction("Details", new { id = product.Id });
                    }
                } else {
                    var product = new Product();

                    product.Id = Guid.NewGuid();
                    product.SKU = model.SKU;
                    product.Name = model.Name;
                    product.Thumnail = model.Thumnail;
                    product.Price = model.Price;
                    product.Manufacturer = model.Manufacturer;
                    product.Warranty = model.Warranty;
                    product.Description = model.Description;
                    product.Color = model.Color;
                    product.Delivery = model.Delivery;
                    product.Quantity = model.Quantity;
                    product.Size = model.Size;
                    product.DetailOverview = model.DetailOverview;
                    product.OrderNo = model.OrderNo;

                    product.MetaKeywords = model.MetaKeywords;
                    product.MetaDescription = model.MetaDescription;
                    product.MetaTitle = model.MetaTitle;
                    product.SeName = model.SeName;

                    product.CreatedBy = User.Identity.GetUserId();
                    product.CreatedDate = DateTime.Now;

                    if (ProductCategories != null) {
                        foreach (var categoryId in ProductCategories) {
                            var category = await db.ProductCategories.SingleOrDefaultAsync(x => x.Id == categoryId);
                            product.ProductCategories.Add(category);
                        }
                    }
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    if (btnSubmit == "Continue") {
                        return RedirectToAction("Details", new { id = product.Id });
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategories = new SelectList(db.ProductCategories.ToList(), "Id", "Name");
            return View(model);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid deletedItemId, string deleteObjectType) {
            if (string.IsNullOrEmpty(deleteObjectType)) {
                Product Product = await db.Products.FindAsync(deletedItemId);
                db.Products.Remove(Product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            } else if (deleteObjectType == "ProductPhoto") {
                var productImage = await db.ProductImages.FindAsync(deletedItemId);
                Guid returnId = productImage.ProductId;
                db.ProductImages.Remove(productImage);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = returnId });
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddProductPhoto(int? id, Guid productId, string productPhoto, string productAlt, string productTitle, int productDisplayOrder) {

            if (id.HasValue) {
                ProductImage photo = await db.ProductImages.FindAsync(id);
                photo.Link = productPhoto;
                photo.Alt = productAlt;
                photo.FileName = productTitle;
                photo.OrderNo = productDisplayOrder;

                photo.ModifiedBy = User.Identity.GetUserId();
                photo.ModifiedDate = DateTime.Now;

                db.Entry(photo).State = EntityState.Modified;
            } else {
                ProductImage photo = new ProductImage();

                photo.Id = Guid.NewGuid();
                photo.ProductId = productId;
                photo.Link = productPhoto;
                photo.Alt = productAlt;
                photo.FileName = productTitle;
                photo.OrderNo = productDisplayOrder;

                photo.CreatedBy = User.Identity.GetUserId();
                photo.CreatedDate = DateTime.Now;
                db.ProductImages.Add(photo);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = productId });
        }

        public JsonResult AddTagToProduct(Guid productId, string ids) {
            var idItems = ids.Split(',');

            var product = db.Products.Include("ProductTags").SingleOrDefault(x => x.Id == productId);
            product.ProductTags.Clear();
            db.SaveChanges();

            var tags = db.ProductTags.Where(x => idItems.Contains(x.Id.ToString())).ToList();
            tags.ForEach(x => x.Products.Add(product));

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
