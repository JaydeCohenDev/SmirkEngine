using System.Runtime.InteropServices;
using Silk.NET.OpenGL;

namespace SmirkEngine.Rendering.OpenGL;

public class GLVao<T> where T : struct
{
    public uint Handle { get; }
    private GL _gl;
    
    public GLVao(GL gl)
    {
        _gl = gl;
        Handle = _gl.GenVertexArray();
    }

    public void SetAttributes()
    {
        var size = Marshal.SizeOf<T>();
        var offset = 0;
    
        foreach (var field in typeof(T).GetFields())
        {
            var attribLocation = Marshal.OffsetOf(typeof(T), field.Name).ToInt32();
    
            if (attribLocation >= 0)
            {
                _gl.EnableVertexAttribArray((uint)attribLocation);
                _gl.VertexAttribPointer((uint)attribLocation, Marshal.SizeOf(field.FieldType), GLEnum.Float, false, (uint)size, (IntPtr)offset);
            }
            
            offset += Marshal.SizeOf(field.FieldType);
        }
    }
    
    public void Bind()
    {
        _gl.BindVertexArray(Handle);
    }
    
    public void Unbind()
    {
        _gl.BindVertexArray(0);
    }
}