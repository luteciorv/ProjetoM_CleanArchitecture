using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Exceptions.Claims;

namespace CleanArchitecture.WebApi.Exceptions
{
    public static class HandlerClaimException
    {
        public static void NotFound(ExceptionContext context)
        {
            if (context.Exception is not ClaimException exception) return;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Title = $"Claim informada não encontrada",
                Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/400",
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
