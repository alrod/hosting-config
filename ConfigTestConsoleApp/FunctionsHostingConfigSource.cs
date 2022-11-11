using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace ConfigTestConsoleApp
{
    /// <summary>
    /// Represents a JSON file as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class FunctionsHostingConfigSource : FileConfigurationSource
    {
        /// <summary>
        /// Builds the <see cref="JsonConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>A <see cref="FunctionsHostingConfigProvider"/></returns>
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {            
            Path = "config.txt";
            Optional = true;
            ReloadOnChange = true;
            ResolveFileProvider();

            EnsureDefaults(builder);
            return new FunctionsHostingConfigProvider(this);
        }
    }
}
