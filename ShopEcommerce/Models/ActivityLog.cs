using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class ActivityLog {
        [Key]
        public Guid Id { get; set; }

        public string Module { get; set; }
        public string ObjectType { get; set; }
        public string ObjectId { get; set; }
        public string ObjectCode { get; set; }
        public string ObjectName { get; set; }
        public string Action { get; set; }
        public string Notes { get; set; }
        public string OldVal { get; set; }
        public string NewVal { get; set; }

        [ForeignKey("UserCreated")]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        public ApplicationUser UserCreated { get; set; }
    }
}