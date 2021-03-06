﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVHomepage.Models;
using CVHomepage.DAL;

namespace CVHomepage.Controllers
{
    public class TagController : Controller
    {
        private CVContext db = new CVContext();

        //// GET: /Tag/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }

        //// GET: /Tag/Details/5
        //[Authorize]
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tag tag = db.Tags.Find(id);
        //    if (tag == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tag);
        //}

        //// GET: /Tag/Create
        //[Authorize]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /Tag/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="ID,Name,Description")] Tag tag)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tags.Add(tag);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(tag);
        //}

        //// GET: /Tag/Edit/5
        //[Authorize]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tag tag = db.Tags.Find(id);
        //    if (tag == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tag);
        //}

        //// POST: /Tag/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="ID,Name,Description")] Tag tag)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tag).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(tag);
        //}

        //// GET: /Tag/Delete/5
        //[Authorize]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tag tag = db.Tags.Find(id);
        //    if (tag == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tag);
        //}

        //// POST: /Tag/Delete/5
        //[Authorize]
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Tag tag = db.Tags.Find(id);
        //    db.Tags.Remove(tag);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
