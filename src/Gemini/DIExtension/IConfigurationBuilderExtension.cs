using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public static class IConfigurationBuilderExtension
{
    public static IConfigurationRoot Configuration;
    public static IConfigurationBuilder AddGeminiConfig(this IConfigurationBuilder configurationBuilder)
    {
        Configuration = configurationBuilder.Build();
        return configurationBuilder;
    }

    public static string ConfigString(this string key)
    {
       return Configuration[key];
    }

    public static long ConfigLong(this string key)
    {
        return Convert.ToInt64(Configuration[key]);
    }
}

