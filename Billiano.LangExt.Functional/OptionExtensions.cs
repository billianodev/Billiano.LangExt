using System.Runtime.CompilerServices;

namespace Billiano.LangExt.Functional;

/// <summary>
/// Provides extension methods for the <see cref="Option{T}"/> type.
/// </summary>
public static class OptionExtensions
{
    /// <summary>
    /// Gets the value of the option if it has a value, otherwise returns the default value for the type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? GetValueOrDefault<T>(this Option<T> option)
    {
        if (option.HasValue)
        {
            return option.Value;
        }

        return default;
    }

    /// <summary>
    /// Gets the value of the option if it has a value, otherwise returns the specified default value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetValueOrDefault<T>(this Option<T> option, T defaultValue)
    {
        if (option.HasValue)
        {
            return option.Value;
        }

        return defaultValue;
    }

    /// <summary>
    /// Gets the value of the option if it has a value, otherwise returns the result of the specified function.
    /// </summary>
    public static T GetValueOrDefault<T>(this Option<T> option, Func<T> func)
    {
        if (option.HasValue)
        {
            return option.Value;
        }

        return func();
    }

    /// <summary>
    /// Executes the specified action if the current <see cref="Option{T}"/> has a value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> IfSome<T>(this Option<T> option, Action<T> action)
    {
        if (option.HasValue)
        {
            action(option.Value);
        }

        return option;
    }

    /// <summary>
    /// Applies the specified function to the value of the current <see cref="Option{T}"/> if it has a value,
    /// otherwise returns a new <see cref="Option{TOut}"/> with no value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<TOut> Then<T, TOut>(this Option<T> option, Func<T, TOut> func)
    {
        if (option.HasValue)
        {
            return func(option.Value);
        }

        return Option.None<TOut>();
    }

    /// <summary>
    /// Executes the specified action if the current <see cref="Option{T}"/> does not have a value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> IfNone<T>(this Option<T> option, Action action)
    {
        if (!option.HasValue)
        {
            action();
        }

        return option;
    }

    /// <summary>
    /// Provides a default value or a function to compute a default value for an <see cref="Option{T}"/> if it does not have a value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Or<T>(this Option<T> option, T defaultValue)
    {
        if (option.HasValue)
        {
            return option;
        }

        return defaultValue;
    }

    /// <summary>
    /// Provides a default value or a function to compute a default value for an <see cref="Option{T}"/> if it does not have a value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Or<T>(this Option<T> option, Func<T> func)
    {
        if (option.HasValue)
        {
            return option;
        }

        return func();
    }

    /// <summary>
    /// Provides a default value or a function to compute a default value for an <see cref="Option{T}"/> if it does not have a value.
    /// </summary>
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
