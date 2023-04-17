using Microsoft.AspNetCore.Mvc.Filters;

namespace KindRegardsApi.Presentation.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class RequireDeviceIdHeaderAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("deviceId"))
            {
                var errors = new Dictionary<string, string>();

                errors.Add("Message", "Missing header: 'deviceId'.");

                context.Result = new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(errors);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This method purely exists because the interface needs to be fully implemented
        }
    }
}
