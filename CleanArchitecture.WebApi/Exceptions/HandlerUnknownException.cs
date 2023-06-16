using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Exceptions
{
    public static class HandlerUnknownException
    {
        public static void Handle(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Um erro inesperado ocorreu na execução da requisição.",
                Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/500",
                Detail = context.Exception.Message
            };

            context.Result = new ObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
