using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class ProductTag {
        [Key]
        public Guid Id { get; set; }
        [Display(Name="Tên thẻ")]
        public string Name { get; set; }

        public IList<Product> Products { get; set; }

        public ProductTag() {
            Products = new List<Product>();
        }
    }
}