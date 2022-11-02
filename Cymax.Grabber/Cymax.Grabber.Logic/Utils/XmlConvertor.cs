using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Cymax.Grabber.Logic.Utils;

public class XmlConvertor
{
    private static Regex _nullDataRegEx = new Regex("\\s+<\\w+ xsi:nil=\"true\" \\/>", RegexOptions.Compiled);
    
    public static string Serialize(object o)
    {
        var xws = new XmlWriterSettings
        {
            OmitXmlDeclaration = false,
            Encoding = Encoding.UTF8,
            Indent = true,
        };

        var formatter = new XmlSerializer(o.GetType());
        
        // XmlWriter ignores XmlWriterSettings.Encoding setting
        var textWriter = new StringWriterWithEncoding(xws.Encoding);
        using var xmlWriter = XmlWriter.Create(textWriter, xws);
        formatter.Serialize(xmlWriter, o);
        var res = textWriter.ToString();
        return _nullDataRegEx.Replace(res, string.Empty);
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