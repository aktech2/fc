using System.Collections.Generic;
using System.Xml.Serialization;

namespace Cymax.Grabber.Tests.Models;

/// <summary>
/// Sample model to test XML serializer
/// </summary>
[XmlRoot("xml")]
public class SimpleTestModel
{

    /// <summary>
    /// Gets or sets the source address.
    /// </summary>
    /// <value>
    /// The source.
    /// </value>
    [XmlElement("source")]
    public string Source { get; set; }

    /// <summary>
    /// Gets or sets the destination address.
    /// </summary>
    /// <value>
    /// The destination.
    /// </value>
    [XmlElement("destination")]
    public string Destination { get; set; }
}