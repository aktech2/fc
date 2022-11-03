using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Cymax.Grabber.CommonUtils;

/// <summary>
/// XML serializer/deserializer 
/// </summary>
public static class XmlConvertor
{
    /// <summary>
    /// RegEx for removing nodes with NULL value
    /// </summary>
    private static readonly Regex NullDataRegEx = new Regex("\\s+<\\w+ xsi:nil=\"true\" \\/>", RegexOptions.Compiled);

    /// <summary>
    /// Serializes the specified object.
    /// </summary>
    /// <param name="o">The object that should be serialized.</param>
    /// <returns></returns>
    public static string Serialize(object o)
    {
        var xws = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            Encoding = Encoding.UTF8,
#if DEBUG
            Indent = true,
#else
            Indent = false,
#endif
            
        };
        var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

        var formatter = new XmlSerializer(o.GetType());
        
        // To prevent ignoring XmlWriterSettings.Encoding setting
        var textWriter = new StringWriterWithEncoding(xws.Encoding);
        using var xmlWriter = XmlWriter.Create(textWriter, xws);
        formatter.Serialize(xmlWriter, o, ns);
        var res = textWriter.ToString();
        return NullDataRegEx.Replace(res, string.Empty);
    }

    /// <summary>
    /// Deserializes the specified XML from stream.
    /// </summary>
    /// <typeparam name="T">Type of return object.</typeparam>
    /// <param name="stream">The stream that should be processed.</param>
    /// <returns></returns>
    public static T Deserialize<T>(Stream stream)
    {
        return Deserialize<T>(new StreamReader(stream));
    }

    /// <summary>
    /// Deserializes the specified XML from string.
    /// </summary>
    /// <typeparam name="T">Type of return object.</typeparam>
    /// <param name="xml">The XML that should be processed.</param>
    /// <returns></returns>
    public static T Deserialize<T>(string xml)
    {
        return Deserialize<T>(new StringReader(xml));
    }

    /// <summary>
    /// Deserializes the specified XML from <see cref="TextReader"/> instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="reader">The reader.</param>
    /// <returns></returns>
    private static T Deserialize<T>(TextReader reader)
    {
        var formatter = new XmlSerializer(typeof(T));
        return (T)formatter.Deserialize(reader);
    }
}