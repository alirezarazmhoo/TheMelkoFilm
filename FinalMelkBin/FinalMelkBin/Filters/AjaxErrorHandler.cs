using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System
{
    public class AjaxErrorHandler : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {

            int ErrorCode = ErrorHandler.GetErrorCode(filterContext.Exception);
            filterContext.Result = new HttpStatusCodeResult(ErrorCode);
            filterContext.ExceptionHandled = true;
        }
    }
}