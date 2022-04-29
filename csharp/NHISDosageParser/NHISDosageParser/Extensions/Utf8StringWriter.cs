using System.IO;
using System.Text;

namespace NHISDosageParser.Extensions
{
    /// <summary>
    /// String Writer with Encoding set to UTF-8
    /// </summary>
    internal class Utf8StringWriter : StringWriter
    {
        /// <summary>
        /// Setting the encoding to UTF-8
        /// </summary>
        public override Encoding Encoding => Encoding.UTF8;
    }
}
