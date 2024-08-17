using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Billiano.LangExt.Functional;
public static class ResultExtensions
{
    public static TOut Match<T, TOut>(this Result<T> result, Func<T, TOut> success, Func<Exception, TOut> fail)
    {
        if (result.IsFailed)
        {
            return fail(result.Exception);
        }

        return success(result.Value);
    }

    public static Result<TOut> Then<T, TOut>(this Result<T> result, Func<T, TOut> func)
    {
        if (result.IsSuccess)
        {
            var value = func(result.Value);
            return new Result<TOut>(value);
        }

        return new Result<TOut>(result.Exception);
    }

    public static Result Then<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value);
            return new Result();
        }

        return new Result(result.Exception);
    }

    public static Result Catch(this Result result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            action(result.Exception);
        }

        return result;
    }

    public static Result<T> Catch<T>(this Result<T> result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            action(result.Exception);
        }

        return result;
    }

    public static Result<T> Catch<T>(this Result<T> result, Func<Exception, T> func)
    {
        if (result.IsFailed)
        {
            var value = func(result.Exception);
            return new Result<T>(value);
        }

        return result;
    }

    [DebuggerNonUserCode]
    public static Result ThrowIfFailed(this Result result)
    {
        if (result.IsFailed)
        {
            throw result.Exception;
        }

        return result;
    }

    [DebuggerNonUserCode]
    public static Result<T> ThrowIfFailed<T>(this Result<T> result)
    {
        if (result.IsFailed)
        {
            throw result.Exception;
        }

        return result;
    }

    public static T? GetValueOrDefault<T>(this Result<T> result)
    {
        if (result.IsFailed)
        {
            return default;
        }

        return result.Value;
    }

    public static T GetValueOrDefault<T>(this Result<T> result, T defaultValue)
    {
        if (result.IsFailed)
        {
            return defaultValue;
        }

        return result.Value;
    }

    public static T GetValueOrDefault<T>(this Result<T> result, Func<T> func)
    {
        if (result.IsFailed)
        {
            return func();
        }

        return result.Value;
    }

    public static bool TryGetValue<T>(this Result<T> result, [NotNullWhen(true)] out T? value)
    {
        if (result.IsFailed)
        {
            value = default;
            return false;
        }

        value = result.Value;
        return true;
    }

    public static bool TryGetException<T>(this Result<T> result, [NotNullWhen(true)] out T? value)
    {
        if (result.IsFailed)
        {
            value = default;
            return false;
        }

        value = result.Value;
        return true;
    }
}
