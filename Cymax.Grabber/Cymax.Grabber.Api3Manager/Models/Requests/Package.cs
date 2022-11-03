﻿using System.Xml.Serialization;

namespace Cymax.Grabber.Api3Manager.Models.Requests;

/// <summary>
///  Package dimensions definition for API 3
/// </summary>
internal class Package
{
    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    /// <value>
    /// The width.
    /// </value>
    [XmlElement("width")]
    public decimal Width { get; set; }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    /// <value>
    /// The height.
    /// </value>
    [XmlElement("height")]
    public decimal Height { get; set; }

    /// <summary>
    /// Gets or sets the depth.
    /// </summary>
    /// <value>
    /// The depth.
    /// </value>
    [XmlElement("depth")]
    public decimal Depth { get; set; }
}