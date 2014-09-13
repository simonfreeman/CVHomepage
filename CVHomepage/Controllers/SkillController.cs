using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CVHomepage.Models;
using CVHomepage.DAL;
using CVHomepage.ViewModels;
using CVHomepage.Helpers.SessionHelpers;
using Microsoft.AspNet.Identity;
using PagedList;

namespace CVHomepage.Controllers
{
    public class SkillController : Controller
    {
        private CVContext db = new CVContext();

        // GET: /Skill/
        [Authorize]
        public ActionResult Index(int? page)
        {
            string user = User.Identity.GetUserId();
            var skills = db.Skills.Include(s => s.Category)
                .Include(s => s.Tags)
                .Where(a => a.User == user)
                .OrderBy(a => a.Name);

            //this is used to tell people what cv they are going to add the skill to
            if (SessionHelpers.CurrentCV != 0)
            {
                var currentCV = db.CVs.Find(SessionHelpers.CurrentCV);
                ViewBag.CVName = currentCV.Name;
            }

            if (TempData["Message"] != null)
            {
                ViewBag.message = TempData["Message"];
            }

            //paged list stuff
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(skills.ToPagedList(pageNumber, pageSize));
        }


        // GET: /Skill/Create
        [Authorize]
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Notes,CVText,CategoryID,User")] Skill skill,string[]selectedTags)
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
                TempData["message"] = skill.Name + " has been created";
                return RedirectToAction("Index");
                
            }
            getTagData(skill);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", skill.CategoryID);
            return View(skill);
        }

        // GET: /Skill/Edit/5
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
                Skill skill = db.Skills
                    .Where(a => a.ID == id && a.User == user)
                    .Single() as Skill;
                getTagData(skill);
                if (skill == null)
                {
                    return HttpNotFound();
                }

                ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", skill.CategoryID);
                return View(skill);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        
            
        }

        // POST: /Skill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include="ID,Name,Notes,CVText,CategoryID,User")]Skill skill, string[] selectedTags)
        {
            //for some reason it's not understanding the skill being returned exists, so I'm using it's info
            // to find and then update a different skill variable.

            string user = User.Identity.GetUserId();
            try
            {
                Skill skillToUpdate = db.Skills
                    .Include(i => i.Tags)
                    .Include(i => i.Category)
                    .Where(a => a.ID == skill.ID && a.User == user)
                    .Single() as Skill;

                if (skillToUpdate == null)
                {
                    return HttpNotFound();
                }

                skillToUpdate.Name = skill.Name;
                skillToUpdate.Notes = skill.Notes;
                skillToUpdate.CVText = skill.CVText;
                skillToUpdate.CategoryID = skill.CategoryID;

                skillToUpdate.Tags = new List<Tag>();
                if (selectedTags != null)
                {
                    foreach (var tag in selectedTags)
                    {
                        var tagToAdd = db.Tags.Find(int.Parse(tag));
                        skillToUpdate.Tags.Add(tagToAdd);
                    }
                }

               

                if (ModelState.IsValid)
                {

                    db.Entry(skillToUpdate).State = EntityState.Modified;

                    db.SaveChanges();

                    TempData["message"] = skill.Name + " has been edited";
                    return RedirectToAction("Index");
                }

                getTagData(skill);
                ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", skill.CategoryID);
                return View(skill);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
         
        }


        // GET: /Skill/Delete/5
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
                Skill skill = db.Skills
                    .Where(a => a.ID == id && a.User == user)
                    .Single() as Skill;
                if (skill == null)
                {
                    return HttpNotFound();
                }

                return View(skill);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
           

        }


        // POST: /Skill/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            string user = User.Identity.GetUserId();
            try
            {
                Skill skill = db.Skills
                    .Where(a => a.ID == id && a.User == user)
                    .Single() as Skill;
                if (skill == null)
                {
                    return HttpNotFound();
                }

                db.Skills.Remove(skill);
                db.SaveChanges();
                TempData["message"] = skill.Name + " has been deleted";
                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //deals with getting all of the tags and marking all the ones the skill is currently using
        [Authorize]
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
