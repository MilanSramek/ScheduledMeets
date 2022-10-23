using MediatR;

namespace ScheduledMeets.Business.RequestPipeline
{
    public interface IDataActiveRequest : IRequest
    {
    }

    public interface IDataActiveRequest<out TResponse> : IRequest<TResponse>
    {
    }
}
