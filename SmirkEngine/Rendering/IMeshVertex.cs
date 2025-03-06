using System.Numerics;

namespace SmirkEngine.Rendering;

public interface IMeshVertex
{
    public Vector3 Position { get; set; }
    public Vector3 Normal { get; set; }
    public Vector2 TexCoord { get; set;}
}