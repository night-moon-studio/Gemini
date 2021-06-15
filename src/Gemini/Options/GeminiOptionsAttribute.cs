using System;
using System.Reflection;


/// <summary>
/// 配置选项标签
/// </summary>
public class GeminiOptionsAttribute : Attribute
{

    public string Prefix;
    public string Root;
    public string[] Positions;

    public GeminiOptionsAttribute() { }
    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="positions">节点名</param>
    public GeminiOptionsAttribute(params string[] positions) : this(default, positions, default) { }
    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="root">根节点名</param>
    /// <param name="parentsType">父节点类型</param>
    public GeminiOptionsAttribute(string root, Type parentsType = default) : this(root, default, parentsType) { }
    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="parentsType">父节点类型</param>
    /// <param name="positions">节点名</param>
    public GeminiOptionsAttribute(Type parentsType, params string[] positions) : this(default, positions, parentsType) { }

    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="root">根节点名</param>
    /// <param name="positions">节点名</param>
    /// <param name="parentsType">父节点类型</param>
    public GeminiOptionsAttribute(string root, string[] positions, Type parentsType = default)
    {

        Positions = positions == default ? new string[0] : positions;
        Root = root;
        if (parentsType != null)
        {
            var optionsAttr = parentsType.GetCustomAttribute<GeminiOptionsAttribute>();

            if (optionsAttr != null)
            {
                Prefix = optionsAttr.Root;
                if (Positions != null)
                {
                    for (int i = 0; i < Positions.Length; i++)
                    {
                        Positions[i] = $"{optionsAttr.Root}:{Positions[i]}";
                    }
                }

            }
            Root = $"{optionsAttr.Root}:{Root}";
        }

    }

}
