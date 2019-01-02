using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class ConsultationController : Controller
    {
        UserDataLayer userdata = new UserDataLayer();

        // GET: Consultation
        [HttpGet]
        public ActionResult Create(string uid)
        {
            Consultation profile = new Consultation();
            if (uid == null)
            {
                TempData["Empty"] = "Empty";
                return View();

            }
            else
            { 
                profile.User = userdata.EditUser(uid);
                ViewData["FName"] = profile.User.FName;
                ViewData["LName"] = profile.User.LName;
                ViewData["PhoneNumber"] = profile.User.PhoneNumber;
                ViewData["UserID"] = profile.User.UserID;
                ViewData["Email"] = profile.User.Email;
                ViewData["ICPassport"] = profile.User.ICPassport;
                ViewData["UserGender"] = profile.User.Gender;
                return View();

            }

        }
        [HttpPost]
        public ActionResult Create(Consultation profile, string uid)
        {
            if(!ModelState.IsValid)
            {
                profile.User = userdata.EditUser(uid);

                ViewData["FName"] = profile.User.FName;
                ViewData["LName"] = profile.User.LName;
                ViewData["PhoneNumber"] = profile.User.PhoneNumber;
                ViewData["UserID"] = profile.User.UserID;
                ViewData["Email"] = profile.User.Email;
                ViewData["ICPassport"] = profile.User.ICPassport;
                ViewData["UserGender"] = profile.User.Gender;
            }
            return View();
        }
    }
}