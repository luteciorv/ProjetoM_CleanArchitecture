using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Exceptions.ECDsaEncryption;

namespace CleanArchitecture.WebApi.Exceptions
{
    public static class HandlerECDsaException
    {
        public static void ECDsaKeysAlreadyExists(ExceptionContext context)
        {
            if (context.Exception is not ECDsaKeyAlreadyExistsException exception) return;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Chaves de criptografia",
                Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/400",
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
