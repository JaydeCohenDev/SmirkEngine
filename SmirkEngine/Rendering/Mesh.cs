using SmirkEngine.AssetHandling;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering;

// Generic asset wrapper
public class Mesh : IAsset
{
    protected List<IMeshSection> _meshSections = [];
    
    public bool LoadFromFile(string path)
    {
        _meshSections = AssimpLoader.LoadModel(path);
        return true;
    }
}