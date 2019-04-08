using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ContaCorrente.CrossCutting.Filters
{
    public class ErrorHandlingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                base.OnActionExecuting(context);
            else
                context.Result = new JsonResult(GetModelStateErrors(context)) { StatusCode = (int)HttpStatusCode.BadRequest };
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ApplicationException)
            {
                context.Result = new JsonResult(GetBusinessExceptionErrors(context)) { StatusCode = (int)HttpStatusCode.BadRequest };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is Exception)
            {
                context.Result = new JsonResult(GetBusinessExceptionErrors(context)) { StatusCode = (int)HttpStatusCode.InternalServerError };
                context.ExceptionHandled = true;
            }
            else
            {
                base.OnActionExecuted(context);
            }
        }

        private IEnumerable<string> GetBusinessExceptionErrors(ActionExecutedContext context)
        {
            return new[] { context.Exception.Message };
        }

        private IEnumerable<string> GetModelStateErrors(ActionExecutingContext context)
        {
            return context.ModelState.Values.Where(x => !context.ModelState.IsValid)
                                            .SelectMany(e => e.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToArray();
        }
    }
}
