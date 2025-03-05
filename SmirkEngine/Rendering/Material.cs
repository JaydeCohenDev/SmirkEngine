namespace SmirkEngine.Rendering;

public class Material
{
    public IShader Shader { get; init; }

    public Material(IShader shader)
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