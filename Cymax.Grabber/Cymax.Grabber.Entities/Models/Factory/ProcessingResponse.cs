using System;

namespace Cymax.Grabber.Entities.Models.Factory;

public class ProcessingResponse
{
    public string ApiManager { get; }
    
    public decimal Value { get; }
    
    public Exception Exception { get; }

    public bool IsSuccess => Exception is null;

    public ProcessingResponse(string apiManager, decimal value)
    {
        ApiManager = apiManager;
        Value = value;
    }
    
    public ProcessingResponse(string apiManager, Exception exception)
    {
        ApiManager = apiManager;
        Exception = exception;
    }
}