using SmirkEngine.AssetHandling;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering;

public class Texture : ITexture, IAsset
{
    protected ITexture? _texture;
    
    public bool LoadFromFile(string path)
    {
        _texture = Game.GetRenderApi().CreateTexture();
        return _texture.LoadFromFile(path);
    }

    public void SetTextureFilterMode(TextureFilterMode mode) => _texture?.SetTextureFilterMode(mode);
    public TextureFilterMode GetTextureFilterMode() => _texture?.GetTextureFilterMode() ?? TextureFilterMode.Linear;
    public void SetTextureWrapMode(TextureWrapMode mode) => _texture?.SetTextureWrapMode(mode);
    public TextureWrapMode GetTextureWrapMode() => _texture?.GetTextureWrapMode() ?? TextureWrapMode.Repeat;
}