using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User _user)
        {
            try
            {
                var res = _user.Login();

                if (res)
                {
                    Session["UserName"] = _user.UserName;
                    Session["Password"] = _user.Password;
                    return RedirectToAction("Home");
                }
            }
            catch
            {
               
            }
            ViewBag.Error = " ";
            return View(_user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                if (_user.CheckID())
                {
                    if (_user.CheckEmail())
                    {
                        _user.Add();
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Error = "Email";
                    }
                }
                else
                {
                    ViewBag.Error = "ID";
                }
            }
            return View(_user);
        }
    }
}