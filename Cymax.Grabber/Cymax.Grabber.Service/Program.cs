using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cymax.Grabber.Entities.Interfaces;
using Cymax.Grabber.Entities.Models.Api1.Requests;
using Cymax.Grabber.Entities.Models.Api2.Requests;
using Cymax.Grabber.Entities.Models.Api3.Requests;
using Cymax.Grabber.Logic;
using Cymax.Grabber.Logic.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Cymax.Grabber.Service
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GlobalApiManager.Instance.Init(CreateProvider());
            var requests = new List<IRequest>()
            {
                new Api1Request(),
                new Api2Request(),
                new Api3Request()
                {
                    Source = "3440, My street, I",
                    Destination = "3440, My Street, My neighbor",
                    Packages = new List<Package>()
                    {
                        new Package()
                        {
                            Width = 1m,
                            Depth = 0.05m,
                            Height = 5.404m
                        },
                        new Package()
                        {
                            Width = 11m,
                            Depth = 1.05m,
                            Height = 0.404m
                        }
                    }
                }
            };
            var res = await GlobalApiManager.Instance.ProcessRequests(requests);
        }

        private static IServiceProvider CreateProvider()
        {
            var collection = new ServiceCollection();
            collection.AddApiManagers();
            return collection.BuildServiceProvider();
        }
    }
}