using SmirkEngine.Rendering;

namespace SmirkEngine.GameFramework.Components;

public class MeshComponent : SceneComponent
{
    public IMesh? Mesh { get; set; }
    
    public override void Render(float deltaTime, IRenderApi renderer)
    {
        if (Mesh is null) return;
        
        renderer.DrawMesh(Mesh, Transform);
    }
}