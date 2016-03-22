using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Questionnaire.Models;

namespace Questionnaire.Controllers
{
    public class HomeController : Controller
    {
        private QuestionnaireDBContext db = new QuestionnaireDBContext();

        public ActionResult Index()
        {
            var questionnaireList = db.QuestionnaireMaster.ToList().OrderBy(i => i.Name);

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

            //var qrName = (from x in db.QuestionnaireMaster
            //              where x.ID == id
            //              select x).FirstOrDefault();

            ViewBag.QuestionnaireName = qr.Name;

            return View();
        }
    }
}