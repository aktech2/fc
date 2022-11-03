using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cymax.Grabber.Entities.Models.Api1.Requests;

/// <summary>
/// Request definition for API 1
/// </summary>
public class Api1Request
{
    /// <summary>
    /// Gets or sets the contact address.
    /// </summary>
    /// <value>
    /// The contact address.
    /// </value>
    [JsonProperty("contact address")]
    public string ContactAddress { get; set; }

    /// <summary>
    /// Gets or sets the warehouse address.
    /// </summary>
    /// <value>
    /// The warehouse address.
    /// </value>
    [JsonProperty("warehouse address")]
    public string WarehouseAddress { get; set; }

    /// <summary>
    /// Gets or sets the package dimensions.
    /// </summary>
    /// <value>
    /// The package dimensions.
    /// </value>
    [JsonProperty("package dimensions")]
    public List<PackageDimension> PackageDimensions { get; set; }
}