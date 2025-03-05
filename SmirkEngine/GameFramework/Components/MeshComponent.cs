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

    public void SetMaterial(Material material)
    {
        if(Mesh is not null)
            Mesh.Material = material;
    }
}