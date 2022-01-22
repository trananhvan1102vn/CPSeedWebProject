using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Controllers {

    public class BaseController : Controller {
        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            using (var db = new ApplicationDbContext()) {
                string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper();
                ViewBag.CurrentController = controller;

                var vendors = db.Vendors
                    //.Where(x => x.IsPublish.HasValue && x.IsPublish.Value)
                    .OrderBy(x=>x.OrderNo)
                    .ToList();
                ViewBag.Vendors = vendors;

                var cofig = db.ConfigSettings.FirstOrDefault();
                if (cofig == null) {
                    cofig = new ConfigSetting();
                }
                ViewBag.ConfigSetting = cofig;

                var productCategories = db.ProductCategories.ToList();
                ViewBag.ProductCategories = productCategories;

                var visitorCounter = db.VisitorCounters.FirstOrDefault();
                if (visitorCounter == null) {
                    visitorCounter = new VisitorCounter();
                }
                ViewBag.VisitorCounter = visitorCounter;

                var plugins = db.Plugins
                   .Where(x => x.Show)
                   .OrderBy(x => x.OrderNo)
                   .ToList();
                ViewBag.Plugins = plugins;
            }
        }
    }
}