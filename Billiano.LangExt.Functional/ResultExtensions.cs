using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Billiano.LangExt.Functional;
public static class ResultExtensions
{
    public static TOut Match<T, TOut>(this Result<T> result, Func<T, TOut> success, Func<Exception, TOut> fail)
    {
        return result.IsSuccess ? success(result.Value) : fail(result.Exception);
    }

    public static Result Then<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsFailed)
        {
            return Result.Fail<T>(result.Exception);
        }

        action(result.Value);
        return Result.Ok();
    }

    public static Result<TOut> Then<T, TOut>(this Result<T> result, Func<T, TOut> func)
    {
        return result.IsSuccess ? func(result.Value) : Result.Fail<TOut>(result.Exception);
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
        return result.IsSuccess ? result : func(result.Exception);
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
        return result.IsSuccess ? result.Value : default;
    }

    public static T GetValueOrDefault<T>(this Result<T> result, T defaultValue)
    {
        return result.IsSuccess ? result.Value : defaultValue;
    }

    public static T GetValueOrDefault<T>(this Result<T> result, Func<T> func)
    {
        return result.IsSuccess ? result.Value : func();
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
