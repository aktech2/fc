using System.IO;
using System.Text;

namespace Cymax.Grabber.CommonUtils;

/// <summary>
/// Writer that allow to serialized XML with encoding
/// </summary>
/// <seealso cref="System.IO.StringWriter" />
public class StringWriterWithEncoding: StringWriter 
{
    /// <summary>
    /// Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.
    /// </summary>
    public override Encoding Encoding { get; }
    /// <summary>
    /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
    /// </summary>
    /// <param name="encoding">The encoding.</param>
    public StringWriterWithEncoding(Encoding encoding)
    { 
        Encoding = encoding; 
    } 
} 