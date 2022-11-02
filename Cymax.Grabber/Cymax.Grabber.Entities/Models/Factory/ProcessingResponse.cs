using System;

namespace Cymax.Grabber.Entities.Models.Factory;

public class ProcessingResponse
{
    public Type RequestType { get; }
    
    public decimal Value { get; }
    
    public Exception Exception { get; }

    public bool IsSuccess => Exception is null;

    public ProcessingResponse(Type requestType, decimal value)
    {
        RequestType = requestType;
        Value = value;
    }
    
    public ProcessingResponse(Type requestType, Exception exception)
    {
        RequestType = requestType;
        Exception = exception;
    }
}