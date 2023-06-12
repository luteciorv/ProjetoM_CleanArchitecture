using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Exceptions.PasswordEntropy;

namespace CleanArchitecture.WebApi.Exceptions
{
    public static class HandlerPasswordEntropyException
    {
        public static void Handle(ExceptionContext context)
        {
            if (context.Exception is not PasswordEntropyException exception) return;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "A senha informada não é forte o suficiente.",
                Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/400",
                Detail = exception.Message
            };
          
            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
