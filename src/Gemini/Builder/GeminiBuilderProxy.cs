using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Gemini.Builder
{

    public class GeminiBuilderProxy<TBuilder,TOptions> where TBuilder : IGeminiBuilder<TOptions>,new() where TOptions : class, new()
    {
        public TBuilder InitBuilder;
        public GeminiBuilderProxy(ServiceProvider serviceProvider)
        {
            var builder = new TBuilder();
            var options = serviceProvider.GetService<IOptionsMonitor<TOptions>>();
            builder.SetOptions(options);
            builder.ConfigFunctions();
            InitBuilder = builder;
        }
    }

}
