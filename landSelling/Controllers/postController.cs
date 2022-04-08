using landSelling.Authorization;
using landSelling.Models.Database;
using landSelling.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace landSelling.Controllers
{
    public class postController : Controller
    {
        // GET: post
        [buyerSellerAccess]
        [HttpGet]
        public ActionResult SingleProperty(int id)
        {
            var db = new landSellingEntity();
            var postInfo = (from e in db.posts
                            where e.id == id
                            select e).FirstOrDefault();
            return View(postInfo);
        }


        [buyerAccess]
        public ActionResult request(int postid, int bidprice)
        {
            var db = new landSellingEntity();
            var sn = Session["UserName"].ToString();
            var userinfo = (from e in db.users
                            where e.username.Equals(sn)
                            select e).FirstOrDefault();
            var duplicate = (from e in db.requests
                            where e.postid == postid && e.userid == userinfo.id
                            select e).FirstOrDefault();
            if (duplicate == null)
            {
                var post = new request();
                post.postid = postid;
                post.userid = userinfo.id;
                post.bidprice = bidprice;
                post.status = "pending";
                post.mark = "unread";
                post.date = DateTime.Now;
                db.requests.Add(post);
                db.SaveChanges();
                return RedirectToAction("BuyerBids");
            }
            else
            {
                if(duplicate.bidprice > bidprice)
                {
                    TempData["msg"] = "Cann't decrease the price than previous bid price!";
                    return RedirectToAction("SingleProperty", new { @id = postid });
                }
                else
                {
                    duplicate.bidprice = bidprice;
                    db.SaveChanges();
                    TempData["msg"] = "Bid price updated!";
                    return RedirectToAction("BuyerBids");
                }
                
            }
        }
        [buyerAccess]
        public ActionResult BuyerBids()
        {
            var db = new landSellingEntity();
            var sn = Session["UserName"].ToString();
            var userinfo = (from e in db.users
                            where e.username.Equals(sn)
                            select e).FirstOrDefault();
            var postInfo = (from e in db.requests 
                            where e.userid == userinfo.id
                            select e).ToList();
            return View(postInfo);
        }


    }
}