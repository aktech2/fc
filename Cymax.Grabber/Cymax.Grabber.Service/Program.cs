using System;
using System.Collections.Generic;
using System.Globalization;
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
            /*
             * Request urls can be set in appsettings.json
             * Api1RequestUrl for Api1Request
             * Api2RequestUrl for Api2Request
             * Api3RequestUrl for Api3Request
             * 
             * GlobalApiManager.Instance.Init is one time operation that must be performed before GlobalApiManager.Instance.ProcessRequests
             * 
             * How IRequest must be processed decided inside GlobalApiManager.Instance.ProcessRequests.
             * It retrieves preferred API Manager according RequestType property from IBaseApiManager interface
             *
             * Response with the cheapest price will always be the first in the result list
             *
             * ProcessingResponse can be matched with IRequest from input list by RequestType property
            */
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
            var result = await GlobalApiManager.Instance.ProcessRequests(requests);
            
            Console.WriteLine("Results:");
            for (int i = 0; i < result.Count; i++)
            {
                var item = result[i];
                var price = item.IsSuccess 
                    ? item.Value.ToString(CultureInfo.InvariantCulture)
                    : "-";
                var cheapestMarker = item.IsSuccess && i == 0
                    ? "[Cheapest]"
                    : string.Empty;
                Console.WriteLine($"Request: {item.RequestType.Name}\tPrice: {price}\t{cheapestMarker}");
            }

            Console.ReadLine();
        }

        private static IServiceProvider CreateProvider()
        {
            var collection = new ServiceCollection();
            collection.AddApiManagers();
            return collection.BuildServiceProvider();
        }
    }
}