using System;
using System.Reflection;

public class GeminiOptionsAttribute : Attribute
{

    public string Prefix;
    public string Root;
    public string[] Positions;

    public GeminiOptionsAttribute() { }
    public GeminiOptionsAttribute(params string[] positions) : this(default, positions, default) { }
    public GeminiOptionsAttribute(string root, Type parentsType = default) : this(root, default, parentsType) { }
    public GeminiOptionsAttribute(Type parentsType, params string[] positions) : this(default, positions, parentsType) { }
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
