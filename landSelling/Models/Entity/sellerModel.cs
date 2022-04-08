using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace landSelling.Models.Entity
{
    public class sellerModel
    {
        public int id { get; set; }
        public int uid { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
        public string presentaddress { get; set; }
        public string permanentaddress { get; set; }
        public string facebooklink { get; set; }
        public Nullable<int> whatsappno { get; set; }
        public string occupation { get; set; }
        public userModel userOb { get; set; }
    }
}