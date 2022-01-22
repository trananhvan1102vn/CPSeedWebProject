using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPSeed.Areas.Admin.Models {
    public class ProductionOrderViewModel {

        [Display(Name = "Số đơn hàng")]
        public string PONumber { get; set; }
        [Display(Name = "Họ tên Khách hàng")]
        public string CustomerName { get; set; }
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Từ ngày")]
        public DateTime? DateFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Đến ngày")]
        public DateTime? DateTo { get; set; }

        public List<ProductionOrder> Results { get; set; }
        public ProductionOrderViewModel() {
            Results = new List<ProductionOrder>();
        }
    }
}