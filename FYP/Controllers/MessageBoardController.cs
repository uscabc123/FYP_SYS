using FYP.Models;
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


         [HttpGet]
        public ActionResult MessageBoard(string cid)
        {
                return View();

        }

        [HttpPost]
        public ActionResult MessageBoard(Messenge message)
        {
            message.MessageContent;
            return View();
        }
        }
}