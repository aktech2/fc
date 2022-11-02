using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cymax.Grabber.Entities.Interfaces;
using Cymax.Grabber.Entities.Models.Common;
using Cymax.Grabber.Entities.Models.Factory;
using Cymax.Grabber.Logic.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cymax.Grabber.Logic;

//Singleton Lazy implementation 
public class GlobalApiManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;
    private List<Type> _managers;

    public GlobalApiManager(IServiceProvider serviceProvider, ILogger<GlobalApiManager> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public void Init()
    {
        var managersAssembly = typeof(GlobalApiManager).Assembly;
        _managers = managersAssembly
            .GetTypes()
            .Where(w => w.IsClass && typeof(IBaseApiManager).IsAssignableFrom(w))
            .ToList();
    }

    public async Task<List<ProcessingResponse>> ProcessRequest(CommonRequest request)
    {
        var works = _managers.Select(managerType => ProcessRequest(request, managerType));
        var result = await Task.WhenAll(works);
        var list = result.ToList();
        list.Sort(new ProcessingResponseComparer());
        return list;
    }

    private async Task<ProcessingResponse> ProcessRequest(CommonRequest request, Type managerType)
    {
        _logger.LogInformation($"Processing of {managerType} started");
        IBaseApiManager manager = null;
        try
        {
            manager = (IBaseApiManager)_serviceProvider.GetRequiredService(managerType);
            var result = await manager.MakeRequest(request);
            return new ProcessingResponse(manager.Name, result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Exception during sending request - {e.Message}");
            return new ProcessingResponse(manager?.Name ?? managerType.Name, e);
        }
        finally
        {
            _logger.LogInformation($"Processing of {managerType} finished");
        }
    }
}