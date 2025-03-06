using SmirkEngine.Rendering;

namespace SmirkEngine.GameFramework.Components;

public class MeshComponent : SceneComponent
{
    public IMeshSection? Mesh { get; set; }
    
    public override void Render(float deltaTime, IRenderApi renderer)
    {
        if (Mesh is null) return;
        
        renderer.DrawMesh(Mesh, Transform);
    }

    public void SetMesh(IMeshSection meshSection)
    {
        Mesh = meshSection;
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