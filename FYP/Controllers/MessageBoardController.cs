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
                return RedirectToAction("Login", "Account");
            }
            else
            {
                message.Sender = Session["UserID"].ToString();
                message.Receiver = cid;
                message.messageData = messagedatalayer.GetMessage(message);
                if (message.messageData != null && message.messageData.Count > 0)
                {
                    message.MessageContent = "";
                    ModelState.Remove("MessageContent");
                    return View(message);
                }
                else
                {
                    message.MessageContent = " ";
                    ModelState.Remove("MessageContent");
                    return View(message);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult MessageBoard(Messenge message, string cid)
        {

            if (string.IsNullOrEmpty(Session["UserID"] as string))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                message.Sender = Session["UserID"].ToString();
                message.Receiver = cid;
                message.messageData = messagedatalayer.AddMessage(message);
                if (message.messageData != null && message.messageData.Count > 0)
                {
                    message.MessageContent = "";
                    ModelState.Remove("MessageContent");
                }
                return View(message);
            }
        }
    }
}