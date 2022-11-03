namespace Cymax.Grabber.Entities.Models.Api2.Requests;

/// <summary>
/// Package dimensions definition for API 2
/// </summary>
public class CartonDimension
{
    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    /// <value>
    /// The width.
    /// </value>
    public decimal Width { get; set; }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    /// <value>
    /// The height.
    /// </value>
    public decimal Height { get; set; }

    /// <summary>
    /// Gets or sets the depth.
    /// </summary>
    /// <value>
    /// The depth.
    /// </value>
    public decimal Depth { get; set; }
}