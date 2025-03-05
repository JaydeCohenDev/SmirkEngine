namespace SmirkEngine.Rendering;

public class Material
{
    public Shader Shader { get; init; }

    public Material(Shader shader)
    {
        Shader = shader;
    }

    public void Bind()
    {
        Shader.Bind();
    }

    public void Unbind()
    {
        Shader.Unbind();
    }
}