namespace Microsoft.Extensions.DependencyInjection
{

    /// <summary>
    /// Galaxy Redis 配置
    /// </summary>
    [GeminiOptions("Redis",typeof(GalaxyOptionsBase))]
    public class GalaxyRedisOptions
    {
        /// <summary>
        /// Redis 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

    }
}
