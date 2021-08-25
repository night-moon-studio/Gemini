using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        private static bool _done;
        private static readonly object _lock;
        internal readonly static ConcurrentDictionary<Type, string> _optionsTypeCache;
        private readonly static MethodInfo _configWithNameMethodInfo;
        private readonly static MethodInfo _configWithoutNameMethodInfo;
        static ServiceCollectionExtension()
        {
            _done = false;
            _lock = new object();
            _optionsTypeCache = new ConcurrentDictionary<Type, string>();
            _configWithNameMethodInfo = typeof(OptionsConfigurationServiceCollectionExtensions).GetMethod("Configure", new Type[] { typeof(IServiceCollection), typeof(string), typeof(IConfiguration) });
            _configWithoutNameMethodInfo = typeof(OptionsConfigurationServiceCollectionExtensions).GetMethod("Configure", new Type[] { typeof(IServiceCollection), typeof(IConfiguration) });
        }


        /// <summary>
        /// 需要被使用, 用于注册所有 Gemini 选项实体
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">配置实体</param>
        public static IServiceCollection AddGeminiBuilder<TBuilder>(this IServiceCollection services, Func<TBuilder, TBuilder> builderAction = null) where TBuilder : IGeminiBuilder, new()
        {
            TBuilder builder = new TBuilder();
            builder.SetServiceCollection(services);
            builder.Configuration();
            builderAction?.Invoke(builder);
            return services;
        }


        /// <summary>
        /// 添加自动选项
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddGeminiOptions(this IServiceCollection services)
        {
            if (!_done)
            {
                lock (_lock)
                {
                    if (!_done)
                    {
                        _done = true;

                        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                        foreach (var asm in assemblies)
                        {
                            if (!asm.IsDynamic)
                            {
                                try
                                {
                                    var types = asm.GetTypes();
                                    foreach (var type in types)
                                    {

                                        var optionsAttr = type.GetCustomAttribute<GeminiOptionsAttribute>();
                                        if (optionsAttr != default)
                                        {
                                            var positions = optionsAttr.Positions;
                                            if (positions.Length == 1)
                                            {
                                                var key = positions[0];
#if DEBUG
                                                Console.WriteLine($"注册配置 [{key}] 节点类型为 {type.Name}!");
#endif
                                                _optionsTypeCache[type] = key;
                                                var methodInfo = _configWithoutNameMethodInfo.MakeGenericMethod(type);
                                                methodInfo.Invoke(null, new object[] { services, IConfigurationBuilderExtension.Configuration.GetSection(key) });
                                            }
                                            else
                                            {
                                                for (int i = 0; i < positions.Length; i += 1)
                                                {

                                                    var key = positions[i];
#if DEBUG
                                                    Console.WriteLine($"注册配置 [{key}] 节点类型为 {type.Name}!");
#endif
                                                    _optionsTypeCache[type] = key;
                                                    var methodInfo = _configWithNameMethodInfo.MakeGenericMethod(type);
                                                    methodInfo.Invoke(null, new object[] { services, key, IConfigurationBuilderExtension.Configuration.GetSection(key) });

                                                }
                                            }

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            return services;
        }
    }

}