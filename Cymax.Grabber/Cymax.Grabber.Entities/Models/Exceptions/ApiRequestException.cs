using System;
using System.Net;

namespace Cymax.Grabber.Entities.Models.Exceptions;

/// <summary>
/// Exception of processing API request
/// </summary>
/// <seealso cref="System.Exception" />
public class ApiRequestException: Exception
{
    /// <summary>
    /// Gets the status code.
    /// </summary>
    /// <value>
    /// The status code.
    /// </value>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiRequestException"/> class.
    /// </summary>
    /// <param name="statusCode">The status code.</param>
    /// <param name="message">The message.</param>
    /// <param name="innerException">The inner exception.</param>
    public ApiRequestException(HttpStatusCode statusCode, string message = "", Exception innerException = null) : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}