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
        MessageDataLayer messagedatalayer = new MessageDataLayer();

        [HttpGet]
        public ActionResult MessageBoard(string cid)
        {
            Messenge message = new Messenge();

            if (string.IsNullOrEmpty(Session["UserID"] as string))
            {
                message.Receiver = cid;
            }
            else
            {
                message.Sender = Session["UserID"].ToString();
                message.Receiver = cid;
                message.messageData = messagedatalayer.GetMessage(message);
                if (message.messageData != null && message.messageData.Count > 0)
                {
                    return View(message);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult MessageBoard(Messenge message)
        {
            if(message.MessageContent != null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View();
        }
    }
}