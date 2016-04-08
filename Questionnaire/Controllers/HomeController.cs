using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Questionnaire.HTMLBuilder;
using Questionnaire.Models;
using Questionnaire.ViewModels;

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

        [HttpPost]
        //save the dynamically generated report.
        public JsonResult Save(UserResponse response)
        {
            Guid reportId = Guid.NewGuid();
            DateTime CurrentTime = Convert.ToDateTime(DateTime.Now, CultureInfo.GetCultureInfo("en-us"));
            DateTime DateOfCall = DateTime.ParseExact(response.staticInputs[0].response, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None); //DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            var basicInfo = new ReportMaster
            {
                //uniqueid
                ReportID = reportId,
                Field1 = DateOfCall,
                Field2 = response.staticInputs[1].response,
                Field3 = response.staticInputs[2].response,
                Field4 = response.staticInputs[3].response,
                Field5 = response.staticInputs[4].response,
                Contact = response.staticInputs[5].response,
                Email = response.staticInputs[6].response,
                CreatedAt = CurrentTime,
                QuestionnaireID = int.Parse(response.qnnrID)
            };

            //save the basic information
            db.ReportMaster.Add(basicInfo);
            db.SaveChanges();

            //System.Data.Entity.DbSet<Question> questionSet = db.Question;
            var questionSet = from q in db.Question
                              select q;

            //save the report
            foreach (var report in response.dynamicInputs)
            {
                int questionID = int.Parse(report.quesID);
                var ques = (from question in questionSet
                            where question.ID == questionID
                            select new
                            {
                                Hierarchy = question.Hierarchy,
                                Text = question.QuesText
                            }).FirstOrDefault();

                var dynUserInfo = new Report
                {
                    ReportID = reportId,
                    Text = ques.Text,
                    Heirarchy = ques.Hierarchy,
                    Response = string.IsNullOrWhiteSpace(report.response) ? null : report.response.Trim()
                };

                db.Report.Add(dynUserInfo);
            }

            db.SaveChanges();

            return Json(Url.Action("Index", "Home"));
        }
    }
}