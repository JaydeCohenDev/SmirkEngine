namespace SmirkEngine.Rendering.ShaderBuilder;

public class ShaderDefinition
{
    protected interface IShaderVariable {}
    protected class Float : IShaderVariable {}
    protected class Vec2 : IShaderVariable {}
    protected class Vec3 : IShaderVariable {}
    protected class Vec4 : IShaderVariable {}
    protected class Mat4 : IShaderVariable {}

    protected abstract class VertexShader
    {
        protected Vec3 VertexPosition;
    }

    protected abstract class FragmentShader
    {
        protected Vec4 FragmentColor;
    }
}