using SmirkEngine.Core;

namespace SmirkEngine.Rendering;

public interface IMesh
{
    public Material Material { get; set; }
    public void Render(Transform transform);
}