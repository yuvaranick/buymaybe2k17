using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SampleAuth1.Models
{
    public class UserCategories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CategoryId", Order = 1)]
        public Int64 CategoryId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ApplicationUser_Id", Order = 2)]
        public String ApplicationUser_Id { get; set; }

    }
}