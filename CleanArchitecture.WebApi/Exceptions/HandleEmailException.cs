using CleanArchitecture.Application.Exceptions.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitecture.WebApi.Exceptions
{
    public static class HandleEmailException
    {
        public static void EmailAlreadyRegistered(ExceptionContext context)
        {
            if (context.Exception is not EmailAlreadyRegisteredException exception) return;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "O e-mail informado já foi cadastrado.",
                Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/400",
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
