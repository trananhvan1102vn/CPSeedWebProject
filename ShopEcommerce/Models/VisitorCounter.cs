using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class VisitorCounter {
        [Key]
        public int Id { get; set; }
        public int Online { get; set; }
        public int Today { get; set; }
        public int Total { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}