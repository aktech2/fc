using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Cymax.Grabber.Logic.Utils;

// Class covered by tests
public static class XmlConvertor
{
    private static readonly Regex NullDataRegEx = new Regex("\\s+<\\w+ xsi:nil=\"true\" \\/>", RegexOptions.Compiled);
    
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

    public static T Deserialize<T>(Stream stream)
    {
        return Deserialize<T>(new StreamReader(stream));
    }
    
    public static T Deserialize<T>(string xml)
    {
        return Deserialize<T>(new StringReader(xml));
    }
    
    private static T Deserialize<T>(TextReader reader)
    {
        var formatter = new XmlSerializer(typeof(T));
        return (T)formatter.Deserialize(reader);
    }
}