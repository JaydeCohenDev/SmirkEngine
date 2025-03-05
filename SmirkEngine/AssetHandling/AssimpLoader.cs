using Silk.NET.Assimp;

namespace SmirkEngine.AssetHandling;

public static class AssimpLoader
{
    private static Silk.NET.Assimp.Assimp? _assimp;
    
    public static void LoadModel(string path)
    {
        unsafe
        {
            var assimp = GetAssimp();
            var scene = assimp.ImportFile(path, (uint)PostProcessSteps.Triangulate);

            if (scene == null || scene->MFlags == Silk.NET.Assimp.Assimp.SceneFlagsIncomplete || scene->MRootNode == null)
            {
                var error = assimp.GetErrorStringS();
                throw new Exception($"Failed to load model from path {path}: {error}");
            }
            
            ProcessNode(scene->MRootNode, scene);
        }
    }

    private static unsafe void ProcessNode(Node* node, Scene* scene)
    {
        
        for (var i = 0; i < node->MNumMeshes; i++)
        {
            var mesh = scene->MMeshes[node->MMeshes[i]];
            //meshes.Add(mesh);
        }

        for (var i = 0; i < node->MNumChildren; i++)
        {
            ProcessNode(node->MChildren[i], scene);
        }
    }

    private static Silk.NET.Assimp.Assimp GetAssimp()
    {
        if(_assimp != null) 
            return _assimp;
        
        _assimp = Silk.NET.Assimp.Assimp.GetApi();
        return _assimp;
    }
}