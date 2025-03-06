using Prism.Attributes;
using Prism.Core;
using Prism.Types;

namespace PrismSandbox.res.shaders;

[ShaderProgram]
public class TestShader : PrismShader
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
            VertexPosition = (projection * view * model) * aPos;
            FragTexCoord = aTexCoord;
        }
    }

    class Fragment : FragmentShader
    {
        [ShaderInput] Vec2 FragTexCoord;

        [ShaderEntry]
        void main()
        {
            Vec3 color = Vec3(FragTexCoord.X, FragTexCoord.Y, 0.5);
            FragmentColor = Vec4(color.X, color.Y, color.Z, 1.0);
        }
    }
}