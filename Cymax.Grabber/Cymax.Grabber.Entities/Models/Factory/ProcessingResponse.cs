using System;

namespace Cymax.Grabber.Entities.Models.Factory;

/// <summary>
/// Result of request to specific API
/// </summary>
public class ProcessingResponse
{
    /// <summary>
    /// Gets the name of the API manager.
    /// </summary>
    /// <value>
    /// The name of the API manager.
    /// </value>
    public string ApiManagerName { get; }

    /// <summary>
    /// Gets the API response value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public decimal Value { get; }

    /// <summary>
    /// Gets the exception that was occurred during request processing.
    /// </summary>
    /// <value>
    /// The exception.
    /// </value>
    public Exception Exception { get; }

    /// <summary>
    /// Gets a value indicating whether request to API was success.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this Exception is null; otherwise, <c>false</c>.
    /// </value>
    public bool IsSuccess => Exception is null;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessingResponse" /> class.
    /// </summary>
    /// <param name="apiManagerName">The API manager name.</param>
    /// <param name="value">The response value.</param>
    public ProcessingResponse(string apiManagerName, decimal value)
    {
        ApiManagerName = apiManagerName;
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessingResponse" /> class.
    /// </summary>
    /// <param name="apiManagerName">The API manager name.</param>
    /// <param name="exception">The exception.</param>
    public ProcessingResponse(string apiManagerName, Exception exception)
    {
        ApiManagerName = apiManagerName;
        Exception = exception;
    }
}