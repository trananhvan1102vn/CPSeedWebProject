using Microsoft.AspNet.Identity;
using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers {
    public class TopicController : AdminBaseController {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Topic
        public async Task<ActionResult> Index() {
            return View(await db.Topics
               .Include("UserCreated")
               .Include("UserModified")
               .ToListAsync());
        }

        // GET: Admin/Category/Details/5
        public async Task<ActionResult> Details(Guid? id) {
            if (id == null) {
                return View(new Topic());
            }
            Topic item = await db.Topics.FindAsync(id);
            if (item == null) {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Admin/Category/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Details([Bind(Include = "Id,Type,Content,OrderNo,AllowDelete,IsPublish")] Topic model,string btnSubmit) {
            if (ModelState.IsValid) {
                if (model.Id != new Guid()) {
                    var item = await db.Topics.SingleOrDefaultAsync(x => x.Id == model.Id);

                    item.Type = model.Type;
                    item.Content = model.Content;
                    item.OrderNo = model.OrderNo;

                    item.AllowDelete = model.AllowDelete;
                    item.IsPublish = model.IsPublish;
                    
                    item.ModifiedBy = User.Identity.GetUserId();
                    item.ModifiedDate = DateTime.Now;

                    db.Entry(item).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    if (btnSubmit == "Continue") {
                        return RedirectToAction("Details", new { id = item.Id });
                    }
                } else {
                    var item = new Topic();

                    item.Id = Guid.NewGuid();
                    item.Type = model.Type;
                    item.Content = model.Content;
                    item.OrderNo = model.OrderNo;

                    item.AllowDelete = model.AllowDelete;
                    item.IsPublish = model.IsPublish;

                    item.CreatedBy = User.Identity.GetUserId();
                    item.CreatedDate = DateTime.Now;
                    db.Topics.Add(item);
                    await db.SaveChangesAsync();
                    if (btnSubmit == "Continue") {
                        return RedirectToAction("Details", new { id = item.Id });
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid deletedItemId) {
            Topic item = await db.Topics.FindAsync(deletedItemId);
            db.Topics.Remove(item);
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