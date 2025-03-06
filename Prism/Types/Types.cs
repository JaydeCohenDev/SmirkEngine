namespace Prism.Types;

public class Float : IShaderVarType
{
    public static Float operator *(Float a, Float b)
    {
        return new Float();
    }
    
    public static implicit operator Float(float value)
    {
        return new Float();
    }

    public static implicit operator Float(double value)
    {
        return new Float();
    }
}

public class Vec2 : IShaderVarType
{
    public Vec2() {}
    public Vec2(Float x, Float y) {}
    
    public Float X, Y;
    
    public static Vec2 operator *(Vec2 a, Vec2 b)
    {
        return new Vec2();
    }
}

public class Vec3 : IShaderVarType
{
    public Vec3() {}
    public Vec3(Float x, Float y, Float z) {}

    public Float X, Y, Z;
    
    public static Vec3 operator *(Vec3 a, Vec3 b)
    {
        return new Vec3();
    }
    
    public static Vec3 operator *(Vec3 a, Mat4 b)
    {
        return new Vec3();
    }
    
    public static Vec3 operator *(Mat4 a, Vec3 b)
    {
        return new Vec3();
    }
}

public class Vec4 : IShaderVarType
{
    public Vec4() {}
    public Vec4(Float x, Float y, Float z, Float w) {}
    
    public Float X, Y, Z, W;
    
    public static Vec4 operator *(Vec4 a, Vec4 b)
    {
        return new Vec4();
    }
}

public class Mat3 : IShaderVarType
{
    public static Mat3 operator *(Mat3 a, Mat3 b)
    {
        return new Mat3();
    }
}

public class Mat4 : IShaderVarType
{
    public static Mat4 operator *(Mat4 a, Mat4 b)
    {
        return new Mat4();
    }

}