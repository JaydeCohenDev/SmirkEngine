using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Prism.Types;

namespace Prism.Compiler;

public interface IShaderCodeGenerator
{
    string GenerateShaderCode(Type shaderType, ShaderVariables variables);
}

public abstract class ShaderCodeGeneratorBase : IShaderCodeGenerator
{
    public abstract string GenerateShaderCode(Type shaderType, ShaderVariables variables);

    protected abstract ShaderTypeMap GetTypeMap();

    protected string MapType(Type type)
    {
        var typeMap = GetTypeMap();
        if(type == typeof(Float)) return typeMap.MapFloat();
        if(type == typeof(Vec2)) return typeMap.MapVec2();
        if(type == typeof(Vec3)) return typeMap.MapVec3();
        if(type == typeof(Vec4)) return typeMap.MapVec4();
        if(type == typeof(Mat3)) return typeMap.MapMat3();
        if(type == typeof(Mat4)) return typeMap.MapMat4();
        if (type == typeof(void)) return "void";
        
        throw new Exception($"Unhandled type {type.Name}");
    }
    
    protected abstract void AppendVariableDeclarations(StringBuilder shaderCode, ShaderVariables variables);

    protected void AppendFunction(StringBuilder shaderCode, MethodInfo methodInfo, MethodDeclarationSyntax methodBody)
    {
        AppendFunctionHeader(shaderCode, methodInfo);
        AppendFunctionBody(shaderCode, methodBody);
    }
    protected abstract void AppendFunctionHeader(StringBuilder shaderCode, MethodInfo methodInfo);
    protected abstract void AppendFunctionBody(StringBuilder shaderCode, MethodDeclarationSyntax methodBody);
}