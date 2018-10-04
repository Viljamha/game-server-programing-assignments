using System;
using web_api.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace web_api.Filters
{
    public class AuditActionFilter : ActionFilterAttribute
    {
        IRepository repository;

        public AuditActionFilter(IRepository repos) {
            repository = repos;
        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            var httpContext = context.HttpContext;
            var request = httpContext.Request;
            var requestType = request.Method.ToUpper();
            string ipString = httpContext.Connection.RemoteIpAddress.ToString();

            if (requestType.Equals("DELETE"))
            {
                repository.LogRequestStart(ipString, DateTime.Now);
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context) {
            var httpContext = context.HttpContext;
            var request = httpContext.Request;
            var requestType = request.Method.ToUpper();
            string ipString = httpContext.Connection.RemoteIpAddress.ToString();
            
            if (requestType.Equals("DELETE"))
            {
                repository.LogRequestFinished(ipString, DateTime.Now);
            }
        }
    }
}