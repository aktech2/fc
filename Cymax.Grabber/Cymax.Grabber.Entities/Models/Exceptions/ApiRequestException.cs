using System;
using System.Net;

namespace Cymax.Grabber.Entities.Models.Exceptions;

public class ApiRequestException: Exception
{
    public HttpStatusCode StatusCode { get; }

    public ApiRequestException(HttpStatusCode statusCode, string message = "", Exception innerException = null) : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}