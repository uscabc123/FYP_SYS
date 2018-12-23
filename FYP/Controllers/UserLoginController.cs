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
            return View();
        }

        // POST: UserLogin/Create
        [HttpPost]
        public ActionResult Login(Login login)
        {
            
                  if(ModelState.IsValid)
                  {
                    int message  = userdata.login(login);

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
                    TempData["Error"] = "Successfull";

                    }

                  }
            return View(login);

        }


    }
}
