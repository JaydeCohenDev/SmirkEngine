using Silk.NET.OpenGL;

namespace SmirkEngine.Rendering.OpenGL;

public static class GLShaderCompiler
{
    public static bool CompileShaderProgram(GL gl, GLShader shaderProgram, GLShaderSourceMap sourceMap)
    {
        List<uint> shaders = [];
        
        foreach(var shaderType in sourceMap.GetShaderTypes())
        {
            var shader = gl.CreateShader(shaderType);
            shaders.Add(shader);
            
            var sourceCode = sourceMap.GetSourceCode(shaderType);
            if (!CompileShader(gl, shader, sourceCode)) return false;
            
            gl.AttachShader(shaderProgram.Handle, shader);
        }

        LinkShader(gl, shaderProgram);
        
        shaders.ForEach(shader =>
        {
            gl.DetachShader(shaderProgram.Handle, shader);
            gl.DeleteShader(shader);
        });

        return true;
    }

    private static bool CompileShader(GL gl, uint shader, string sourceCode)
    {
        gl.ShaderSource(shader, sourceCode);
        gl.CompileShader(shader);
        gl.GetShader(shader, ShaderParameterName.CompileStatus, out var compileStatus);
        if (compileStatus == (int)GLEnum.True)
            return true;
        
        gl.GetShaderInfoLog(shader, out var infoLog);
        throw new Exception($"Shader compilation failed: {infoLog}");
        return false;
    }

    private static bool LinkShader(GL gl, GLShader shaderProgram)
    {
        gl.LinkProgram(shaderProgram.Handle);
        gl.GetProgram(shaderProgram.Handle, GLEnum.LinkStatus, out var linkStatus);
        if (linkStatus == (int)GLEnum.True) return true;
        
        gl.GetProgramInfoLog(shaderProgram.Handle, out var infoLog);
        throw new Exception($"Shader linking failed: {infoLog}");
        return false;
    }
}