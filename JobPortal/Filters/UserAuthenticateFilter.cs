using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Filters
{
    public class UserAuthenticateFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string controller = (string)context.RouteData.Values["controller"];

            if (context.HttpContext.Session.GetString("userEmail") == null)
            {
                context.HttpContext.Response.Redirect("/");
            }
            else
            {
                if (!context.HttpContext.Session.GetString("groupName").Equals(controller))
                {
                    context.HttpContext.Response.Redirect("/");
                }
            }

        }
    }
}
