using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
namespace WEB_API_CORE_AuthenticationFilterDemo
{
  
   public class CustomAuthenticationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the Authorization header exists
            var authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authHeader))
            {
                context.Result = new UnauthorizedResult(); // Return 401 Unauthorized
                return;
            }

            // Validate the header (e.g., Basic Authentication format: Basic <Base64(username:password)>)
            if (!authHeader.ToString().StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Decode and validate credentials
            var encodedCredentials = authHeader.ToString().Substring("Basic ".Length).Trim();
            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
            var credentials = decodedCredentials.Split(':', 2);

            if (credentials.Length != 2 || !IsValidUser(credentials[0], credentials[1]))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // If authentication succeeds, proceed
        }

        private bool IsValidUser(string username, string password)
        {
               return username == "student" && password == "india";
        }
    }

}
