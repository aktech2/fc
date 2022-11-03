using Cymax.Grabber.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cymax.Grabber.Api3Manager.Utils;

public static class ServiceCollectionHelper
{
    public static void AddApi3Manager(this IServiceCollection collection)
    {
        collection.AddHttpClient<IBaseApiManager, Api3Manager>();
    }
}