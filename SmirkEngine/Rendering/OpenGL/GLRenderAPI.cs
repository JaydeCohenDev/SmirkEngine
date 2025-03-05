using System.Drawing;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering.OpenGL;

public class GLRenderAPI : IRenderApi
{
    private GL? _gl;
    
    public void Initialize(IWindow window)
    {
        _gl = GL.GetApi(window);
        
        Console.WriteLine("OpenGL Initialized " + _gl.GetStringS(GLEnum.Version));
        Console.WriteLine("OpenGL Vendor " + _gl.GetStringS(GLEnum.Vendor));
        Console.WriteLine("OpenGL Renderer " + _gl.GetStringS(GLEnum.Renderer));
        Console.WriteLine("OpenGL Shading Language " + _gl.GetStringS(GLEnum.ShadingLanguageVersion));
        Console.WriteLine("OpenGL Extensions " + _gl.GetStringS(GLEnum.Extensions));
    }

    public void ClearScreen(Color color)
    {
        _gl?.ClearColor(color);
        _gl?.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }

    public void SetViewport(uint width, uint height)
    {
        _gl?.Viewport(0, 0, width, height);
    }

    public void DrawMesh(IMesh mesh, Transform transform)
    {
        mesh.Render(transform);
    }

    public IShader CreateShader()
    {
        return new GLShader(_gl!);
    }

    public IMesh CreateMesh()
    {
        return new GLMesh(_gl!);
    }

    public void PresentFrame()
    {
        throw new NotImplementedException();
    }

    public void Shutdown()
    {
        Console.WriteLine("OpenGL Shutdown");
    }
}