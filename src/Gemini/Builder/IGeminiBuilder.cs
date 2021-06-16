using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public abstract class IGeminiBuilder<TOptions> where TOptions : class, new()
{
    protected TOptions _options;

    /// <summary>
    /// 调用顺序1
    /// </summary>
    /// <param name="options"></param>
    public virtual void SetOptions(IOptionsMonitor<TOptions> options)
    {
        _options = options.CurrentValue;
    }
    /// <summary>
    /// 设置服务,调用顺序2
    /// </summary>
    /// <param name="services"></param>
    public virtual void ConfigServices(IServiceCollection services) { }
}
