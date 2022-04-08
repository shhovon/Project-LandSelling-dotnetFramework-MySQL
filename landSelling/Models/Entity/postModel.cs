using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace landSelling.Models.Entity
{
    public class postModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Field can't be empty!")]
        public string title { get; set; }
        [Required(ErrorMessage = "Field can't be empty!")]
        public string description { get; set; }
        [Required(ErrorMessage = "Field can't be empty!")]
        public string location { get; set; }
        public string image { get; set; }
        [Required(ErrorMessage = "Field can't be empty!")]
        public int price { get; set; }
        public string propertyType { get; set; }
        public string status { get; set; }
        public string mark { get; set; }
        public System.DateTime date { get; set; }
        [Required(ErrorMessage = "Field can't be empty!")]
        public int area { get; set; }
        [Required(ErrorMessage = "Field can't be empty!")]
        public int beds { get; set; }
        [Required(ErrorMessage = "Field can't be empty!")]
        public int baths { get; set; }
        [Required(ErrorMessage = "Field can't be empty!")]
        public int garage { get; set; }
        public int uid { get; set; }
    }
}