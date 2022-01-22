using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Controllers {
    public class ProductController : BaseController {

        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ActionResult> Index() {

            return View(await db.Products
                .Include("UserCreated")
                .Include("UserModified")
                .ToListAsync());
        }
        public async Task<ActionResult> Category(Guid? id) {
            ViewBag.ProductCategories = db.ProductCategories
              .OrderBy(x => x.CreatedDate).ToList();
            ViewBag.Tags = db.ProductTags.ToList();
            if (Session["PRODUCT_VIEWER"] == null) {
                Session["PRODUCT_VIEWER"] = new List<ProductViewer>();
            }
            List<ProductViewer> productViewers = Session["PRODUCT_VIEWER"] as List<ProductViewer>;
            ViewBag.ProductViewers = productViewers;
            return View(await db.ProductCategories
                .Include("Products").FirstOrDefaultAsync(x => x.Id == id));
        }


        // GET: Admin/Product/Details/5
        public async Task<ActionResult> Details(Guid? id) {
            ViewBag.ProductCategories = new SelectList(db.ProductCategories.ToList(), "Id", "Name");
            if (id == null) {
                return View(new Product());
            }
            Product product = await db.Products.Include("ProductTags").FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) {
                return HttpNotFound();
            }
            product.ViewCount++;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.Tags = db.ProductTags.ToList();
            ViewBag.ProductCanLikes = db.Products.OrderByDescending(x => x.ViewCount).Take(10).ToList();

            if (Session["PRODUCT_VIEWER"] == null) {
                Session["PRODUCT_VIEWER"] = new List<ProductViewer>();
            }
            List<ProductViewer> productViewers = Session["PRODUCT_VIEWER"] as List<ProductViewer>;
            if (productViewers.FirstOrDefault(x => x.Product.Id == product.Id) == null) {
                var productView = new ProductViewer {
                    Product = product,
                    CreatedDate = DateTime.Now
                };
                productViewers.Add(productView);
            } else {
                ProductViewer item = productViewers.FirstOrDefault(m => m.Product.Id == product.Id);
                item.CreatedDate = DateTime.Now;
            }
            ViewBag.ProductViewers = productViewers;
            return View(product);
        }

        public async Task<JsonResult> GetProductJson(Guid? id) {
            var product = await db.Products.Select(x => new {
                id = x.Id,
                name = x.Name,
                desc = x.Description,
                price = x.Price,
                sku = x.SKU,
                color = x.Color,
                delivery = x.Delivery,
                qty = x.Quantity,
                size = x.Size,
                thumnail = x.Thumnail,
            }).SingleOrDefaultAsync(x => x.id == id);

            return Json(product, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}