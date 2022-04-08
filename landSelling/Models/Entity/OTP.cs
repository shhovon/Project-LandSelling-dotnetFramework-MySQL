using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace landSelling.Models.Entity
{
    public class OTP
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Please provide your otp code")]
        public string otp { get; set; }
    }
}