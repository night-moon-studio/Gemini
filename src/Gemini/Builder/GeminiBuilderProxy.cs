using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Gemini.Builder
{

    public class GeminiBuilderProxy<TBuilder,TOptions> where TBuilder : IGeminiBuilder<TOptions>,new() where TOptions : class, new()
    {
        public TBuilder InitBuilder;
        public GeminiBuilderProxy(IServiceCollection services)
        {

            var builder = new TBuilder();
            var options = services.BuildServiceProvider().GetService<IOptionsMonitor<TOptions>>();
            builder.SetOptions(options);
            builder.ConfigServices(services);
            InitBuilder = builder;

        }
    }

}
