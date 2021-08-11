using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 功能初始化抽象类
/// </summary>
public abstract class IGeminiBuilder
{
    protected IServiceCollection _service;
    internal void SetServiceCollection(IServiceCollection service)
    {
        _service = service;
    }


    /// <summary>
    /// 获取选项实体
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public TOptions GetOptions<TOptions>(string key = default)
    {
        if (key == default)
        {
            key = ServiceCollectionExtension._optionsTypeCache[typeof(TOptions)];
        }
        var section = IConfigurationBuilderExtension.Configuration.GetSection(key);
        return section.Get<TOptions>();
    }

    /// <summary>
    /// 需要被实现,进行必要的初始化
    /// </summary>
    public abstract void Configuration();
}
