using Gainwell.Controllers.Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gainwell.Filters;

public class UserJwtClaimsFilter : IActionFilter {

    public void OnActionExecuting(ActionExecutingContext context) {
        if (context.Controller is not IController controller) return;

        var userClaims = context.HttpContext.User;
        if (userClaims == null || userClaims.Identity == null || !userClaims.Identity.IsAuthenticated) {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim == null) {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (int.TryParse(userIdClaim.Value, out int userId)) {
            controller.UserId = userId;
        }
        else context.Result = new UnauthorizedResult();
    }

    public void OnActionExecuted(ActionExecutedContext context) {
        // Not implemented
    }

}