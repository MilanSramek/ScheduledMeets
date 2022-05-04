using Dawn;

using MediatR;

using ScheduledMeets.Business.Interfaces;

namespace ScheduleMeets.Infrastructure;

class VoidRequestProcessor<TRequest> : IProcessor<TRequest> where TRequest : IRequest
{
    private readonly ISender _sender;

    public VoidRequestProcessor(ISender sender) =>
        _sender = Guard.Argument(sender).NotNull().Value;

    public Task ProcessAsync(TRequest request, CancellationToken cancellationToken) =>
        _sender.Send(request, cancellationToken);
}
