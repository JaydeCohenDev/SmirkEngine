using SmirkEngine.Rendering;

namespace SmirkEngine;

public interface IRenderable
{
    void Render(float deltaTime, IRenderApi renderer);
}