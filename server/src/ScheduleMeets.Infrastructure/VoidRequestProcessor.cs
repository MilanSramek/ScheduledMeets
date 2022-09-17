using MediatR;

using ScheduledMeets.Business.Interfaces;

namespace ScheduledMeets.Infrastructure;

class VoidRequestProcessor<TRequest> : IProcessor<TRequest> where TRequest : IRequest
{
    private readonly ISender _sender;

    public VoidRequestProcessor(ISender sender) =>
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    public Task ProcessAsync(TRequest request, CancellationToken cancellationToken) =>
        _sender.Send(request, cancellationToken);
}
