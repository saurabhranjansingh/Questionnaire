﻿using System;
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
        //This method is called while CREATING a new dropdown question.
        public ActionResult _ViewDropDownItems(int id)
        {
            /*********
            id == 1 : Action method is called while creating question
            id == 2 : Action method is called while editing question
            *********/

            List<DrpDwnItem> DDItemsList;
            if (id == 1)
            {
                if (Session["NewQuestionDDItems"] == null)
                {
                    DDItemsList = new List<DrpDwnItem>();

                    Session["NewQuestionDDItems"] = DDItemsList;
                }
                else
                {
                    DDItemsList = (List<DrpDwnItem>)Session["NewQuestionDDItems"];
                }
            }
            else //if (id == 2)
            {
                DDItemsList = (List<DrpDwnItem>)Session["ExistingDDValues"];
            }
            
            return View(DDItemsList);
        }
   

        // GET: DropDownValue
        public ActionResult AddNewItem(string mode, string item)
        {
            List<DrpDwnItem> DDItemsList;

            if (mode.Equals("create"))
            {
                if (Session["NewQuestionDDItems"] == null)
                {
                    DDItemsList = new List<DrpDwnItem>();
                }
                else
                {
                    DDItemsList = (List<DrpDwnItem>)Session["NewQuestionDDItems"];
                }

                DDItemsList.Add(new DrpDwnItem { Value = item });
                //Session["NewQuestionDDItems"] = DDItemsList;
            }
            else //mode == "edit"
            {
                DDItemsList = (List<DrpDwnItem>)Session["ExistingDDValues"];
                DDItemsList.Add(new DrpDwnItem { Value = item });
                //Session["ExistingDDValues"] = DDItemsList;
            }

            return View("_ViewDropDownItems", DDItemsList);
        }

        public ActionResult RemoveItem(string ItemName)
        {
            List<DrpDwnItem> DDItemsList;

            //if Session["NewQuestionDDItems"] contains dropdown elements it means we are creating new question
            if (Session["NewQuestionDDItems"] != null)
            {
                DDItemsList = (List<DrpDwnItem>)Session["NewQuestionDDItems"];
                
            }
            else //We are editing an existing question
            {
                DDItemsList = (List<DrpDwnItem>)Session["ExistingDDValues"];
            }

            foreach (var item in DDItemsList)
            {
                if (item.Value.ToString().ToUpper().Trim().Equals(ItemName.ToUpper().Trim()))
                {
                    DDItemsList.Remove(item);
                    break;
                }
            }
            return View("_ViewDropDownItems", DDItemsList);            
        }
        

        // GET: DropDownValue/Create
        //public ActionResult Create()
        //{
        //    ViewBag.QuestionID = new SelectList(db.Question, "ID", "QuesText");
        //    return View();
        //}

  
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,QuestionID,Value")] DropDownValues dropDownValues)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.DropDownValues.Add(dropDownValues);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.QuestionID = new SelectList(db.Question, "ID", "QuesText", dropDownValues.QuestionID);
        //    return View(dropDownValues);
        //}

        //// GET: DropDownValue/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DropDownValues dropDownValues = db.DropDownValues.Find(id);
        //    if (dropDownValues == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.QuestionID = new SelectList(db.Question, "ID", "QuesText", dropDownValues.QuestionID);
        //    return View(dropDownValues);
        //}

        //// POST: DropDownValue/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,QuestionID,Value")] DropDownValues dropDownValues)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(dropDownValues).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.QuestionID = new SelectList(db.Question, "ID", "QuesText", dropDownValues.QuestionID);
        //    return View(dropDownValues);
        //}

        //// GET: DropDownValue/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DropDownValues dropDownValues = db.DropDownValues.Find(id);
        //    if (dropDownValues == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dropDownValues);
        //}

        //// POST: DropDownValue/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    DropDownValues dropDownValues = db.DropDownValues.Find(id);
        //    db.DropDownValues.Remove(dropDownValues);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
