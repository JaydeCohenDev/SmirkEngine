using SmirkEngine.AssetHandling;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering;

// Asset wrapper for shader (so game doesn't have to care what rendering API is used)
public class Shader : IShader, IAsset
{
    private IShader? _shader;
    
    public bool LoadFromFile(string path)
    {
        _shader = Game.GetRenderApi()!.CreateShader();
        _shader.LoadFromFile(path);
        return true;
    }

    public void Bind()
    {
        _shader?.Bind();
    }

    public void Unbind()
    {
        _shader?.Unbind();
    }
}