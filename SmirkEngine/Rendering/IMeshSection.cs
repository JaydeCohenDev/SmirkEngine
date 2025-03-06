using SmirkEngine.AssetHandling;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering;

public interface IMeshSection
{
    public Material Material { get; set; }
    public void Render(Transform transform);
    void SetVertices(List<IMeshVertex> vertices);
    void SetIndices(List<uint> indices);
    IMeshVertex CreateVertex();
}