using Microsoft.Extensions.Options;

public abstract class IGeminiBuilder<TOptions> where TOptions : class, new()
{
    protected TOptions _options;
    public virtual void SetOptions(IOptionsMonitor<TOptions> options)
    {
        _options = options.CurrentValue;
    }

    protected internal abstract void ConfigFunctions();
}
