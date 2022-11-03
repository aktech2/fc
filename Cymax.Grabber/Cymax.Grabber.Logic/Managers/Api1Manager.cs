using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cymax.Grabber.Entities;
using Cymax.Grabber.Entities.Interfaces;
using Cymax.Grabber.Entities.Models.Api1.Requests;
using Cymax.Grabber.Entities.Models.Api1.Responses;
using Cymax.Grabber.Entities.Models.Common;
using Cymax.Grabber.Logic.Utils;
using Microsoft.Extensions.Configuration;

namespace Cymax.Grabber.Logic.Managers;

/// <summary>
/// API Manager for API 1
/// </summary>
/// <seealso cref="Cymax.Grabber.Entities.Interfaces.IBaseApiManager" />
internal class Api1Manager: IBaseApiManager
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name => "API1";

    /// <summary>
    /// The HTTP client
    /// </summary>
    private readonly HttpClient _client;

    /// <summary>
    /// The application configuration
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Gets the request URL.
    /// </summary>
    /// <value>
    /// The request URL.
    /// </value>
    private string _requestUrl => _configuration[Constants.Api1RequestUrlConfigurationRootName];

    /// <summary>
    /// Initializes a new instance of the <see cref="Api1Manager"/> class.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="configuration">The configuration.</param>
    public Api1Manager(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    /// <summary>
    /// Makes the request to specific API.
    /// </summary>
    /// <param name="request">The request that should be processed.</param>
    /// <returns></returns>
    public async Task<decimal> MakeRequest(CommonRequest request)
    {
        var mapped = new Api1Request()
        {
            WarehouseAddress = request.From,
            ContactAddress = request.To,
            PackageDimensions = request.Packages.Select(s => new PackageDimension()
            {
                Width = s.Width,
                Height = s.Height,
                Depth = s.Depth
            }).ToList()
        };
        var content = HttpHelpers.CreateJsonHttpContent(mapped);
        var timeout = HttpHelpers.GetRequestTimeout(_configuration);
        using var tokenSource = timeout.HasValue
            ? new CancellationTokenSource(timeout.Value)
            : new CancellationTokenSource();
        var token = tokenSource.Token;
        var response = await _client.PostAsync(_requestUrl, content, token);
        var data = await HttpHelpers.ProcessApiJsonResponse<Api1Response>(response);
        return data.Total;
    }
}