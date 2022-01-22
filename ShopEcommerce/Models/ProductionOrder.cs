using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class ProductionOrder {
        [Key]
        public Guid Id { get; set; }

        public virtual IList<CartItem> CartItems { get; set; }

        [Display(Name = "Số đơn hàng")]
        public string PONumber { get; set; }
        [Display(Name = "Họ tên")]
        public string Name { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }
        [Display(Name = "Ghi chú")]
        public string Note { get; set; }
        [Display(Name = "Địa chỉ giao hàng")]
        public string Address { get; set; }
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [ForeignKey("UserCreated")]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }
        [Display(Name = "Ngày đặt hàng")]
        public DateTime CreatedDate { get; set; }
        public ApplicationUser UserCreated { get; set; }

        [Display(Name = "Tổng thanh toán")]
        public decimal Total { get { return CartItems.Sum(x => x.Total); } }

        [Display(Name = "Số lượng")]
        public decimal Quantity { get { return CartItems.Sum(x => x.Quantity); } }
        public ProductionOrder() {
            CartItems = new List<CartItem>();
        }
    }
}