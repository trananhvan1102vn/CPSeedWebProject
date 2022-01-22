using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class ProductImage {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Link { get; set; }

        [Display(Name = "Tiêu đề")]
        public string FileName { get; set; }

        [Display(Name = "STT")]
        public int OrderNo { get; set; }
        [Display(Name = "Alt")]
        public string Alt { get; set; }

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

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Display(Name = "Sản phẩm")]
        public Guid ProductId { get; set; }
    }
}