using System.Runtime.CompilerServices;

namespace Billiano.LangExt.Functional;

public static class OptionExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetValueOrDefault<T>(this Option<T> option, T defaultValue)
    {
        return option.HasValue ? option.Value : defaultValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetValueOrDefault<T>(this Option<T> option, Func<T> func)
    {
        return option.HasValue ? option.Value : func();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? GetValueOrDefault<T>(this Option<T> option)
    {
        return option.HasValue ? option.Value : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> IfSome<T>(this Option<T> option, Action<T> action)
    {
        if (option.HasValue)
        {
            action(option.Value);
        }

        return option;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<TOut> Then<T, TOut>(this Option<T> option, Func<T, TOut> func)
    {
        if (option.HasValue)
        {
            return func(option.Value);
        }

        return Option.None<TOut>();
    }

    public static Option<T> IfNone<T>(this Option<T> option, Action action)
    {
        if (!option.HasValue)
        {
            action();
        }

        return option;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Or<T>(this Option<T> option, T defaultValue)
    {
        return option.HasValue ? option : defaultValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Or<T>(this Option<T> option, Func<T> func)
    {
        return option.HasValue ? option : func();
    }
}
