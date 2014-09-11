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

namespace CVHomepage.Controllers
{
    public class CVController : Controller
    {
        private CVContext db = new CVContext();

        // GET: /CV/
        public ActionResult Index()
        {
            
            return View(db.CVs.ToList());
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
            CV cv = db.CVs.Find(id);
            if (cv == null)
            {
                return HttpNotFound();
            }
            return View(cv);
        }

        // POST: /CV/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,User")] CV cv, string[] removingSkills)
        {
            var cvToUpdate = db.CVs
                .Include(s => s.Skills)
                .Where(s => s.ID == cv.ID)
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
            CV cv = db.CVs.Find(id);
            if (cv == null)
            {
                return HttpNotFound();
            }
            return View(cv);
        }

        // POST: /CV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CV cv = db.CVs.Find(id);
            db.CVs.Remove(cv);
            db.SaveChanges();
            return RedirectToAction("Index");
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
