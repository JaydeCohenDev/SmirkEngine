using SmirkEngine.Rendering;

namespace SmirkEngine;

public partial class World : ITickable, IRenderable
{
    protected readonly List<Actor> _actors = [];
    
    public Actor Spawn(Actor actor)
    {
        _actors.Add(actor);
        actor.InvokeBeginPlay(this);
        return actor;
    }
    
    public T Spawn<T>(Actor actor) where T : Actor
    {
        return (T)Spawn(actor);
    }

    public T Spawn<T>() where T : Actor
    {
        return Spawn<T>(Activator.CreateInstance<T>());
    }
    
    public void Tick(float deltaTime) => _actors.ForEach(actor => actor.Tick(deltaTime));
    public void Render(float deltaTime, IRenderApi renderer) => _actors.ForEach(actor => actor.Render(deltaTime, renderer));

    public void DestroyActor(Actor actor)
    {
        _actors.Remove(actor);
        actor.InvokeEndPlay();
    }
}