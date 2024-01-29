using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManagmentSystem.Filters
{
    public class AuthorizeFilter : IAuthorizationFilter
    {
        public readonly IAuthorizationService authorizationService;

        public AuthorizeFilter(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string controllerName = context.ActionDescriptor.RouteValues["controller"];
            string actionName = context.ActionDescriptor.RouteValues["action"];

            bool isLoginPage = controllerName == "UserEntityProfile" && actionName == "Login";
            bool isReadMePage = controllerName == "Home" && actionName == "ReadMe";
            bool isFeedbackPage = controllerName == "Feedback" && actionName == "Index";

            if (!context.HttpContext.User.Identity.IsAuthenticated && !isLoginPage && !isReadMePage && !isFeedbackPage)
            {
                context.Result = new RedirectToActionResult("ReadMe", "Home", null);
                return;
            }

            return;
        }
    }
}
