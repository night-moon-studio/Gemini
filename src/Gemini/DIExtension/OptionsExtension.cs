using Gemini;
using Microsoft.Extensions.Options;


public static class OptionsExtension
{
    public static T GeminiGet<T>(this IOptionsMonitor<T> options, string nodeName) where T : class, new()
    {

        return options.Get($"{GeminiOptionsRegister<T>.Prefix}:{nodeName}");

    }

    public static T GeminiGet<T>(this IOptionsSnapshot<T> options, string nodeName) where T : class, new()
    {

        return options.Get($"{GeminiOptionsRegister<T>.Prefix}:{nodeName}");
    }
}

