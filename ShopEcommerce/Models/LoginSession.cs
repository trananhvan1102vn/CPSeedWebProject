using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class LoginSession {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Đăng nhập lúc")]
        public DateTime LoginTime { get; set; }

        [Display(Name = "Đăng xuất lúc")]
        public DateTime? LogoffTime { get; set; }

        [Display(Name = "Host Name")]
        public string HostName { get; set; }
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [ForeignKey("UserCreated")]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        public ApplicationUser UserCreated { get; set; }
    }
}