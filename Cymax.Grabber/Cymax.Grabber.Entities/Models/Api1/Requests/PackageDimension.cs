﻿namespace Cymax.Grabber.Entities.Models.Api1.Requests;

/// <summary>
/// Package dimensions definition for API 1
/// </summary>
public class PackageDimension
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