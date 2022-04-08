using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace landSelling.Models.Entity
{
    public class admin
    {
        public int id { get; set; }
        public int uid { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
        public string address { get; set; }
    }
}