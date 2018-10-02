using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace web_api.Middleware
{
    public class AuthenticationKey
    {
        public string AuthKey {get; set;}
    }

    public class AuthenticationMiddleware
    {
        private readonly AuthenticationKey _key;
        private readonly string _adminKey;
        private RequestDelegate _next;

        public AuthenticationMiddleware(IOptions<AuthenticationKey> key, RequestDelegate next)
        {
            _key = key.Value;
            _next = next;
            _adminKey = "admin123";

        }

        public async Task Invoke(HttpContext context)
        {
            bool adminOnly = false;
            var requestType = context.Request.Method.ToUpper();
            if (requestType.Equals("DELETE"))
            {
                adminOnly = true;
            }
            StringValues values;

            bool hasKey = context.Request.Headers.TryGetValue("authKey", out values);
            bool authorizedUser = false;

            if (hasKey)
            {
                string[] keys = values.ToArray();
                foreach (string k in keys)
                {
                    if (adminOnly)
                    {
                        if (k.Equals(_adminKey)) authorizedUser = true;
                    }
                    else
                    {
                        if (k.Equals(_adminKey)|| k.Equals(_key.AuthKey)) authorizedUser = true;
                    }
                }
            }

            if (authorizedUser)
            {
                await _next(context);
            }
            else if (!hasKey)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Bad request");
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden");
            }
        } 
    }
}