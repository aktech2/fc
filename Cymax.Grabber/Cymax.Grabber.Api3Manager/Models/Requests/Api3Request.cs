using System.Collections.Generic;
using System.Xml.Serialization;

namespace Cymax.Grabber.Api3Manager.Models.Requests;

/// <summary>
/// Request definition for API 3. 
/// This class is public since <see cref="XmlSerializer"/> do not accept non-public objects.
/// </summary>
[XmlRoot("xml")]
public class Api3Request
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

    /// <summary>
    /// Gets or sets the packages.
    /// </summary>
    /// <value>
    /// The packages.
    /// </value>
    [XmlArray("packages")]
    [XmlArrayItem(typeof(Package), ElementName = "package")]
    public List<Package> Packages { get; set; }
}