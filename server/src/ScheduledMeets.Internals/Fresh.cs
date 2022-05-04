using ScheduledMeets.Internals.Extensions;

namespace ScheduledMeets.Internals;

public class Fresh<T>
{
    private readonly Func<Task<T>> _valueTaskFactory;
    private readonly Func<T, bool> _valueReviewer;

    private volatile Task<T>? _valueTask;

    public Fresh(Func<Task<T>> valueTaskFactory, Func<T, bool> valueReviewer)
    {
        _valueTaskFactory = valueTaskFactory
            ?? throw new ArgumentNullException(nameof(valueTaskFactory));
        _valueReviewer = valueReviewer
            ?? throw new ArgumentNullException(nameof(valueReviewer));
    }

    public async Task<T> GetAsync(CancellationToken cancellationToken = default)
    {
        Task<T>? valueTask = _valueTask;

        if (valueTask is null || valueTask.IsFaulted)
            return await RefreshAsync(valueTask, cancellationToken);

        T value = await valueTask.WaitAsync(cancellationToken);

        return _valueReviewer(value)
            ? value
            : await RefreshAsync(valueTask, cancellationToken);
    }

    private async Task<T> RefreshAsync(Task<T>? valueTask, CancellationToken cancellationToken)
    {
        TaskCompletionSource<T> taskSource = new();
        Task<T>? originalValueTask = Interlocked.CompareExchange(ref _valueTask, taskSource.Task,
            valueTask);

        if (originalValueTask != valueTask)
            return await originalValueTask!.WaitAsync(cancellationToken);

        if (cancellationToken != default)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Factory.StartNew(MakeRefreshAsync, taskSource);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            return await taskSource.Task.WaitAsync(cancellationToken);
        }

        await MakeRefreshAsync(taskSource);
        return await taskSource.Task;
    }

    private async Task MakeRefreshAsync(TaskCompletionSource<T> source)
    {
        try
        {
            T value = await _valueTaskFactory();
            source.SetResult(value);
        }
        catch (Exception ex)
        {
            source.SetException(ex);
        }
    }
}
