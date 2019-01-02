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
            
            if (string.IsNullOrEmpty(Session["UserID"] as string))
            {
                return RedirectToAction("Login", "Account");

            }
            else
            {
                var statuslist = new UserDataLayer().GetAccountStatusData();
                ViewBag.StatusList = new SelectList(statuslist, "UserStatusListID", "UserStatusListValue");
                var rolelist = new UserDataLayer().GetRoleData();
                ViewBag.Role = new SelectList(rolelist, "UserRoleListID", "UserRoleListValue");
                return View();
            }
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
        public ActionResult Search()
        {
            //SearchUser userlist = new SearchUser();
            ////List<SearchUser> list = new List<SearchUser>();
            //userlist.userdata = userdata.GetAllUser();
            //return View(userlist);
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchUser search)
        {
          

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                SearchUser userlist = new SearchUser();
                userlist.userdata = userdata.GetSearchResult(search.searchvalue);

                if (userlist.userdata != null && userlist.userdata.Count > 0)
                {
                    return View(userlist);
                }
                return View();


            }
        }




        [HttpGet]
        public ActionResult EditUser(string uid)
        {
            User profile = userdata.EditUser(uid);

            ViewData["FName"] = profile.FName;
            ViewData["LName"] = profile.LName;
            ViewData["PhoneNumber"] = profile.PhoneNumber;
            ViewData["UserID"] = profile.UserID;
            ViewData["UserStatusID"] = profile.UserStatusID;
            ViewData["UserRoleID"] = profile.UserRoleID;
            ViewData["Email"] = profile.Email;
            ViewData["ICPassport"] = profile.ICPassport;
            ViewData["UserGender"] = profile.Gender;
            var statuslist = new UserDataLayer().GetAccountStatusData();
            ViewBag.StatusList = new SelectList(statuslist, "UserStatusListID", "UserStatusListValue", ViewData["UserStatusID"]);
            var rolelist = new UserDataLayer().GetRoleData();
            ViewBag.Role = new SelectList(rolelist, "UserRoleListID", "UserRoleListValue", ViewData["UserRoleID"]);

            var genderlist = new List<SelectListItem>()
            {
            new SelectListItem() { Value = "0", Text = "Male"},
            new SelectListItem() { Value = "1", Text = "Female"}

            };

            ViewBag.UserGender = new SelectList(genderlist, "Value", "Text", ViewData["UserGender"]);

            return View();
        }

        [HttpPost]
        public ActionResult EditUser(string uid, [Bind]User user)
        {
            user.UserID = uid;



            if (!ModelState.IsValid)
            {

                ViewData["FName"] = user.FName;
                ViewData["LName"] = user.LName;
                ViewData["PhoneNumber"] = user.PhoneNumber;
                ViewData["UserID"] = user.UserID;
                ViewData["UserStatusID"] = user.UserStatusID;
                ViewData["UserRoleID"] = user.UserRoleID;
                ViewData["Email"] = user.Email;
                ViewData["ICPassport"] = user.ICPassport;
                ViewData["UserGender"] = user.Gender;

                var statuslist = new UserDataLayer().GetAccountStatusData();
                ViewBag.StatusList = new SelectList(statuslist, "UserStatusListID", "UserStatusListValue", ViewData["UserStatusID"]);
                var rolelist = new UserDataLayer().GetRoleData();
                ViewBag.Role = new SelectList(rolelist, "UserRoleListID", "UserRoleListValue", ViewData["UserRoleID"]);
                var genderlist = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value = "0", Text = "Male"},
                        new SelectListItem() { Value = "1", Text = "Female"}
                    };
                ViewBag.UserGender = new SelectList(genderlist, "Value", "Text", ViewData["UserGender"]);

                return View();
            }
            else
            {
                int status = userdata.UpdateUserProfile(user);

                if (status == 1)
                {
                    ViewData["FName"] = user.FName;
                    ViewData["LName"] = user.LName;
                    ViewData["PhoneNumber"] = user.PhoneNumber;
                    ViewData["UserID"] = user.UserID;
                    ViewData["UserStatusID"] = user.UserStatusID;
                    ViewData["UserRoleID"] = user.UserStatusID;
                    ViewData["Email"] = user.Email;
                    ViewData["ICPassport"] = user.ICPassport;
                    ViewData["UserGender"] = user.Gender;
                    var statuslist = new UserDataLayer().GetAccountStatusData();
                    ViewBag.StatusList = new SelectList(statuslist, "UserStatusListID", "UserStatusListValue", ViewData["UserStatusID"]);
                    var rolelist = new UserDataLayer().GetRoleData();
                    ViewBag.Role = new SelectList(rolelist, "UserRoleListID", "UserRoleListValue", ViewData["UserRoleID"]);
                    var genderlist = new List<SelectListItem>()
                        {
                            new SelectListItem() { Value = "0", Text = "Male"},
                            new SelectListItem() { Value = "1", Text = "Female"}
                        };
                    ViewBag.UserGender = new SelectList(genderlist, "Value", "Text", ViewData["UserGender"]);
                    TempData["Success"] = "User Profile Updated Successfully!";


                }
                else if (status == 2)

                {
                    User profile = userdata.EditUser(uid);

                    ViewData["FName"] = profile.FName;
                    ViewData["LName"] = profile.LName;
                    ViewData["PhoneNumber"] = profile.PhoneNumber;
                    ViewData["UserID"] = profile.UserID;
                    ViewData["UserStatusID"] = profile.UserStatusID;
                    ViewData["UserRoleID"] = profile.UserRoleID;
                    ViewData["Email"] = profile.Email;
                    ViewData["ICPassport"] = profile.ICPassport;
                    ViewData["UserGender"] = profile.Gender;
                    var statuslist = new UserDataLayer().GetAccountStatusData();
                    ViewBag.StatusList = new SelectList(statuslist, "UserStatusListID", "UserStatusListValue", ViewData["UserStatusID"]);
                    var rolelist = new UserDataLayer().GetRoleData();
                    ViewBag.Role = new SelectList(rolelist, "UserRoleListID", "UserRoleListValue", ViewData["UserRoleID"]);

                    var genderlist = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value = "0", Text = "Male"},
                        new SelectListItem() { Value = "1", Text = "Female"}

                    };

                    ViewBag.UserGender = new SelectList(genderlist, "Value", "Text", ViewData["UserGender"]);

                    TempData["Error"] = "Email has been used by other user account.";
               }
            }

                    return View();

        }
        [HttpGet]

        public ActionResult Delete()
        {
            return View();


        }



        //[HttpGet]
        //public ActionResult UserProfile()
        //{

        //    if (string.IsNullOrEmpty(Session["Email"] as string))
        //    {
        //        return RedirectToAction("Login", "UserLogin");

        //    }
        //    else
        //    {
        //        string emailvalue = Session["Email"].ToString();

        //        User profile = userdata.Profile(emailvalue);

        //        ViewData["FName"] = profile.FName;
        //        ViewData["LName"] = profile.LName;
        //        ViewData["PhoneNumber"] = profile.PhoneNumber;
        //        ViewData["UserID"] = profile.UserID;
        //        ViewData["Email"] = profile.Email;
        //        ViewData["ICPassport"] = profile.ICPassport;
        //        ViewData["UserGender"] = profile.Gender;
        //        var genderlist = new List<SelectListItem>()
        //    {
        //    new SelectListItem() { Value = "0", Text = "Male"},
        //    new SelectListItem() { Value = "1", Text = "Female"}

        //    };
        //        ViewBag.UserGender = new SelectList(genderlist, "Value", "Text", ViewData["UserGender"]);

        //        return View();
        //    }

        //}
        //[HttpPost]
        //public ActionResult UserProfile(User user)
        //{

        //    if (string.IsNullOrEmpty(Session["Email"] as string))
        //    {
        //        return RedirectToAction("Login", "UserLogin");
        //    }
        //    else
        //    {
        //        user.UserID = Session["UserID"].ToString();

        //        if (ModelState.IsValid)
        //        {

        //            ViewData["FName"] = user.FName;
        //            ViewData["LName"] = user.LName;
        //            ViewData["PhoneNumber"] = user.PhoneNumber;
        //            ViewData["UserID"] = user.UserID;
        //            ViewData["Email"] = user.Email;
        //            ViewData["ICPassport"] = user.ICPassport;
        //            ViewData["UserGender"] = user.Gender;

        //            var genderlist = new List<SelectListItem>()
        //            {
        //                new SelectListItem() { Value = "0", Text = "Male"},
        //                new SelectListItem() { Value = "1", Text = "Female"}
        //            };
        //            ViewBag.UserGender = new SelectList(genderlist, "Value", "Text", ViewData["UserGender"]);

        //            return View();
        //        }
        //        else
        //        {
        //            int status = userdata.UpdateProfile(user);

        //            if (status == 1)
        //            {
        //                ViewData["FName"] = user.FName;
        //                ViewData["LName"] = user.LName;
        //                ViewData["PhoneNumber"] = user.PhoneNumber;
        //                ViewData["UserID"] = user.UserID;
        //                ViewData["Email"] = user.Email;
        //                ViewData["ICPassport"] = user.ICPassport;
        //                ViewData["UserGender"] = user.Gender;
        //                var genderlist = new List<SelectListItem>()
        //                {
        //                    new SelectListItem() { Value = "0", Text = "Male"},
        //                    new SelectListItem() { Value = "1", Text = "Female"}
        //                };
        //                ViewBag.UserGender = new SelectList(genderlist, "Value", "Text", ViewData["UserGender"]);
        //                TempData["Success"] = "User Profile Updated Successfully!";

        //                return View();

        //            }
        //            else if (status == 2)

        //            {
        //                string emailvalue = Session["Email"].ToString();

        //                User profile = userdata.Profile(emailvalue);

        //                ViewData["FName"] = profile.FName;
        //                ViewData["LName"] = profile.LName;
        //                ViewData["PhoneNumber"] = profile.PhoneNumber;
        //                ViewData["UserID"] = profile.UserID;
        //                ViewData["Email"] = profile.Email;
        //                ViewData["ICPassport"] = profile.ICPassport;
        //                ViewData["UserGender"] = profile.Gender;

        //                var genderlist = new List<SelectListItem>()
        //                {
        //                    new SelectListItem() { Value = "0", Text = "Male"},
        //                    new SelectListItem() { Value = "1", Text = "Female"}
        //                };
        //                ViewBag.UserGender = new SelectList(genderlist, "Value", "Text", ViewData["UserGender"]);

        //                TempData["Error"] = "Email has been used by other user account.";
        //                return View();
        //            }
        //        }
        //        return View();

        //    }
        //}
       
        //[HttpGet]
        //public ActionResult ForgetPassword()
        //{

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ForgetPassword(string email)
        //{

        //    return View();
        //}

    }

}