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
        return type == typeof(void) ? "void" : GetTypeMap().Map(type);
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