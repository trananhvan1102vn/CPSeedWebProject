using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class RequestProduct {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; }
        public string Email { get; set; }
        public bool Notification { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }
    }
}