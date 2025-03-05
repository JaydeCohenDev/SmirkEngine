using SmirkEngine.Rendering;

namespace SmirkEngine.GameFramework;

public interface IRenderable
{
    void Render(float deltaTime, IRenderApi renderer);
}