using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace web_api.Exceptions
{
    [Serializable()]
    public class ForbiddenItemType : System.Exception
    {
        public ForbiddenItemType() : base() { }
        public ForbiddenItemType(string message) : base(message) { }
        public ForbiddenItemType(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected ForbiddenItemType(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
    
    public class LevelFilter : ExceptionFilterAttribute {
        public override void OnException(ExceptionContext context) 
        {
            if (context.ExceptionHandled)
                return;

            if (context.Exception is ForbiddenItemType)
            {
                context.ExceptionHandled = true;
                var viewResult = new ViewResult();
                viewResult.ViewData["ForbiddenItemType"] = context.Exception.Message;
                context.Result = viewResult;
            }
        }
    }
}