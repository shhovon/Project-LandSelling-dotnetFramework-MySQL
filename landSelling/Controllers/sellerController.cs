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
    [Authorize]
    public class sellerController : Controller
    {
        // GET: seller

        [HttpGet]
        public ActionResult ViewProfile()
        {
            var sn = Session["UserName"].ToString();
            var db = new landSellingEntity();
            var u = (from e in db.sellers
                     where e.user.username.Equals(sn)
                     select e).FirstOrDefault();
            var usM = new usModel();
            usM.id = u.id;
            usM.uid = u.uid;
            usM.name = u.name;
            usM.email = u.email;
            usM.phone = u.phone;
            usM.presentaddress = u.presentaddress;
            usM.permanentaddress = u.permanentaddress;
            usM.facebooklink = u.facebooklink;
            usM.whatsappno = u.whatsappno;
            usM.occupation = u.occupation;
            usM.userid = u.user.id;
            usM.username = u.user.username;
            usM.password = u.user.password;
            return View(usM);
        }

        [HttpPost]
        public ActionResult ViewProfile(usModel ed)
        {
            var db = new landSellingEntity();
            var usernameV = Session["UserName"].ToString();
            var usr = (from e in db.users
                       where e.username.Equals(usernameV)
                       select e).FirstOrDefault();

            var edituserlis = (from e in db.sellers
                               where e.uid == usr.id
                               select e).FirstOrDefault();
            edituserlis.name = ed.name;
            edituserlis.email = ed.email;
            edituserlis.phone = ed.phone;
            edituserlis.presentaddress = ed.presentaddress;
            edituserlis.permanentaddress = ed.permanentaddress;
            edituserlis.facebooklink = ed.facebooklink;
            edituserlis.whatsappno = ed.whatsappno;
            edituserlis.occupation = ed.occupation;
            db.SaveChanges();

            var chgdb = (from e in db.users
                         where e.id == usr.id
                         select e).FirstOrDefault();
            chgdb.password = ed.password;

            db.SaveChanges();
            return RedirectToAction("ViewProfile");
        }



        [sellerAccess]
        [HttpGet]
        public ActionResult AddPost()
        {
            return View(new postModel());
        }
        [HttpPost]

        public ActionResult AddPost(postModel value)
        {
            if (ModelState.IsValid)
            {
                var db = new landSellingEntity();
                var usernameV = Session["UserName"].ToString();
                var userid = (from e in db.users
                              where e.username.Equals(usernameV)
                              select e).FirstOrDefault();
                var p = new post();
                p.title = value.title;
                p.description = value.description;
                p.location = value.location;
                //string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                //file.SaveAs(path);
                p.price = value.price;
                p.propertyType = value.propertyType;
                p.status = "pending";
                p.mark = "unread";
                p.date = DateTime.Now;
                p.area = value.area;
                p.beds = value.beds;
                p.baths = value.baths;
                p.garage = value.garage;
                p.uid = userid.id;
                db.posts.Add(p);
                
                db.SaveChanges();
                return RedirectToAction("DisplayPost");
            }
            return View(value);
        }

        public ActionResult DisplayPost()
        {
            var db = new landSellingEntity();
            var data = (from e in db.posts
                        where e.status.Equals("Approve")
                        select e).ToList();
            return View(data);
        }

        [sellerAccess]
        public ActionResult MyPost()
        {
            var db = new landSellingEntity();
            var usernameV = Session["UserName"].ToString();
            var userid = (from e in db.users
                          where e.username.Equals(usernameV)
                          select e).FirstOrDefault();
            var data = (from e in db.posts
                        where e.uid == userid.id
                        select e).ToList();
            return View(data);
        }
        [sellerAccess]
        public ActionResult DeletePost(int id)
        {
            var db = new landSellingEntity();
            //var usernameV = Session["UserName"].ToString();
            var postList = (from e in db.requests
                          where e.postid == id
                          select e).ToList();
            foreach(var li in postList)
            {
                db.requests.Remove(li);
            }
            db.SaveChanges();
            var data = (from e in db.posts
                        where e.id == id
                        select e).FirstOrDefault();
            db.posts.Remove(data);
            db.SaveChanges();
            return RedirectToAction("MyPost");
        }
        [sellerAccess]
        public ActionResult viewPostDetails(int id)
        {
            var db = new landSellingEntity();
            var req = (from e in db.requests
                       where e.postid == id
                       select e).ToList();
            ViewBag.post = (from e in db.posts
                            where e.id == id
                            select e).FirstOrDefault();
            return View(req);
        }
        public ActionResult AcceptBid(int id)
        {
            var db = new landSellingEntity();
            var req = (from e in db.requests
                       where e.id == id
                       select e).FirstOrDefault();
            var li = (from e in db.requests
                            where e.postid == req.postid
                            select e).ToList();
            foreach(var i in li)
            {
                if(i.id != req.id)
                {
                    db.requests.Remove(i);
                }
            }
            db.SaveChanges();
            req.status = "sold";
            db.SaveChanges();
            var postInfo = (from e in db.posts
                       where e.id == req.postid
                       select e).FirstOrDefault();
            postInfo.status = "sold";
            db.SaveChanges();
            return RedirectToAction("viewPostDetails" ,new { @id = req.postid});
        }


    }

}