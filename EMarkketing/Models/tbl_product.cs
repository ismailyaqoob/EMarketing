//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMarkketing.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_product
    {
        public int pro_id { get; set; }
        [Display(Name ="Name")]
        [Required(ErrorMessage ="Name is required")]
        public string pro_name { get; set; }
        [Display(Name = "Decription")]
        [Required(ErrorMessage = "Decription is required")]
        public string pro_des { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        public int pro_price { get; set; }
        [Display(Name = "Image")]
        public string u_image { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is required")]
        public Nullable<int> pro_fk_cat { get; set; }
        public Nullable<int> pro_fk_user { get; set; }
    
        public virtual tbl_category tbl_category { get; set; }
        public virtual tbl_user tbl_user { get; set; }
    }
}
