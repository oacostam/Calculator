using Calculator.Sdk.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Calculator.Api.Infrastructure
{
    public class IdHeaderParserFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.ContainsKey("X-­Evi-­Tracking-­Id"))
            {
                if (int.TryParse(context.HttpContext.Request.Headers["X-­Evi-­Tracking-­Id"], out int result))
                {
                    IHaveOperationId tmp = (IHaveOperationId)context.ActionArguments.Values.AsQueryable().FirstOrDefault(v => v is IHaveOperationId);
                    if (tmp != null)
                    {
                        tmp.OperationId = result;
                    }
                }
            }
        }
    }
}
