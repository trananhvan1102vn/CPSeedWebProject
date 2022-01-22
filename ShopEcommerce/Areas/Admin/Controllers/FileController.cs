using Microsoft.AspNet.Identity;
using CPSeed.Areas.Admin.Datasets;
using CPSeed.Areas.Admin.Reports;
using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Areas.Admin.Controllers {
    public class FileController : AdminBaseController {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult GenerateProductionOrder(Guid id) {
            ProductionOrderDataSet ds = new ProductionOrderDataSet();
            ProductionOrderDataSet.ProductionOrderRow poRow = ds.ProductionOrder.NewProductionOrderRow();
            ProductionOrder po = db.ProductionOrders.SingleOrDefault(s => s.Id == id);
            ConfigSetting configSetting = db.ConfigSettings.FirstOrDefault();
            if (configSetting == null) {
                configSetting = new ConfigSetting();
            }
            if (po == null) {
                return null;
            }
            poRow.PONumber = po.PONumber;
            poRow.Name = po.Name;
            poRow.PhoneNumber = po.PhoneNumber;
            poRow.Email = po.Email;
            poRow.Note = po.Note;
            poRow.Address = po.Address;
            poRow.CreatedDate = po.CreatedDate;
            poRow.CreatedBy = po.CreatedBy;
            poRow.Total = po.Total;
            poRow.Quantity = po.Quantity;
            poRow.ShopName = configSetting.AppName;
            poRow.ShopAddress = configSetting.Address;
            poRow.ShopPhoneNumber = configSetting.PhoneNumber + (!string.IsNullOrEmpty(configSetting.MobilePhone) ? " - " + configSetting.MobilePhone : "");
            poRow.PaymentMethod = "Thanh toán khi nhận hàng (COD)";

            poRow.USER_DEFINE = "";

            poRow.PrintedBy = User.Identity.GetUserName();
            poRow.PrintedDate = DateTime.Now;

            ds.ProductionOrder.AddProductionOrderRow(poRow);

            int counter = 1;
            foreach (var item in po.CartItems) {
                ProductionOrderDataSet.CartItemRow itemRow = ds.CartItem.NewCartItemRow();
                itemRow.OrderNo = counter++;
                itemRow.ProductName = item.ProductName;
                itemRow.Quantity = item.Quantity;
                itemRow.Price = item.Price;
                itemRow.Total = item.Total;
                ds.CartItem.AddCartItemRow(itemRow);
            }
            rptProductionOrder report = new rptProductionOrder();
            report.SetDataSource((DataTable)ds.ProductionOrder);
            report.Subreports["rptCartItem"].SetDataSource((DataTable)ds.CartItem);
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}