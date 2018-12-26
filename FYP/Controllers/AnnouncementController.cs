using FYP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class AnnouncementController : Controller
    {
        AnnouncementDataLayer announcedata = new AnnouncementDataLayer();

        // GET: Announcement
        public ActionResult Index()
        {
            return View();
        }

        // GET: Announcement/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: Announcement/Create
        public ActionResult Create()
        {

            var statuslist = announcedata.GetStatusData();
            ViewBag.Status = new SelectList(statuslist, "comboStatusID", "comboStatusValue");
            return View();
        }

        // POST: Announcement/Create
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

                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    string ImagePath = System.IO.Path.Combine(Server.MapPath("~/Upload/"),ImageName);

                        file.SaveAs(ImagePath);

                        announce.Path = "~/Upload/" + ImageName;
                        announce.CreatedBy = Session["UserID"].ToString();
                        announce.AnnouncementDate = DateTime.Now;
                    }
                    else
                    {
                        announce.Path = "N/A";
                        announce.CreatedBy = Session["UserID"].ToString();
                        announce.AnnouncementDate = DateTime.Now;
                    }

                    int status = announcedata.AddAnnouncement(announce);
                    if(status == 1)
                    {
                        TempData["Success"] = "New Announcement Added Successfully.";
                    }
                    else
                    {
                        TempData["Error"] = "New Announcement Added Failed.";

                    }
                     return View();


                }
                
         
        }

        // GET: Announcement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Announcement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Announcement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Announcement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
