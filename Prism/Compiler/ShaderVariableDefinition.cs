using System.Reflection;
using Prism.Types;

namespace Prism.Compiler;

public class ShaderVariableDefinition
{
    public string Name { get; set; } = string.Empty;
    public Type Type { get; set; } = null!;
    public bool IsInput { get; set; } = false;
    public bool IsOutput { get; set; } = false;
    public bool IsUniform { get; set; } = false;
    public int Binding { get; set; } = -1;
    public int Location { get; set; } = -1;
    public MemberInfo MemberInfo { get; set; } = null!;

    public override string ToString()
    {
        if (IsInput)
        {
            return $"Input: {Name} ({Type.Name}) - Binding: {Binding} - Member: {MemberInfo.ToString()}";
        }
        else if(IsOutput)
        {
            return $"Output: {Name} ({Type.Name}) - Location: {Location} - Member: {MemberInfo.ToString()}";
        }

        return $"Invalid variable {Name}";
    }
}
