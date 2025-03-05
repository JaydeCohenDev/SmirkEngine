using SmirkEngine.Core;
using SmirkEngine.Rendering;

namespace SmirkEngine.GameFramework;

public abstract class SceneComponent : ActorComponent, IRenderable
{
    public Transform Transform { get; init; } = new();
    public virtual void Render(float deltaTime, IRenderApi renderer) {}
}