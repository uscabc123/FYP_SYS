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
        Consultation profile = new Consultation();

        // GET: Consultation
        [HttpGet]
        public ActionResult Create(string uid)
        {
            if (uid == null)
            {
                TempData["Empty"] = "Empty";
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
        public ActionResult EditConsultation(int cid, string uid)
        {

                ConsultationSearch consult = new ConsultationSearch();
                   consult.consultID = cid;
                Consultation userconsult =  consultdata.ConsultationDetail(consult);

            var followup = new List<SelectListItem>()
    {

                    new SelectListItem() {  Value = "1", Text = "Yes"},
                    new SelectListItem() {Value = "0", Text = "No"}

    };
            ViewBag.followupstatus = new SelectList(followup, "Value", "Text", ViewData["followup"]);

            profile.User = userdata.EditUser(uid);
                ViewData["FName"] = profile.User.FName;
                ViewData["LName"] = profile.User.LName;
                ViewData["PhoneNumber"] = profile.User.PhoneNumber;
                ViewData["UserID"] = profile.User.UserID;
                ViewData["Email"] = profile.User.Email;
                ViewData["ICPassport"] = profile.User.ICPassport;
                ViewData["UserGender"] = profile.User.Gender;
                Session["ConsultationID"] = userconsult.ConsultationId;
                ViewData["diagnose"] = userconsult.diagnose;
                ViewData["user_symptoms"] = userconsult.symptoms;
                ViewData["user_remarks"] = userconsult.remarks;
                ViewData["followup"] = userconsult.followup;
                return View();
       }
        [HttpPost]
        public ActionResult EditConsultation(Consultation consult)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Session["ConsultationID"] as string))
                {

                    consult.ConsultationId = Session["ConsultationID"].ToString();
                    var followup = new List<SelectListItem>()
                    {

                    new SelectListItem() {  Value = "1", Text = "Yes"},
                    new SelectListItem() {Value = "0", Text = "No"}
                    };
                    ViewBag.followupstatus = new SelectList(followup, "Value", "Text", ViewData["followup"]);
                }
                TempData["Success"] = "Consultation Detail Updated Successfully!";

                consultdata.UpdateConsultationDetail(consult);
                return View();
            }
            else
            {
                var followup = new List<SelectListItem>()
                {

                    new SelectListItem() {  Value = "1", Text = "Yes"},
                    new SelectListItem() {Value = "0", Text = "No"}

                };
                ViewBag.followupstatus = new SelectList(followup, "Value", "Text", ViewData["followup"]);
            }
            return View();
            }
        
    }
}