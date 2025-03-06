namespace Prism.Types;

public abstract class ShaderTypeMap
{
    public abstract string Map(Type type);

    public abstract string MapTypeString(string typeString);
}