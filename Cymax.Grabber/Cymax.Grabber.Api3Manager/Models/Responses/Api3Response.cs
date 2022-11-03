using System.Xml.Serialization;

namespace Cymax.Grabber.Api3Manager.Models.Responses;

/// <summary>
/// Definition of response from API 3
/// This class is public since <see cref="XmlSerializer"/> do not accept non-public objects.
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