using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class CodeTable {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public bool Continuous { get; set; }
    }
}