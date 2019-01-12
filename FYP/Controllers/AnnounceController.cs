using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class AnnounceController : Controller
    {
        // GET: Announce
        AnnouncementDataLayer announcedata = new AnnouncementDataLayer();
        [HttpGet]
        public ActionResult Search()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            return View();

        }
        [HttpGet]
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Session["UserID"] as string))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var statuslist = announcedata.GetStatusData();
                ViewBag.ComboStatus = new SelectList(statuslist, "comboStatusID", "comboStatusValue");
                return View();
            }

        }
        [HttpPost]
        public ActionResult Create([Bind]Announcement announce, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                var statuslist = new AnnouncementDataLayer().GetStatusData();
                ViewBag.ComboStatus = new SelectList(statuslist, "comboStatusID", "comboStatusValue");
                return View();
            }
            else
            {
                var statuslist = new AnnouncementDataLayer().GetStatusData();
                ViewBag.ComboStatus = new SelectList(statuslist, "comboStatusID", "comboStatusValue");
                if (file != null && file.ContentLength > 0)
                {
                    String date = DateTime.Now.ToString("ddMMyyy");

                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    HttpPostedFileBase filebase = Request.Files.Get(0);
                    var allowedExtensions = new string[] { ".jpg",".jpeg", ".png" };
                    string extension = System.IO.Path.GetExtension(file.FileName);

                    if (allowedExtensions.Contains(extension))
                    {
                        string ImagePath = System.IO.Path.Combine(Server.MapPath("~/Upload/"), date + ImageName);
                        file.SaveAs(ImagePath);
                        announce.Path = "~/Upload/" + date + ImageName;
                        announce.CreatedBy = Session["UserID"].ToString();
                        announce.AnnouncementDate = DateTime.Now;
                        TempData["Success"] = "New Announcement Added Successfully.";
                        int status = announcedata.AddAnnouncement(announce);
                        if (status == 1)
                        {
                            TempData["Success"] = "New Announcement Added Successfully.";
                        }
                        else
                        {
                            TempData["Error"] = "New Announcement Added Failed.";
                        }
                        return View();

                    }
                    else
                    {
                        TempData["Error"] = "Invalid File Format Attached.";
                        return View();

                    }
                }
                else
                {
                    announce.Path = "N/A";
                    announce.CreatedBy = Session["UserID"].ToString();
                    announce.AnnouncementDate = DateTime.Now;
                    int status = announcedata.AddAnnouncement(announce);
                    if (status == 1)
                    {
                        TempData["Success"] = "New Announcement Added Successfully.";
                    }
                    else
                    {
                        TempData["Error"] = "New Announcement Added Failed.";
                    }
                }
                return View();
            }
        }
        [HttpGet]
        public ActionResult Display_Announcement()
        {

            DisplayAnnouncement displayAnnouncement = new DisplayAnnouncement();
            displayAnnouncement.Announcesdata = announcedata.GetAnnouncements();
            return View(displayAnnouncement);
        }

        [HttpPost]
        public ActionResult Display_Announcement(Announcement announce)
        {
            return View();
        }
    }
}