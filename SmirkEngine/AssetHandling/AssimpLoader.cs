using System.Numerics;
using Silk.NET.Assimp;
using Silk.NET.OpenGL;
using SmirkEngine.Core;
using SmirkEngine.Rendering;
using SmirkEngine.Rendering.OpenGL;
using Mesh = Silk.NET.Assimp.Mesh;

namespace SmirkEngine.AssetHandling;

public static class AssimpLoader
{
    private static Silk.NET.Assimp.Assimp? _assimp;
    
    public static List<IMeshSection> LoadModel(string path)
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

            List<IMeshSection> meshes = [];
            ProcessNode(scene->MRootNode, scene, meshes);
            assimp.FreeScene(scene);

            return meshes;
        }
    }

    private static unsafe void ProcessNode(Node* node, Scene* scene, List<IMeshSection> meshes)
    {
        
        for (var i = 0; i < node->MNumMeshes; i++)
        {
            var mesh = scene->MMeshes[node->MMeshes[i]];
            meshes.Add(ProcessMesh(mesh, scene));
        }

        for (var i = 0; i < node->MNumChildren; i++)
        {
            ProcessNode(node->MChildren[i], scene, meshes);
        }
    }

    private static unsafe IMeshSection ProcessMesh(Mesh* mesh, Scene* scene)
    {
        var createdMesh = Game.GetRenderApi()!.CreateMesh();
        
        List<IMeshVertex> vertices = [];
        List<uint> indices = [];

        // Extract vertices
        for (var i = 0; i < mesh->MNumVertices; i++)
        {
            var vertex = createdMesh.CreateVertex();
            vertex.Position = new Vector3(mesh->MVertices[i].X, mesh->MVertices[i].Y, mesh->MVertices[i].Z);
            vertex.Normal = mesh->MNormals != null
                ? new Vector3(mesh->MNormals[i].X, mesh->MNormals[i].Y, mesh->MNormals[i].Z)
                : Vector3.Zero;
            vertex.TexCoord = mesh->MTextureCoords[0] != null
                ? new Vector2(mesh->MTextureCoords[0][i].X, mesh->MTextureCoords[0][i].Y)
                : Vector2.Zero;
            vertices.Add(vertex);
        }
        
        // Extract indices
        for (var i = 0; i < mesh->MNumFaces; i++)
        {
            var face = mesh->MFaces[i];
            for (var j = 0; j < face.MNumIndices; j++)
            {
                indices.Add(face.MIndices[j]);
            }
        }

        createdMesh.SetVertices(vertices);
        createdMesh.SetIndices(indices);
        
        return createdMesh;
    }

    private static Silk.NET.Assimp.Assimp GetAssimp()
    {
        if(_assimp != null) 
            return _assimp;
        
        _assimp = Silk.NET.Assimp.Assimp.GetApi();
        return _assimp;
    }
}