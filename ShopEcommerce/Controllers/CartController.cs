using Microsoft.AspNet.Identity;
using CPSeed.Helpers;
using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Controllers {
    public class CartController : BaseController {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Cart
        public ActionResult Index() {

            var userId = User.Identity.GetUserId();
            ViewBag.CurrentUser = db.Users.SingleOrDefault(x => x.Id == userId);

            List<CartItem> items = Session["CART_ITEMS"] as List<CartItem>;
            if (items == null) {
                items = new List<CartItem>();
            }
            return View(items);
        }

        public JsonResult AddToCartJson(Guid productId) {

            if (Session["CART_ITEMS"] == null) { // Nếu giỏ hàng chưa được khởi tạo
                // Khởi tạo Session["CART_ITEMS"] là 1 List<CartItem>
                Session["CART_ITEMS"] = new List<CartItem>();
            }
            // Gán qua biến CART_ITEMS dễ code
            List<CartItem> items = Session["CART_ITEMS"] as List<CartItem>;

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa
            // ko co product nay trong gio hang
            if (items.FirstOrDefault(m => m.ProductId == productId) == null) {
                // tim product theo ProductId
                Product product = db.Products.Find(productId);
                // Tạo ra 1 CartItem mới
                CartItem newItem = new CartItem() {
                    ProductId = productId,
                    ProductName = product.Name,
                    Quantity = 1,
                    Thumail = product.Thumnail,
                    Price = product.Price
                };
                // Thêm CartItem vào giỏ 
                items.Add(newItem);
            } else {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem item = items.FirstOrDefault(m => m.ProductId == productId);
                item.Quantity++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết product khi khách hàng đặt vào giỏ thành công.
            // Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return Json(items.Count(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Checkout() {
            if (Session["CART"] == null) {
                return RedirectToAction("Index");
            }
            ProductionOrder model = Session["CART"] as ProductionOrder;
            return View(model);
        }

        [HttpPost]
        public ActionResult Checkout(ProductionOrder model) {

            if (Session["CART_ITEMS"] == null) {
                return RedirectToAction("Index");
            }
            List<CartItem> items = Session["CART_ITEMS"] as List<CartItem>;

            ProductionOrder productionOrder = new ProductionOrder();
            productionOrder.Name = model.Name;
            productionOrder.PhoneNumber = model.PhoneNumber;
            productionOrder.Email = model.Email;
            productionOrder.Address = model.Address;
            productionOrder.Note = model.Note;
            productionOrder.CartItems = items;

            Session["CART"] = productionOrder;
            return View(productionOrder);
        }

        public ActionResult Finish(string btnSubmit) {

            if (Session["CART"] == null) {
                return RedirectToAction("Index");
            }
            ProductionOrder model = Session["CART"] as ProductionOrder;

            ProductionOrder productionOrder = new ProductionOrder();
            productionOrder.Id = Guid.NewGuid();
            productionOrder.PONumber = KeyHelper.GetNextKey("ProductionOrder");
            productionOrder.Name = model.Name;
            productionOrder.PhoneNumber = model.PhoneNumber;
            productionOrder.Email = model.Email;
            productionOrder.Address = model.Address;
            productionOrder.Note = model.Note;
            productionOrder.CreatedDate = DateTime.Now;
            productionOrder.CreatedBy = User.Identity.GetUserId();
            productionOrder.Status = "Pending";
            foreach (var item in model.CartItems) {
                CartItem cartItem = new CartItem {
                    Id = Guid.NewGuid(),
                    Price = item.Price,
                    ProductId = item.ProductId,
                    ProductionOrder = productionOrder,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Thumail = item.Thumail
                };
                db.CartItems.Add(cartItem);
            }
            db.ProductionOrders.Add(productionOrder);
            db.SaveChanges();
            Session["CART_ITEMS"] = null;
            Session["CART"] = null;
            return View();
        }

        public ActionResult AddToCart(Guid id) {

            if (Session["CART_ITEMS"] == null) { // Nếu giỏ hàng chưa được khởi tạo
                // Khởi tạo Session["CART_ITEMS"] là 1 List<CartItem>
                Session["CART_ITEMS"] = new List<CartItem>();
            }
            // Gán qua biến CART_ITEMS dễ code
            List<CartItem> items = Session["CART_ITEMS"] as List<CartItem>;

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa
            // ko co product nay trong gio hang
            if (items.FirstOrDefault(m => m.ProductId == id) == null) {
                // tim product theo ProductId
                Product product = db.Products.Find(id);
                // Tạo ra 1 CartItem mới
                CartItem newItem = new CartItem() {
                    ProductId = id,
                    ProductName = product.Name,
                    Quantity = 1,
                    Thumail = product.Thumnail,
                    Price = product.Price
                };
                // Thêm CartItem vào giỏ 
                items.Add(newItem);
            } else {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem item = items.FirstOrDefault(m => m.ProductId == id);
                item.Quantity++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết product khi khách hàng đặt vào giỏ thành công.
            // Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return RedirectToAction("Index");
        }

        public JsonResult RemoveToCart(Guid productId) {
            List<CartItem> items = Session["CART_ITEMS"] as List<CartItem>;
            CartItem itemDelete = items.FirstOrDefault(m => m.ProductId == productId);
            if (itemDelete != null) {
                items.Remove(itemDelete);
            }
            return Json(new {
                number = items.Count().ToString("N0")
                , qty = items.Sum(x => x.Quantity).ToString("N0") + " Sản phẩm"
                , total = items.Sum(x => x.Total).ToString("N0") + " đ"
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditQuantity(Guid productId, int quantity) {
            // tìm carditem muon sua
            List<CartItem> items = Session["CART_ITEMS"] as List<CartItem>;
            CartItem itemEdit = items.FirstOrDefault(m => m.ProductId == productId);
            if (itemEdit != null) {
                itemEdit.Quantity = quantity;
            }
            return Json(new {
                number = items.Count().ToString("N0")
                 , qty = items.Sum(x => x.Quantity).ToString("N0") + " Sản phẩm"
                 , total = items.Sum(x => x.Total).ToString("N0") + " đ"
                 , thisTotal = itemEdit.Total.ToString("N0") + " đ"
            }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}