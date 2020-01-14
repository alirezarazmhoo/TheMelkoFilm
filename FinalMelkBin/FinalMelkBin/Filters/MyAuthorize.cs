using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System
{
    public class MyAuthorize:AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            
            filterContext.Result = new RedirectResult("../Error/Forbbidden");
        }
    }
    public class AjaxAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            throw new AuthorizeException();
        }
    }

    public class MyValidation : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData.ModelState.IsValid)
                return;

            var _list = (from value in filterContext.Controller.ViewData.ModelState.Values
                        from Error in value.Errors
                        select Error.ErrorMessage).ToList();

            string str = "خطا:";
            foreach (var item in _list)
            {
                str += "\n - "+item;
            }
            var ob = new {NotValidation=true,Message=str };
            JsonResult js = new JsonResult();
            js.Data = ob;
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            filterContext.Result = js;
        }
    }
}