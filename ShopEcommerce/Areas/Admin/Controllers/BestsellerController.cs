using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers {
    public class BestsellerController : AdminBaseController {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Bestseller
        public ActionResult Index() {
            return View();
        }
    }
}