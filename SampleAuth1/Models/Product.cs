using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleAuth1.Models
{
    public class Product
    {
        public int Id { get; set; }
                
        public string ASIN { set; get; }
        [DataType(DataType.Url)]
        public string DetailPageURL { get; set; }
        [DataType(DataType.ImageUrl)]
        public string LargeImage { get; set; }

        public string Title { get; set; }
        public string Brand { get; set; }
        public Int64 CategoryId { get; set; }
        public string OfferListingId { set; get; }
        public string OtherInfo { get; set; }


    }
}