using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace web_api.Exceptions
{
    [Serializable()]
    public class ForbiddenItemType : Exception
    {
        public ForbiddenItemType() : base() { }
        public ForbiddenItemType(string message) : base(message) { }
        public ForbiddenItemType(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected ForbiddenItemType(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }

    public class ForbiddenItemTypeResult : ObjectResult
    {
        public ForbiddenItemTypeResult() : base(null)
        {
        }

        public ForbiddenItemTypeResult(object value) : base(value)
        {
        }

        public override Task ExecuteResultAsync(ActionContext context) {
            var response = context.HttpContext.Response;
            response.StatusCode = StatusCode ?? 400;
            return Task.CompletedTask;
        }
    }

    public class LevelFilterAttribute : ExceptionFilterAttribute {
        public override void OnException(ExceptionContext context) 
        {

            if (context.Exception is ForbiddenItemType) {
                var result = new ContentResult();
                result.StatusCode = 400;
                result.Content = context.Exception.Message;
                context.Result = result;
            }
            /*if (context.ExceptionHandled)
                return;

            if (context.Exception is ForbiddenItemType)
            {
                context.ExceptionHandled = true;
                var viewResult = new ViewResult();
                viewResult.ViewData["ForbiddenItemType"] = context.Exception.Message;
                context.Result = viewResult;
            }*/
        }
    }
}