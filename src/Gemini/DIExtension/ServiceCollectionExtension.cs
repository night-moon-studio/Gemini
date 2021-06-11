using Gemini;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {

        public static void AddGeminiOptions(this IServiceCollection services, IConfiguration configuration)
        {

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i += 1)
            {

                var types = assemblies[i].GetTypes();
                for (int j = 0; j < types.Length; j += 1)
                {
                    try
                    {
                        var attr = types[j].GetCustomAttribute<GeminiOptionsAttribute>();
                        if (attr != null)
                        {
                            typeof(GeminiOptionsRegister<>).MakeGenericType(types[j]).GetMethod("Init").Invoke(null,null);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }

            foreach (var action in GeminiOptionRegisterManagement.OptionsRegisterCache)
            {
                action(services, configuration);
            }
            GeminiOptionRegisterManagement.OptionsRegisterCache.Clear();
        }

    }

}