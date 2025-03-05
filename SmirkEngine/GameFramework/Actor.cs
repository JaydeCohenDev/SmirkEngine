using SmirkEngine.Core;
using SmirkEngine.Rendering;

namespace SmirkEngine.GameFramework;

public partial class World
{
    public abstract class Actor : ITickable, IRenderable
    {
        public Transform Transform { get; init; } = new();
        public World? World { get; internal set; }
        public virtual void BeginPlay() {}
        public virtual void EndPlay() {}
        public virtual void Tick(float deltaTime) {}
        public virtual void Render(float deltaTime, IRenderApi renderer) {}

        public void Destroy()
        {
            if (World is null)
            {
                EndPlay();
            }
            else
            {
                World.DestroyActor(this);
            }
        }
    }
}