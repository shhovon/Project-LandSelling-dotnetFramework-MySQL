using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace landSelling.Models.Entity
{
    public class requestModel
    {
        public int id { get; set; }
        public int postid { get; set; }
        public int userid { get; set; }
        public string status { get; set; }
        public string mark { get; set; }
        public System.DateTime date { get; set; }

        [Required(ErrorMessage = "Field couldn't be empty!")]
        public int bidprice { get; set; }
    }
}