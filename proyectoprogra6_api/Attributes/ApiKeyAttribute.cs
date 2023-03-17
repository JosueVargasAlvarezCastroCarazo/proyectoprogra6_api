using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace proyectoprogra6_api.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {

        private readonly string _apiKey = "P6ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey,out var ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api key no proporcionado"
                };

                return;

            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var ApiKeyValue = appSettings.GetValue<string>(_apiKey);

            if (!ApiKeyValue.Equals(ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api key no coincide"
                };

                return;
            }

            await next();

        }
       
    }
}
