using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVHomepage.Models;
using CVHomepage.DAL;
using CVHomepage.Services;
using Microsoft.AspNet.Identity;
using CVHomepage.Helpers.SessionHelpers;

namespace CVHomepage.Controllers
{
    public class CVController : Controller
    {
        private CVContext db = new CVContext();

        
        // GET: /CV/
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.message = TempData["Message"];
            }
            return View(db.CVs.ToList().Where(a => a.User == User.Identity.GetUserId() ));
        }

        // GET: /CV/Upcoming
        public ActionResult Planned()
        {
            return View();
        }
       



        // GET: /CV/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        
        // POST: /CV/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,User")] CV cv)
        {
            if (ModelState.IsValid)
            {
                db.CVs.Add(cv);
                db.SaveChanges();
                TempData["message"] = cv.Name + " has been created";
                return RedirectToAction("Index");
            }

            return View(cv);
        }

        
        // GET: /CV/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string user = User.Identity.GetUserId();
            try
            {
                CV cv = db.CVs
                    .Where(a => a.ID == id && a.User == user)
                    .Single() as CV;
                if (cv == null)
                {
                    return HttpNotFound();
                }

                return View(cv);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            
        }

        
        // POST: /CV/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,User")] CV cv, string[] removingSkills)
        {
            string realUser = User.Identity.GetUserId();
            var cvToUpdate = db.CVs
                .Include(s => s.Skills)
                .Where(s => s.ID == cv.ID && s.User == realUser)
                .Single();

            cvToUpdate.Name = cv.Name;

            if (removingSkills != null)
            {
                foreach (var skill in removingSkills)
                {
                    var skillToRemove = db.Skills.Find(int.Parse(skill));
                    cvToUpdate.Skills.Remove(skillToRemove);
                }
            }



            if (ModelState.IsValid)
            {
                db.Entry(cvToUpdate).State = EntityState.Modified;

                db.SaveChanges();
                TempData["message"] = cv.Name + " has been edited";
                return RedirectToAction("Index");
            }

            return View(cv);

           
        }

        
        // GET: /CV/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string user = User.Identity.GetUserId();
            try
            {
                CV cv = db.CVs
                    .Where(a => a.ID == id && a.User == user)
                    .Single() as CV;
                if (cv == null)
                {
                    return HttpNotFound();
                }

                //deletes the cv from the CurrentCV in session to prevent a bug in skill controller 
                if (SessionHelpers.CurrentCV == id)
                {
                    SessionHelpers.CurrentCV = 0;
                }
                return View(cv);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            
        }

        
        // POST: /CV/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string user = User.Identity.GetUserId();
            try
            {
                CV cv = db.CVs
                    .Where(a => a.ID == id && a.User == user)
                    .Single() as CV;
                if (cv == null)
                {
                    return HttpNotFound();
                }

                db.CVs.Remove(cv);
                db.SaveChanges();
                TempData["message"] = cv.Name + " has been deleted";
                return RedirectToAction("Index");

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }

        //GET: /cv/view/5
        [Authorize]
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string user = User.Identity.GetUserId();
            try
            {
                CV cv = db.CVs
                    .Include(s => s.Skills)
                    .Where(a => a.ID == id && a.User == user)
                    .Single() as CV;
                if (cv == null)
                {
                    return HttpNotFound();
                }

                ViewBag.CV = CVBuilder.Build(cv);
                return View();

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }

        [Authorize]
        public ActionResult Select(int id)
        {
            SessionHelpers.CurrentCV = id;
            CV cv = db.CVs.Find(id);
            var skillList = new List<int>();
            foreach (var skill in cv.Skills)
            {
                skillList.Add(skill.ID);
            }
            SessionHelpers.CurrentSkills = skillList;
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AddSkill(int id, string url)
        {
            CV cv = db.CVs.Find(SessionHelpers.CurrentCV);
            Skill skill = db.Skills.Find(id);
            cv.Skills.Add(skill);
            db.Entry(cv).State = EntityState.Modified;
            db.SaveChanges();

            SessionHelpers.CurrentSkills.Add(id);
            TempData["message"] = "Add " + skill.Name + " to " + cv.Name;
            return Redirect(url);
        }

        [Authorize]
        public ActionResult RemoveSkill(int id, string url)
        {
            CV cv = db.CVs.Find(SessionHelpers.CurrentCV);
            Skill skill = db.Skills.Find(id);
            cv.Skills.Remove(skill);
            db.Entry(cv).State = EntityState.Modified;
            db.SaveChanges();

            SessionHelpers.CurrentSkills.Remove(id);
            TempData["message"] = "Removed " + skill.Name + " from " + cv.Name;
            return Redirect(url);
        }

     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
