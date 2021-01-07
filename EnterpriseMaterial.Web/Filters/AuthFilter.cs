using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EnterpriseMaterial.Web.Filters
{
    public class AuthFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.Session.GetModel<Model.User>("User");
            if (user == null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("/Sign/index");
            }


        }
    }
}
