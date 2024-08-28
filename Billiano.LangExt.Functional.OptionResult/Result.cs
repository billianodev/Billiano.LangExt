using System.Runtime.CompilerServices;

namespace Billiano.LangExt.Functional.OptionResult;

/// <summary>
/// Represents a result of an operation that can either be successful or fail with an exception.
/// </summary>
public readonly struct Result : IResult
{
    private readonly Exception? _exception;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> struct representing a successful operation.
    /// </summary>
    public Result()
    {
        IsSuccess = true;
    }

    internal Result(Exception ex)
    {
        _exception = ex;
    }

    /// <summary>
    /// Gets the exception associated with the failed operation.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    public Exception Exception => IsFailed ? _exception! : throw new InvalidOperationException();

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailed => !IsSuccess;

    /// <summary>
    /// Returns a successful <see cref="Result"/> instance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Ok() => default;

    /// <summary>
    /// Returns a failed <see cref="Result"/> instance with the specified exception.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result Fail(Exception exception) => new(exception);

    /// <summary>
    /// Returns a successful <see cref="Result{T}"/> instance with the specified value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Ok<T>(T value) => new(value);

    /// <summary>
    /// Returns a failed <see cref="Result{T}"/> instance with the specified exception.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Fail<T>(Exception exception) => new(exception);

    /// <summary>
    /// Executes the given action and returns a <see cref="Result"/> indicating success or failure.
    /// </summary>
    /// <remarks>
    /// The action is executed within a try-catch block. If the action throws an exception,
    /// the returned <see cref="Result"/> will be in a failed state with the exception.
    /// </remarks>
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

    /// <summary>
    /// Executes the given function and returns a <see cref="Result{T}"/> indicating success or failure.
    /// </summary>
    /// <remarks>
    /// The function is executed within a try-catch block. If the function throws an exception,
    /// the returned <see cref="Result{T}"/> will be in a failed state with the exception.
    /// </remarks>
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

    /// <summary>
    /// Executes the given function and returns a <see cref="Result"/> indicating success or failure.
    /// </summary>
    /// <remarks>
    /// The function is executed within a try-catch block. If the function throws an exception,
    /// the returned <see cref="Result"/> will be in a failed state with the exception.
    /// </remarks>
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

    /// <summary>
    /// Executes the given function and returns a <see cref="Result{T}"/> indicating success or failure.
    /// </summary>
    /// <remarks>
    /// The function is executed within a try-catch block. If the function throws an exception,
    /// the returned <see cref="Result{T}"/> will be in a failed state with the exception.
    /// </remarks>
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

/// <summary>
/// Represents a result of an operation that can either be successful or fail with an exception.
/// </summary>
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

    /// <summary>
    /// Gets the value associated with the successful operation.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    public T Value => IsSuccess ? _value! : throw new InvalidOperationException();

    /// <summary>
    /// Gets the exception associated with the failed operation.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    public Exception Exception => IsFailed ? _exception! : throw new InvalidOperationException();

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailed => !IsSuccess;

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to a successful <see cref="Result{T}"/> instance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Result<T>(T value) => new(value);
}