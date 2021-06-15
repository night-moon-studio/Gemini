using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;

namespace Gemini
{

    public class GeminiOptionRegisterManagement
    {
        public static readonly ConcurrentBag<Action<IServiceCollection, IConfiguration>> OptionsRegisterCache;
        static GeminiOptionRegisterManagement()
        {
            OptionsRegisterCache = new ConcurrentBag<Action<IServiceCollection, IConfiguration>>();
        }
    }

}
