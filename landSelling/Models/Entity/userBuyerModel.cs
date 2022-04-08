using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace landSelling.Models.Entity
{
    public class userBuyerModel
    {
        /*public int id { get; set; }
        [Required(ErrorMessage = "Please provide username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please provide your password")]
        public string password { get; set; }
        public string role { get; set; }*/


        public int id { get; set; }
        public int uid { get; set; }
        [Required(ErrorMessage = "Field couldn't be empty!")]
        public string username { get; set; }
        [Required(ErrorMessage = "Field couldn't be empty!")]
        public string name { get; set; }
        [Required(ErrorMessage = "Field couldn't be empty!")]
        public string email { get; set; }
        [Required(ErrorMessage = "Field couldn't be empty!")]
        public string password { get; set; }
        [Required(ErrorMessage = "Field couldn't be empty!")]
        public string password2 { get; set; }
        public string occupation { get; set; }
        public Nullable<int> netincome { get; set; }
    }
}