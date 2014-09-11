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
using CVHomepage.ViewModels;
using CVHomepage.Helpers.SessionHelpers;
namespace CVHomepage.Controllers
{
    public class SkillController : Controller
    {
        private CVContext db = new CVContext();

        // GET: /Skill/
        public ActionResult Index()
        {
            var skills = db.Skills.Include(s => s.Category)
                .Include(s => s.Tags);
            if (SessionHelpers.CurrentCV != 0)
            {
                var currentCV = db.CVs.Find(SessionHelpers.CurrentCV);
                ViewBag.CVName = currentCV.Name;
            }
            return View(skills.ToList());
        }

        // GET: /Skill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // GET: /Skill/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            Skill skill = new Skill();
            skill.Tags = new List<Tag>();
            getTagData(skill);
            return View();
        }

        // POST: /Skill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Notes,CVText,CategoryID")] Skill skill,string[]selectedTags)
        {
            if (selectedTags!=null)
            {
                skill.Tags = new List<Tag>();
                foreach(var tag in selectedTags)
                {
                    var tagToAdd = db.Tags.Find(int.Parse(tag));
                    skill.Tags.Add(tagToAdd);
                }
                
            }
            if (ModelState.IsValid)
            {
                db.Skills.Add(skill);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", skill.CategoryID);
            return View(skill);
        }

        // GET: /Skill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            getTagData(skill);
            if (skill == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", skill.CategoryID);
           
            return View(skill);
        }

        // POST: /Skill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Notes,CVText,CategoryID")]Skill skill, string[] selectedTags)
        {
            //for some reason it's not understanding the skill being returned exists, so I'm using it's info
            // to find and then update a different skill variable.
            var skillToUpdate = db.Skills
               .Include(i => i.Tags)
               .Include(i => i.Category)
               .Where(i => i.ID == skill.ID )
               .Single();
            skillToUpdate.Name = skill.Name;
            skillToUpdate.Notes = skill.Notes;
            skillToUpdate.CVText = skill.CVText;
            skillToUpdate.CategoryID = skill.CategoryID;
            
            skillToUpdate.Tags = new List<Tag>();
            foreach (var tag in selectedTags)
            {
                var tagToAdd = db.Tags.Find(int.Parse(tag));

                skillToUpdate.Tags.Add(tagToAdd);
            }
            if (ModelState.IsValid)
            {
                
                db.Entry(skillToUpdate).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", skill.CategoryID);
            return View(skill);
        }

        // GET: /Skill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: /Skill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill skill = db.Skills.Find(id);
            db.Skills.Remove(skill);
            db.SaveChanges();
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

        private void getTagData(Skill skill)
        {
            var allTags = db.Tags;
            var skillTags = new HashSet<int>(skill.Tags.Select(c => c.ID));
            var viewModel = new List<SkillTagData>();
            foreach (var tag in allTags)
            {
                viewModel.Add(new SkillTagData
                {
                    ID = tag.ID,
                    Name = tag.Name,
                    Assigned = skillTags.Contains(tag.ID)
                });
            }
            ViewBag.Tags = viewModel;
        }
    }
}
