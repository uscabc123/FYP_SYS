using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class AccountController : Controller
    {
        AccountDataLayer userdata = new AccountDataLayer();

        // GET: Account
        public ActionResult Login()
        {
            Logout();
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["RoleID"] = null;
            Session["StatusID"] = null;
            Session["Fname"] = null;
            Session["Lname"] = null;
            Session["Email"] = null;
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.AddHeader("Cache-control", "no-store, must-revalidate, private, no-cache");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            Response.AppendToLog("window.location.reload();");

            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                int message = userdata.login(login);
                if (message == 1)
                {
                    TempData["Error"] = "Incorrect Password or Username.";
                    return RedirectToAction("Login");
                }
                else if (message == 2)
                {
                    TempData["Error"] = "Your account currently inactive. Please contact with administration for more information.";
                    return RedirectToAction("Login");
                }
                else
                {
                    Session["Email"] = login.Username;
                    string useremail = Session["Email"].ToString();
                    var useracc = userdata.getUserData(useremail);
                    Session["RoleID"] = useracc.role;
                    Session["StatusID"] = useracc.status;
                    Session["Fname"] = useracc.Fname;
                    Session["Lname"] = useracc.Lname;
                    Session["UserID"] = useracc.UserID;
                    return RedirectToAction("UserProfile");
                }
            }
            return View(login);
        }
        [HttpGet]
        public ActionResult UserProfile()
        {
            if (string.IsNullOrEmpty(Session["Email"] as string))
            {
                return RedirectToAction("Login");
            }
            else
            {
                string emailvalue = Session["Email"].ToString();
                UserProfile profile = userdata.Profile(emailvalue);
                ViewData["FName"] = profile.FName;
                ViewData["LName"] = profile.LName;
                ViewData["PhoneNumber"] = profile.PhoneNumber;
                ViewData["UserID"] = profile.UserID;
                ViewData["Email"] = profile.Email;
                ViewData["ICPassport"] = profile.ICPassport;
                ViewData["UserGender"] = profile.Gender;
                var genderlist = new List<SelectListItem>()
            {
            new SelectListItem() { Value = "0", Text = "Male"},
            new SelectListItem() { Value = "1", Text = "Female"}
            };
                ViewBag.UserGender = new SelectList(genderlist, "Value", "Text", ViewData["UserGender"]);
                return View();
            }
        }
        [HttpPost]
        public ActionResult UserProfile(UserProfile user)
        {

            if (string.IsNullOrEmpty(Session["Email"] as string))
            {
                return RedirectToAction("Login");
            }
            else
            {
                user.UserID = Session["UserID"].ToString();

                if (!ModelState.IsValid)
                {
                    ViewData["FName"] = user.FName;
                    ViewData["LName"] = user.LName;
                    ViewData["PhoneNumber"] = user.PhoneNumber;
                    ViewData["UserID"] = user.UserID;
                    ViewData["Email"] = user.Email;
                    ViewData["ICPassport"] = user.ICPassport;
                    ViewData["UserGender"] = user.Gender;

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
                    int status = userdata.UpdateProfile(user);

                    if (status == 1)
                    {
                        ViewData["FName"] = user.FName;
                        ViewData["LName"] = user.LName;
                        ViewData["PhoneNumber"] = user.PhoneNumber;
                        ViewData["UserID"] = user.UserID;
                        ViewData["Email"] = user.Email;
                        ViewData["ICPassport"] = user.ICPassport;
                        ViewData["UserGender"] = user.Gender;
                        var genderlist = new List<SelectListItem>()
                        {
                            new SelectListItem() { Value = "0", Text = "Male"},
                            new SelectListItem() { Value = "1", Text = "Female"}
                        };
                        ViewBag.UserGender = new SelectList(genderlist, "Value", "Text", ViewData["UserGender"]);
                        TempData["Success"] = "User Profile Updated Successfully!";

                        return View();

                    }
                    else if (status == 2)

                    {
                        string emailvalue = Session["Email"].ToString();

                        UserProfile profile = userdata.Profile(emailvalue);

                        ViewData["FName"] = profile.FName;
                        ViewData["LName"] = profile.LName;
                        ViewData["PhoneNumber"] = profile.PhoneNumber;
                        ViewData["UserID"] = profile.UserID;
                        ViewData["Email"] = profile.Email;
                        ViewData["ICPassport"] = profile.ICPassport;
                        ViewData["UserGender"] = profile.Gender;

                        var genderlist = new List<SelectListItem>()
                        {
                            new SelectListItem() { Value = "0", Text = "Male"},
                            new SelectListItem() { Value = "1", Text = "Female"}
                        };
                        ViewBag.UserGender = new SelectList(genderlist, "Value", "Text", ViewData["UserGender"]);

                        TempData["Error"] = "Email has been used by other user account.";
                        return View();
                    }
                }
                return View();

            }
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Password password)
        {
            if (string.IsNullOrEmpty(Session["Email"] as string))
            {
                return RedirectToAction("Login");
            }
            else if (string.IsNullOrEmpty(Session["UserID"] as string))
            {
                return RedirectToAction("Login");
            }
            else if (!string.IsNullOrEmpty(Session["UserID"] as string))
            {
               if (!string.IsNullOrEmpty(Session["Email"] as string))
                {
                     password.Email = Session["Email"].ToString();
                     password.UserID = Session["UserID"].ToString();

                    var currentpassword = password.CurrentPassword;
                    var newpassword = password.NewPassword;
                    var confirmpassword = password.ConfirmPassword;

                    int message = userdata.CheckPassword(password);
                    if (message == 1)
                    {
                        TempData["Error"] = "Incorrect Current Password.";
                        return RedirectToAction("ChangePassword");
                    }
                    else if (message == 2)
                    {
                        if (newpassword == confirmpassword)
                        {
                            int password_status = userdata.ChangePassword(password);
                            if (password_status == 3)
                            {
                                TempData["Success"] = "Password Changed Successfully.";
                                return RedirectToAction("ChangePassword");
                            }
                            else if (password_status == 1)
                            {
                                return RedirectToAction("Login");
                            }
                        }
                        else if (newpassword != confirmpassword)
                        {
                            TempData["Error"] = "New Password and Current Password doesn't match.";
                            return RedirectToAction("ChangePassword");
                        }
                    }
                    else if (message == 3)
                    {
                        return RedirectToAction("Login");
                    }
                }
               else
                {
                    return RedirectToAction("Login");
                }
            }
            return View();

        }
        [HttpGet]
        public ActionResult ForgetPassword(Password password)
        {
            int password_status = userdata.Find_Account(password);
            if (password_status == 1)
            {
                TempData["Error"] = "Email doesn't exists.";
                return RedirectToAction("ForgetPassword");
            }
            else if (password_status == 2)
            {
                TempData["Success"] = "Email has been sent to your provided email.";
                return RedirectToAction("ForgetPassword");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {

            return View();
        }


    }
}
