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
        ConsultationDataLayer consultdata = new ConsultationDataLayer();
        // GET: Consultation
        [HttpGet]
        public ActionResult Create(string uid)
        {
            Consultation profile = new Consultation();
            if (uid == null)
            {
                //ConsultationSearch search = new ConsultationSearch();
                //profile.User = userdata.EditUser(search.patientid);
                //ViewData["FName"] = profile.User.FName;
                //ViewData["LName"] = profile.User.LName;
                //ViewData["PhoneNumber"] = profile.User.PhoneNumber;
                //ViewData["UserID"] = profile.User.UserID;
                //ViewData["Email"] = profile.User.Email;
                //ViewData["ICPassport"] = profile.User.ICPassport;
                //ViewData["UserGender"] = profile.User.Gender; 
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
            //ConsultationSearch consultlist = new ConsultationSearch();
            //if (!string.IsNullOrEmpty(Session["UserID"] as string))
            //{
            //    consultation.userid = Session["UserID"].ToString();
            //    consultation.UserRole = Session["RoleID"].ToString();
            //    consultation.searchvalue = null;
            //    consultlist.consultsdata = consultdata.SearchConsultation(consultation);
            //} return View(consultlist);
            return View();

        }       
        [HttpPost]
        public ActionResult SearchConsultation(ConsultationSearch consultation, string cid)
        {
            ConsultationSearch consultlist = new ConsultationSearch();

            if (!string.IsNullOrEmpty(Session["UserID"] as string))
            {
                ConsultationSearch consult = new ConsultationSearch();
                consultation.userid = Session["UserID"].ToString();
                consultation.UserRole = Session["RoleID"].ToString();
                consultlist.consultsdata =  consultdata.SearchConsultation(consultation);
                if (consultlist.consultsdata != null && consultlist.consultsdata.Count > 0)
                {
                    return View(consultlist);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditConsultation()
        {
            //ConsultationSearch consultlist = new ConsultationSearch();
            //if (!string.IsNullOrEmpty(Session["UserID"] as string))
            //{
            //    consultation.userid = Session["UserID"].ToString();
            //    consultation.UserRole = Session["RoleID"].ToString();
            //    consultation.searchvalue = null;
            //    consultlist.consultsdata = consultdata.SearchConsultation(consultation);
            //} return View(consultlist);
            return View();

        }
        [HttpPost]
        public ActionResult EditConsultation(string cid)
        {
            //ConsultationSearch consultlist = new ConsultationSearch();
            //if (!string.IsNullOrEmpty(Session["UserID"] as string))
            //{
            //    consultation.userid = Session["UserID"].ToString();
            //    consultation.UserRole = Session["RoleID"].ToString();
            //    consultation.searchvalue = null;
            //    consultlist.consultsdata = consultdata.SearchConsultation(consultation);
            //} return View(consultlist);
            return View();

        }
    }
}