using System.IO;
using System.Text;

namespace Cymax.Grabber.Logic.Utils;

public class StringWriterWithEncoding: StringWriter 
{
    public override Encoding Encoding { get; }
    public StringWriterWithEncoding(Encoding encoding)
    { 
        Encoding = encoding; 
    } 
} 