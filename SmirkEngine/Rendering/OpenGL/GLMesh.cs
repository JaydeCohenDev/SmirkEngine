using System.Numerics;
using System.Runtime.InteropServices;
using Silk.NET.OpenGL;
using SmirkEngine.Core;

namespace SmirkEngine.Rendering.OpenGL;

public class GLMesh : IMesh
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GLMeshVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TexCoord;
    }
    
    public Material Material { get; set; }
    
    private GL _gl;
    private GLVbo<GLMeshVertex> _vbo;
    private GLVao<GLMeshVertex> _vao;
    private GLEbo _ebo;

    private List<GLMeshVertex> _vertices = [];
    private List<uint> _indices = [];
    
    public GLMesh(GL gl)
    {
        _gl = gl;
        
        _vbo = new GLVbo<GLMeshVertex>(gl, BufferTargetARB.ArrayBuffer);
        _vao = new GLVao<GLMeshVertex>(gl);
        _ebo = new GLEbo(gl, BufferTargetARB.ElementArrayBuffer);
    }

    public void AddVertex(GLMeshVertex vertex)
    {
        _vertices.Add(vertex);
    }

    public void SetVertices(List<GLMeshVertex> vertices)
    {
        _vertices = vertices;
    }

    public void Render(Transform transform)
    {
        Material.Bind();
        _vao.Bind();
        
        _gl.DrawElements(PrimitiveType.Triangles, (uint)_indices.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
        
        _vao.Unbind();
        Material.Unbind();
    }

}