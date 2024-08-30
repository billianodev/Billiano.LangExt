using System.Runtime.CompilerServices;

namespace Billiano.LangExt.Functional.OptionResult;

/// <summary>
/// Provides extension methods for the <see cref="Result"/> and <see cref="Result{T}"/> types.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Gets the value of the <see cref="Result{T}"/> if successful, or the default value of T if the Result is failed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? GetValueOrDefault<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return result.Value;
        }

        return default;
    }

    /// <summary>
    /// Gets the value of the <see cref="Result{T}"/> if successful, or the specified default value if the Result is failed.
    /// </summary>
    public static T GetValueOrDefault<T>(this Result<T> result, T defaultValue)
    {
        if (result.IsSuccess)
        {
            return result.Value;
        }

        return defaultValue;
    }

    /// <summary>
    /// Gets the value of the <see cref="Result{T}"/> if successful, or the result of the specified function if the Result is failed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetValueOrDefault<T>(this Result<T> result, Func<T> func)
    {
        if (result.IsSuccess)
        {
            return result.Value;
        }

        return func();
    }

    /// <summary>
    /// Applies the specified functions based on the success or failure of the Result.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Match<TOut>(this Result result, Func<TOut> success, Func<Exception, TOut> failed)
    {
        if (result.IsFailed)
        {
            return failed(result.Exception);
        }

        return success();
    }

    /// <summary>
    /// Applies the specified functions based on the success or failure of the Result.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Match<T, TOut>(this Result<T> result, Func<T, TOut> success, Func<Exception, TOut> failed)
    {
        if (result.IsFailed)
        {
            return failed(result.Exception);
        }

        return success(result.Value);
    }

    /// <summary>
    /// Execute action if the <see cref="Result"/> is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result IfSuccess(this Result result, Action action)
    {
        if (result.IsSuccess)
        {
            action();
        }

        return result;
    }

    /// <summary>
    /// Execute action if the <see cref="Result{T}"/> is successful.
    /// </summary>
    /// <returns>The original <see cref="Result{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> IfSuccess<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value);
        }

        return result;
    }

    /// <summary>
    /// Applies the specified action if the Result is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Then(this Result result, Action action)
    {
        if (result.IsSuccess)
        {
            action();
            return Result.Ok();
        }

        return result;
    }

    /// <summary>
    /// Applies the specified function if the Result is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Then(this Result result, Func<Result> func)
    {
        if (result.IsSuccess)
        {
            return func();
        }

        return result;
    }

    /// <summary>
    /// Applies the specified function if the Result is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> Then<TOut>(this Result result, Func<TOut> func)
    {
        if (result.IsSuccess)
        {
            return func();
        }

        return Result.Fail<TOut>(result.Exception);
    }

    /// <summary>
    /// Applies the specified function if the Result is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> Then<TOut>(this Result result, Func<Result<TOut>> func)
    {
        if (result.IsSuccess)
        {
            return func();
        }

        return Result.Fail<TOut>(result.Exception);
    }

    /// <summary>
    /// Applies the specified action if the <see cref="Result{T}"/> is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Then<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value);
            return Result.Ok();
        }

        return Result.Fail(result.Exception);
    }

    /// <summary>
    /// Applies the specified function if the <see cref="Result{T}"/> is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Then<T>(this Result<T> result, Func<T, Result> func)
    {
        if (result.IsSuccess)
        {
            return func(result.Value);
        }

        return Result.Fail(result.Exception);
    }

    /// <summary>
    /// Applies the specified function if the <see cref="Result{T}"/> is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> Then<T, TOut>(this Result<T> result, Func<T, TOut> func)
    {
        if (result.IsSuccess)
        {
            return func(result.Value);
        }

        return Result.Fail<TOut>(result.Exception);
    }

    /// <summary>
    /// Applies the specified function if the <see cref="Result{T}"/> is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> Then<T, TOut>(this Result<T> result, Func<T, Result<TOut>> func)
    {
        if (result.IsSuccess)
        {
            return func(result.Value);
        }

        return Result.Fail<TOut>(result.Exception);
    }

    /// <summary>
    /// Applies the specified action if the Result is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result TryThen(this Result result, Action action)
    {
        if (result.IsSuccess)
        {
            return Result.From(action);
        }

        return result;
    }

    /// <summary>
    /// Applies the specified function if the Result is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result TryThen(this Result result, Func<Result> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(func);
        }

        return result;
    }

    /// <summary>
    /// Applies the specified function if the Result is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> TryThen<TOut>(this Result result, Func<TOut> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(func);
        }

        return Result.Fail<TOut>(result.Exception);
    }

    /// <summary>
    /// Applies the specified function if the Result is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> TryThen<TOut>(this Result result, Func<Result<TOut>> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(func);
        }

        return Result.Fail<TOut>(result.Exception);
    }

    /// <summary>
    /// Applies the specified action if the <see cref="Result{T}"/> is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result TryThen<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
        {
            return Result.From(() => action(result.Value));
        }

        return Result.Fail(result.Exception);
    }

    /// <summary>
    /// Applies the specified function if the <see cref="Result{T}"/> is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result TryThen<T>(this Result<T> result, Func<T, Result> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(() => func(result.Value));
        }

        return Result.Fail(result.Exception);
    }

    /// <summary>
    /// Applies the specified function if the <see cref="Result{T}"/> is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> TryThen<T, TOut>(this Result<T> result, Func<T, TOut> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(() => func(result.Value));
        }

        return Result.Fail<TOut>(result.Exception);
    }

    /// <summary>
    /// Applies the specified function if the <see cref="Result{T}"/> is successful.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TOut> TryThen<T, TOut>(this Result<T> result, Func<T, Result<TOut>> func)
    {
        if (result.IsSuccess)
        {
            return Result.From(() => func(result.Value));
        }

        return Result.Fail<TOut>(result.Exception);
    }

    /// <summary>
    /// Execute action to the exception if the Result is failed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result IfFailed(this Result result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            action(result.Exception);
        }

        return result;
    }

    /// <summary>
    /// Execute action to the exception if the <see cref="Result{T}"/> is failed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> IfFailed<T>(this Result<T> result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            action(result.Exception);
        }

        return result;
    }

    /// <summary>
    /// Throws the exception if the Result is failed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result ThrowIfFailed(this Result result)
    {
        if (result.IsFailed)
        {
            throw result.Exception;
        }

        return result;
    }

    /// <summary>
    /// Throws the exception if the <see cref="Result{T}"/> is failed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> ThrowIfFailed<T>(this Result<T> result)
    {
        if (result.IsFailed)
        {
            throw result.Exception;
        }

        return result;
    }

    /// <summary>
    /// Applies the specified action to the exception if the Result is failed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Catch(this Result result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            action(result.Exception);
        }

        return Result.Ok();
    }

    /// <summary>
    /// Applies the specified action to the exception if the <see cref="Result{T}"/> is failed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Catch<T>(this Result<T> result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            action(result.Exception);
        }

        return Result.Ok();
    }

    /// <summary>
    /// Applies the specified function to the exception if the <see cref="Result{T}"/> is failed, returning a new <see cref="Result{T}"/> with the transformed value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Catch<T>(this Result<T> result, Func<Exception, T> func)
    {
        if (result.IsFailed)
        {
            return func(result.Exception);
        }

        return result;
    }

    /// <summary>
    /// Applies the specified function to the exception if the <see cref="Result{T}"/> is failed, returning a new <see cref="Result{T}"/> with the transformed Result.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Catch<T>(this Result<T> result, Func<Exception, Result<T>> func)
    {
        if (result.IsFailed)
        {
            return func(result.Exception);
        }

        return result;
    }

    /// <summary>
    /// Applies the specified action to the exception if the Result is failed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result TryCatch(this Result result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            return Result.From(() => action(result.Exception));
        }

        return Result.Ok();
    }

    /// <summary>
    /// Applies the specified action to the exception if the <see cref="Result{T}"/> is failed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result TryCatch<T>(this Result<T> result, Action<Exception> action)
    {
        if (result.IsFailed)
        {
            return Result.From(() => action(result.Exception));
        }

        return Result.Ok();
    }

    /// <summary>
    /// Applies the specified function to the exception if the <see cref="Result{T}"/> is failed, returning a new <see cref="Result{T}"/> with the transformed value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> TryCatch<T>(this Result<T> result, Func<Exception, T> func)
    {
        if (result.IsFailed)
        {
            return Result.From(() => func(result.Exception));
        }

        return result;
    }

    /// <summary>
    /// Applies the specified function to the exception if the <see cref="Result{T}"/> is failed, returning a new <see cref="Result{T}"/> with the transformed Result.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> TryCatch<T>(this Result<T> result, Func<Exception, Result<T>> func)
    {
        if (result.IsFailed)
        {
            return Result.From(() => func(result.Exception));
        }

        return result;
    }
}
