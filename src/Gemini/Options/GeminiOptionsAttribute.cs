using System;
using System.Reflection;


/// <summary>
/// 配置选项标签
/// </summary>
public class GeminiOptionsAttribute : Attribute
{

    public string Prefix;
    public string[] Positions;

    public GeminiOptionsAttribute() { }
    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="positions">节点名</param>
    public GeminiOptionsAttribute(params string[] positions) : this(positions, default) { }
    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="root">根节点名</param>
    /// <param name="parentsType">父节点类型</param>
    public GeminiOptionsAttribute(string root, Type parentsType = default) : this(parentsType, root) { }
    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="parentsType">父节点类型</param>
    /// <param name="positions">节点名</param>
    public GeminiOptionsAttribute(Type parentsType, params string[] positions) : this(positions, parentsType) { }

    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="root">根节点名</param>
    /// <param name="positions">节点名</param>
    /// <param name="parentsType">父节点类型</param>
    public GeminiOptionsAttribute(string[] positions, Type parentsType = default)
    {

        Positions = positions == default ? new string[1] { "" } : positions;
        if (parentsType != null)
        {
            var parentsAttr = parentsType.GetCustomAttribute<GeminiOptionsAttribute>();
            if (parentsAttr != null)
            {
                if (Positions != null)
                {
                    for (int i = 0; i < Positions.Length; i++)
                    {
                        Positions[i] = $"{parentsAttr.Prefix}:{Positions[i]}";
                    }
                }
            }
        }
        Prefix = Positions[0];

    }

}
