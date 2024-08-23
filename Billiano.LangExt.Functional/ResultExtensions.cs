using System.Runtime.CompilerServices;

namespace Billiano.LangExt.Functional;

public static class ResultExtensions
{
    #region GetValueOrDefault
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? GetValueOrDefault<T>(this Result<T> result)
    {
        return result.IsSuccess ? result.Value : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetValueOrDefault<T>(this Result<T> result, T defaultValue)
    {
        return result.IsSuccess ? result.Value : defaultValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetValueOrDefault<T>(this Result<T> result, Func<T> func)
    {
        return result.IsSuccess ? result.Value : func();
    }
    #endregion

    #region Match
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Match<TOut>(this Result result, Func<TOut> success, Func<Exception, TOut> failed)
    {
        if (result.IsFailed)
        {
            return failed(result.Exception);
        }

        return success();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Match<T, TOut>(this Result<T> result, Func<T, TOut> success, Func<Exception, TOut> failed)
    {
        if (result.IsFailed)
        {
            return failed(result.Exception);
        }

        return success(result.Value);
    }
    #endregion

    #region Then
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Then(this Result result, Action action)
    {
        if (result.IsSuccess)
        {
            return Result.From(action);
        }

        return Result.Fail(result.Exception);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Then(this Result result, Func<Result> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(func);
        }

        return Result.Fail(result.Exception);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> Then<TOut>(this Result result, Func<TOut> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(func);
        }

        return Result.Fail<TOut>(result.Exception);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> Then<TOut>(this Result result, Func<Result<TOut>> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(func);
        }

        return Result.Fail<TOut>(result.Exception);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Then<T>(this Result<T> result, Action action)
    {
        if (result.IsSuccess)
        {
            return Result.From(action);
        }

        return Result.Fail(result.Exception);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Then<T>(this Result<T> result, Func<Result> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(func);
        }

        return Result.Fail(result.Exception);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> Then<T, TOut>(this Result<T> result, Func<TOut> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(func);
        }

        return Result.Fail<TOut>(result.Exception);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> Then<T, TOut>(this Result<T> result, Func<Result<TOut>> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(func);
        }

        return Result.Fail<TOut>(result.Exception);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> Then<T, TOut>(this Result<T> result, Func<T, TOut> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(() => func(result.Value));
        }

        return Result.Fail<TOut>(result.Exception);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> Then<T, TOut>(this Result<T> result, Func<T, Result<TOut>> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(() => func(result.Value));
        }

        return Result.Fail<TOut>(result.Exception);
    }
    #endregion

    #region Catch
    public static Result Catch(this Result result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            action(result.Exception);
        }

        return Result.Ok();
    }

    public static Result Catch<T>(this Result<T> result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            action(result.Exception);
        }

        return Result.Ok();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Catch<T>(this Result<T> result, Func<Exception, T> func)
    {
        if (result.IsFailed)
        {
            return Result.From(() => func(result.Exception));
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Catch<T>(this Result<T> result, Func<Exception, Result<T>> func)
    {
        if (result.IsFailed)
        {
            return Result.From(() => func(result.Exception));
        }

        return result;
    }
    #endregion

    #region IfFailed
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result IfFailed(this Result result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            action(result.Exception);
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> IfFailed<T>(this Result<T> result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            action(result.Exception);
        }

        return result;
    }
    #endregion

    #region ThrowIfFailed
    public static Result ThrowIfFailed(this Result result)
    {
        if (result.IsFailed)
        {
            throw result.Exception;
        }

        return result;
    }

    public static Result<T> ThrowIfFailed<T>(this Result<T> result)
    {
        if (result.IsFailed)
        {
            throw result.Exception;
        }

        return result;
    }
    #endregion
}
