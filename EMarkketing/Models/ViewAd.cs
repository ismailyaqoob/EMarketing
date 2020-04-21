using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EMarkketing.Models
{
    public class ViewAd
    {
        public int pro_id { get; set; }
        [Display(Name = "Name")]
        public string pro_name { get; set; }
        [Display(Name = "Decription")]
        public string pro_des { get; set; }
        [Display(Name = "Price")]
        public int pro_price { get; set; }
        public string pro_image { get; set; }
        public Nullable<int> pro_fk_cat { get; set; }
        public Nullable<int> pro_fk_user { get; set; }
        [Display(Name = "Name")]
        public string u_name { get; set; }
        [Display(Name = "Phone Number")]
        public string u_contact { get; set; }
        public string u_image { get; set; }
        [Display(Name = "Name")]
        public string cat_name { get; set; }

    }
}