using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class ConfigSetting {

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Tên viết tắt")]
        public string AppNameShort { get; set; }

        [Display(Name = "Tên công ty")]
        public string AppName { get; set; }

        [Display(Name = "Logo công ty")]
        public string AppLogo { get; set; }

        [Display(Name = "Tiêu đề trang web")]
        public string Title { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Di động")]
        public string MobilePhone { get; set; }
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }
        [Display(Name = "Địa chỉ liên hệ")]
        public string Address { get; set; }

        [Display(Name = "Facebook")]
        public string Facebook { get; set; }
        [Display(Name = "Zalo")]
        public string Zalo { get; set; }
        [Display(Name = "Youtube")]
        public string Youtube { get; set; }
        [Display(Name = "Instagram")]
        public string Instagram { get; set; }

        [Display(Name = "Chính sách bán hàng")]
        public string Policy { get; set; }
        [Display(Name = "Dịch vụ")]
        public string Service { get; set; }
    }
}