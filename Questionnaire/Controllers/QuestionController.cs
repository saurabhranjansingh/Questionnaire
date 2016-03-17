using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using Questionnaire.Models;

namespace Questionnaire.Controllers
{
    public class QuestionController : Controller
    {
        private QuestionnaireDBContext db = new QuestionnaireDBContext();

        // GET: Question
        public ActionResult Index(int id)
        {
            var qr = db.QuestionnaireMaster.Find(id);
            if (qr == null)
            {
                return HttpNotFound();
            }

            
            //var qrName = (from x in db.QuestionnaireMaster
            //                            where x.ID == id
            //                            select x).FirstOrDefault();

            ViewBag.QuestionnaireName = qr.Name;
            ViewBag.QuestionnaireID = qr.ID;

            var question = from x in db.Question
                           join y in db.QuestionnaireMaster on x.QuestionnaireID equals y.ID
                           where y.ID == id
                           select x;

            return View(question.ToList());
        }

        // GET: Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Question/Create
        public ActionResult Create(int id)
        {
            var qr = db.QuestionnaireMaster.Find(id);
            if (qr == null)
            {
                return HttpNotFound();
            }

            ViewBag.QuestionnaireName = qr.Name;
            ViewBag.QuestionnaireID = qr.ID;

            ViewBag.QuestionType = new SelectList(db.QuestionType, "ID", "QuesType");
            return View();
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,QuestionnaireID,QuestionType,Hierarchy,QuesText")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Question.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionnaireID = new SelectList(db.QuestionnaireMaster, "ID", "Name", question.QuestionnaireID);
            ViewBag.QuestionType = new SelectList(db.QuestionType, "ID", "QuesType", question.QuestionType);
            return View(question);
        }

        // GET: Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionnaireID = new SelectList(db.QuestionnaireMaster, "ID", "Name", question.QuestionnaireID);
            ViewBag.QuestionType = new SelectList(db.QuestionType, "ID", "QuesType", question.QuestionType);
            return View(question);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,QuestionnaireID,QuestionType,Hierarchy,QuesText")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionnaireID = new SelectList(db.QuestionnaireMaster, "ID", "Name", question.QuestionnaireID);
            ViewBag.QuestionType = new SelectList(db.QuestionType, "ID", "QuesType", question.QuestionType);
            return View(question);
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Question.Find(id);
            db.Question.Remove(question);
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
