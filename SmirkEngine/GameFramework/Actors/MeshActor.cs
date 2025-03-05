using SmirkEngine.GameFramework.Components;

namespace SmirkEngine.GameFramework.Actors;

public class MeshActor : World.Actor
{
    public MeshComponent MeshComponent { get; init; }
    
    public MeshActor()
    {
        MeshComponent = AddComponent<MeshComponent>();
    }
}