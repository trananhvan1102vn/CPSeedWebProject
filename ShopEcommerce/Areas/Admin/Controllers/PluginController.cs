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
    public class PluginController : AdminBaseController {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ReturnRequest
        public ActionResult Index() {
            var items = db.Plugins.ToList();
            return View(items);
        }
        // GET: Admin/Plugin/Details/5
        public async Task<ActionResult> Details(Guid? id) {
            List<KeyValuePair<int?, string>> result = new List<KeyValuePair<int?, string>>();
            result.Add(new KeyValuePair<int?, string>(1, "Sau thẻ <Head>"));
            result.Add(new KeyValuePair<int?, string>(2, "Cuối thẻ <Head>"));
            result.Add(new KeyValuePair<int?, string>(3, "Sau thẻ <Body>"));
            result.Add(new KeyValuePair<int?, string>(4, "Giữa thẻ <Body>"));
            result.Add(new KeyValuePair<int?, string>(5, "Cuối thẻ <Body>"));
            ViewBag.Positions = new SelectList(result, "Key", "Value");
            if (id == null) {
                return View(new Plugin());
            }
            Plugin plugin = await db.Plugins.FindAsync(id);
            if (plugin == null) {
                return HttpNotFound();
            }
            
            return View(plugin);
        }

        // POST: Admin/Plugin/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Details([Bind(Include = "Id,Name,OrderNo,Show,Html,Style,Script,Postion")] Plugin model, string btnSubmit) {
            if (ModelState.IsValid) {
                if (model.Id != new Guid()) {
                    var plugin = await db.Plugins.SingleOrDefaultAsync(x => x.Id == model.Id);
                    plugin.OrderNo = model.OrderNo;
                    plugin.Show = model.Show;
                    plugin.Name = model.Name;
                    plugin.Html = model.Html;
                    plugin.Style = model.Style;
                    plugin.Script = model.Script;
                    plugin.Postion = model.Postion;

                    plugin.UserId = User.Identity.GetUserId();
                    plugin.AddedDate = DateTime.Now;

                    db.Entry(plugin).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    if (btnSubmit == "Continue") {
                        return RedirectToAction("Details", new { id = plugin.Id });
                    }
                } else {
                    var plugin = new Plugin();

                    plugin.Id = Guid.NewGuid();
                    plugin.OrderNo = model.OrderNo;
                    plugin.Show = model.Show;
                    plugin.Name = model.Name;
                    plugin.Html = model.Html;
                    plugin.Style = model.Style;
                    plugin.Script = model.Script;
                    plugin.Postion = model.Postion;

                    plugin.UserId = User.Identity.GetUserId();
                    plugin.AddedDate = DateTime.Now;
                    db.Plugins.Add(plugin);
                    await db.SaveChangesAsync();

                    if (btnSubmit == "Continue") {
                        return RedirectToAction("Details", new { id = plugin.Id });
                    }
                }

                return RedirectToAction("Index");
            }
            List<KeyValuePair<int?, string>> result = new List<KeyValuePair<int?, string>>();
            result.Add(new KeyValuePair<int?, string>(1, "Sau thẻ <Head>"));
            result.Add(new KeyValuePair<int?, string>(2, "Cuối thẻ <Head>"));
            result.Add(new KeyValuePair<int?, string>(3, "Sau thẻ <Body>"));
            result.Add(new KeyValuePair<int?, string>(4, "Giữa thẻ <Body>"));
            result.Add(new KeyValuePair<int?, string>(5, "Cuối thẻ <Body>"));
            ViewBag.Positions = new SelectList(result, "Key", "Value");
            return View(model);
        }

        // POST: Admin/Plugin/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid deletedItemId) {
            Plugin plugin = await db.Plugins.FindAsync(deletedItemId);
            db.Plugins.Remove(plugin);
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