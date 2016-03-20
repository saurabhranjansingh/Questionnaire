﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using Questionnaire.Models;
using Questionnaire.ViewModels;

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

            var questions = from x in db.Question
                           join y in db.QuestionnaireMaster on x.QuestionnaireID equals y.ID
                           where y.ID == id
                           select new ViewQuestionsViewModel
                           {
                               ID = x.ID,
                               Hierarchy = x.Hierarchy,
                               QuesText = x.QuesText,
                               QuestionType = x.QuestionType1.QuesType
                           };


            return View(questions.ToList());
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
            ViewBag.QuestionPosition = qr.Question.Count + 1;

            ViewBag.QuestionType = new SelectList(db.QuestionType, "ID", "QuesType");
            return View();
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,QuestionnaireID,QuestionType,Hierarchy,QuesText")] Question question)
        public ActionResult Create(CreateQuestionViewModel cqVM)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Question.Add(question);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.QuestionnaireID = new SelectList(db.QuestionnaireMaster, "ID", "Name", question.QuestionnaireID);
            //ViewBag.QuestionType = new SelectList(db.QuestionType, "ID", "QuesType", question.QuestionType);
            return View();
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
