using Cymax.Grabber.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cymax.Grabber.Api1Manager.Utils;

public static class ServiceCollectionHelper
{
    public static void AddApi1Manager(this IServiceCollection collection)
    {
        collection.AddHttpClient<IBaseApiManager, Api1Manager>();
    }
}