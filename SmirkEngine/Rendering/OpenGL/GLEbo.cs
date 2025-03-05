using Silk.NET.OpenGL;

namespace SmirkEngine.Rendering.OpenGL;

public class GLEbo : GLVbo<uint>
{
    public GLEbo(GL gl, BufferTargetARB target) : base(gl, target) {}
}