using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace ConfigTestConsoleApp
{
    /// <summary>
    /// A hosting configuration file based <see cref="FileConfigurationProvider"/>.
    /// </summary>
    public class FunctionsHostingConfigProvider : FileConfigurationProvider
    {
        private readonly string _filePath = "config.txt";

        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings.</param>
        public FunctionsHostingConfigProvider(FunctionsHostingConfigSource source) : base(source) { }

        public override void Load(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            Data = Parse(text);
        }

        private static Dictionary<string, string> Parse(string settings)
        {
            // Expected settings: "ENABLE_FEATUREX=1,A=B,TimeOut=123"
            return string.IsNullOrEmpty(settings)
                ? new Dictionary<string, string>()
                : settings
                    .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                    .Where(a => a.Length == 2)
                    .ToDictionary(a => a[0], a => a[1], StringComparer.OrdinalIgnoreCase);
        }
    }
}
