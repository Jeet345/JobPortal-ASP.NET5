using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Filters
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class GlobalFilter : Attribute, IActionFilter,IOrderedFilter
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
