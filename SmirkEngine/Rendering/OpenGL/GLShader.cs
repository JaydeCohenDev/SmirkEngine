using System.Text;
using Silk.NET.OpenGL;

namespace SmirkEngine.Rendering.OpenGL;

public partial class GLShader : IShader
{
    public uint Handle { get; private set; }
    protected readonly GL _gl;

    public GLShader(GL gl)
    {
        _gl = gl;
    }
    
    public bool LoadFromFile(string path)
    {
        return GLShaderLoader.Load(_gl, this, path);
    }

    public void Bind()
    {
        _gl.UseProgram(Handle);
    }

    public void Unbind()
    {
        _gl.UseProgram(0);
    }
}