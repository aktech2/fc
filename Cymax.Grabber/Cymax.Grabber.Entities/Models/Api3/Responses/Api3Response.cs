﻿using System.Xml.Serialization;

namespace Cymax.Grabber.Entities.Models.Api3.Responses;

/// <summary>
/// Definition of response from API 3
/// </summary>
[XmlRoot("xml")]
public class Api3Response
{
    /// <summary>
    /// Gets or sets the quote for packages.
    /// </summary>
    /// <value>
    /// The quote.
    /// </value>
    [XmlElement("quote")]
    public decimal Quote { get; set; }
}