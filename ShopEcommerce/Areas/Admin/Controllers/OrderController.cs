using CPSeed.Areas.Admin.Models;
using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers {
    public class OrderController : AdminBaseController {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index() {
            ProductionOrderViewModel searchCriteria = new ProductionOrderViewModel();
            if (Session["ProductionOrderViewModel"] != null) {
                searchCriteria = (ProductionOrderViewModel)Session["ProductionOrderViewModel"];
            }
            searchCriteria = Search(searchCriteria);
            return View(searchCriteria);
        }
        [HttpPost]
        public ActionResult Index(ProductionOrderViewModel searchCriteria) {
            searchCriteria = Search(searchCriteria);
            return View(searchCriteria);
        }
        private ProductionOrderViewModel Search(ProductionOrderViewModel searchCriteria) {
            DateTime? endRange = null;
            if (searchCriteria.DateTo.HasValue) {
                endRange = searchCriteria.DateTo.Value.AddDays(1).AddMinutes(-1);
            }

            List<ProductionOrder> items = db.ProductionOrders.Include("UserCreated")
                    .Where(x => (string.IsNullOrEmpty(searchCriteria.Status) || x.Status == searchCriteria.Status)
                             && (string.IsNullOrEmpty(searchCriteria.PONumber) || x.PONumber == searchCriteria.PONumber)
                             && (string.IsNullOrEmpty(searchCriteria.CustomerName) || x.Name == searchCriteria.CustomerName)
                             && (!searchCriteria.DateFrom.HasValue || x.CreatedDate >= searchCriteria.DateFrom)
                             && (!searchCriteria.DateTo.HasValue || x.CreatedDate <= searchCriteria.DateTo)
                    ).OrderByDescending(x => x.CreatedDate).ToList();
            searchCriteria.Results = items;
            Session["ProductionOrderViewModel"] = searchCriteria;
            return searchCriteria;
        }

        public async Task<ActionResult> Details(Guid? id) {
            var item = await db.ProductionOrders.FindAsync(id);
            if (item == null) {
                return HttpNotFound();
            }
            return View(item);
        }
        [HttpPost]
        public async Task<ActionResult> Details(ProductionOrder model, string btnSubmit) {
            var item = await db.ProductionOrders.FindAsync(model.Id);
            if (item == null) {
                return HttpNotFound();
            }
            if (btnSubmit == "ok") {
                item.Status = "Processing";
            } else {
                item.Status = "Rejected";
            }
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(item);
        }

    }
}