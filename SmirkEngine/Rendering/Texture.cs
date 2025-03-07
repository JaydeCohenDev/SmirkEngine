using SmirkEngine.AssetHandling;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering;

public abstract class Texture : ITexture, IAsset
{
    protected ITexture? _texture;
    
    public bool LoadFromFile(string path)
    {
        _texture = Game.GetRenderApi().CreateTexture();
        return _texture.LoadFromFile(path);
    }
}