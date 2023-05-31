using CleanArchitecture.Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Application.Common.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var errors = _validators
                .Select(e => e.Validate(context))
                .SelectMany(e => e.Errors)
                .Where(e => e is not null)
                .Select(e => e.ErrorMessage)
                .Distinct()
                .ToArray();

            if(errors.Any())
                throw new BadRequestException(errors);

            return await next();
        }
    }
}
