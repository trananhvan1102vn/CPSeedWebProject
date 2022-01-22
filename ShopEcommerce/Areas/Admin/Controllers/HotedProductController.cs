using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers
{
    public class HotedProductController : AdminBaseController {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/FeaturedProduct
        public async Task<ActionResult> Index() {
            return View(await db.Products
                .Include("UserCreated")
                .Include("UserModified")
                .ToListAsync());
        }
        public JsonResult AddProduct(string ids) {
            var idItems = ids.Split(',');
            var products = db.Products.Where(x => idItems.Contains(x.Id.ToString())).ToList();
            var allProducts = db.Products.ToList();

            allProducts.ToList().ForEach(x => x.IsHot = false);
            //allProducts.ForEach(x => db.Entry(x));

            foreach (var product in products) {
                product.IsHot = true;
                db.Entry(product).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}