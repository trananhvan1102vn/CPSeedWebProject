using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopEcommerce.Models {
    public class Country {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string CountryCode { get; set; }
        [StringLength(100)]
        public string CommonName { get; set; }
        [StringLength(100)]
        public string FormalName { get; set; }
        [StringLength(100)]
        public string CountryType { get; set; }
        [StringLength(100)]
        public string CountrySubType { get; set; }
        [StringLength(100)]
        public string Sovereignty { get; set; }
        [StringLength(100)]
        public string Capital { get; set; }
        [StringLength(100)]
        public string CurrencyCode { get; set; }
        [StringLength(100)]
        public string CurrencyName { get; set; }
        [StringLength(100)]
        public string TelephoneCode { get; set; }
        [StringLength(100)]
        public string CountryCode3 { get; set; }
        [StringLength(100)]
        public string CountryNumber { get; set; }
        [StringLength(100)]
        public string InternetCountryCode { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsPublished { get; set; }
        [StringLength(50)]
        public string Flags { get; set; }
        public bool? IsDeleted { get; set; }
    }
}