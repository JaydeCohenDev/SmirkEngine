using SmirkEngine.AssetHandling;

namespace SmirkEngine.Rendering;

public interface IShader : IAsset
{
    public void Bind();
    public void Unbind();
}