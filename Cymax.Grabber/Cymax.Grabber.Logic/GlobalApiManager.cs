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

/// <summary>
/// Factory that process <see cref="CommonRequest"/> on different API Managers
/// </summary>
public class GlobalApiManager
{
    /// <summary>
    /// The service provider
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// List of available managers
    /// </summary>
    private static List<Type> _managers;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalApiManager"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="logger">The logger.</param>
    public GlobalApiManager(IServiceProvider serviceProvider, ILogger<GlobalApiManager> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <summary>
    /// Initializes this instance. Method required to be called once on startup
    /// </summary>
    public void Init()
    {
        var managersAssembly = typeof(GlobalApiManager).Assembly;
        _managers = managersAssembly
            .GetTypes()
            .Where(w => w.IsClass && typeof(IBaseApiManager).IsAssignableFrom(w))
            .ToList();
    }

    /// <summary>
    /// Processes <see cref="CommonRequest"/>.
    /// Return a list of information about possible delivery price.
    /// List sorted in ascending order by <see cref="ProcessingResponse.Value"/> property (unsuccessful responses presented at the end of the list).
    /// That means that response with cheapest delivery price located at the first position of the list.
    /// Pay attention that all API request can be unsuccessful so <see cref="ProcessingResponse.IsSuccess"/> must be verified even for first element in the list
    /// </summary>
    /// <param name="request">The <see cref="CommonRequest"/></param>
    /// <returns></returns>
    public async Task<List<ProcessingResponse>> ProcessRequest(CommonRequest request)
    {
        var works = _managers.Select(managerType => ProcessRequest(request, managerType));
        var result = await Task.WhenAll(works);
        var list = result.ToList();
        list.Sort(new ProcessingResponseComparer());
        return list;
    }

    /// <summary>
    /// Initialize on of API Managers and process request on it.
    /// </summary>
    /// <param name="request">The <see cref="CommonRequest"/></param>
    /// <param name="managerType">Type of the manager.</param>
    /// <returns></returns>
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