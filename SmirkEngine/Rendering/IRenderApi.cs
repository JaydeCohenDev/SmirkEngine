using System.Drawing;
using Silk.NET.Windowing;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering;

public interface IRenderApi
{
    void Initialize(IWindow window);
    void ClearScreen(Color color);
    void SetViewport(uint width, uint height);
    void DrawMesh(IMeshSection meshSection, Transform transform);
    IShader CreateShader();
    ITexture CreateTexture();
    IMeshSection CreateMesh();
    void PresentFrame();
    void Shutdown();
}