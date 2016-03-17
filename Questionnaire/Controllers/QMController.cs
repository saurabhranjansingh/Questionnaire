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
    public class QMController : Controller
    {
        private QuestionnaireDBContext db = new QuestionnaireDBContext();

        // GET: QM
        public ActionResult Index()
        {
            return View(db.QuestionnaireMaster.ToList());
        }
        

        // GET: QM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] QuestionnaireMaster questionnaireMaster)
        {
            if (ModelState.IsValid)
            {
                db.QuestionnaireMaster.Add(questionnaireMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionnaireMaster);
        }

        // GET: QM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionnaireMaster questionnaireMaster = db.QuestionnaireMaster.Find(id);
            if (questionnaireMaster == null)
            {
                return HttpNotFound();
            }
            return View(questionnaireMaster);
        }

        // POST: QM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] QuestionnaireMaster questionnaireMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionnaireMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionnaireMaster);
        }

        // GET: QM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionnaireMaster questionnaireMaster = db.QuestionnaireMaster.Find(id);
            if (questionnaireMaster == null)
            {
                return HttpNotFound();
            }
            return View(questionnaireMaster);
        }

        // POST: QM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionnaireMaster questionnaireMaster = db.QuestionnaireMaster.Find(id);
            db.QuestionnaireMaster.Remove(questionnaireMaster);
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
