using System.Runtime.CompilerServices;

namespace Billiano.LangExt.Functional;

public static class OptionExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? GetValueOrDefault<T>(this Option<T> option)
    {
        if (option.HasValue)
        {
            return option.Value;
        }

        return default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetValueOrDefault<T>(this Option<T> option, T defaultValue)
    {
        if (option.HasValue)
        {
            return option.Value;
        }

        return defaultValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetValueOrDefault<T>(this Option<T> option, Func<T> func)
    {
        if (option.HasValue)
        {
            return option.Value;
        }

        return func();
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        if (option.HasValue)
        {
            return option;
        }

        return defaultValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Or<T>(this Option<T> option, Func<T> func)
    {
        if (option.HasValue)
        {
            return option;
        }

        return func();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Or<T>(this Option<T> option, Func<Option<T>> func)
    {
        if (option.HasValue)
        {
            return option;
        }

        return func();
    }
}
