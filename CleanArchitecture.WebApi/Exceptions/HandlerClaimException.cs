using CleanArchitecture.Application.Exceptions.ECDsaEncryption;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Domain.Statics;

namespace CleanArchitecture.WebApi.Exceptions
{
    public static class HandlerClaimException
    {
        public static void EmailNotFound(ExceptionContext context)
        {
            if (context.Exception is not ECDsaKeyAlreadyExistsException exception) return;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Title = $"Claim {UserClaims.EMAIL}",
                Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/400",
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
