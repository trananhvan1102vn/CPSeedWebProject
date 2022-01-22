using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class CartItem {
        [Key]
        public Guid Id { get; set; }
        public string Thumail { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total { get { return Quantity * Price; } }

        public Guid ProductionOrderId { get; set; }
        [ForeignKey("ProductionOrderId")]
        public virtual ProductionOrder ProductionOrder { get; set; }

    }
}