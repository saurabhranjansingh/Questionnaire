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
    public class ReportMasterController : Controller
    {
        private QuestionnaireDBContext db = new QuestionnaireDBContext();

        // GET: ReportMasters
        public ActionResult Index()
        {
            var reportMaster = db.ReportMaster.Include(r => r.QuestionnaireMaster);
            return View(reportMaster.ToList());
        }

        // GET: ReportMasters/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportMaster reportMaster = db.ReportMaster.Find(id);
            if (reportMaster == null)
            {
                return HttpNotFound();
            }
            return View(reportMaster);
        }

        // GET: ReportMasters/Create
        public ActionResult Create()
        {
            ViewBag.QuestionnaireID = new SelectList(db.QuestionnaireMaster, "ID", "Name");
            return View();
        }

        // POST: ReportMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportID,QuestionnaireID,Field1,Field2,Field3,Field4,Field5,Contact,Email,CreatedAt")] ReportMaster reportMaster)
        {
            if (ModelState.IsValid)
            {
                reportMaster.ReportID = Guid.NewGuid();
                db.ReportMaster.Add(reportMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionnaireID = new SelectList(db.QuestionnaireMaster, "ID", "Name", reportMaster.QuestionnaireID);
            return View(reportMaster);
        }

        // GET: ReportMasters/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportMaster reportMaster = db.ReportMaster.Find(id);
            if (reportMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionnaireID = new SelectList(db.QuestionnaireMaster, "ID", "Name", reportMaster.QuestionnaireID);
            return View(reportMaster);
        }

        // POST: ReportMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportID,QuestionnaireID,Field1,Field2,Field3,Field4,Field5,Contact,Email,CreatedAt")] ReportMaster reportMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionnaireID = new SelectList(db.QuestionnaireMaster, "ID", "Name", reportMaster.QuestionnaireID);
            return View(reportMaster);
        }

        // GET: ReportMasters/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportMaster reportMaster = db.ReportMaster.Find(id);
            if (reportMaster == null)
            {
                return HttpNotFound();
            }
            return View(reportMaster);
        }

        // POST: ReportMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ReportMaster reportMaster = db.ReportMaster.Find(id);
            db.ReportMaster.Remove(reportMaster);
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
