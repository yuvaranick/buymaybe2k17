using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleAuth1.Models
{
    public class WishList
    {
        public int Id { set; get; }
        [Required]
        public String  ApplicationUser_Id { get; set; }
        [Required]
        public int Product_Id { get; set; }
        public WishList()
        {

        }

        
    }
}