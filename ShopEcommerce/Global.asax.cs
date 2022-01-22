using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CPSeed {
    public class MvcApplication : System.Web.HttpApplication {

        private ApplicationDbContext db = new ApplicationDbContext();
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_OnStart(object Sender, EventArgs E) {
            Application.Lock();
            var visitorCounter = db.VisitorCounters.FirstOrDefault();

            if (visitorCounter.LastUpdate.Date != DateTime.Today) {
                visitorCounter.LastUpdate = DateTime.Today;
                visitorCounter.Today = 0;
            }
            visitorCounter.Online += 1;
            visitorCounter.Today += 1;
            visitorCounter.Total += 1;
            db.Entry(visitorCounter).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            Application.UnLock();
        }
        protected void Session_OnEnd(object Sender, EventArgs E) {
            Application.Lock();
            var visitorCounter = db.VisitorCounters.FirstOrDefault();
            if (visitorCounter.Online > 0) {
                visitorCounter.Online -= 1;
            }
            if (visitorCounter.Today > 0) {
                visitorCounter.Today -= 1;
            }
            db.Entry(visitorCounter).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            Application.UnLock();
        }

    }
}
