using SmirkEngine.AssetHandling;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering;

public interface IMesh : IAsset
{
    public Material Material { get; set; }
    public void Render(Transform transform);
}