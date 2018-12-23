using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Models
{
    public class AddAnnouncementController : Controller
    {
        // GET: AddAnnouncement
        public ActionResult Index()
        {
            return View();
        }

        // GET: AddAnnouncement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AddAnnouncement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddAnnouncement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AddAnnouncement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AddAnnouncement/Edit/5
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

        // GET: AddAnnouncement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AddAnnouncement/Delete/5
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
