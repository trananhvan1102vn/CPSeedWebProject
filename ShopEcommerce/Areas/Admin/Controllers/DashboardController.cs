using CPSeed.Areas.Admin.Models;
using CPSeed.Helpers;
using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers {
    public class DashboardController : AdminBaseController {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Dashboard
        public ActionResult Index() {
            SummaryViewModel model = new SummaryViewModel();
            model.NewOrderCounter = db.ProductionOrders.Count(x => x.Status == "Pending");
            model.CustomerCounter = db.Users.Count();


            DateTime StartOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateTime EndOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Sunday).AddDays(1);

            var orders = db.ProductionOrders.ToList();
            model.OrderPendingToday = orders.Where(x => x.Status == "Pending" && x.CreatedDate.Date == DateTime.Today).Sum(x => x.Total);
            model.OrderPendingThisWeek = orders.Where(x => x.Status == "Pending" && x.CreatedDate.Date >= StartOfWeek && x.CreatedDate < EndOfWeek).Sum(x => x.Total);

            model.OrderPendingThisMonth = orders.Where(x => x.Status == "Pending"
                    && x.CreatedDate.Month == DateTime.Today.Month
                    && x.CreatedDate.Year == DateTime.Today.Year
                    ).Sum(x => x.Total);
            model.OrderPendingThisYear = orders.Where(x => x.Status == "Pending"
                    && x.CreatedDate.Year == DateTime.Today.Year
                    ).Sum(x => x.Total);
            model.OrderPendingTotal = orders.Where(x => x.Status == "Pending").Sum(x => x.Total);

            model.OrderProcessingToday = orders.Where(x => x.Status == "Processing" && x.CreatedDate.Date == DateTime.Today).Sum(x => x.Total);

            model.OrderProcessingThisWeek = orders.Where(x => x.Status == "Processing" && x.CreatedDate.Date >= StartOfWeek && x.CreatedDate < EndOfWeek).Sum(x => x.Total);
            model.OrderProcessingThisMonth = orders.Where(x => x.Status == "Processing"
                    && x.CreatedDate.Month == DateTime.Today.Month
                    && x.CreatedDate.Year == DateTime.Today.Year
                    ).Sum(x => x.Total);
            model.OrderProcessingThisYear = orders.Where(x => x.Status == "Processing"
                    && x.CreatedDate.Year == DateTime.Today.Year
                    ).Sum(x => x.Total);
            model.OrderProcessingTotal = orders.Where(x => x.Status == "Processing").Sum(x => x.Total);

            model.OrderCompletedToday = orders.Where(x => x.Status == "Completed" && x.CreatedDate.Date == DateTime.Today).Sum(x => x.Total);
            model.OrderCompletedThisWeek = orders.Where(x => x.Status == "Completed" && x.CreatedDate.Date >= StartOfWeek && x.CreatedDate < EndOfWeek).Sum(x => x.Total);

            model.OrderCompletedThisMonth = orders.Where(x => x.Status == "Completed"
                    && x.CreatedDate.Month == DateTime.Today.Month
                    && x.CreatedDate.Year == DateTime.Today.Year
                    ).Sum(x => x.Total);
            model.OrderCompletedThisYear = orders.Where(x => x.Status == "Completed"
                    && x.CreatedDate.Year == DateTime.Today.Year
                    ).Sum(x => x.Total);
            model.OrderCompletedTotal = orders.Where(x => x.Status == "Completed").Sum(x => x.Total);

            model.OrderCancelledToday = orders.Where(x => x.Status == "Rejected" && x.CreatedDate.Date == DateTime.Today).Sum(x => x.Total);
            model.OrderCancelledThisWeek = orders.Where(x => x.Status == "Rejected" && x.CreatedDate.Date >= StartOfWeek && x.CreatedDate < EndOfWeek).Sum(x => x.Total);
            model.OrderCancelledThisMonth = orders.Where(x => x.Status == "Rejected"
                    && x.CreatedDate.Month == DateTime.Today.Month
                    && x.CreatedDate.Year == DateTime.Today.Year
                    ).Sum(x => x.Total);
            model.OrderCancelledThisYear = orders.Where(x => x.Status == "Rejected"
                    && x.CreatedDate.Year == DateTime.Today.Year
                    ).Sum(x => x.Total);
            model.OrderCancelledTotal = orders.Where(x => x.Status == "Rejected").Sum(x => x.Total);

            model.OrderUnFinisheds = orders
                .Where(x => x.Status != "Completed")
                .OrderByDescending(x => x.CreatedDate).Take(4)
                .Select(x => new Bestseller {
                    ProductId = x.Id,
                    Name = x.PONumber,
                    TotalQuantity = x.Quantity,
                    TotalAmount = x.Total,
                }).ToList();


            model.OrderNewlests = orders
                .OrderByDescending(x => x.CreatedDate).Take(5)
                .Select(x => new OrderNewlest {
                    OrderId = x.Id,
                    CustomerName = x.Name,
                    OrderDate = x.CreatedDate,
                    PONumber = x.PONumber,
                    Status = x.Status
                }).ToList();

            model.BestsellerByAmounts = db.CartItems
              .AsEnumerable()
              .OrderByDescending(x => x.Total).Take(5)
              .Select(x => new Bestseller {
                  ProductId = x.ProductId,
                  Name = x.ProductName,
                  TotalQuantity = x.Quantity,
                  TotalAmount = x.Total,
              }).ToList();

            model.BestsellerByQuantities = db.CartItems
              .AsEnumerable()
              .OrderByDescending(x => x.Quantity).Take(5)
              .Select(x => new Bestseller {
                  ProductId = x.ProductId,
                  Name = x.ProductName,
                  TotalQuantity = x.Quantity,
                  TotalAmount = x.Total,
              }).ToList();
            return View(model);
        }
    }
}