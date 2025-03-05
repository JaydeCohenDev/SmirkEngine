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

    public void SetMesh(IMesh mesh)
    {
        Mesh = mesh;
    }
    
    public void SetMaterial(Material material)
    {
        if (Mesh != null)
        {
            Mesh.Material = material;
        }
    }
    public Material? GetMaterial => Mesh?.Material;
}