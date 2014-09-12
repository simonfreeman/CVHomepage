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
using CVHomepage.Helpers.SessionHelpers;
using Microsoft.AspNet.Identity;

namespace CVHomepage.Controllers
{
    public class CVController : Controller
    {
        private CVContext db = new CVContext();

        // GET: /CV/
        public ActionResult Index()
        {
            
            return View(db.CVs.ToList().Where(a => a.User == User.Identity.GetUserId() ));
        }

        
        // GET: /CV/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cv = db.CVs.Find(id);
            if (cv == null)
            {
                return HttpNotFound();
            }


            return View(cv);
        }

  

        // GET: /CV/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CV/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,User")] CV cv)
        {
            if (ModelState.IsValid)
            {
                db.CVs.Add(cv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cv);
        }

        // GET: /CV/Edit/5
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
                return RedirectToAction("Edit", new { id = cv.ID });
            }

            return View(cv);

           
        }

        // GET: /CV/Delete/5
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

                return View(cv);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            
        }

        // POST: /CV/Delete/5
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
                return RedirectToAction("Index");

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

           
        }

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

        public ActionResult AddSkill(int id, string url)
        {
            CV cv = db.CVs.Find(SessionHelpers.CurrentCV);
            Skill skill = db.Skills.Find(id);
            cv.Skills.Add(skill);

            db.Entry(cv).State = EntityState.Modified;
            db.SaveChanges();
            SessionHelpers.CurrentSkills.Add(id);
            //return RedirectToAction("Index", "Skill", new { name = skill.Name });
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
