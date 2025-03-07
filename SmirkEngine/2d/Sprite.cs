using System.Drawing;
using SmirkEngine.AssetHandling;
using SmirkEngine.Rendering;

namespace SmirkEngine._2d;

public class Sprite
{
    public Texture? Texture
    {
        get => _texture ?? (Texture = Asset.Load<Texture>(_textureAssetPath));
        protected set => _texture = value;
    }

    public Rectangle SourceRegion { get; init; }
    protected readonly string? _textureAssetPath;
    private Texture? _texture;
    
    public Sprite(Texture texture, Rectangle sourceRegion)
    {
        Texture = texture;
        SourceRegion = sourceRegion;
    }

    public Sprite(string textureAssetPath, Rectangle sourceRegion)
    {
        _textureAssetPath = textureAssetPath;
        SourceRegion = sourceRegion;
    }
    
}