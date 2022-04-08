﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace landSelling.Models.Entity
{
    public class userAdminModel : admin
    {
        public int userid { get; set; }
        [Required(ErrorMessage = "Please provide username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please provide your password")]
        public string password { get; set; }
        public string role { get; set; }
    }
}