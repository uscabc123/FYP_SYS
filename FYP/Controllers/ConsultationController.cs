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
        public ActionResult Create([Bind]Consultation profile, string uid)
        {

            profile.User = userdata.EditUser(uid);

            if (!ModelState.IsValid)
            {
                ViewData["FName"] = profile.User.FName;
                ViewData["LName"] = profile.User.LName;
                ViewData["PhoneNumber"] = profile.User.PhoneNumber;
                ViewData["UserID"] = profile.User.UserID;
                ViewData["Email"] = profile.User.Email;
                ViewData["ICPassport"] = profile.User.ICPassport;
                ViewData["UserGender"] = profile.User.Gender;
            }
            else
            {
                profile.patientid = profile.User.UserID;
                if (!string.IsNullOrEmpty(Session["UserID"] as string))
                {
                    profile.doctorid = Session["UserID"].ToString();
                    profile.ConsultationDate = DateTime.Now;
                    profile.patientid = profile.User.UserID;
                    
                    ConsultationDataLayer addconsultation = new ConsultationDataLayer();

                    addconsultation.AddConsultation(profile);

                    TempData["Success"] = "New Consultation Record Added Successfully.";
                    ViewData["FName"] = profile.User.FName;
                    ViewData["LName"] = profile.User.LName;
                    ViewData["PhoneNumber"] = profile.User.PhoneNumber;
                    ViewData["UserID"] = profile.User.UserID;
                    ViewData["Email"] = profile.User.Email;
                    ViewData["ICPassport"] = profile.User.ICPassport;
                    ViewData["UserGender"] = profile.User.Gender;
                }



            }
            return View();
        }

        [HttpGet]
        public ActionResult SearchConsultation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchConsultation(string uid)
        {
            return View();
        }
    }
}