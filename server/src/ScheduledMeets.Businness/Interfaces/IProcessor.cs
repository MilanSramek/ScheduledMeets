using MediatR;

namespace ScheduledMeets.Business.Interfaces;

public interface IProcessor<TRequest> where TRequest : IRequest
{
    Task ProcessAsync(TRequest request, CancellationToken cancellationToken = default);
}

public interface IProcessor<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken = default);
}