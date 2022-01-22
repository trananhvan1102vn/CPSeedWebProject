using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class KeyData {
        [Key]
        public int Id { get; set; }
        public string KeyType { get; set; }
        public int KeyYear { get; set; }
        public int KeyMonth { get; set; }
        public int KeyValue { get; set; }
        public int KeyDay { get; set; }

    }
}