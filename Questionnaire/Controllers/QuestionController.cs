using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Questionnaire.Filters;
using Questionnaire.Models;
using Questionnaire.ViewModels;

namespace Questionnaire.Controllers
{
    [AdminAuthorize]
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


            ViewBag.QuestionnaireName = qr.Name;
            ViewBag.QuestionnaireID = qr.ID;

            var questions = from x in db.Question
                            join y in db.QuestionnaireMaster on x.QuestionnaireID equals y.ID
                            where y.ID == id
                            orderby x.Hierarchy ascending
                            select new ViewQuestionsViewModel
                            {
                                ID = x.ID,
                                Hierarchy = x.Hierarchy,
                                QuesText = x.QuesText,
                                QuestionType = x.QuestionType1.QuesType
                            };


            return View(questions.ToList());
        }


        // GET: Question/Create
        public ActionResult Create(int id)
        {
            Session["NewQuestionDDItems"] = null;
            Session["ExistingDDValues"] = null;

            var qr = db.QuestionnaireMaster.Find(id);
            if (qr == null)
            {
                return HttpNotFound();
            }

            ViewBag.QuestionnaireName = qr.Name;

            var cqVM = new CreateQuestionViewModel
            {
                Hierarchy = qr.Question.Count + 1,
                QuestionnaireID = id
            };


            //ViewBag.QuestionnaireID = qr.ID;
            //ViewBag.QuestionPosition = qr.Question.Count + 1;

            ViewBag.QuestionType = new SelectList(db.QuestionType, "ID", "QuesType");
            return View(cqVM);
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateQuestionViewModel cqVM)
        {
            if (ModelState.IsValid)
            {
                List<DropDownValues> d = new List<DropDownValues>();

                //If its a dropdown question
                if (cqVM.QuestionType == 2)
                {
                    var DDItemsList = (List<DrpDwnItem>)Session["NewQuestionDDItems"];

                    foreach (var item in DDItemsList)
                    {
                        d.Add(new DropDownValues { Value = item.Value });
                    }
                }

                Question q = new Question
                {
                    QuestionnaireID = cqVM.QuestionnaireID,
                    QuesText = cqVM.QuesText,
                    QuestionType = cqVM.QuestionType,
                    Hierarchy = cqVM.Hierarchy,
                    DropDownValues = d

                };

                db.Question.Add(q);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = cqVM.QuestionnaireID });
            }

            return View(cqVM);
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

            Session["NewQuestionDDItems"] = null;
            Session["ExistingDDValues"] = null;

            List<DrpDwnItem> ddValues = (from x in question.DropDownValues
                                         where x.QuestionID == question.ID
                                         select new DrpDwnItem
                                         {
                                             QuestionID = question.ID,
                                             ID = x.ID,
                                             Value = x.Value
                                         }).ToList();

            Session["ExistingDDValues"] = ddValues;

            EditQuestionViewModel eqVM = new EditQuestionViewModel
            {
                QuestionID = question.ID,
                QuestionnaireID = question.QuestionnaireID,
                Questionnaire = question.QuestionnaireMaster.Name,
                QuestionType = question.QuestionType1.QuesType,
                QuesText = question.QuesText,
                Hierarchy = question.Hierarchy
            };
            return View(eqVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditQuestionViewModel eqVM)
        {
            var ques = (from q in db.Question
                        where q.ID == eqVM.QuestionID
                        select q).FirstOrDefault();

            //Update the question text if required
            if (!ques.QuesText.Equals(eqVM.QuesText.Trim()))
            {
                ques.QuesText = eqVM.QuesText;
            }

            //Update the dropdown values
            if (ques.QuestionType == 2)
            {
                //Remove all the existing items.
                db.DropDownValues.RemoveRange(db.DropDownValues.Where(k => k.QuestionID == ques.ID));

                //add new rows
                var d = new List<DropDownValues>();

                var DDItemsList = (List<DrpDwnItem>)Session["ExistingDDValues"];

                foreach (var item in DDItemsList)
                {
                    var ddv = new DropDownValues
                    {
                        QuestionID = ques.ID,
                        Value = item.Value
                    };
                    db.DropDownValues.Add(ddv);
                }
            }

            db.SaveChanges();

            return RedirectToAction("Index", new { id = eqVM.QuestionnaireID });
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
            //Call the Stored Proc spDeleteQuestion
            int? questionnaireId = db.DeleteQuestion(id).FirstOrDefault();
            return RedirectToAction("Index", new { id = questionnaireId });
        }

        //GET
        public ActionResult Hierarchy(int id)
        {
            var qr = db.QuestionnaireMaster.Find(id);
            if (qr == null)
            {
                return HttpNotFound();
            }

            ViewBag.QuestionnaireName = qr.Name;
            ViewBag.QuestionnaireID = qr.ID;

            var questions = from x in db.Question
                            join y in db.QuestionnaireMaster on x.QuestionnaireID equals y.ID
                            where y.ID == id
                            orderby x.Hierarchy ascending
                            select new EditHierarchyViewModel
                            {
                                QuestionID = x.ID,
                                Hierarchy = x.Hierarchy,
                                QuesText = x.QuesText,
                                QuestionType = x.QuestionType1.QuesType
                            };


            return View(questions.ToList());
        }

        [HttpPost]
        public ActionResult Hierarchy(UpdatedHierarchyOrder s)
        {
            var DbNeedsToBeUpdated = false;
            for (int i = 1; i <= s.newOrder.Count; i++)
            {
                //The id of the element is in form : id-<quesID>-<Hierarchy>. eg id-1109-3
                var arr = s.newOrder[i - 1].Split('-');

                var oriHierarchy = int.Parse(arr[2]);
                var newHierarchy = i;
                if (newHierarchy != oriHierarchy)
                {
                    var quesId = int.Parse(arr[1]);
                    //Hierarchy needs to be updated.   
                    var ques = (from q in db.Question
                                where q.ID == quesId
                                select q).FirstOrDefault();

                    ques.Hierarchy = newHierarchy;
                    DbNeedsToBeUpdated = true;
                }
            }
            if (DbNeedsToBeUpdated)
            {
                db.SaveChanges();
            }

            return Json(Url.Action("Index", "Question", new { id = s.qnnrID}));
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
