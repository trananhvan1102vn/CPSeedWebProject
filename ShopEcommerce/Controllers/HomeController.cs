using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Controllers {
    public class HomeController : BaseController {

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index() {
            ViewBag.ProductCategories = db.ProductCategories
                .OrderBy(x => x.CreatedDate).ToList();

            ViewBag.ProductFeatureds = db.Products.Where(x => x.IsFeatured.HasValue && x.IsFeatured.Value).ToList();
            ViewBag.ProductHoteds = db.Products.Where(x => x.IsHot.HasValue && x.IsHot.Value).ToList();

            ViewBag.ProductNews = db.Products.OrderBy(x => x.CreatedDate).Take(10).ToList();

            ViewBag.Sliders = db.Banners.Where(x => x.BannerCategory.Name == "Slider").OrderBy(x => x.OrderNo).ToList();
            return View();
        }
        // Hiện tại không cần dùng đến phần này
        //public JsonResult SaveRequestProduct(RequestProduct model) {
        //    model.CreatedDate = DateTime.Now;
        //    db.RequestProducts.Add(model);
        //    db.SaveChanges();
        //    return Json("Sản phẩm yêu cầu của bạn đã được ghi nhận, chúng tôi sẽ liên hệ lại bạn ngay khi có sản phẩm.", JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SubscribeEmail(string SubscribeEmail) {
        //    var check = db.Subscriptions.Any(x => x.Email == SubscribeEmail);
        //    if (!check) {
        //        Subscription subscription = new Subscription {
        //            Email = SubscribeEmail,
        //            CreatedDate = DateTime.Now
        //        };
        //        db.Subscriptions.Add(subscription);
        //        db.SaveChanges();
        //        return Json("Cảm ơn bạn đã đăng ký nhận thông tin khi có sản phẩm mới, chúc bạn 1 ngày làm việc đầy năng lượng.", JsonRequestBehavior.AllowGet);
        //    }
        //    return Json("Email của bạn đã được đăng ký nhận tin rồi. Chúc bạn 1 ngày làm việc đầy năng lượng.", JsonRequestBehavior.AllowGet);
        //}

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}