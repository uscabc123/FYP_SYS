using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class MessageBoardController : Controller
    {
        // GET: MessageBoard
        //public ActionResult Index()
        //{

        //    return View();

        //}



        public ActionResult MessageBoard()
            {
                return View();

            }
    public ActionResult SendMessage()
    {
        return View();
    }
    }
}