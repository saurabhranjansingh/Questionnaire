using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Questionnaire.Models;
using Questionnaire.ViewModels;

namespace Questionnaire.Controllers
{
    public class DropDownValueController : Controller
    {
        private QuestionnaireDBContext db = new QuestionnaireDBContext();

        // GET: DropDownValue
        public ActionResult _ViewDropDownItems(int id)
        {
            List<NewDDItem> DDItemsList;

            if (Session["NewQuestionDDItems"] == null)
            {
                DDItemsList = new List<NewDDItem>();
                DDItemsList.Add(new NewDDItem {QuestionID = 1, Value = "ABC"});
                DDItemsList.Add(new NewDDItem { QuestionID = 1, Value = "DEF" });

                Session["NewQuestionDDItems"] = DDItemsList;
            }
            else
            {
                DDItemsList = (List<NewDDItem>) Session["NewQuestionDDItems"];
            }
            return View(DDItemsList);
            //var ddItems = from x in db.DropDownValues
            //              where x.QuestionID == id
            //              select new ViewDDItemsViewModel
            //              {
            //                  ID = x.ID,
            //                  QuestionID = x.QuestionID,
            //                  Value = x.Value
            //              };

            //var ddItemsViewModel = new DropDownItemsViewModel
            //{
            //    ID =
            //}

            //var dropDownValues = db.DropDownValues.Include(d => d.Question);
            //return System.Web.UI.WebControls.View(ddItems.ToList());
        }

        public ActionResult RemoveItem(string ItemName)
        {
            List<NewDDItem> DDItemsList = (List<NewDDItem>) Session["NewQuestionDDItems"];
            foreach (var item in DDItemsList)
            {
                if(item.Value.ToString().ToUpper().Trim().Equals(ItemName.ToUpper().Trim()))
                {
                    DDItemsList.Remove(item);
                    break;
                }
            }
            return View("_ViewDropDownItems", DDItemsList);            
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
