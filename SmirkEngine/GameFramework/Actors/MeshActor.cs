using SmirkEngine.Components;

namespace SmirkEngine.Actors;

public class MeshActor : World.Actor
{
    public MeshComponent MeshComponent { get; init; }
    
    public MeshActor()
    {
        MeshComponent = AddComponent<MeshComponent>();
    }
}