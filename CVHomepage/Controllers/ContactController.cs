using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CVHomepage.ViewModels;

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
                return RedirectToAction("Index", new { sent = true });
            }
            return RedirectToAction("Index");
        }
	}
}