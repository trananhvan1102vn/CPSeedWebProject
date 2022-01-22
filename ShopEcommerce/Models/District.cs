using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopEcommerce.Models {
    public class District {
        [Key]
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Type { get; set; }
        [StringLength(50)]
        public string LatiLongTude { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }
        public int ProvinceId { get; set; }

        public int ?SortOrder { get; set; }
        public bool? IsPublished { get; set; }
        public bool ?IsDeleted { get; set; }
    }
}