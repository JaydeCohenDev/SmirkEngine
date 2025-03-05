using Silk.NET.OpenGL;

namespace SmirkEngine.Rendering.OpenGL;

public static class GLShaderCompiler
{
    public static bool CompileShaderProgram(GL gl, GLShader shaderProgram, GLShaderSourceMap sourceMap)
    {
        if (!CompileAndAttachShaders(gl, sourceMap, shaderProgram, out var shaders))
            return false;

        if (!LinkShader(gl, shaderProgram))
            return false;

        if (!CleanupShaders(gl, shaderProgram, shaders)) 
            return false;

        return true;
    }

    private static bool CleanupShaders(GL gl, GLShader shaderProgram, List<uint> shaders)
    {
        shaders.ForEach(shader =>
        {
            gl.DetachShader(shaderProgram.Handle, shader);
            gl.DeleteShader(shader);
        });

        return true;
    }

    private static bool CompileAndAttachShaders(GL gl, GLShaderSourceMap sourceMap, GLShader shaderProgram, out List<uint> shaders)
    {
        shaders = [];
        foreach(var shaderType in sourceMap.GetShaderTypes())
        { 
            var sourceCode = sourceMap.GetSourceCode(shaderType);
            if(!CompileAndAttachShader(gl, shaderType, shaderProgram, sourceCode, out var shader)) 
                return false;
            shaders.Add(shader);
        }

        return true;
    }

    private static bool CompileAndAttachShader(GL gl, ShaderType shaderType, GLShader shaderProgram, string sourceCode, out uint shader)
    {
        shader = gl.CreateShader(shaderType);
        
        if (!CompileShader(gl, shader, sourceCode)) 
            return false;
            
        gl.AttachShader(shaderProgram.Handle, shader);
        return true;
    }

    private static bool CompileShader(GL gl, uint shader, string sourceCode)
    {
        gl.ShaderSource(shader, sourceCode);
        gl.CompileShader(shader);
        
        return !HasCompileErrors(gl, shader);
    }

    private static bool HasCompileErrors(GL gl, uint shader)
    {
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
        
        return !HasLinkErrors(gl, shaderProgram);
    }

    private static bool HasLinkErrors(GL gl, GLShader shaderProgram)
    {
        gl.GetProgram(shaderProgram.Handle, GLEnum.LinkStatus, out var linkStatus);
        if (linkStatus == (int)GLEnum.True) 
            return true;
        
        gl.GetProgramInfoLog(shaderProgram.Handle, out var infoLog);
        throw new Exception($"Shader linking failed: {infoLog}");
        return false;
    }
}