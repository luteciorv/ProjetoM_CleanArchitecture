using CleanArchitecture.Application.Exceptions.Request;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Exceptions
{
    public static class HandleInvalidRequestException
    {
        public static void InvalidUserRequest(ExceptionContext context)
        {
            if (context.Exception is not InvalidUserRequestException exception) return;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "O nome do usuário informado já foi cadastrado.",
                Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/400",
                Detail = exception.Message
            };
            details.Extensions.Add("Invalid Fields", exception.DataException);

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
