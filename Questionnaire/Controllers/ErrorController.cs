using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Controllers
{
    public class ErrorController : Controller
    {
        //403
        public ActionResult Unauthorised()
        {
            return View();
        }

        //500
        public ActionResult UnexpectedError()
        {
            return View();
        }

        //404
        public ActionResult PageNotFound()
        {
            return View();
        }

        
    }
}
