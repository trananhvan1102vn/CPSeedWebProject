using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPSeed.Models {
    public class Menu {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string ActionName { get; set; }
        public bool IsGroupHeader { get; set; }
        public string ModuleName { get; set; }
        public string Link { get; set; }
        public int OrderNo { get; set; }
        public int GroupOrder { get; set; }
        public string CssClass { get; set; }
        public string Target { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Menu Parent { get; set; }

        public virtual IList<Menu> MenuChilds { get; set; }

        public virtual IList<ApplicationRole> Roles { get; set; }

        //public string RoleId { get; set; }
        //[ForeignKey("RoleId")]
        //public virtual IdentityRole Role { get; set; }
    }
}