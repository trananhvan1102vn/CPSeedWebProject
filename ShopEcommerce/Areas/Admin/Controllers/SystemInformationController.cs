using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers {
    public class SystemInformationController : AdminBaseController {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/SystemInformation
        public async Task<ActionResult> Index() {
            ConfigSetting item = await db.ConfigSettings.FirstOrDefaultAsync();
            if (item == null) {
                return View(new ConfigSetting());
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Index([Bind(Include = "Id,AppName,Title,PhoneNumber,MobilePhone,Address,Email,Facebook,Zalo,Youtube,Instagram,Policy,Service,AppNameShort,AppLogo")] ConfigSetting model, string btnSubmit) {
            if (ModelState.IsValid) {
                if (model.Id != new Guid()) {
                    var item = await db.ConfigSettings.SingleOrDefaultAsync(x => x.Id == model.Id);

                    item.AppNameShort = model.AppNameShort;
                    item.AppName = model.AppName;
                    item.Title = model.Title;
                    item.AppLogo = model.AppLogo;

                    item.PhoneNumber = model.PhoneNumber;
                    item.MobilePhone = model.MobilePhone;
                    item.Email = model.Email;
                    item.Address = model.Address;

                    item.Facebook = model.Facebook;
                    item.Zalo = model.Zalo;
                    item.Youtube = model.Youtube;
                    item.Instagram = model.Instagram;

                    item.Policy = model.Policy;
                    item.Service = model.Service;

                    db.Entry(item).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    if (btnSubmit == "Continue") {
                        return RedirectToAction("Index");
                    }
                } else {
                    var item = new ConfigSetting();

                    item.Id = Guid.NewGuid();
                    item.AppNameShort = model.AppNameShort;
                    item.AppName = model.AppName;
                    item.Title = model.Title;
                    item.AppLogo = model.AppLogo;

                    item.PhoneNumber = model.PhoneNumber;
                    item.MobilePhone = model.MobilePhone;
                    item.Email = model.Email;
                    item.Address = model.Address;

                    item.Facebook = model.Facebook;
                    item.Zalo = model.Zalo;
                    item.Youtube = model.Youtube;
                    item.Instagram = model.Instagram;

                    item.Policy = model.Policy;
                    item.Service = model.Service;
                    db.ConfigSettings.Add(item);
                    await db.SaveChangesAsync();
                    if (btnSubmit == "Continue") {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }


    }
}