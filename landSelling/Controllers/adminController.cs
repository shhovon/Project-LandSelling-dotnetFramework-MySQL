using AutoMapper;
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
    public class adminController : Controller
    {
        // GET: dashboard
        [AdminAccess]
        public ActionResult Dashboard()
        {
            var db = new landSellingEntity();
            var user = db.users.ToList().Count();
            var posts = db.posts.ToList().Count();
            var activeUsers = (from e in db.users
                               where e.status == "active"
                               select e).ToList().Count();
            var blockUsers = (from e in db.users
                              where e.status == "Blocked"
                              select e).ToList().Count();
            ViewBag.TotalUser = user;
            ViewBag.TotalPost = posts;
            ViewBag.ActiveUser = activeUsers;
            ViewBag.BlockedPost = blockUsers;

            // user.status = "active";
            // db.SaveChanges();
            // return RedirectToAction("BlockUser");
            return View();
        }

        [AdminAccess]
        public ActionResult ViewPost()
        {
            var db = new landSellingEntity();
            var postList = db.posts.ToList();
            return View(postList);
        }

        [AdminAccess]
        public ActionResult PostDetails(int id)
        {
            var db = new landSellingEntity();
            var post = (from e in db.posts
                        where e.id == id
                        select e).FirstOrDefault();
            post.mark = "read";
            db.SaveChanges();
            var ddb = new landSellingEntity();
            var userlist = ddb.sellers;
            var u = (from e in userlist
                     where e.uid == post.uid
                     select e).ToList();
            ViewBag.post = post;
            //ViewBag.ulist = u;
            /*var ddb = new landSellingEntities();
            var config = new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<user, userModel>();
                    cfg.CreateMap<seller, sellerModel>();
                }
                );
            var userlist = db.sellers.ToList();
            Mapper mapper = new Mapper(config);
            var data = mapper.Map<List<usModel>>(userlist);*/
            return View(u);
        }
        [AdminAccess]
        public ActionResult AcceptPost(int id)
        {
            var db = new landSellingEntity();
            var post = (from e in db.posts
                        where e.id == id
                        select e).FirstOrDefault();
            post.status = "Approve";
            db.SaveChanges();
            return RedirectToAction("PostDetails", new { @id = id });
        }
        [AdminAccess]
        public ActionResult DeletePost(int id)
        {
            var db = new landSellingEntity();
            var reqPost = (from e in db.requests
                        where e.postid == id
                        select e).ToList();
            foreach(var item in reqPost)
            {
                db.requests.Remove(item);
            }
            db.SaveChanges();
            var post = (from e in db.posts
                        where e.id == id
                        select e).FirstOrDefault();
            db.posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("ViewPost");
        }
        public ActionResult MakePending(int id)
        {
            var db = new landSellingEntity();
            var post = (from e in db.posts
                        where e.id == id
                        select e).FirstOrDefault();
            post.status = "pending";
            db.SaveChanges();
            return RedirectToAction("PostDetails", new { @id = id });
        }

        //shanto
        [onlyAdminAccess]
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [onlyAdminAccess]
        [HttpPost]
        public ActionResult AddUser(userAdminModel u)
        {
            landSellingEntity db = new landSellingEntity();

            if (ModelState.IsValid)
            {
                var useradd = new user();
                useradd.username = u.username;
                useradd.password = u.password;
                useradd.role = u.role;
                useradd.status =  "active";

                db.users.Add(useradd);
                db.SaveChanges();

                var Admin = new administration();
                Admin.uid = useradd.id;
                Admin.name = u.name;
                Admin.email = u.email;
                Admin.phone = u.phone;
                Admin.address = u.address;

                db.administrations.Add(Admin);
                db.SaveChanges();

                return RedirectToAction("ViewUser");
            }
            return View();
        }

        public ActionResult ViewUser()
        {
            landSellingEntity db = new landSellingEntity();

            var data = db.administrations.ToList();
            var userlist = new List<userAdminModel>();
            foreach (var item in data)
            {
                var v = new userAdminModel();
                v.uid = item.uid;
                v.name = item.name;
                v.email = item.email;
                v.phone = item.phone;
                v.address = item.address;
                v.username = item.user.username;
                v.password = item.user.password;
                v.role = item.user.role;
                userlist.Add(v);
            }



            return View(userlist);
        }

        [onlyAdminAccess]
        [HttpGet]
        public ActionResult userEdit(int id)
        {

            landSellingEntity db = new landSellingEntity();
            var data = db.administrations;
            //var edituserlist = new List<userAdminModel>();

            var uam = new userAdminModel();

            var edituser = (from s in data
                            where s.uid == id
                            select s).FirstOrDefault();

            uam.uid = edituser.uid;
            uam.username = edituser.user.username;
            uam.password = edituser.user.password;
            uam.role = edituser.user.role;
            uam.name = edituser.name;
            uam.email = edituser.email;
            uam.phone = edituser.phone;
            uam.address = edituser.address;

            return View(uam);
        }

        [onlyAdminAccess]
        [HttpPost]
        public ActionResult userEdit(userAdminModel b)
        {
            landSellingEntity db = new landSellingEntity();
            var data = db.administrations;

            var edituserlis = (from s in data
                               where s.uid == b.uid
                               select s).FirstOrDefault();

            edituserlis.name = b.name;
            edituserlis.email = b.email;
            edituserlis.phone = b.phone;
            edituserlis.address = b.address;
            db.SaveChanges();
            var data2 = db.users;

            var chgdb = (from s in data2
                         where s.id == b.uid
                         select s).FirstOrDefault();
            chgdb.password = b.password;
            chgdb.role = b.role;

            db.SaveChanges();
            return RedirectToAction("ViewUser");
        }


        [onlyAdminAccess]
        public ActionResult userDelete(int id)
        {
            landSellingEntity db = new landSellingEntity();
            var data3 = db.administrations;

            var deletedata = (from s in data3
                              where s.uid == id
                              select s).FirstOrDefault();
            db.administrations.Remove(deletedata);
            db.SaveChanges();

            var data4 = db.users;

            var deletedata4 = (from s in data4
                               where s.id == id
                               select s).FirstOrDefault();
            db.users.Remove(deletedata4);
            db.SaveChanges();
            return RedirectToAction("ViewUser");

        }
        [onlyAdminAccess]
        public ActionResult BlockUser()
        {
            var db = new landSellingEntity();
            var user = db.users.ToList();
            return View(user);
        }

        [onlyAdminAccess]
        public ActionResult BlockUserB(int id)
        {
            var db = new landSellingEntity();
            var user = (from e in db.users
                        where e.id == id
                        select e).FirstOrDefault();
            user.status = "Blocked";
            db.SaveChanges();
            return RedirectToAction("BlockUser");
        }
        [onlyAdminAccess]
        public ActionResult UnblockUser(int id)
        {

            var db = new landSellingEntity();
            var user = (from e in db.users
                        where e.id == id
                        select e).FirstOrDefault();
            user.status = "active";
            db.SaveChanges();
            return RedirectToAction("BlockUser");


        }




    }
}