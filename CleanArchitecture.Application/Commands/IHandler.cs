namespace CleanArchitecture.Application.Commands
{
    public interface IHandler<TRequest, TResponse>
        where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}
