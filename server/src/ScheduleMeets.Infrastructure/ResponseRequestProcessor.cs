using MediatR;

using ScheduledMeets.Business.Interfaces;

namespace ScheduledMeets.Infrastructure;

class ResponseRequestProcessor<TRequest, TResponse> : IProcessor<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ISender _sender;

    public ResponseRequestProcessor(ISender sender) =>
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    public Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken)  =>
        _sender.Send(request, cancellationToken);
}
