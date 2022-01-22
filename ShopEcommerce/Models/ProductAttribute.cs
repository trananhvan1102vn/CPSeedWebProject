using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class ProductAttribute {
        [Key]
        public Guid Id { get; set; }

        [Display(Name ="Thuộc tính")]
        public string Key { get; set; }
        [Display(Name = "Giá trị")]
        public string Value { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}