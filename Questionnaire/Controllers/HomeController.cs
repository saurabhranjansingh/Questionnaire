using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Questionnaire.HTMLBuilder;
using Questionnaire.Models;

namespace Questionnaire.Controllers
{
    public class HomeController : Controller
    {
        private QuestionnaireDBContext db = new QuestionnaireDBContext();

        public ActionResult Index()
        {
            var questionnaireList = db.QuestionnaireMaster.Where(k => k.IsActive == true).ToList().OrderBy(i => i.Name);

            if (!questionnaireList.Any())
            {
                ViewBag.NoQnnrExist = true;
            }
            else
            {
                ViewBag.NoQnnrExist = false;

                var items = new List<SelectListItem>();

                foreach (var i in questionnaireList)
                {
                    items.Add(new SelectListItem { Text = i.Name, Value = i.ID.ToString() });
                }

                ViewBag.QuestionnaireList = items;
            }

            return View();
        }

        public ActionResult Fillup(int id)
        {
            var qr = db.QuestionnaireMaster.Find(id);
            if (qr == null)
            {
                return HttpNotFound();
            }

            ViewBag.NumberOfQues = qr.Question.Count;
            ViewBag.QuestionnaireName = qr.Name;
            ViewBag.QuestionairreID = qr.ID;
            ViewBag.GeneratedHtml = FetchOutputHtml(id);
           
            return View();
        }

        [NonAction]
        private string FetchOutputHtml(int QnnrId)
        {
            //Get List of all the questions for this Questionnaire
            var questions = (from q in db.Question
                             where q.QuestionnaireID == QnnrId
                             orderby q.Hierarchy
                             select q).ToList();
           

            //Get List of all the dropdown values for the questions in Questionnaire
            var dropDownChoices = (from ddv in db.DropDownValues
                                   join q in db.Question on ddv.QuestionID equals q.ID
                                   where q.QuestionnaireID == QnnrId
                                   select ddv).ToList();
            
            return HtmlBuilder.Build(questions, dropDownChoices);
        }
    }
}