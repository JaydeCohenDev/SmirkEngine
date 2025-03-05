using SmirkEngine.AssetHandling;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering;

// Asset wrapper for shader (so game doesn't have to care what rendering API is used)
public class Shader : IShader, IAsset
{
    private IShader? _shader;
    
    public bool LoadFromFile(string path)
    {
        if (Game.RenderAPI is null)
        {
            throw new Exception("Cannot create new shader, render API is null");
            return false;
        }
        
        _shader = Game.RenderAPI.CreateShader();
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