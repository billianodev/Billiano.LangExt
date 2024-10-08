﻿namespace Billiano.LangExt.Functional.OptionResult;

/// <summary>
/// Represents an optional value.
/// </summary>
/// <typeparam name="T">The type of the optional value.</typeparam>
public interface IOption<T>
{
    /// <summary>
    /// Gets the value of the option if it has one.
    /// </summary>
    T Value { get; }

    /// <summary>
    /// Gets a value indicating whether the option has a value.
    /// </summary>
    bool HasValue { get; }
}