using SmirkEngine.Rendering.ShaderBuilder;

namespace EngineTesting;


public class TestShader : ShaderDefinition
{
    class Vertex : VertexShader
    {
        [ShaderInput] Vec3 aPos;
        [ShaderInput] Vec2 aTexCoord;

        [ShaderUniform] Mat4 model, view, projection;

        [ShaderOutput] Vec2 FragTexCoord;

        [ShaderEntry]
        void main()
        {
            VertexPosition = projection * view * model * Vec4(aPos, 1.0);
            FragTexCoord = aTexCoord;
        }
    }

    class Fragment : FragmentShader
    {
        [ShaderInput] Vec2 FragTexCoord;

        [ShaderEntry]
        void main()
        {
            Vec3 color = Vec3(FragTexCoord.x, FragTexCoord.y, 0.5);
            FragmentColor = Vec4(color, 1.0);
        }
    }
}