namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Libra配置
    /// </summary>
    [GeminiOptions("Main",typeof(GalaxyOptionsBase))]
    public class GalaxyOptions
    {
        /// <summary>
        /// 是否对 JWT 进行校验
        /// </summary>
        public bool NeedValidateToken { get; set; }
        /// <summary>
        /// 刷新 JWT 的服务端通信地址
        /// </summary>
        public string RefreshAddress { get; set; }
       
    }
}
