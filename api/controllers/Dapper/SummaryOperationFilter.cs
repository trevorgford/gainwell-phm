using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Gainwell.Controllers.Dapper;

public class SummaryOperationFilter : IOperationFilter {

    public void Apply(OpenApiOperation operation, OperationFilterContext context) {

        if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor) {

            switch (controllerActionDescriptor.ActionName) {
                case "GetAll":
                    operation.Summary = $"Gets all {controllerActionDescriptor.ControllerName} records";
                    break;
                case "GetById":
                    operation.Summary = $"Gets a {controllerActionDescriptor.ControllerName}";
                    break;
                case "Create":
                    operation.Summary = $"Creates a {controllerActionDescriptor.ControllerName}";
                    break;
                case "Deactivate":
                    operation.Summary = $"Deactivates a {controllerActionDescriptor.ControllerName}";
                    break;
                case "Activate":
                    operation.Summary = $"Activates a {controllerActionDescriptor.ControllerName}";
                    break;
            }

        }

    }

}
