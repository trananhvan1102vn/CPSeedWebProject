using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPSeed.Models {
    public class Topic {
        public Guid Id { get; set; }
        [Display(Name ="Loại chủ đề")]
        public string Type { get; set; }

        [AllowHtml]
        [DataType(DataType.Html)]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Display(Name = "STT")]
        public int OrderNo { get; set; }

        [Display(Name = "Cho phép xóa")]
        public bool AllowDelete { get; set; }

        [Display(Name = "Được xuất bản")]
        public bool IsPublish { get; set; }

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
    }
}