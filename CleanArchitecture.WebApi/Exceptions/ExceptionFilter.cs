using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Exceptions.Email;
using CleanArchitecture.Application.Exceptions.PasswordEntropy;
using CleanArchitecture.Application.Exceptions.Request;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace CleanArchitecture.WebApi.Exceptions
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ExceptionFilter()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(InvalidUserRequestException), HandleInvalidRequestException.InvalidUserRequest },
                { typeof(EmailAlreadyRegisteredException), HandleEmailException.EmailAlreadyRegistered },
                { typeof(UsernameAlreadyRegisteredException), HandlerUsernameException.UsernameAlreadyRegistered },
                { typeof(PoorPasswordEntropyException), HandlerPasswordEntropyException.Handle },
                { typeof(WeakPasswordEntropyException), HandlerPasswordEntropyException.Handle },
            };
        }

        public override void OnException(ExceptionContext context)
        {            
            HandleException(context);
            base.OnException(context);            
        }

        private void HandleException(ExceptionContext context)
        {
            Log.Warning($"Uma exceção foi gerada. \nExceção: {context.Exception.Message}");

            var type = context.Exception.GetType();
            if(_exceptionHandlers.ContainsKey(type))
            {
                Log.Warning($"Tratando a exceção...");
                _exceptionHandlers[type].Invoke(context);
                Log.Warning("Exceção tratada com sucesso.");
            }
            else
            {
                Log.Error(context.Exception, "EXCEÇÃO DESCONHECIDA E NÃO MAPEADA.");
                HandleUnknownException.Handle(context);
            }
        }
    }
}
