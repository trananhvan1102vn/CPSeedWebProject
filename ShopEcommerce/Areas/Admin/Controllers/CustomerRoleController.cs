using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers {
    public class CustomerRoleController : AdminBaseController {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CustomerRole
        public async Task<ActionResult> Index() {

            return View(await db.Roles
                .ToListAsync());
        }
        public async Task<ActionResult> Details(string id) {
            var role = await db.Roles.Include("Users").SingleOrDefaultAsync(x => x.Id == id);
            if (role == null) {
                return HttpNotFound();
            }
            ViewBag.Users = db.Users.Where(x => x.Roles.Any(z => z.RoleId == role.Id)).ToList();
            return View(role);
        }

        public async Task<ActionResult> AddUser(string id) {
            var role = await db.Roles.Include("Users").SingleOrDefaultAsync(x => x.Id == id);
            if (role == null) {
                return HttpNotFound();
            }
            ViewBag.Users = db.Users.ToList();
            return View(role);
        }

        public JsonResult AddUsersToRole(string roleId, string ids) {
            var userStore = new ApplicationUserStore(db);
            var userManager = new ApplicationUserManager(userStore);

            var idItems = ids.Split(',');

            var role = db.Roles.SingleOrDefault(x => x.Id == roleId);
            role.Users.Clear();
            db.SaveChanges();

            var users = db.Users.Where(x => idItems.Contains(x.Id.ToString())).ToList();
            foreach (var user in users) {
                userManager.AddToRole(user.Id, role.Name);
            }

            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddMenu(string id) {
            var role = await db.Roles.Include("Users").SingleOrDefaultAsync(x => x.Id == id);
            if (role == null) {
                return HttpNotFound();
            }
            ViewBag.Menus = db.Menus
                //.Where(x => !x.ParentId.HasValue)
                .ToList();
            return View(role);
        }

        public JsonResult AddMenusToRole(string roleId, string ids) {
            var userStore = new ApplicationUserStore(db);
            var userManager = new ApplicationUserManager(userStore);

            var idItems = ids.Split(',');

            var role = db.Roles.SingleOrDefault(x => x.Id == roleId);
            role.Menus.Clear();
            db.SaveChanges();

            var menus = db.Menus.Where(x => idItems.Contains(x.Id.ToString())).ToList();
            menus.ForEach(x => x.Roles.Add(role));

            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}