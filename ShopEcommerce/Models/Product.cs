using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class Product {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "SKU")]
        public string SKU { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Ảnh thu nhỏ")]
        public string Thumnail { get; set; }

        [Display(Name = "Giá")]
        public decimal Price { get; set; }
        [Display(Name = "Nơi sản xuất")]
        public string Manufacturer { get; set; }
        [Display(Name = "Bảo hành")]
        public string Warranty { get; set; }
        [Display(Name = "Mô tả ngắn")]
        public string Description { get; set; }
        [Display(Name = "Màu sắc")]
        public string Color { get; set; }
        [Display(Name = "Giao hàng")]
        public string Delivery { get; set; }

        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
        [Display(Name = "Kích cỡ")]
        public string Size { get; set; }

        [Display(Name = "Mô tả chi tiết")]
        public string DetailOverview { get; set; }
        [Display(Name = "STT")]
        public int OrderNo { get; set; }

        [Display(Name = "Được xuất bản")]
        public bool? IsPublish { get; set; }

        [Display(Name = "Là sản phẩm hot")]
        public bool? IsHot { get; set; }
        [Display(Name = "Là sản phẩm nổi bật")]
        public bool? IsFeatured { get; set; }
        public int ViewCount { get; set; }

        [ForeignKey("UserCreated")]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        public ApplicationUser UserCreated { get; set; }

        [ForeignKey("UserModified")]
        [Display(Name = "Người cập nhật")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public DateTime? ModifiedDate { get; set; }
        public ApplicationUser UserModified { get; set; }


        public virtual IList<ProductCategory> ProductCategories { get; set; }
        public virtual IList<ProductImage> ProductImages { get; set; }

        [Display(Name= "Meta keywords")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Meta description")]
        public string MetaDescription { get; set; }
        [Display(Name = "Meta title	")]
        public string MetaTitle { get; set; }
        [Display(Name = "Search engine friendly page name")]
        public string SeName { get; set; }

        public IList<ProductTag> ProductTags { get; set; }
        public virtual IList<CartItem> CartItems { get; set; }
        public virtual IList<ProductAttribute> ProductAttributes { get; set; }
        public Product() {
            ProductCategories = new List<ProductCategory>();
            ProductImages = new List<ProductImage>();
            ProductTags = new List<ProductTag>();
            CartItems = new List<CartItem>();
            ProductAttributes = new List<ProductAttribute>();
        }
    }
}