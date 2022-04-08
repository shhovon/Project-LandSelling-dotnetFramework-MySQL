using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace landSelling.Models.Entity
{
    public class postUserModel : userModel
    {
        public List<postModel> Posts { get; set; }
        public postUserModel()
        {
            Posts = new List<postModel>();
        }
    }
}