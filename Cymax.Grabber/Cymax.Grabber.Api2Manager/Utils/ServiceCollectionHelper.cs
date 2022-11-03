using Cymax.Grabber.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cymax.Grabber.Api2Manager.Utils;

public static class ServiceCollectionHelper
{
    public static void AddApi2Manager(this IServiceCollection collection)
    {
        collection.AddHttpClient<IBaseApiManager, Api2Manager>();
    }
}