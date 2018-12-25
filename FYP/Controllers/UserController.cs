using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class UserController : Controller
    {

        UserDataLayer userdata = new UserDataLayer();

        public object UrlEncryptionHelper { get; private set; }



        // GET: User
        [HttpGet]
        public ActionResult AddUser()
        {
            var statuslist = new UserDataLayer().GetAccountStatusData();
            ViewBag.StatusList = new SelectList(statuslist, "UserStatusListID", "UserStatusListValue");
            var rolelist = new UserDataLayer().GetRoleData();
            ViewBag.Role = new SelectList(rolelist, "UserRoleListID", "UserRoleListValue");
            return View();
        }


        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser([Bind]User user)
        {
            if (!ModelState.IsValid)
            {
                var statuslist = new UserDataLayer().GetAccountStatusData();
                ViewBag.StatusList = new SelectList(statuslist, "UserStatusListID", "UserStatusListValue");
                var rolelist = new UserDataLayer().GetRoleData();
                ViewBag.Role = new SelectList(rolelist, "UserRoleListID", "UserRoleListValue");
                return View();
            }
            else
            {
                    int status = userdata.AddUser(user);

                    if (status == 2)
                    {
                        TempData["Success"] = "New user added successfully!";
                        return RedirectToAction("AddUser");
                    }
                    else
                    {
                        TempData["Error"] = "Email exist in the system!";
                        return RedirectToAction("AddUser");
                    }
            }
        }
        [HttpGet]
        public ActionResult SearchUser()
        {
            List<User> userlist = new List<User>();
            userlist = userdata.GetAllUser();
            var userdatalist = new User { userdata = userlist };
            return View(userdatalist);
        }
        [HttpGet]
        public ActionResult EditUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditUser(string id, [Bind]User user)
        {
            return View();

        }
        [HttpGet]

        public ActionResult Delete()
        {
            return View();


        }



        [HttpGet]

        public ActionResult UserProfile()
        {
          string emailvalue = Session["Email"].ToString();

          var profile =  userdata.Profile(emailvalue);

            User userprofile = new User();
            ViewData["FName"] = profile.FName;
            ViewData["LName"] = profile.LName;
            ViewData["PhoneNumber"] = profile.PhoneNumber;
            ViewData["UserID"] = profile.UserID;
            ViewData["Email"] = profile.Email;
            ViewData["ICPassport"] = profile.ICPassport;
            return View();


          

        }


    }

}