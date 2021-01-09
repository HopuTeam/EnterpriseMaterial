using Microsoft.AspNetCore.Mvc.Filters;


namespace EnterpriseMaterial.Web.Filters
{
    public class AuthFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var user = context.HttpContext.Session.GetModel<Model.User>("User");
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
