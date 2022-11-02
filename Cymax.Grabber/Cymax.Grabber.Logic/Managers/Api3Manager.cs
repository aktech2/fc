using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cymax.Grabber.Entities;
using Cymax.Grabber.Entities.Interfaces;
using Cymax.Grabber.Entities.Models.Api3.Requests;
using Cymax.Grabber.Entities.Models.Api3.Responses;
using Cymax.Grabber.Entities.Models.Common;
using Cymax.Grabber.Logic.Utils;
using Microsoft.Extensions.Configuration;

namespace Cymax.Grabber.Logic.Managers;

internal class Api3Manager: IBaseApiManager
{
    public string Name => "API3";
        
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private string _requestUrl => _configuration[Constants.Api3RequestUrlConfigurationRootName];

    public Api3Manager(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    public async Task<decimal> MakeRequest(CommonRequest request)
    {
        var mapped = new Api3Request()
        {
            Source = request.From,
            Destination = request.To,
            Packages = request.Packages.Select(s => new Package()
            {
                Width = s.Width,
                Height = s.Height,
                Depth = s.Depth
            }).ToList()
        };
        var content = HttpHelpers.CreateXmlHttpContent(mapped);
        var timeout = HttpHelpers.GetRequestTimeout(_configuration);
        using var tokenSource = timeout.HasValue
            ? new CancellationTokenSource(timeout.Value)
            : new CancellationTokenSource();
        var token = tokenSource.Token;
        var response = await _client.PostAsync(_requestUrl, content, token);
        var data = await HttpHelpers.ProcessApiXmlResponse<Api3Response>(response);
        return data.Quote;
    }
}