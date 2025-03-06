using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Prism.Analyzer;

public class ShaderAnalyzer
{
    public static ClassDeclarationSyntax AnalyzeShader(Type shaderType)
    {
        var code = GetSourceCode(shaderType);
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        return syntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault(c => c.Identifier.ValueText == shaderType.Name);
    }

    private static string GetSourceCode(Type type)
    {
        return File.ReadAllText("res/shaders/" + type.DeclaringType.Name + ".cs");
    }
}