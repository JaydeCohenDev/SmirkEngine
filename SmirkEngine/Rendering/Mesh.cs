using SmirkEngine.AssetHandling;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering;

// Generic wrapper
public class Mesh : IAsset
{
    private IMesh? _mesh;
    
    public bool LoadFromFile(string path)
    {
        if (Game.RenderAPI is null)
        {
            throw new Exception("Cannot create new mesh, render API is null");
            return false;
        }
        
        _mesh = Game.RenderAPI.CreateMesh();
        return _mesh.LoadFromFile(path);
    }
}