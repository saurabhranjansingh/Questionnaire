using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Questionnaire.Models;

namespace Questionnaire.Controllers
{
    public class DropDownValueController : Controller
    {
        private QuestionnaireDBContext db = new QuestionnaireDBContext();

        // GET: DropDownValue
        public ActionResult Index()
        {
            var dropDownValues = db.DropDownValues.Include(d => d.Question);
            return View(dropDownValues.ToList());
        }

        // GET: DropDownValue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DropDownValues dropDownValues = db.DropDownValues.Find(id);
            if (dropDownValues == null)
            {
                return HttpNotFound();
            }
            return View(dropDownValues);
        }

        // GET: DropDownValue/Create
        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.Question, "ID", "QuesText");
            return View();
        }

        // POST: DropDownValue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,QuestionID,Value")] DropDownValues dropDownValues)
        {
            if (ModelState.IsValid)
            {
                db.DropDownValues.Add(dropDownValues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Question, "ID", "QuesText", dropDownValues.QuestionID);
            return View(dropDownValues);
        }

        // GET: DropDownValue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DropDownValues dropDownValues = db.DropDownValues.Find(id);
            if (dropDownValues == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Question, "ID", "QuesText", dropDownValues.QuestionID);
            return View(dropDownValues);
        }

        // POST: DropDownValue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,QuestionID,Value")] DropDownValues dropDownValues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dropDownValues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Question, "ID", "QuesText", dropDownValues.QuestionID);
            return View(dropDownValues);
        }

        // GET: DropDownValue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DropDownValues dropDownValues = db.DropDownValues.Find(id);
            if (dropDownValues == null)
            {
                return HttpNotFound();
            }
            return View(dropDownValues);
        }

        // POST: DropDownValue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DropDownValues dropDownValues = db.DropDownValues.Find(id);
            db.DropDownValues.Remove(dropDownValues);
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
    }
}
