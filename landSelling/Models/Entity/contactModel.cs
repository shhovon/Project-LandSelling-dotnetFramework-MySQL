using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace landSelling.Models.Entity
{
    public class contactModel
    {
        [Required(ErrorMessage = "Field couldn't be empty!")]
        public string message { get; set; }
        [Required(ErrorMessage = "Field couldn't be empty!")]
        public string subject { get; set; }

        [Required(ErrorMessage = "Field couldn't be empty!")]
        public string name { get; set; }

        [Required(ErrorMessage = "Field couldn't be empty!")]
        public string email { get; set; }
    }
}