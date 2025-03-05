using SmirkEngine.Rendering;

namespace SmirkEngine.GameFramework;

public partial class World : ITickable, IRenderable
{
    protected readonly List<Actor> _actors = [];
    
    public void Spawn(Actor actor)
    {
        _actors.Add(actor);
        actor.World = this;
        actor.BeginPlay();
    }

    public void Spawn<T>() where T : Actor
    {
        Spawn(Activator.CreateInstance<T>());
    }
    
    public void Tick(float deltaTime) => _actors.ForEach(actor => actor.Tick(deltaTime));
    public void Render(float deltaTime, IRenderApi renderer) => _actors.ForEach(actor => actor.Render(deltaTime, renderer));

    public void DestroyActor(Actor actor)
    {
        _actors.Remove(actor);
        actor.World = null;
        actor.EndPlay();
    }
}