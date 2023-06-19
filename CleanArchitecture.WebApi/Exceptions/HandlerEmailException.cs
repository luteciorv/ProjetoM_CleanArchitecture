using CleanArchitecture.Application.Exceptions.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitecture.WebApi.Exceptions
{
    public static class HandlerEmailException
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

        public static void EmailNotRegistered(ExceptionContext context)
        {
            if (context.Exception is not EmailAlreadyRegisteredException exception) return;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "O e-mail informado não existe no banco de dados.",
                Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/400",
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        public static void EmailAlreadyConfirmed(ExceptionContext context)
        {
            if (context.Exception is not EmailAlreadyRegisteredException exception) return;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "O e-mail informado já foi confirmado.",
                Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/400",
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
