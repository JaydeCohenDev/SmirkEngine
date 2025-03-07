using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Prism.Core;

namespace Prism.Analyzer;

public class ShaderAnalyzer
{
    public static ClassDeclarationSyntax AnalyzeShader(Type shaderType)
    {
        var code = GetSourceCode(shaderType);
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        return syntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault(c => c.Identifier.ValueText == shaderType.Name);
    }

    public static PrismShaderProgramDefinition AnalyzeProgram(Type programType)
    {
        var program = new PrismShaderProgramDefinition
        {
            Name = programType.Name,
            ShaderProgramType = programType,
            SourcePath = $"{programType.Name}.cs"
        };
        program.RootNode = CSharpSyntaxTree.ParseText(program.LoadSource()).GetRoot();

        foreach (var subType in programType.GetNestedTypes().OfType<PrismShader.ISubShader>())
        {
            var subShader = new PrismSubShaderDefinition
            {

            };
            program.SubShaders.Add(subShader);
        }
        
        return program;
    }
    
    private static string GetSourceCode(Type type)
    {
        return File.ReadAllText("res/shaders/" + type.DeclaringType.Name + ".cs");
    }
}