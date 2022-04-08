using landSelling.Email;
using landSelling.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace landSelling.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(new contactModel());
        }
        [HttpPost]
        public ActionResult Contact(contactModel c)
        {
            ViewBag.Message = "Your contact page.";
            if(ModelState.IsValid)
            {
                var mail = new EmailClass();
                mail.SendEmail(c.email, c.subject, c.message);
                TempData["msg"] = "Your message has been sent. Thank you!";
                return RedirectToAction("Contact");
            }

            return View(c);
        }
    }
}