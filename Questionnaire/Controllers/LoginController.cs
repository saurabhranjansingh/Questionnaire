using Questionnaire.Models;
using Questionnaire.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PerformLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            Session["IsAdmin"] = null;

            if (model.Password.Equals("12345"))
            {
                Session["IsAdmin"] = true;
                return RedirectToAction("Index", "QM");
            }
            else
            {
                ModelState.AddModelError("INVALID_PWD", "Invalid login attempt.");
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            if (Session["IsAdmin"] != null)
                Session["IsAdmin"] = null;

            return View();
        }
    }
}