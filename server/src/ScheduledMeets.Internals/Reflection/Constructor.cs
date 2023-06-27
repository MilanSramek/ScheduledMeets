using System.Reflection;

namespace ScheduledMeets.Internals.Reflection;

public static class Constructor
{
    public static ConstructorInfo Tuple<T1, T2>() => typeof(Tuple<T1,T2>)
        .GetConstructor(new[] { typeof(T1), typeof(T2) })!;
}
