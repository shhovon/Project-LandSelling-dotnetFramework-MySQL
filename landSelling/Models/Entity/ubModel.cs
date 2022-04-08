using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace landSelling.Models.Entity
{
    public class ubModel : buyerModel 
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}