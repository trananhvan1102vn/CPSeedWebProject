using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class BannerCategory {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "STT")]
        public int OrderNo { get; set; }

        [Display(Name = "Được xuất bản")]
        public bool? IsPublish { get; set; }

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

        public virtual IList<Banner> Banners { get; set; }

        public BannerCategory() {
            Banners = new List<Banner>();
        }
    }
}