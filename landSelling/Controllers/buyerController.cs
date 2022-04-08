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
    public class buyerController : Controller
    {
        // GET: buyer
        [buyerAccess]
        [HttpGet]
        public ActionResult ViewBuyerProfile()
        {
            var sn = Session["UserName"].ToString();
            var db = new landSellingEntity();
            var u = (from e in db.buyers
                     where e.user.username.Equals(sn)
                     select e).FirstOrDefault();
            var usM = new ubModel();
            usM.id = u.id;
            usM.uid = u.uid;
            usM.name = u.name;
            usM.email = u.email;
            usM.occupation = u.occupation;
            usM.netincome = u.netincome;
            usM.userid = u.user.id;
            usM.username = u.user.username;
            usM.password = u.user.password;
            return View(usM);
        }
        [buyerAccess]
        [HttpPost]
        public ActionResult ViewBuyerProfile(ubModel ed)
        {
            var db = new landSellingEntity();
            var usernameV = Session["UserName"].ToString();
            var usr = (from e in db.users
                       where e.username.Equals(usernameV)
                       select e).FirstOrDefault();

            var edituserlis = (from e in db.buyers
                               where e.uid == usr.id
                               select e).FirstOrDefault();
            edituserlis.name = ed.name;
            edituserlis.email = ed.email;
            edituserlis.netincome = ed.netincome;
            edituserlis.occupation = ed.occupation;
            db.SaveChanges();

            var chgdb = (from e in db.users
                         where e.id == usr.id
                         select e).FirstOrDefault();
            chgdb.password = ed.password;

            db.SaveChanges();
            return RedirectToAction("ViewBuyerProfile");
        }
        [buyerAccess]
        public ActionResult DeleteBid(int id)
        {
            var db = new landSellingEntity();
            var data = (from e in db.requests
                       where e.id == id
                       select e).FirstOrDefault();
            db.requests.Remove(data);
            db.SaveChanges();
            return RedirectToAction("BuyerBids","post");
        }
    }
}