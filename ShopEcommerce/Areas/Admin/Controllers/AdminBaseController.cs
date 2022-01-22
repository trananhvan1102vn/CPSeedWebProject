using Microsoft.AspNet.Identity;
using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers {
    [Authorize(Roles = "Admin")]
    public class AdminBaseController : Controller {
        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            using (var db = new ApplicationDbContext()) {
                string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper();
                ViewBag.CurrentController = controller;

                var userId = User.Identity.GetUserId();
                var menus = db.Roles.Where(x => x.Users.Any(z => z.UserId == userId)).SelectMany(x=>x.Menus);
                ViewBag.MyMenus = menus.ToList();
            }
        }
    }
}