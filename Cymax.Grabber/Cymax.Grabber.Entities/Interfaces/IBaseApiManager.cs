using System.Threading.Tasks;
using Cymax.Grabber.Entities.Models.Common;

namespace Cymax.Grabber.Entities.Interfaces;

/// <summary>
/// Base interface for API Managers
/// API Manager is service to processing <see cref="CommonRequest" /> on specific external service (e.g. HTTP Web API)
/// </summary>
public interface IBaseApiManager
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    string Name { get; }

    /// <summary>
    /// Makes the request to specific API.
    /// </summary>
    /// <param name="request">The request that should be processed.</param>
    /// <returns></returns>
    Task<decimal> MakeRequest(CommonRequest request);
}