using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Exceptions;

namespace CleanArchitecture.WebApi.Exceptions
{
    public class HandlerUsernameException
    {
        public static void UsernameAlreadyRegistered(ExceptionContext context)
        {
            if (context.Exception is not UsernameAlreadyRegisteredException exception) return;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Nome de usuário informado já cadastrado.",
                Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/400",
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
