namespace ScheduledMeets.Internals.Extensions;

public static class TaskFactoryExtensions
{
    public static Task<TResult> StartNew<TResult, TState>(this TaskFactory factory,
        Func<TState, TResult> function, TState state)
    {
        if (factory is null) throw new ArgumentNullException(nameof(factory));
        if (state is null) throw new ArgumentNullException(nameof(state));

        TResult DerivedFunction(object? state) => function((TState)state!);

        return factory.StartNew(DerivedFunction, state);
    }
}
