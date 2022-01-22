using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CPSeed.Models;
using Microsoft.AspNet.Identity;

namespace CPSeed.Areas.Admin.Controllers {
    public class CustomerController : AdminBaseController {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ApplicationUser
        public async Task<ActionResult> Index() {
            return View(await db.Users
                .ToListAsync());
        }


        // GET: Admin/ApplicationUser/Details/5
        public async Task<ActionResult> Details(string id) {
            if (id == null) {
                return View(new ApplicationUser());
            }
            ApplicationUser item = await db.Users.FirstOrDefaultAsync(x=>x.Id== id);
            if (item == null) {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Admin/ApplicationUser/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string deletedItemId) {
            ApplicationUser item = await db.Users.FirstOrDefaultAsync(x => x.Id == deletedItemId);
            db.Users.Remove(item);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
