using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class Subscription {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }
    }
}