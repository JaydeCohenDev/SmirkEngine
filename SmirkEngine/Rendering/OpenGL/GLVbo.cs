using Silk.NET.OpenGL;

namespace SmirkEngine.Rendering.OpenGL;

public class GLVbo<T> where T : unmanaged
{
    public uint Handle { get; }
    private readonly GL _gl;
    private readonly BufferTargetARB _target;
    
    public GLVbo(GL gl, BufferTargetARB target)
    {
        _gl = gl;
        _target = target;

        Handle = gl.GenBuffer();
        Bind();
    }

    public void SetData(ReadOnlySpan<T> data)
    {
        _gl.BufferData(_target, data, BufferUsageARB.StaticDraw);
    }

    public void Bind()
    {
        _gl.BindBuffer(_target, Handle);
    }

    public void Unbind()
    {
        _gl.BindBuffer(_target, 0);
    }
}