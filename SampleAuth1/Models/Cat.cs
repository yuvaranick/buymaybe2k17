using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SampleAuth1.Models
{
    public class Cat
    {

        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Cat() { }
    }
}