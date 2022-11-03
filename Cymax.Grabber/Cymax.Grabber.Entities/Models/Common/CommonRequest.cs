using System.Collections.Generic;

namespace Cymax.Grabber.Entities.Models.Common;

/// <summary>
/// Common definition of request that should be processed on different APIs
/// </summary>
public class CommonRequest
{
    /// <summary>
    /// Gets or sets start address.
    /// </summary>
    /// <value>
    /// From.
    /// </value>
    public string From { get; set; }

    /// <summary>
    /// Gets or sets end address.
    /// </summary>
    /// <value>
    /// To.
    /// </value>
    public string To { get; set; }

    /// <summary>
    /// Gets or sets the packages.
    /// </summary>
    /// <value>
    /// The packages.
    /// </value>
    public List<CommonPackage> Packages { get; set; }
}