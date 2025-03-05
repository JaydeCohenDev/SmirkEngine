using System.Drawing;
using Silk.NET.Windowing;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering;

public interface IRenderApi
{
    void Initialize(IWindow window);
    void ClearScreen(Color color);
    void SetViewport(uint width, uint height);
    void DrawMesh(IMesh mesh, Transform transform);
    IShader CreateShader();
    void PresentFrame();
    void Shutdown();
}