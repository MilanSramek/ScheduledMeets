using MediatR.Pipeline;

using ScheduledMeets.Business.Interfaces;

namespace ScheduledMeets.Business.RequestPipeline;

internal class SaveChagesPostprocessor<TRequest, TResponse> 
    : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : IDataActiveRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveChagesPostprocessor(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public Task Process(TRequest _1, TResponse _2, CancellationToken cancellationToken) =>
        _unitOfWork.SaveChanges(cancellationToken);
}
