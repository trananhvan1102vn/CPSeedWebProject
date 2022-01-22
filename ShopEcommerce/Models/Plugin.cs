using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class Plugin {
        public Plugin() {
        }
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Tên Plugin")]
        public string Name { get; set; }

        [Display(Name = "Style")]
        public string Style { get; set; }
        [Display(Name = "Script")]
        public string Script { get; set; }
        [Display(Name = "Html")]
        public string Html { get; set; }

        [Display(Name = "Vị trí")]
        public int Postion { get; set; }


        [Display(Name = "Thứ tự")]
        public int OrderNo { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime AddedDate { get; set; }

        [Display(Name = "Người cập nhật")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Áp dụng")]
        public bool Show { get; set; }
    }
}