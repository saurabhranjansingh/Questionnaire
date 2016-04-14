using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Questionnaire.Filters
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session == null || httpContext.Session["IsAdmin"] == null)
                return false;

            if ((bool)httpContext.Session["IsAdmin"])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                        new
                        {
                            controller = "Error",
                            action = "Unauthorised"
                        }));
        }

    }
}