using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SampleAuth1.Models
{
    public class Category
    {
        [Column(TypeName = "bigint")]
        public Int64 CategoryId { get; set;}
        public string CategoryName { get; set;}
        [NotMapped]
        public Boolean Selected { get; set; }
        public Category()
        {
            this.Selected = false;
        }

    }
}