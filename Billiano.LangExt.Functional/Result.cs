using System.Runtime.CompilerServices;

namespace Billiano.LangExt.Functional;

public readonly struct Result : IResult
{
    private readonly Exception? _exception;

    public Result()
    {
        IsSuccess = true;
    }

    internal Result(Exception ex)
    {
        _exception = ex;
    }

    public Exception Exception => IsFailed ? _exception! : throw new InvalidOperationException();
    public bool IsSuccess { get; }
    public bool IsFailed => !IsSuccess;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Ok() => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Fail(Exception exception) => new(exception);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Ok<T>(T value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Fail<T>(Exception exception) => new(exception);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result From(Action action)
    {
        try
        {
            action();
            return Ok();
        }
        catch (Exception ex)
        {
            return Fail(ex);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> From<T>(Func<T> action)
    {
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            return Fail<T>(ex);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result From(Func<Result> func)
    {
        try
        {
            return func();
        }
        catch (Exception ex)
        {
            return Fail(ex);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> From<T>(Func<Result<T>> func)
    {
        try
        {
            return func();
        }
        catch (Exception ex)
        {
            return Fail<T>(ex);
        }
    }
}

public readonly struct Result<T> : IResult<T>
{
    private readonly T? _value;
    private readonly Exception? _exception;

    internal Result(T value)
    {
        _value = value;
        IsSuccess = true;
    }

    internal Result(Exception exception)
    {
        _exception = exception;
    }

    public T Value => IsSuccess ? _value! : throw new InvalidOperationException();
    public Exception Exception => IsFailed ? _exception! : throw new InvalidOperationException();
    public bool IsSuccess { get; }
    public bool IsFailed => !IsSuccess;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Result<T>(T value) => new(value);
}