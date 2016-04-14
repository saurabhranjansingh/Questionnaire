using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Questionnaire.Filters;
using Questionnaire.Models;
using Questionnaire.ViewModels;

namespace Questionnaire.Controllers
{
    [AdminAuthorize]
    public class QMController : Controller
    {
        private QuestionnaireDBContext db = new QuestionnaireDBContext();

        // GET: QM
        public ActionResult Index()
        {
            return View(db.QuestionnaireMaster.ToList().OrderBy(i => i.Name));
        }
        

        // GET: QM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QM/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateQuestionnaireViewModel cqVM)
        {
            if (ModelState.IsValid)
            {
                var questionnaireMaster = new QuestionnaireMaster {Name = cqVM.Name, IsActive = cqVM.IsActive};
                db.QuestionnaireMaster.Add(questionnaireMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cqVM);
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

            string QuesCountMsg = String.Empty;
            if (questionnaireMaster.Question.Count == 0)
            {
                QuesCountMsg = "No questions have been setup for this questionnaire.";
            }
            else if (questionnaireMaster.Question.Count == 1)
            {
                QuesCountMsg = "This questionnaire has only 1 question.";
            }
            else
            {
                QuesCountMsg = "This questionnaire has " + questionnaireMaster.Question.Count +  " questions.";
            }

            ViewBag.QuesCountMsg = QuesCountMsg;

            EditQuestionnaireViewModel eqVM = new EditQuestionnaireViewModel
            {
                ID = questionnaireMaster.ID,
                Name = questionnaireMaster.Name,
                IsActive = questionnaireMaster.IsActive
            };
            return View(eqVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditQuestionnaireViewModel eqVM)
        {
            if (ModelState.IsValid)
            {
                var qm = new QuestionnaireMaster {ID = eqVM.ID, Name = eqVM.Name, IsActive = eqVM.IsActive};

                db.Entry(qm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eqVM);
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
