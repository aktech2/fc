using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cymax.Grabber.Entities.Interfaces;
using Cymax.Grabber.Entities.Models.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cymax.Grabber.Logic;

//Singleton Lazy implementation 
public class GlobalApiManager
{
    public static GlobalApiManager Instance => _lazy.Value;
    
    private Dictionary<Type, Type> _requestToManagerMapping = new Dictionary<Type, Type>();
    private IServiceProvider _serviceProvider;
    private ILogger _logger;
    private static readonly Lazy<GlobalApiManager> _lazy = new Lazy<GlobalApiManager>(() => new GlobalApiManager());

    private GlobalApiManager()
    {
    }
    
    public void Init(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = serviceProvider.GetRequiredService<ILogger<GlobalApiManager>>();
        var modelsAssembly = typeof(ProcessingResponse).Assembly;
        var managersAssembly = typeof(GlobalApiManager).Assembly;
        
        var requests = modelsAssembly
            .GetTypes()
            .Where(w => w.IsClass && typeof(IRequest).IsAssignableFrom(w))
            .ToList();

        var managers = managersAssembly
            .GetTypes()
            .Where(w => w.IsClass && typeof(IBaseApiManager).IsAssignableFrom(w))
            .Select(s => (IBaseApiManager)serviceProvider.GetRequiredService(s))
            .ToList();

        foreach (var request in requests)
        {
            var manager = managers.FirstOrDefault(f => f.RequestType == request);
            if (manager == null)
                throw new Exception($"There are no manager for request of type \"{request.FullName}\"");
            
            _requestToManagerMapping.Add(request, manager.GetType());
        }
    }

    public async Task<List<ProcessingResponse>> ProcessRequests(List<IRequest> requests)
    {
        var tasks = requests.Select(ProcessRequest);
        var result = await Task.WhenAll(tasks);
        return result.ToList();
    }

    private async Task<ProcessingResponse> ProcessRequest(IRequest request)
    {
        var requestType = request.GetType();
        try
        {
            var manager = (IBaseApiManager)_serviceProvider.GetRequiredService(_requestToManagerMapping[requestType]);
            var result = await manager.MakeRequest(request);
            return new ProcessingResponse(requestType, result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Exception during sending request - {e.Message}");
            return new ProcessingResponse(requestType, e);
        }
    }
}