using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers {
    public class LogController : AdminBaseController {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Log
        public ActionResult Index() {
            var items = db.ActivityLogs.Include("UserCreated").OrderByDescending(x => x.Id).Take(100).ToList();
            return View(items);
        }
    }
}