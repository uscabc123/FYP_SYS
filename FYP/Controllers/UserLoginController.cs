using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class UserLoginController : Controller
    {
        UserDataLayer userdata = new UserDataLayer();

        public object ListBoxControl { get; private set; }


        // GET: UserLogin/Create
        public ActionResult Login()
        {
            Logout();
            return View();
        }

        // POST: UserLogin/Create
        [HttpPost]
        public ActionResult Login(Login login)
        {

            if (ModelState.IsValid)
            {
                int message = userdata.login(login);

                if (message == 1)
                {
                    TempData["Error"] = "Invalid login detail";

                    return RedirectToAction("Login");
                }
                else if (message == 2)
                {

                    TempData["Error"] = "Your account currently inactive. Please contact with administration for more information";

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

                    return RedirectToAction("UserProfile", "User");
                }

            }
            return View(login);

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

            return RedirectToAction("Index", "Login");
        }



    }
}
