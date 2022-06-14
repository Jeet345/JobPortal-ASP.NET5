using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace JobPortal.Filters
{
    
    public class GlobalFilter : Attribute, IActionFilter, IOrderedFilter
    {
        public GlobalFilter(int order = -20)
        {
            Order = order;
        }

        public int Order { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
            Console.WriteLine("Global Filter Executed");
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {

            Console.WriteLine("Global Filter Executing");
            string currentPath = context.HttpContext.Request.Path;

            if (currentPath.Equals("/"))
            {
                if (context.HttpContext.Session.GetString("groupName") != null)
                {
                    if (context.HttpContext.Session.GetString("groupName").Equals("Employer"))
                    {
                        context.HttpContext.Response.Redirect("/Employer");
                    }
                    else if (context.HttpContext.Session.GetString("groupName").Equals("Admin"))
                    {
                        context.HttpContext.Response.Redirect("/Admin");
                    }
                    else
                    {
                        context.HttpContext.Response.Redirect("/Jobseeker");
                    }
                }
                else
                {
                    context.HttpContext.Response.Redirect("/Jobseeker");
                }

            }
        }

    }
}
