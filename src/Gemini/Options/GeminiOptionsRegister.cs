using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Gemini
{

    /// <summary>
    /// 将选项实体生成注册委托
    /// </summary>
    /// <typeparam name="TOption"></typeparam>
    public static class GeminiOptionsRegister<TOption> where TOption : class, new()
    {
        public static readonly string Prefix;
        static GeminiOptionsRegister()
        {

            var optionsAttr = typeof(TOption).GetCustomAttribute<GeminiOptionsAttribute>();
            Prefix = optionsAttr.Prefix;
            Action <IServiceCollection, IConfiguration> Register = (service, config) =>
            {
                if (optionsAttr.Root!=default)
                {
                    service.Configure<TOption>(config.GetSection(optionsAttr.Root));
                }

                var positions = optionsAttr.Positions;
                for (int i = 0; i < positions.Length; i+=1)
                {
                    service.Configure<TOption>(positions[i], config.GetSection(positions[i]));
                }
                
            };
            GeminiOptionRegisterManagement.OptionsRegisterCache.Add(Register);
        }

        public static void Init()
        {

        }
    }
}
