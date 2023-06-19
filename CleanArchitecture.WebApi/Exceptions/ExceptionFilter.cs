using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Exceptions.Claims;
using CleanArchitecture.Application.Exceptions.ECDsaEncryption;
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
                { typeof(InvalidUserRequestException), HandlerInvalidRequestException.InvalidUserRequest },
                { typeof(UsernameAlreadyRegisteredException), HandlerUsernameException.UsernameAlreadyRegistered },

                { typeof(EmailAlreadyRegisteredException), HandlerEmailException.EmailAlreadyRegistered },
                { typeof(EmailNotRegisteredException), HandlerEmailException.EmailNotRegistered },
                { typeof(EmailAlreadyConfirmedException), HandlerEmailException.EmailAlreadyConfirmed },

                { typeof(PoorPasswordEntropyException), HandlerPasswordEntropyException.Handle },
                { typeof(WeakPasswordEntropyException), HandlerPasswordEntropyException.Handle },

                { typeof(ECDsaKeyAlreadyExistsException), HandlerECDsaException.ECDsaKeysAlreadyExists },

                { typeof(EmailClaimException), HandlerClaimException.EmailNotFound },
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
                HandlerUnknownException.Handle(context);
            }
        }
    }
}
