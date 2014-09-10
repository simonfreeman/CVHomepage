using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CVHomepage.ViewModels;
using System.Net.Mail;
using System.Net;

namespace CVHomepage.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/
        public ActionResult Index(bool? sent)
        {
            if (sent == true)
            {
                ViewBag.Sent = "Your message has been sent";
            }
            return View();
        }

        //
        // GET: /Post/
        [HttpPost]
        public ActionResult Index(Contact message)
        {
            if (ModelState.IsValid)
            {

                SmtpClient client = new SmtpClient("smtp.gmail.com", 465);
                client.Credentials = new NetworkCredential("USERNAME@gmail.com", "PASSWORD");
                client.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(message.Email);
                mail.To.Add("mailaddresshere");
                mail.Subject = message.Subject;
                mail.Body = message.MessageBody;

                client.Send(mail);
                return RedirectToAction("Index", new { sent = true });
            }
            return RedirectToAction("Index");
        }
	}
}