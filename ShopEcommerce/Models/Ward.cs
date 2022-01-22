using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopEcommerce.Models {
    public class Ward {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Type { get; set; }
        [StringLength(50)]
        public string LatiLongTude { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
        public int DistrictId { get; set; }

        public int? SortOrder { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
    }
}