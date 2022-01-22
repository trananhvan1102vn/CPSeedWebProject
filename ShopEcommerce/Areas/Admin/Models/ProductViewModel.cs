using CPSeed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPSeed.Areas.Admin.Models {
    public class ProductViewModel {

        [Display(Name = "SKU")]
        public string SKU { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Danh mục")]
        public List<Guid?> CategoryIds { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Từ ngày")]
        public DateTime? DateFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Đến ngày")]
        public DateTime? DateTo { get; set; }

        public List<Product> Results { get; set; }
        public ProductViewModel() {
            Results = new List<Product>();
            CategoryIds = new List<Guid?>();
        }
    }
}