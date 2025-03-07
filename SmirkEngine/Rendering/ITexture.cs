using SmirkEngine.AssetHandling;

namespace SmirkEngine.Rendering;

public enum TextureFilterMode
{
    Nearest,
    Linear,
    Trilinear
}

public enum TextureWrapMode
{
    Repeat,
    Clamp,
    Mirror
}

public interface ITexture : IAsset
{
    void SetTextureFilterMode(TextureFilterMode mode);
    TextureFilterMode GetTextureFilterMode(); 
    void SetTextureWrapMode(TextureWrapMode mode);
    TextureWrapMode GetTextureWrapMode();
}