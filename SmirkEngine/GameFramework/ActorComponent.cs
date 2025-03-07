namespace SmirkEngine;

public abstract class ActorComponent : ITickable
{
    public virtual void BeginPlay() {}
    public virtual void EndPlay() {}
    public virtual void Tick(float deltaTime) {}
}