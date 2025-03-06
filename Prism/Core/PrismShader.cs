using Prism.Attributes;
using Prism.Types;

namespace Prism.Core;

[ShaderProgram]
public abstract class PrismShader
{
    public interface ISubShader {}
    protected abstract class VertexShader : ISubShader
    {
        protected Vec3 VertexPosition;

        protected abstract void main();
    }

    protected abstract class FragmentShader : ISubShader
    {
        protected Vec4 FragmentColor;
        
        protected abstract void main();
    }

    protected static Vec2 Vec2(Float x, Float y)
    {
        return new Vec2(x, y);
    }

    protected static Vec3 Vec3(Float x, Float y, Float z)
    {
        return new Vec3(x, y, z);
    }

    protected static Vec4 Vec4(Float x, Float y, Float z, Float w)
    {
        return new Vec4(x, y, z, w);
    }
}