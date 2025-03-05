using System.Text;
using Silk.NET.OpenGL;

namespace SmirkEngine.Rendering.OpenGL;

public partial class GLShader
{
    private static class GLShaderLoader
    {
        private const string SHADER_TYPE_DIRECTIVE = "#//";
        
        public static bool Load(GL gl, GLShader shader, string path)
        {
            var source = ParseShaderSourceFile(path);
            return GLShaderCompiler.CompileShaderProgram(gl, shader, source);
        }

        private static GLShaderSourceMap ParseShaderSourceFile(string path)
        {
            var lines = File.ReadAllLines(path);
            return ParseShaderSourceLines(lines);
        }

        private static GLShaderSourceMap ParseShaderSourceLines(string[] lines)
        {
            GLShaderSourceMap sourceMap = new();
            ShaderType? curShaderType = null;
            
            foreach (var line in lines)
            {
                if (TryGetShaderTypeDirective(line, out var shaderDirectiveType))
                {
                    curShaderType = shaderDirectiveType;
                    sourceMap.RegisterType((ShaderType)shaderDirectiveType!);
                }
                else if(curShaderType != null)
                {
                    sourceMap.AddLine((ShaderType)curShaderType, line);
                }
            }

            return sourceMap;
        }

        private static bool TryGetShaderTypeDirective(string line, out ShaderType? directiveShaderType)
        {
            directiveShaderType = null;
            
            if (!IsLinePossibleDirective(line)) 
                return false;
            
            directiveShaderType = GetShaderTypeFromDirective(line);
            
            return directiveShaderType is not null;
        }

        private static bool IsLinePossibleDirective(string line)
        {
            return line.StartsWith(SHADER_TYPE_DIRECTIVE);
        }

        private static ShaderType? GetShaderTypeFromDirective(string line)
        {
            var directive = line.Replace(SHADER_TYPE_DIRECTIVE, "");
            
            if (Enum.TryParse<ShaderType>(directive, true, out var shaderType))
                return shaderType;

            return null;
        }
    }
}
