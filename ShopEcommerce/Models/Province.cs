using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopEcommerce.Models {
    public class Province {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Type { get; set; }
        public int? TelephoneCode { get; set; }
        [StringLength(20)]
        public string ZipCode { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }

        public int? SortOrder { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }
    }
}