using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using landSelling.Models.Database;
using landSelling.Email;
using landSelling.Models.Entity;

namespace landSelling.Controllers
{
    public class authController : Controller
    {
        // GET: auth
        [HttpGet]
        public ActionResult Login()
        {
            return View(new userModel());
        }
        [HttpPost]
        public ActionResult Login(userModel u)
        {
            if (ModelState.IsValid)
            {
                var db = new landSellingEntity();
                var val = (from e in db.users
                           where e.username.Equals(u.username) &&
                           e.password.Equals(u.password)
                           select e).FirstOrDefault();
                if (val != null)
                {
                    if (val.status.Equals("inactive"))
                    {
                        string useremail = "";
                        if (val.role.Equals("seller")) 
                        {
                            var uMail = (from e in db.sellers where e.uid == val.id select e).FirstOrDefault();
                            useremail = uMail.email;
                        }
                        if (val.role.Equals("buyer")) 
                        {
                            var uMail = (from e in db.buyers  where e.uid == val.id select e).FirstOrDefault();
                            useremail = uMail.email;
                        }
                        
                        Random rnd = new Random();
                        Session["id"] = val.id;
                        Session["OTP"] = rnd.Next(10000, 99999);
                        var mail = new EmailClass();
                        mail.SendEmail(useremail, "OTP Code", Session["OTP"].ToString());
                        return RedirectToAction("OtpValidate");
                    }
                    else if (val.status.Equals("Blocked"))
                    {
                        TempData["msg"] = "Your account has been blocked for some reason, contact with the admin!";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(val.username, false);
                        Session["UserName"] = val.username;
                        Session["UserType"] = val.role;
                        if (val.role.Equals("admin"))
                        {
                            return RedirectToAction("Dashboard", "admin");
                        }
                        else if (val.role.Equals("employee"))
                        {
                            return RedirectToAction("Dashboard", "admin");
                        }
                        else if (val.role.Equals("buyer"))
                        {
                            return RedirectToAction("ViewBuyerProfile", "buyer");
                        }
                        else if (val.role.Equals("seller"))
                        {
                            return RedirectToAction("ViewProfile", "seller");
                        }
                    }
                }
                else 
                {
                    TempData["msg"] = "Invalid username and password provided!";
                    return RedirectToAction("Login");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult OtpValidate()
        {
            return View(new OTP());
        }
        [HttpPost]
        public ActionResult OtpValidate(OTP obj)
        {
            if(ModelState.IsValid)
            {
                if(Session["id"] != null && Session["OTP"] != null) 
                {
                    obj.id = Int16.Parse(Session["id"].ToString());
                    if(Session["OTP"].ToString().Equals(obj.otp.ToString()))
                    {
                        var db = new landSellingEntity();
                        var userDetails = (from e in db.users 
                                          where e.id == obj.id 
                                          select e).FirstOrDefault();
                        userDetails.status = "active";
                        db.SaveChanges();
                        FormsAuthentication.SetAuthCookie(userDetails.username, false);
                        Session["UserName"] = userDetails.username;
                        Session["UserType"] = userDetails.role;
                        Session["id"] = null;
                        Session["OTP"] = null;
                        if (userDetails.role.Equals("admin"))
                        {
                            return RedirectToAction("Dashboard", "admin");
                        }
                        else if (userDetails.role.Equals("employee"))
                        {
                            return RedirectToAction("Dashboard", "admin");
                        }
                        else if (userDetails.role.Equals("buyer"))
                        {
                            return RedirectToAction("ViewBuyerProfile", "buyer");
                        }
                        else if (userDetails.role.Equals("seller"))
                        {
                            return RedirectToAction("ViewProfile", "seller");
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Wrong OTP!";
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            return View(obj);
        }
        [HttpGet]
        public ActionResult SignUpAsSeller()
        {
            ViewBag.err = null;
            return View(new userSellerModel());
        }
        
        
        [HttpPost]
        public ActionResult SignUpAsSeller(userSellerModel value)
        {
            if (ModelState.IsValid)
            {
                if (value.password == value.password2)
                {
                    var db = new landSellingEntity();
                    var userCount = (from e in db.users
                                     where e.username == value.username
                                     select e).FirstOrDefault();
                    if (userCount == null)
                    {
                        var u = new user();
                        u.username = value.username;
                        u.password = value.password;
                        u.role = "seller";
                        u.status = "inactive";
                        db.users.Add(u);
                        db.SaveChanges();
                        var s = new seller();
                        s.uid = u.id;
                        s.name = value.name;
                        s.email = value.email;
                        s.phone = value.phone;
                        s.presentaddress = value.presentaddress;
                        s.permanentaddress = value.permanentaddress;
                        s.facebooklink = value.facebooklink;
                        s.whatsappno = value.whatsappno;
                        s.occupation = value.occupation;
                        db.sellers.Add(s);
                        db.SaveChanges();
                        Random rnd = new Random();
                        Session["id"] = u.id;
                        Session["OTP"] = rnd.Next(10000, 99999);
                        var mail = new EmailClass();
                        mail.SendEmail(value.email, "OTP Code", Session["OTP"].ToString());

                        return RedirectToAction("OtpValidate");
                    }
                    else
                    {
                        ViewBag.errun = "Username already exist!";
                        return View(value);
                    }
                }
                else
                {
                    ViewBag.err = "Password doesn't match!";
                    return View(value);
                    //RedirectToAction("SignUpAsSeller");
                }

            }
            return View(value);
        }
        [HttpGet]
        public ActionResult SignUpAsBuyer()
        {
            ViewBag.err = null;
            return View(new userBuyerModel());
        }
        [HttpPost]
        public ActionResult SignUpAsBuyer(userBuyerModel value)
        {
            if (ModelState.IsValid)
            {
                if (value.password == value.password2)
                {
                    var db = new landSellingEntity();
                    var userCount = (from e in db.users
                                     where e.username == value.username
                                     select e).FirstOrDefault();
                    if(userCount == null)
                    { 
                        var u = new user();
                        u.username = value.username;
                        u.password = value.password;
                        u.role = "buyer";
                        u.status = "inactive";
                        db.users.Add(u);
                        db.SaveChanges();
                        var s = new buyer();
                        s.uid = u.id;
                        s.name = value.name;
                        s.email = value.email;
                        s.occupation = value.occupation;
                        s.netincome = value.netincome;
                        db.buyers.Add(s);
                        db.SaveChanges();
                        Random rnd = new Random();
                        Session["id"] = u.id;
                        Session["OTP"] = rnd.Next(10000, 99999);
                        var mail = new EmailClass();
                        mail.SendEmail(value.email, "OTP Code", Session["OTP"].ToString());
                        return RedirectToAction("OtpValidate");
                    }
                    else
                    {
                        ViewBag.errun = "Username already exist!";
                        return View(value);
                    }
                }
                else
                {
                    ViewBag.err = "Password doesn't match!";
                    return View(value);
                }
            }
         return View(value);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session["UserName"] = null;
            Session["UserType"] = null;
            return RedirectToAction("Login");
        }
    }
}