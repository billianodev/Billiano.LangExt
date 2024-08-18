using System.Diagnostics.CodeAnalysis;

namespace Billiano.LangExt.Functional;

public class Result
{
    private static readonly Result OkResult = new();

#if NETSTANDARD2_1_OR_GREATER
    [NotNull]
#endif
    public Exception? Exception { get; }

#if NET6_0_OR_GREATER
    [MemberNotNullWhen(false, nameof(Exception))]
    public virtual bool IsSuccess { get; }
#else
    public bool IsSuccess { get; }
#endif

#if NET6_0_OR_GREATER
    [MemberNotNullWhen(true, nameof(Exception))]
    public virtual bool IsFailed => !IsSuccess;
#else
    public bool IsFailed => !IsSuccess;
#endif

    protected Result()
    {
        IsSuccess = true;
    }

    public Result(Exception exception)
    {
        if (exception is null)
        {
            throw new ArgumentNullException(nameof(exception));
        }

        Exception = exception;
        IsSuccess = false;
    }

    public static Result Ok()
    {
        return OkResult;
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value);
    }

    public static Result Fail(Exception ex)
    {
        return new(ex);
    }

    public static Result<T> Fail<T>(Exception ex)
    {
        return new(ex);
    }

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

    public static Result<T> From<T>(Func<T> func)
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

    public static async Task<Result> FromAsync(Func<Task> func)
    {
        try
        {
            await func();
            return Ok();
        }
        catch (Exception ex)
        {
            return Fail(ex);
        }
    }

    public static async Task<Result<T>> FromAsync<T>(Func<Task<T>> func)
    {
        try
        {
            return await func();
        }
        catch (Exception ex)
        {
            return Fail<T>(ex);
        }
    }

    [Obsolete("Use Result.Fail instead")]
    public static implicit operator Result(Exception ex) => new(ex);
}

public sealed class Result<T> : Result
{
#if NETSTANDARD2_1_OR_GREATER
    [NotNull]
#endif
    public T? Value { get; }

#if NET6_0_OR_GREATER
    [MemberNotNullWhen(true, nameof(Value))]
    public override bool IsSuccess => base.IsSuccess;

    [MemberNotNullWhen(false, nameof(Value))]
    public override bool IsFailed => base.IsFailed;
#endif

    internal Result(T value)
    {
        Value = value;
    }

    internal Result(Exception exception) : base(exception)
    {
    }

    public static implicit operator Result<T>(T value) => new(value);

    [Obsolete("Use Result.Fail instead")]
    public static implicit operator Result<T>(Exception ex) => new(ex);
}