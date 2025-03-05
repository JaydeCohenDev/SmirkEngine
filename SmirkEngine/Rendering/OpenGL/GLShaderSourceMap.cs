using System.Text;
using Silk.NET.OpenGL;

namespace SmirkEngine.Rendering.OpenGL;

public class GLShaderSourceMap
{
    protected readonly Dictionary<ShaderType, StringBuilder> _sourceMap = [];
    
    public void RegisterType(ShaderType directiveShaderType)
    {
        _sourceMap.TryAdd(directiveShaderType, new StringBuilder());
    }
    
    public void AddLine(ShaderType curShaderType, string line)
    {
        _sourceMap[(ShaderType)curShaderType].AppendLine(line);
    }

    public IEnumerable<ShaderType> GetShaderTypes()
    {
        return _sourceMap.Keys;
    }

    public string GetSourceCode(ShaderType shaderType)
    {
        return _sourceMap[shaderType].ToString();
    }
}