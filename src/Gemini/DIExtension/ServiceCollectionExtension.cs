using Gemini;
using Gemini.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 需要被使用, 用于注册所有 Gemini 选项实体
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">配置实体</param>
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


        /// <summary>
        /// 注册并创建 Builder
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static TBuilder ConfigGeminiBuilder<TBuilder>(this IServiceCollection services) where TBuilder : new()
        {
            var optionsType = typeof(TBuilder).BaseType.GetGenericArguments()[0];
            var proxyType = typeof(GeminiBuilderProxy<,>).MakeGenericType(typeof(TBuilder), optionsType);
            Debug.WriteLine(proxyType.FullName);
            DynamicMethod method = new DynamicMethod("InitGeminiBuilder" + Guid.NewGuid().ToString(), typeof(TBuilder), new Type[] { typeof(IServiceCollection) });
            ILGenerator il = method.GetILGenerator();
            FieldInfo builder = proxyType.GetField("InitBuilder");

            ConstructorInfo ctor = proxyType.GetConstructors()[0];
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Newobj, ctor);
            il.Emit(OpCodes.Ldfld, builder);
            il.Emit(OpCodes.Ret);
            var func = (Func<IServiceCollection, TBuilder>)(method.CreateDelegate(typeof(Func<IServiceCollection, TBuilder>)));
            return func(services);

        }

    }

}