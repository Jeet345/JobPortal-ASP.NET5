using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Filters
{
    public class AccountFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if (!isAjaxCall)
            {
                string action = (string)context.RouteData.Values["action"];

                if (context.HttpContext.Session.GetString("userEmail") != null
                    && !action.Equals("logout")
                    && !action.Equals("ChangePassword"))
                {
                    context.HttpContext.Response.Redirect("/");
                }
            }
        }
    }
}
